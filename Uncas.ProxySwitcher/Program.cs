using System.Configuration;

namespace Uncas.ProxySwitcher
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string proxyServerKey = args.Length > 0 ? args[0] : string.Empty;
            string server = ConfigurationManager.AppSettings[proxyServerKey];
            var proxyUtility = new ProxyUtility();
            if (string.IsNullOrWhiteSpace(server))
                proxyUtility.DisableProxy();
            else
                proxyUtility.EnableProxy(server);
        }
    }
}