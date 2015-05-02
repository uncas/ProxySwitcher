Proxy switcher
==============

Command-line tool for switching machine proxy.

Usage
-----

Uncas.ProxySwitcher\app.config: Add list of shortcuts to proxy servers like:
  <appSettings>
    <add key="qa1" value="qa1.proxy.mysite.dk" />
    <add key="qa2" value="qa2.proxy.mysite.dk" />
  </appSettings>

build.cmd: Build the solution.

proxy.cmd qa1: Enable proxy server qa1.proxy.mysite.dk

proxy.cmd: Disable proxy server.


Tip
---

Create a powershell function in your powershell profile:
function proxy($proxy){
& "C:\Projects\uncas.github\Uncas.ProxySwitcher\Uncas.ProxySwitcher\bin\Debug\Uncas.ProxySwitcher.exe" $proxy
}
such that you can everywhere write 'proxy qa1', 'proxy' etc.

Or copy proxy.cmd to your tool folder which is in the path,
and then add the full path to that proxy.cmd,
for example C:\Projects\uncas.github\Uncas.ProxySwitcher\Uncas.ProxySwitcher\bin\Debug\Uncas.ProxySwitcher.exe %1
such that you can everywhere write 'proxy qa1', 'proxy' etc.
