﻿namespace RoXMX.Utilities.Templates
{
    internal class Aftman
    {
        static string RojoVersion = Configuration.RojoVersionFallback;

        public static string Content = $@"# Generated by RoXMX by ninjaninja140 - Version {Configuration.Version}
# This file lists tools managed by Aftman, a cross-platform toolchain manager.
# For more information, see https://github.com/LPGhatguy/aftman

# To add a new tool, add an entry to this table.
[tools]
rojo = ""rojo-rbx/rojo@{RojoVersion}""
";

        public static string SetContext(string version)
        {
            RojoVersion = version;
            return $@"# Generated by RoXMX by ninjaninja140 - Version {Configuration.Version}
# This file lists tools managed by Aftman, a cross-platform toolchain manager.
# For more information, see https://github.com/LPGhatguy/aftman

# To add a new tool, add an entry to this table.
[tools]
rojo = ""rojo-rbx/rojo@{RojoVersion}""
";
        }
    }
}
