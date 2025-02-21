using MySql.Data.MySqlClient;
using MyApp.Infrastructure.Database;
using System;
using System.Windows.Forms;

namespace apos_gestor_caja.componentes
{
    public partial class UsuariosForm : Form
    {
        private SqlHelper dbConnection;

        public UsuariosForm()
        {
            dbConnection = new SqlHelper();
            InitializeComponent();
        }

        private void UsuariosForm_Load(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void Nombre_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string usuario = textBox1.Text;
            string nombre = this.name.Text;
            string password = this.password.Text;

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (InsertarUsuario(usuario, nombre, password))
            {
                MessageBox.Show("Usuario registrado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error al registrar el usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool InsertarUsuario(string usuario, string nombre, string password)
        {
            MySqlConnection connection = null;
            try
            {
                connection = dbConnection.ObtenerConexion();
                string query = "INSERT INTO usuarios (usuario, nombre, password, activo) VALUES (@usuario, @nombre, @password, @activo)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@usuario", usuario);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@activo", true);

                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (connection != null)
                {
                    dbConnection.CerrarConexion(connection); // Pasamos la conexión específica
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}