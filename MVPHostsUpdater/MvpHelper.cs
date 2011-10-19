using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

using Microsoft.Win32;

using MVPHostsUpdater.Properties;

namespace MVPHostsUpdater
{
    public static class MvpHelper
    {
        private static string _hostsFilePath;

        private static string FetchMvpFile()
        {
            string result = string.Empty;
            WebRequest request = WebRequest.Create(Settings.Default.MvpUrl);
            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using(StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }

            response.Close();

            return result;
        }

        private static void WriteToHostsFile(string text)
        {
            File.WriteAllText(GetPathOfHostsFile(), text);
        }

        private static string ReadHostsFile()
        {
            using(StreamReader reader = File.OpenText(GetPathOfHostsFile()))
            {
                return reader.ReadToEnd();
            }
        }

        public static string GetPathOfHostsFile()
        {
            if (string.IsNullOrEmpty(_hostsFilePath))
            {
                RegistryKey rk = Registry.LocalMachine.OpenSubKey(Settings.Default.HostsRegistryPath);

                if (rk == null)
                {
                    throw new KeyNotFoundException(Settings.Default.HostsRegistryPath);
                }

                _hostsFilePath = rk.GetValue(Settings.Default.HostsRegistryKey.ToUpper()) + "\\hosts";
            }
            return _hostsFilePath;
        }

        internal static void UpdateMvpHostsFile()
        {
            List<string> currentHostsFile = ParseHostsFile(ReadHostsFile());
            List<string> mvpHostsFile = ParseHostsFile(FetchMvpFile());

            mvpHostsFile.RemoveAll(s => s.EndsWith("   localhost"));
            currentHostsFile = currentHostsFile.Except(mvpHostsFile).ToList();


            string result =
                @"# Copyright (c) 1993-2009 Microsoft Corp.
#
# This is a sample HOSTS file used by Microsoft TCP/IP for Windows.
#
# This file contains the mappings of IP addresses to host names. Each
# entry should be kept on an individual line. The IP address should
# be placed in the first column followed by the corresponding host name.
# The IP address and the host name should be separated by at least one
# space.
#
# Additionally, comments (such as these) may be inserted on individual
# lines or following the machine name denoted by a '#' symbol.
#
# For example:
#
#      102.54.94.97     rhino.acme.com          # source server
#       38.25.63.10     x.acme.com              # x client host
#
#
# This hosts file is autoupdated with MvpHostsUpdater (), using the MVPS hosts file (http://winhelp2002.mvps.org/hosts.htm)


# Custom Hosts entries
# -------------------------------------
";

            result += string.Join("\r\n", currentHostsFile);
            result += @"

# MVPS Hosts entries
# ------------------------------------
";

            result += string.Join("\r\n", mvpHostsFile);

            WriteToHostsFile(result);
        }

        private static List<string> ParseHostsFile(string file)
        {
            List<string> lines = file.Split("\r\n".ToCharArray()).ToList();
            lines = lines.Where(l => !string.IsNullOrEmpty(l) && !l.StartsWith("#")).ToList();
            return lines.Distinct().ToList();
        }
    }
}