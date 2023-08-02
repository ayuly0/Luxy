using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Luxy
{
    public partial class ClipperForm : Form
    {
        public ClipperForm()
        {
            InitializeComponent();
        }

        public Dictionary<string, Tuple<string, bool>> Clipper_settings
        {
            get
            {
                var settings = new Dictionary<string, Tuple<string, bool>>();
                var btc = Tuple.Create<string, bool>(btc_tb.Text, btc_ts.Checked);
                var eth = Tuple.Create<string, bool>(eth_tb.Text, eth_ts.Checked);
                var doge = Tuple.Create<string, bool>(doge_tb.Text, doge_ts.Checked);
                var ltc = Tuple.Create<string, bool>(ltc_tb.Text, ltc_ts.Checked);
                var dash = Tuple.Create<string, bool>(dash_tb.Text, dash_ts.Checked);
                var xmr = Tuple.Create<string, bool>(xmr_tb.Text, xmr_ts.Checked);
                var bch = Tuple.Create<string, bool>(bch_tb.Text, bch_ts.Checked);
                

                settings.Add("btc", btc);
                settings.Add("eth", eth);
                settings.Add("doge", doge);
                settings.Add("ltc", ltc);
                settings.Add("dash", dash);
                settings.Add("xmr", xmr);
                settings.Add("bch", bch);

                return settings;
            }
        }
    }
}
