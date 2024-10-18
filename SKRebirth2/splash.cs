using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Animations;
using MaterialSkin.Controls;
using MaterialSkin.Properties;
namespace SKRebirth2
{
    public partial class splash : MaterialForm
    {
        public splash()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Pink700, Primary.Pink700, Primary.Pink700, Accent.Pink700, TextShade.WHITE);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (materialProgressBar1.Value < 100)
            {
                materialProgressBar1.Value = materialProgressBar1.Value + 4;
            }
            else
            {
                timer1.Enabled = false;
                this.Visible = false;
                Login outro = new Login();
                outro.Show();
            }
        }
    }
}
