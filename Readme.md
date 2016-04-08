# Startup using OwinHost

* Install OwinHost 
```powershell
iex ((new-object net.webclient).DownloadString('https://chocolatey.org/install.ps1'))
cist OwinHost
```
* Library must be in `./bin` folder
* Start `owinhost` from `.` folder

see also: [OWIN Startup class detection](http://www.asp.net/aspnet/overview/owin-and-katana/owin-startup-class-detection)

# Self-Hosting 

* install Nuget `Microsoft.AspNet.WebApi.Owin.SelfHost`
* start in `Main()` method of application: 
```csharp
using (WebApp.Start<Startup>("http://localhost:5000"))
{
    Console.WriteLine("Server active");
    Console.ReadLine();
}
```

# IIS Hosting 

* Create empty ASP.NET project
* add reference to `.dll` library with OWIN starup 
* create empty `index.html` file to prevent IIS pipeline to return an error 
* add nuget `Microsoft.Owin.Host.SystemWeb`
* add configuration section to Web.config
```xml
<system.webServer>
  <modules runAllManagedModulesForAllRequests="true" />
</system.webServer>
```