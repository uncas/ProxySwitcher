using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Uncas.ProxySwitcher
{
    public class ProxyUtility
    {
        public const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
        public const int INTERNET_OPTION_REFRESH = 37;
        private static bool settingsReturn, refreshReturn;

        [DllImport("wininet.dll")]
        public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);

        public void EnableProxy(string proxy)
        {
            RegistryKey registry = GetRegistry();
            SetProxyEnable(registry, 1);
            SetProxyServer(registry, proxy);
            RefreshSettings(registry);
        }

        public void DisableProxy()
        {
            RegistryKey registry = GetRegistry();
            SetProxyEnable(registry, 0);
            SetProxyServer(registry, "");
            RefreshSettings(registry);
        }

        private static void SetProxyServer(RegistryKey registry, string proxy)
        {
            registry.SetValue("ProxyServer", proxy);
        }

        private static void SetProxyEnable(RegistryKey registry, int value)
        {
            registry.SetValue("ProxyEnable", value);
        }

        private void RefreshSettings(RegistryKey registry)
        {
            settingsReturn = InternetSetOption(
                IntPtr.Zero,
                INTERNET_OPTION_SETTINGS_CHANGED,
                IntPtr.Zero,
                0);
            refreshReturn = InternetSetOption(
                IntPtr.Zero,
                INTERNET_OPTION_REFRESH,
                IntPtr.Zero,
                0);
            OutputStatus(registry);
        }

        private void OutputStatus(RegistryKey registry)
        {
            if (IsProxyEnabled(registry))
            {
                string proxyServer = GetProxyServer(registry);
                Console.WriteLine("Proxy server enabled: {0}.", proxyServer);
            }
            else
            {
                Console.WriteLine("Proxy server disabled.");
            }
        }

        private static string GetProxyServer(RegistryKey registry)
        {
            return (string) registry.GetValue("ProxyServer");
        }

        private RegistryKey GetRegistry()
        {
            RegistryKey registry =
                Registry.CurrentUser.OpenSubKey(
                    "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
            if (registry == null)
                throw new InvalidOperationException("Registry not opened. Not admin?");
            return registry;
        }

        private static bool IsProxyEnabled(RegistryKey registry)
        {
            return (int) registry.GetValue("ProxyEnable") == 1;
        }
    }
}