using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Owin;
using MyWebApiHost;

[assembly: AssemblyTitle("MyWebApiHost")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("MyWebApiHost")]
[assembly: AssemblyCopyright("Copyright ©  2016")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: Guid("ed1d3775-a3c5-43f2-b3ce-e47107e51973")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly:OwinStartup(typeof(Startup))]