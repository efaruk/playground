using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebAutoLogin.Client
{
    public class TokenDetector
    {
        public delegate void TokenChangeDetected(string token);

        private CancellationTokenSource _cancellationTokenSource;
        private bool _running;
        private Task _usbCheckTask;
        private static string _lastToken;

        public void Start()
        {
            if (_usbCheckTask == null)
            {
                _cancellationTokenSource = new CancellationTokenSource();
                _usbCheckTask = new Task(() => ControlStickState(_cancellationTokenSource.Token),
                    _cancellationTokenSource.Token);
                _usbCheckTask.Start();
            }
            _running = true;
        }

        public void Stop()
        {
            if (_usbCheckTask != null)
            {
                _cancellationTokenSource.Cancel();
                _usbCheckTask = null;
            }
            _running = false;
        }

        public void Toggle()
        {
            if (_running)
            {
                Stop();
            }
            else
            {
                Start();
            }
        }

        private void ControlStickState(CancellationToken token)
        {
            while (!_running)
            {
                if (token.IsCancellationRequested)
                {
                    token.ThrowIfCancellationRequested();
                }
                var driveInfos = DriveInfo.GetDrives()
                    .Where(
                        drive =>
                            drive.IsReady && drive.DriveType == DriveType.Removable);
                var anyToken = false;
                foreach (var di in driveInfos)
                {
                    var path = Path.Combine(di.Name, GlobalModule.TokenFileName);
                    var tmpToken = ReadToken(path);
                    if (string.IsNullOrWhiteSpace(tmpToken)) continue;
                    anyToken = true;
                    if (tmpToken == _lastToken) break;
                    _lastToken = tmpToken;
                    TokenChange(tmpToken);
                    break;
                }
                if (!anyToken)
                {
                    _lastToken = "";
                    TokenChange("");
                }
                Thread.Sleep(GlobalModule.TokenDetectionInterval);
            }
        }

        public event TokenChangeDetected OnTokenChange;

        private void TokenChange(string token)
        {
            if (!_running) return;
            if (OnTokenChange != null)
            {
                OnTokenChange(token);
            }
        }

        private string ReadToken(string path)
        {
            var rc = "";
            if (File.Exists(path))
            {
                rc = File.ReadAllText(path);
            }
            return rc;
        }
    }
}