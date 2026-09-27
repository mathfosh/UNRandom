using System.Reflection;
using SecRandom;

[assembly: AssemblyVersion(GitInfo.AssemblyVersion)]
[assembly: AssemblyInformationalVersion($"{GitInfo.Tag}+{GitInfo.CommitHash}")]
[assembly: AssemblyTitle("UNRandom")]
[assembly: AssemblyProduct("UNRandom")]
[assembly: AssemblyCompany("李骏健")]
[assembly: AssemblyCopyright("Copyright (C) SECTL / 黎泽懿_Aionflux; UNRandom fork maintained by 李骏健")]

#if NETCOREAPP
// [assembly: SupportedOSPlatform("Windows")]
#endif
#if Platforms_MacOs
[assembly:SupportedOSPlatform("macos")]
#endif