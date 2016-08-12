using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebAutoLogin.Client
{
    public class DriveDetector
    {
        public delegate void UsbDeviceChanged(string lastWrite, string volumeLabel, DateTime creationTime);

        private CancellationTokenSource _cancellationTokenSource;
        private DateTime _creationTime = DateTime.MinValue;
        private string _driveName = "";
        private readonly int _interval = 3000;
        private bool _running;
        private readonly string _stickSuffix = "sabet";
        private Task _usbCheckTask;
        private string _volumeLabel = "";

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

        private void ControlStickState(CancellationToken token)
        {
            while (!_running)
            {
                if (token.IsCancellationRequested)
                {
                    token.ThrowIfCancellationRequested();
                }
                var drives = DriveInfo.GetDrives()
                    .Where(
                        drive =>
                            drive.IsReady && drive.DriveType == DriveType.Removable &&
                            drive.VolumeLabel.ToLowerInvariant().Contains(_stickSuffix));
                if (drives.Any())
                {
                    var drive = drives.FirstOrDefault();
                    if (_creationTime != drive.RootDirectory.CreationTime || _volumeLabel != drive.VolumeLabel)
                    {
                        _creationTime = drive.RootDirectory.CreationTime;
                        _volumeLabel = drive.VolumeLabel;
                        _driveName = drive.Name;
                        UsbDriveChanged(_driveName, _volumeLabel, _creationTime);
                    }
                }
                Thread.Sleep(_interval);
            }
        }

        public event UsbDeviceChanged OnUsbDeviceChanged;

        private void UsbDriveChanged(string driveName, string volumeLabel, DateTime creationTime)
        {
            if (!_running) return;
            if (OnUsbDeviceChanged != null) OnUsbDeviceChanged(driveName, volumeLabel, creationTime);
        }
    }
}