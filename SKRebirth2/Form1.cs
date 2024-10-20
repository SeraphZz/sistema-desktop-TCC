using MaterialSkin;
using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Net.Http;
using System.Net.Mail;
using System.Windows.Forms;

namespace SKRebirth2
{
    public partial class Form1 : MaterialForm
    {
        // parte visual do formulário
        public Form1()
        {
            InitializeComponent(); var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.EnforceBackcolorOnAllComponents = false;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Pink400, Primary.Pink500, Primary.Pink600, Accent.Pink700, TextShade.WHITE);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'stucchiDataSet.empresa_internacional'. Você pode movê-la ou removê-la conforme necessário.
            this.empresa_internacionalTableAdapter.Fill(this.stucchiDataSet.empresa_internacional);
            // TODO: esta linha de código carrega dados na tabela 'stucchiDataSet.empresa_nacional'. Você pode movê-la ou removê-la conforme necessário.
            this.empresa_nacionalTableAdapter.Fill(this.stucchiDataSet.empresa_nacional);
            // TODO: esta linha de código carrega dados na tabela 'stucchiDataSet.empresa_nacional'. Você pode movê-la ou removê-la conforme necessário.
            this.empresa_nacionalTableAdapter.Fill(this.stucchiDataSet.empresa_nacional);
            // TODO: esta linha de código carrega dados na tabela 'stucchiDataSet.empresa_nacional'. Você pode movê-la ou removê-la conforme necessário.
            this.empresa_nacionalTableAdapter.Fill(this.stucchiDataSet.empresa_nacional);
            // TODO: esta linha de código carrega dados na tabela 'stucchiDataSet.funcionarios'. Você pode movê-la ou removê-la conforme necessário.
            this.funcionariosTableAdapter.Fill(this.stucchiDataSet.funcionarios);
            // TODO: esta linha de código carrega dados na tabela 'stucchiDataSet.funcionarios'. Você pode movê-la ou removê-la conforme necessário.
            this.funcionariosTableAdapter.Fill(this.stucchiDataSet.funcionarios);
            // TODO: esta linha de código carrega dados na tabela 'stucchiDataSet.funcionarios'. Você pode movê-la ou removê-la conforme necessário.
            this.funcionariosTableAdapter.Fill(this.stucchiDataSet.funcionarios);
            // TODO: esta linha de código carrega dados na tabela 'stucchiDataSet.empresa_internacional'. Você pode movê-la ou removê-la conforme necessário.
            this.empresa_internacionalTableAdapter.Fill(this.stucchiDataSet.empresa_internacional);
            // TODO: esta linha de código carrega dados na tabela 'stucchiDataSet.empresa_nacional'. Você pode movê-la ou removê-la conforme necessário.
            this.empresa_nacionalTableAdapter.Fill(this.stucchiDataSet.empresa_nacional);

            // código essencial para o backup, não alterar pfv :3

            string connection = "server=localhost; user id = root; password =;database=stucchi";
            MySqlConnection con = new MySqlConnection(connection);

            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
            this.reportViewer2.RefreshReport();
            this.reportViewer3.RefreshReport();
        }

        // menu de cadastro de empresas nacionais

        private void materialButton1_Click(object sender, EventArgs e)
        {
            this.empresa_nacionalBindingSource.AddNew();
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            string cnpj = MskTxTcnpj.Text;

            cnpj = cnpj.Trim();
            cnpj = cnpj.Trim().Replace(".", "").Replace("-", "").Replace("/", "");

            if (!ValidadorCNPJ.ValidarCNPJ(cnpj))
            {
                MaterialMessageBox.Show("CNPJ Inválido!");
                return;
            }

            if (!ValidarEmail.IsValidEmail(email_empresa.Text))
            {
                MaterialMessageBox.Show("Email Invalido");
                return;
            }

            if (!ValidarEmail.IsValidEmail(email_representante.Text))
            {
                MaterialMessageBox.Show("Email Invalido");
                return;
            }

            if (CNPJExists(MskTxTcnpj.Text))
            {
                MessageBox.Show("CNPJ já cadastrado! Por favor, verifique as informações e tente novamente.");
            }

            if (string.IsNullOrWhiteSpace(nome_empresa.Text) ||
                string.IsNullOrWhiteSpace(nome_representante.Text) ||
                string.IsNullOrWhiteSpace(setor_atuacao.Text) ||
                string.IsNullOrWhiteSpace(email_empresa.Text) ||
                string.IsNullOrWhiteSpace(email_representante.Text) ||
                string.IsNullOrWhiteSpace(telefone_empresa.Text) ||
                string.IsNullOrWhiteSpace(MskTxTcnpj.Text) ||
                string.IsNullOrWhiteSpace(classificacaoComboBox.Text) ||
                string.IsNullOrWhiteSpace(mskCEP.Text) ||
                string.IsNullOrWhiteSpace(txtCidade.Text) ||
                string.IsNullOrWhiteSpace(txtBairro.Text) ||
                string.IsNullOrWhiteSpace(txtRua.Text) ||
                string.IsNullOrWhiteSpace(numeroTextBox.Text) ||
                data_contrato.Value == null)
            {
                MaterialMessageBox.Show("Campo em branco! Por favor verifique as informações e tente novamente.");
            }
            else
            {
                this.Validate();
                this.empresa_nacionalBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.stucchiDataSet);
                MaterialMessageBox.Show("Empresa cadastrada com sucesso!");
            }
        }

        private bool CNPJExists(string cnpj)
        {
            bool exists = false;
            string connectionString = "server=localhost; user id = root; password =;database=stucchi";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM empresa_nacional WHERE cnpj = @cnpj";
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@cnpj", cnpj);
                conn.Open();
                int count = Convert.ToInt32(command.ExecuteScalar());
                if (count > 0)
                {
                    exists = true;
                }
            }
            return exists;
        }

        private void mskCEP_Leave(object sender, EventArgs e)
        {
            string strURL = string.Format("https://viacep.com.br/ws/{0}/json/", mskCEP.Text.Trim());
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var responde = client.GetAsync(strURL).Result;
                    if (responde.IsSuccessStatusCode)
                    {
                        var result = responde.Content.ReadAsStringAsync().Result;
                        Resultado res = JsonConvert.DeserializeObject<Resultado>(result);
                        txtCidade.Text = res.Localidade;
                        txtBairro.Text = res.Bairro;
                        txtRua.Text = res.Logradouro;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // tela de consulta de empresas nacionais


        private void materialTextBox1_TextChanged(object sender, EventArgs e)
        {
            string connection = "server=localhost; user id = root; password =;database=stucchi";
            MySqlConnection con = new MySqlConnection(connection);
            MySqlDataAdapter da;
            DataTable dt;
            con.Open();
            da = new MySqlDataAdapter("SELECT * FROM empresa_nacional WHERE nome_empresa LIKE'" + this.materialTextBox1.Text + "%'", con);
            dt = new DataTable();
            da.Fill(dt);
            empresa_nacionalBindingSource.DataSource = dt;
            con.Close();
        }

        private void materialTextBox2_TextChanged(object sender, EventArgs e)
        {
            string connection = "server=localhost; user id = root; password =;database=stucchi";
            MySqlConnection con = new MySqlConnection(connection);
            MySqlDataAdapter da;
            DataTable dt;
            con.Open();
            da = new MySqlDataAdapter("SELECT * FROM empresa_nacional WHERE setor_atuacao LIKE'" + this.materialTextBox2.Text + "%'", con);
            dt = new DataTable();
            da.Fill(dt);
            empresa_nacionalBindingSource.DataSource = dt;
            con.Close();
        }

        private void materialButton11_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView currentRow = (DataRowView)empresa_nacionalBindingSource.Current;
                string cnpj = (string)currentRow["CNPJ"];

                string connection = "server=localhost;user id=root;password=;database=stucchi";
                using (MySqlConnection con = new MySqlConnection(connection))
                {
                    con.Open();
                    string query = "DELETE FROM empresa_nacional WHERE CNPJ = @CNPJ";
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@CNPJ", cnpj);
                        cmd.ExecuteNonQuery();
                    }
                }
                empresa_nacionalBindingSource.RemoveCurrent();
                this.Validate();
                this.empresa_nacionalBindingSource.EndEdit();
                this.empresa_nacionalTableAdapter.Update(this.stucchiDataSet);

                MaterialMessageBox.Show("Registro deletado com sucesso.");
            }
            catch (Exception ex)
            {
                MaterialMessageBox.Show("Erro ao deletar registro: " + ex.Message);
            }
        }

        private void materialButton12_Click(object sender, EventArgs e)
        {
            empresa_nacionalDataGridView.ReadOnly = false;
            this.Validate();
            this.empresa_nacionalTableAdapter.Update(this.stucchiDataSet);
            this.empresa_nacionalBindingSource.EndEdit();
            MaterialMessageBox.Show("Alterações salvas com sucesso.");
        }

        // tela de cadastro de empresas internacionais

        private void materialButton3_Click(object sender, EventArgs e)
        {
            if (!ValidarEmail.IsValidEmail(EmailEmpresaInt.Text))
            {
                MaterialMessageBox.Show("Email Invalido");
                return;
            }

            if (!ValidarEmail.IsValidEmail(EmailRepInt.Text))
            {
                MaterialMessageBox.Show("Email Invalido");
                return;
            }

            if (string.IsNullOrWhiteSpace(nomeEmpresaInt.Text) ||
                string.IsNullOrWhiteSpace(RepEmpresaInt.Text) ||
                string.IsNullOrWhiteSpace(SetorAtuacaoInt.Text) ||
                string.IsNullOrWhiteSpace(EmailEmpresaInt.Text) ||
                string.IsNullOrWhiteSpace(EmailRepInt.Text) ||
                string.IsNullOrWhiteSpace(TelEmpresaInt.Text) ||
                string.IsNullOrWhiteSpace(RUC.Text) ||
                string.IsNullOrWhiteSpace(RUT.Text) ||
                string.IsNullOrWhiteSpace(paisComboBox.Text) ||
                string.IsNullOrWhiteSpace(CidadeInt.Text) ||
                string.IsNullOrWhiteSpace(BairroInt.Text) ||
                string.IsNullOrWhiteSpace(RuaInt.Text) ||
                string.IsNullOrWhiteSpace(NumInter.Text) ||
                string.IsNullOrWhiteSpace(ComplInt.Text) ||
                string.IsNullOrWhiteSpace(classInt.Text) ||
                dataInt.Value == null)
            {
                MaterialMessageBox.Show("Campo em branco! Por favor verifique as informações e tente novamente.");
            }
            else
            {
                this.Validate();
                this.empresa_internacionalBindingSource.EndEdit();
                this.empresa_internacionalTableAdapter.Update(this.stucchiDataSet);
                MaterialMessageBox.Show("Empresa cadastrada com sucesso!");
            }
        }

        private void materialButton4_Click(object sender, EventArgs e)
        {
            this.empresa_internacionalBindingSource.AddNew();
        }

        // menu de cadastros de funcionários

        private bool CpfExists(string cpf)
        {
            bool exists = false;
            string connectionString = "server=localhost; user id = root; password =;database=stucchi";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM funcionarios WHERE cpf = @cpf";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@cpf", cpf);
                connection.Open();
                int count = Convert.ToInt32(command.ExecuteScalar());
                if (count > 0)
                {
                    exists = true;
                }
            }
            return exists;
        }

        private bool EmailExists(string email)
        {
            bool exists = false;
            string connectionString = "server=localhost; user id = root; password =;database=stucchi";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM funcionarios WHERE email = @email";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@email", email);
                connection.Open();
                int count = Convert.ToInt32(command.ExecuteScalar());
                if (count > 0)
                {
                    exists = true;
                }
            }
            return exists;
        }

        private void materialButton6_Click(object sender, EventArgs e)
        {
            string CPF = cpf.Text;

            CPF = CPF.Trim();
            CPF = CPF.Replace(".", "").Replace("-", "");

            if (!ValidadorCPF.Validar(CPF))
            {
                MaterialMessageBox.Show("CPF Inválido!");
                return;
            }

            if (!ValidarEmail.IsValidEmail(email.Text))
            {
                MaterialMessageBox.Show("Email Invalido");
                return;
            }
            if (EmailExists(email.Text))
            {
                MaterialMessageBox.Show("E-mail já cadastrado! Por favor, verifique as informações e tente novamente.");
                return;
            }

            if (CpfExists(cpf.Text))
            {
                MaterialMessageBox.Show("CPF já cadastrado! Por favor, verifique as informações e tente novamente.");
                return;
            }

            if (string.IsNullOrWhiteSpace(nome.Text) ||
                string.IsNullOrWhiteSpace(email.Text) ||
                string.IsNullOrWhiteSpace(cpf.Text) ||
                string.IsNullOrWhiteSpace(telefone.Text) ||
                string.IsNullOrWhiteSpace(senha.Text) ||
                string.IsNullOrWhiteSpace(nivelCombo.Text) ||
                string.IsNullOrWhiteSpace(mskCep2.Text) ||
                string.IsNullOrWhiteSpace(txtCidade1.Text) ||
                string.IsNullOrWhiteSpace(txtBairro1.Text) ||
                string.IsNullOrWhiteSpace(txtRua1.Text) ||
                string.IsNullOrWhiteSpace(txtNumero1.Text) ||
                string.IsNullOrWhiteSpace(estadoCombo.Text))
            {
                MaterialMessageBox.Show("Campo em branco! Por favor verifique as informações e tente novamente.");
            }
            else
            {
                try
                {
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress("stucchisistemas@gmail.com");
                        mail.To.Add(email.Text);
                        mail.Subject = "Dados de Acesso";
                        mail.Body =

                            mail.Body =

           @"

            <body>
            <img src='https://www.hydraquip.com/wp-content/uploads/2017/12/stucchi_logo_cmyk_vert-1.png' width='150px' height='170px' alt='Stucchi Logo'>
            <h3>Olá " + nome.Text + @",</>
            <h1>Estamos felizes em compartilhar essa novidade com você!</h1>
            <h2>Seja bem-vindo aos sistemas da Stucchi!</h2>
            <p>Aqui segue em anexo os dados necessários para efetuar a entrada no sistema:</p>
            <p><strong>Email:</strong> " + email.Text + @"</p>
            <p><strong>Senha:</strong> " + senha.Text + @"</p>
            <p>Stucchi © 2024</p>
            <p>Source Kode ©; Todos os direitos reservados.</p>
            </body>
            </html>";
                        mail.IsBodyHtml = true;

                        using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                        {
                            smtp.Credentials = new System.Net.NetworkCredential("stucchisistemas@gmail.com", "vwtl inxu jhqx wdlt");
                            smtp.EnableSsl = true;
                            smtp.Send(mail);

                            this.Validate();
                            this.funcionariosBindingSource.EndEdit();
                            this.funcionariosTableAdapter.Update(this.stucchiDataSet);
                            MaterialMessageBox.Show("Funcionário cadastrado com sucesso.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MaterialMessageBox.Show(ex.Message);
                }
            }
        }

        private void mskCep2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(mskCep2.Text))
            {
                MaterialMessageBox.Show("Digite um Cep valido");
            }
            else
            {
                string strURL = string.Format("https://viacep.com.br/ws/{0}/json/", mskCep2.Text.Trim());
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        var responde = client.GetAsync(strURL).Result;
                        if (responde.IsSuccessStatusCode)
                        {
                            var result = responde.Content.ReadAsStringAsync().Result;
                            Resultado res = JsonConvert.DeserializeObject<Resultado>(result);
                            txtCidade1.Text = res.Localidade;
                            txtBairro1.Text = res.Bairro;
                            txtRua1.Text = res.Logradouro;
                            estadoCombo.Text = res.UF;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MaterialMessageBox.Show(ex.Message);
                }
            }
        }

        private void materialButton5_Click(object sender, EventArgs e)
        {
            this.funcionariosBindingSource.AddNew();
        }

        // Consulta de funcionários

        private void materialTextBox3_TextChanged(object sender, EventArgs e)
        {
            string connection = "server=localhost; user id = root; password =;database=stucchi";
            MySqlConnection con = new MySqlConnection(connection);
            MySqlDataAdapter da;
            DataTable dt;
            con.Open();
            da = new MySqlDataAdapter("SELECT * FROM funcionarios WHERE nome LIKE'" + this.materialTextBox3.Text + "%'", con);
            dt = new DataTable();
            da.Fill(dt);
            funcionariosDataGridView.DataSource = dt;
            con.Close();
        }

        private void materialTextBox4_TextChanged(object sender, EventArgs e)
        {
            string connection = "server=localhost; user id = root; password =;database=stucchi";
            MySqlConnection con = new MySqlConnection(connection);
            MySqlDataAdapter da;
            DataTable dt;
            con.Open();
            da = new MySqlDataAdapter("SELECT * FROM funcionarios WHERE nivel LIKE'" + this.materialTextBox4.Text + "%'", con);
            dt = new DataTable();
            da.Fill(dt);
            funcionariosDataGridView.DataSource = dt;
            con.Close();
        }

        private void materialButton14_Click(object sender, EventArgs e)
        {
            try 
            {
                int rowIndex = funcionariosDataGridView.CurrentRow.Index;
                DataRowView currentRow = (DataRowView)funcionariosBindingSource[rowIndex];
                currentRow.Delete();
                this.Validate();
                this.funcionariosBindingSource.EndEdit();
                this.funcionariosTableAdapter.Update(this.stucchiDataSet);
                MaterialMessageBox.Show("Registro deletado com sucesso.");
                funcionariosDataGridView.DataSource = null;
                funcionariosDataGridView.DataSource = funcionariosBindingSource;
            }
            catch (Exception ex)
            {
                MaterialMessageBox.Show("Erro ao deletar registro: " + ex.Message);
            }
        }
        // menu de backup

        private void materialButton7_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog escolhaArquivo = new FolderBrowserDialog();
            if (escolhaArquivo.ShowDialog() == DialogResult.OK)
            {
                localtxt.Text = escolhaArquivo.SelectedPath;
            }
        }

        private void materialButton8_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(arquivotxt.Text))
                {
                    MaterialMessageBox.Show("Defina um nome para o arquivo de backup!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(localtxt.Text))
                {
                    MaterialMessageBox.Show("Defina um local para o arquivo de backup!");
                    return;
                }
                else
                {
                    string connection = "server=localhost; user id = root; password =;database=stucchi";
                    MySqlConnection con = new MySqlConnection(connection);
                    string caminho = localtxt.Text + "\\";
                    string nome = arquivotxt.Text + ".sql";
                    string nomearquivo = caminho + nome;
                    MySqlCommand cmd = new MySqlCommand("", con);
                    MySqlBackup mb = new MySqlBackup(cmd);
                    con.Open();
                    mb.ExportToFile(nomearquivo);
                    con.Close();
                    MaterialMessageBox.Show("Backup Realizado com sucesso");
                }
            }
            catch (Exception ex)
            {
                MaterialMessageBox.Show("Erro:", ex.Message);
            }
        }

        private void materialButton10_Click(object sender, EventArgs e)
        {
            OpenFileDialog escolhaArquivo = new OpenFileDialog();
            if (escolhaArquivo.ShowDialog() == DialogResult.OK)
            {
                arquivo2.Text = escolhaArquivo.FileName;
            }
        }

        private void materialButton9_Click(object sender, EventArgs e)
        {
            string connection = "server=localhost; user id=root; password=;database=stucchi";
            using (MySqlConnection con = new MySqlConnection(connection))
            {
                con.Open();
                string dropTablesQuery = 
                    "DROP TABLE IF EXISTS `empresa_internacional`, `empresa_nacional`, `funcionarios`; ";
                using (MySqlCommand cmd = new MySqlCommand(dropTablesQuery, con))
                {
                    cmd.ExecuteNonQuery();
                }
                string arquivo = arquivo2.Text;
                using (MySqlCommand cmd = new MySqlCommand("", con))
                {
                    MySqlBackup mb = new MySqlBackup(cmd);
                    mb.ImportFromFile(arquivo);
                }
                con.Close();
            }


            this.empresa_nacionalTableAdapter.Fill(this.stucchiDataSet.empresa_nacional);
            empresa_nacionalBindingSource.DataSource = this.stucchiDataSet.empresa_nacional; 
            empresa_nacionalBindingSource.ResetBindings(false); 
            empresa_nacionalDataGridView.DataSource = null;
            empresa_nacionalDataGridView.DataSource = empresa_nacionalBindingSource;

            this.empresa_internacionalTableAdapter.Fill(this.stucchiDataSet.empresa_internacional);
            empresa_internacionalDataGridView.DataSource = null;
            empresa_internacionalDataGridView.DataSource = empresa_internacionalBindingSource;

            this.funcionariosTableAdapter.Fill(this.stucchiDataSet.funcionarios);
            funcionariosDataGridView.DataSource = null;
            funcionariosDataGridView.DataSource = funcionariosBindingSource;

            MaterialMessageBox.Show("Backup importado com sucesso.");
        }

        // menu consulta internacional


        private void materialTextBox5_TextChanged(object sender, EventArgs e)
        {
            string connection = "server=localhost; user id = root; password =;database=stucchi";
            MySqlConnection con = new MySqlConnection(connection);
            MySqlDataAdapter da;
            DataTable dt;
            con.Open();
            da = new MySqlDataAdapter("SELECT * FROM empresa_internacional WHERE nome_empresa LIKE'" + this.materialTextBox5.Text + "%'", con);
            dt = new DataTable();
            da.Fill(dt);
            empresa_internacionalDataGridView.DataSource = dt;
            con.Close();
        }

        private void materialTextBox6_TextChanged(object sender, EventArgs e)
        {
            string connection = "server=localhost; user id = root; password =;database=stucchi";
            MySqlConnection con = new MySqlConnection(connection);
            MySqlDataAdapter da;
            DataTable dt;
            con.Open();
            da = new MySqlDataAdapter("SELECT * FROM empresa_internacional WHERE setor_atuacao LIKE'" + this.materialTextBox6.Text + "%'", con);
            dt = new DataTable();
            da.Fill(dt);
            empresa_internacionalDataGridView.DataSource = dt;
            con.Close();
        }

        private void materialTextBox7_TextChanged(object sender, EventArgs e)
        {
            string connection = "server=localhost; user id = root; password =;database=stucchi";
            MySqlConnection con = new MySqlConnection(connection);
            MySqlDataAdapter da;
            DataTable dt;
            con.Open();
            da = new MySqlDataAdapter("SELECT * FROM empresa_internacional WHERE pais LIKE'" + this.materialTextBox7.Text + "%'", con);
            dt = new DataTable();
            da.Fill(dt);
            empresa_internacionalDataGridView.DataSource = dt;
            con.Close();
        }

        private void materialButton13_Click_1(object sender, EventArgs e)
        {
            try
            {
                DataRowView currentRow = (DataRowView)empresa_internacionalBindingSource.Current;
                string ruc = (string)currentRow["ruc"];
                string rut = (string)currentRow["rut"];
                string connection = "server=localhost;user id=root;password=;database=stucchi";
                using (MySqlConnection con = new MySqlConnection(connection))
                {
                    con.Open();
                    string query = "DELETE FROM empresa_internacional WHERE ruc = @rut AND rut = @rut";
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ruc", ruc);
                        cmd.Parameters.AddWithValue("@rut", rut);
                        cmd.ExecuteNonQuery();
                    }
                }
                this.Validate();
                empresa_internacionalBindingSource.RemoveCurrent();
                this.empresa_internacionalBindingSource.EndEdit();
                this.empresa_internacionalTableAdapter.Update(this.stucchiDataSet);
                empresa_internacionalDataGridView.DataSource = null;
                empresa_internacionalDataGridView.DataSource = empresa_internacionalBindingSource;

                MaterialMessageBox.Show("Registro deletado com sucesso.");
            }
            catch (Exception ex)
            {
                MaterialMessageBox.Show("Erro ao deletar registro: " + ex.Message);
            }
        }



        private void Logout_Click(object sender, EventArgs e)
        {
            if (MaterialMessageBox.Show("Tem certeza que deseja sair da sua conta??", "Sair", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Login telalogin = new Login();
                telalogin.Show();
                this.Hide();
            }
        }


    }
}
    