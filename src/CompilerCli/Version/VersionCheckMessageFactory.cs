using System;

namespace CompilerCli.Version
{
    public static class VersionCheckMessageFactory
    {
        public static string BuildMessage(System.Version currentVersion, string latestReleaseTag)
        {
            if (string.IsNullOrEmpty(latestReleaseTag))
            {
                return null;
            }

            if (!System.Version.TryParse(latestReleaseTag, out System.Version latestVersion))
            {
                return null;
            }

            var normalisedCurrent = new System.Version(currentVersion.Major, currentVersion.Minor, currentVersion.Build);
            var normalisedLatest = new System.Version(latestVersion.Major, latestVersion.Minor, latestVersion.Build);

            if (normalisedLatest <= normalisedCurrent)
            {
                return null;
            }

            return
                $"A newer version of the Sector File Compiler is available: {FormatVersion(normalisedLatest)} (you are running {FormatVersion(normalisedCurrent)})." +
                Environment.NewLine +
                "Download it from https://github.com/VATSIM-UK/sector-file-compiler/releases/latest";
        }

        private static string FormatVersion(System.Version version)
        {
            return $"{version.Major}.{version.Minor}.{version.Build}";
        }
    }
}
