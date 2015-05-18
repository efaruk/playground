using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace XFF
{
    public class XForwardedFor : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += context_BeginRequest;
        }

        private void context_BeginRequest(object sender, EventArgs e)
        {
            var application = (HttpApplication) sender;
            ReplaceServerVariables(application);
        }

        
        private void ReplaceServerVariables(HttpApplication application)
        {
            BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
            var request = application.Request;
            Type type = request.ServerVariables.GetType();

            if (request.ServerVariables.AllKeys.Contains(SettingsWrapper.ForwardedHeader) && request.ServerVariables[SettingsWrapper.ForwardedHeader].ToString(CultureInfo.InvariantCulture).Length > 2)
            {
                var setOnDemand = type.GetMethod("SetNoDemand", bindingFlags);
                var makeReadWrite = type.GetMethod("MakeReadWrite", bindingFlags);
                var makeReadOnly = type.GetMethod("MakeReadOnly", bindingFlags);
                //Set ReadOnly Off
                makeReadWrite.Invoke(request.ServerVariables, null);
                //Get Forwarded
                var forwarded = request.ServerVariables[SettingsWrapper.ForwardedHeader];
                //Take RealIp
                string clientIp = forwarded;
                if (forwarded.Contains(SettingsWrapper.Separator))
                {
                    var ips = forwarded.Split(new[] {SettingsWrapper.Separator}, StringSplitOptions.RemoveEmptyEntries);
                    clientIp = ips[SettingsWrapper.ClientIpIndex];
                }
                //Set Client IP
                var parameters = new object[] {SettingsWrapper.HEADER_REMOTE_ADDR, clientIp};
                setOnDemand.Invoke(request.ServerVariables, parameters);
                //Make ReadOnly Again
                //makeReadOnly.Invoke(request.ServerVariables, null);
            }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
