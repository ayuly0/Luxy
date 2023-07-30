using System.Windows.Forms;

namespace Luxy
{
    public partial class StealerForm : Form
    {
        public StealerForm()
        {
            InitializeComponent();
        }

        public bool[] Stealer_settings
        {
            get
            {
                bool[] stealer_settings = { passwords_ts.Checked, cookies_ts.Checked, token_ts.Checked, games_ts.Checked, telegram_ss_ts.Checked, sysinfo_ts.Checked, network_ts.Checked, wallets_ts.Checked, take_webcam_ts.Checked, take_screenshot_ts.Checked };
                return stealer_settings;
            }
        }
    }
}
