using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace Esoft.Framework.Utility.Common
{
    public class NetUtil
    {
        public static bool Online(int retryTime)
        {
            Ping ping = new Ping();
            PingOptions options = new PingOptions();
            options.DontFragment = true;
            PingReply reply;
            byte[] buffer = System.Text.Encoding.ASCII.GetBytes(string.Empty);

            int index = 0;

            while (index < retryTime)
            {
                try
                {
                    reply = ping.Send("www.baidu.com", 5000, buffer, options);

                    if (reply.Status.Equals(IPStatus.Success))
                        return true;
                }
                catch
                {
                    
                }

                index++;
            }

            return false;
        }
    }
}
