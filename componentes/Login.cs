using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MyApp.Infrastructure.Database;
using apos_gestor_caja.componentes;

namespace apos_gestor_caja.componentes
{
    public partial class Login : Form
    {
        private SqlHelper dbConecction;

        public Login()
        {
            InitializeComponent();
            this.dbConecction = new SqlHelper();
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Login_keyup);
            this.KeyPreview = true;
        }

        private void Login_keyup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.loginButton.PerformClick();
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
        }

        private void Login_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string usuario = this.usuario.Text;
            string password = this.password.Text;

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (LoginUsuario(usuario, password))
            {
                principal vistaPrincipal = new principal();
                vistaPrincipal.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool LoginUsuario(string usuario, string password)
        {
            MySqlConnection connection = null;
            try
            {
                connection = dbConecction.ObtenerConexion();
                string query = "SELECT COUNT(*) FROM usuarios WHERE usuario = @usuario AND password = @password";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@password", password);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (connection != null)
                {
                    dbConecction.CerrarConexion(connection); // Ahora pasamos la conexión específica
                }
            }
        }

        private void password_TextChanged(object sender, EventArgs e)
        {
        }
    }
}