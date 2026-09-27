using System.Reflection;
using SecRandom;

[assembly: AssemblyVersion(GitInfo.AssemblyVersion)]
[assembly: AssemblyInformationalVersion($"{GitInfo.Tag}+{GitInfo.CommitHash}")]
[assembly: AssemblyTitle("UNRandom")]
[assembly: AssemblyProduct("UNRandom")]
[assembly: AssemblyCopyright("Copyright (C) SECTL / 黎泽懿_Aionflux; UNRandom is a derivative build")]

#if NETCOREAPP
// [assembly: SupportedOSPlatform("Windows")]
#endif
#if Platforms_MacOs
[assembly:SupportedOSPlatform("macos")]
#endif