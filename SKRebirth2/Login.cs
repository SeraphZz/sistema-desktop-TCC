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
    public partial class Login : MaterialForm
    {
        public Login()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.EnforceBackcolorOnAllComponents = false;
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Pink500, Primary.Pink600, Primary.Pink700, Accent.Pink700, TextShade.WHITE);
        }

        private void Login_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'stucchiDataSet.funcionarios'. Você pode movê-la ou removê-la conforme necessário.
            this.funcionariosTableAdapter.Fill(this.stucchiDataSet.funcionarios);

        }

        private void funcionariosBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.funcionariosBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.stucchiDataSet);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            DataTableReader read = new DataTableReader(stucchiDataSet.funcionarios); 

            bool logado = false;
            string nivel = "";
            string nome = "";

            if (String.Compare(emailTextBox.Text, "") != 0 && (String.Compare(senhaTextBox.Text, "") != 0))

            {
                while (read.Read())
                {
                    if (String.Compare(emailTextBox.Text, read.GetString(1)) == 0 &&
                       (String.Compare(senhaTextBox.Text, read.GetString(10))) == 0)
                    {
                        nivel = read.GetString(11);
                        logado = true;
                        break;
                    }
                }

                if (logado)
                {

                    if (nivel == "ADM")
                    {
                        Form1 principal = new Form1();
                        nome = read.GetString(0);
                        principal.label1.Text = "Olá! " + nome + ", Seja bem-vindo aos Sistemas da Stucchi";
                        principal.Show();
                        this.Close();
                    }

                    else
                    {
                        Form1 principal = new Form1();
                        principal.backuptab.Parent = null;
                        principal.funcionariostab.Parent = null;
                        principal.funcrelatorio.Parent = null;
                        nome = read.GetString(0);
                        principal.label1.Text = "Olá! " + nome + ", Seja bem-vindo aos Sistemas da Stucchi";
                        principal.Show();
                        this.Close();
                    }
                }
                else
                {
                    MaterialMessageBox.Show("Login ou Senha incorretas, por favor verifique as informações e tente novamente.");
                }
            }
            else
            {
                MaterialMessageBox.Show("Por favor, preencha todos os campos.");
            }
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            if (MaterialMessageBox.Show("Tem certeza que deseja sair da sua conta??", "Sair", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
