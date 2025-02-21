using apos_gestor_caja.applicationLayer.interfaces;
using apos_gestor_caja.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace apos_gestor_caja.componentes
{
    public partial class BancoForm: Form
    {
        private readonly IBancoService _bancoRepository;
        public BancoForm(IBancoService bancoRepository)
        {
            InitializeComponent();
            _bancoRepository = bancoRepository;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        // Event handler for when the user clicks 'Guardar' button
        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Banco nuevoBanco = new Banco { Nombre = nombreBanco.Text };
                await _bancoRepository.AddBancoAsync(nuevoBanco);
                MessageBox.Show("Banco guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Or refresh the form or navigate to another part of the app
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el banco: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Log the error for diagnostic purposes
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
