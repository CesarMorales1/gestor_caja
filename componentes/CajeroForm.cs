using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using apos_gestor_caja.Domain.Models;
using apos_gestor_caja.ApplicationLayer.Services;

namespace apos_gestor_caja.Forms
{
    public partial class CajeroForm : Form
    {
        private readonly CajeroService _cajeroService;
        private bool isEditMode = false;
        private int? editingCajeroId = null;
        private const string PLACEHOLDER_USUARIO = "Ingrese el usuario";
        private const string PLACEHOLDER_CLAVE = "Ingrese la clave";
        private const string PLACEHOLDER_NOMBRE = "Ingrese el nombre";

        public CajeroForm()
        {
            InitializeComponent();
            _cajeroService = new CajeroService();

            ConfigurarFormulario();
            ConfigurarDataGridView();
            ConfigurarPlaceholders();

            // Suscribir al evento Load para cargar los datos de forma asíncrona
            this.Load += async (s, e) => await InicializarDatosAsync();
        }

        private async Task InicializarDatosAsync()
        {
            try
            {
                await CargarCajerosAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos iniciales: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarFormulario()
        {
            // Configurar combo de nivel
            cajComboNivel.Items.AddRange(new object[] { 5, 9 });
            cajComboNivel.SelectedIndex = 0;

            // Configurar búsqueda con RegexFilterComboBox
            cajBusquedaCombo.Text = "Buscar cajero...";
            cajBusquedaCombo.ForeColor = Color.Gray;
            cajBusquedaCombo.TextChanged += async (s, e) => await BuscarCajeros();

            // Suscribir eventos
            cajBotonNuevo.Click += (s, e) => LimpiarFormulario();
            cajBotonGuardar.Click += async (s, e) => await GuardarCajero();
            cajBotonCancelar.Click += (s, e) => LimpiarFormulario();

            // Configurar eventos de búsqueda
            cajBusquedaCombo.Enter += (s, e) =>
            {
                if (cajBusquedaCombo.Text == "Buscar cajero...")
                {
                    cajBusquedaCombo.Text = "";
                    cajBusquedaCombo.ForeColor = Color.Black;
                }
            };

            cajBusquedaCombo.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(cajBusquedaCombo.Text))
                {
                    cajBusquedaCombo.Text = "Buscar cajero...";
                    cajBusquedaCombo.ForeColor = Color.Gray;
                }
            };
        }

        private void ConfigurarDataGridView()
        {
            // Configuración básica
            cajListadoGrid.AutoGenerateColumns = false;
            cajListadoGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            cajListadoGrid.MultiSelect = false;
            cajListadoGrid.ReadOnly = true;
            cajListadoGrid.AllowUserToAddRows = false;
            cajListadoGrid.RowHeadersVisible = false;
            cajListadoGrid.Columns.Clear();

            // Configurar columnas
            var idColumn = new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                HeaderText = "ID",
                Width = 60,
                Visible = true
            };

            var usuarioColumn = new DataGridViewTextBoxColumn
            {
                Name = "Usuario",
                DataPropertyName = "Usuario",
                HeaderText = "Usuario",
                Width = 120
            };

            var nombreColumn = new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                DataPropertyName = "Nombre",
                HeaderText = "Nombre",
                Width = 200
            };

            var nivelColumn = new DataGridViewTextBoxColumn
            {
                Name = "NivelAcceso",
                DataPropertyName = "NivelAcceso",
                HeaderText = "Nivel",
                Width = 80
            };

            var barraColumn = new DataGridViewTextBoxColumn
            {
                Name = "Barra",
                DataPropertyName = "Barra",
                HeaderText = "Barra",
                Width = 80
            };

            cajListadoGrid.Columns.AddRange(new DataGridViewColumn[]
            {
                idColumn,
                usuarioColumn,
                nombreColumn,
                nivelColumn,
                barraColumn
            });

            // Configurar evento de doble clic
            cajListadoGrid.CellDoubleClick += async (s, e) =>
            {
                try
                {
                    if (e.RowIndex >= 0)
                    {
                        var row = cajListadoGrid.Rows[e.RowIndex];
                        if (row != null)
                        {
                            var idCell = row.Cells["Id"];
                            if (idCell != null && idCell.Value != null)
                            {
                                var idValue = idCell.Value.ToString();
                                Console.WriteLine($"ID seleccionado: {idValue}"); // Debug log

                                if (int.TryParse(idValue, out int id))
                                {
                                    await CargarCajeroParaEdicion(id);
                                }
                                else
                                {
                                    MessageBox.Show($"Error: El ID '{idValue}' no es válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Error: No se pudo obtener el ID del cajero seleccionado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al seleccionar cajero: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            StyleDataGridView();
        }

        private void StyleDataGridView()
        {
            cajListadoGrid.EnableHeadersVisualStyles = false;
            cajListadoGrid.BorderStyle = BorderStyle.None;
            cajListadoGrid.BackgroundColor = Color.WhiteSmoke;
            cajListadoGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 123, 255);
            cajListadoGrid.DefaultCellStyle.SelectionForeColor = Color.White;
            cajListadoGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 123, 255);
            cajListadoGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            cajListadoGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            cajListadoGrid.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            cajListadoGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
        }

        private async Task CargarCajerosAsync()
        {
            try
            {
                var cajeros = await _cajeroService.ObtenerCajerosAsync();
                Console.WriteLine($"Cajeros cargados: {cajeros.Count}"); // Debug log
                ActualizarGrilla(cajeros);
                ActualizarBusquedaCombo(cajeros);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los cajeros: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarBusquedaCombo(List<Cajero> cajeros)
        {
            cajBusquedaCombo.SetItems(cajeros.ConvertAll(c => c.ToString()));
        }

        private async Task BuscarCajeros()
        {
            if (cajBusquedaCombo.Text == "Buscar cajero...") return;

            try
            {
                var cajeros = await _cajeroService.BuscarCajerosPorNombreAsync(cajBusquedaCombo.Text.Trim());
                ActualizarGrilla(cajeros);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar cajeros: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarGrilla(List<Cajero> cajeros)
        {
            try
            {
                cajListadoGrid.DataSource = null;

                // Debug: Imprimir información de cada cajero
                foreach (var cajero in cajeros)
                {
                    Console.WriteLine($"Cajero a mostrar - ID: {cajero.Id}, Usuario: {cajero.Usuario}, Nombre: {cajero.Nombre}");
                }

                var bindingSource = new BindingSource();
                bindingSource.DataSource = cajeros;
                cajListadoGrid.DataSource = bindingSource;

                // Verificar que las columnas estén correctamente configuradas
                foreach (DataGridViewColumn col in cajListadoGrid.Columns)
                {
                    Console.WriteLine($"Columna: {col.Name}, DataPropertyName: {col.DataPropertyName}, Visible: {col.Visible}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar la grilla: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarCajeroParaEdicion(int id)
        {
            try
            {
                Console.WriteLine($"Intentando cargar cajero con ID: {id}"); // Debug log
                var cajero = await _cajeroService.ObtenerCajeroPorIdAsync(id);
                if (cajero != null)
                {
                    Console.WriteLine($"Cajero encontrado - ID: {cajero.Id}, Usuario: {cajero.Usuario}"); // Debug log
                    editingCajeroId = cajero.Id;
                    isEditMode = true;

                    // Actualizar campos con los datos del cajero
                    cajInputUsuario.Text = cajero.Usuario;
                    cajInputUsuario.ForeColor = Color.Black;

                    cajInputNombre.Text = cajero.Nombre;
                    cajInputNombre.ForeColor = Color.Black;

                    cajComboNivel.SelectedItem = cajero.NivelAcceso;

                    // Resetear campo de clave
                    cajInputClave.Text = PLACEHOLDER_CLAVE;
                    cajInputClave.ForeColor = Color.Gray;
                    cajInputClave.PasswordChar = '\0';

                    cajLabelTitulo.Text = "Editar Cajero";
                }
                else
                {
                    MessageBox.Show($"No se encontró el cajero con ID: {id}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el cajero: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task GuardarCajero()
        {
            if (!ValidarFormulario()) return;

            try
            {
                var cajero = new Cajero
                {
                    Id = editingCajeroId ?? 0,
                    Usuario = cajInputUsuario.Text.Trim(),
                    Clave = cajInputClave.Text == PLACEHOLDER_CLAVE ? "" : cajInputClave.Text,
                    Nombre = cajInputNombre.Text.Trim(),
                    NivelAcceso = Convert.ToInt32(cajComboNivel.SelectedItem),
                    Activo = true
                };

                bool resultado;
                if (isEditMode)
                    resultado = await _cajeroService.ActualizarCajeroAsync(cajero);
                else
                    resultado = await _cajeroService.CrearCajeroAsync(cajero);

                if (resultado)
                {
                    MessageBox.Show("Cajero guardado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                    await CargarCajerosAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el cajero: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarFormulario()
        {
            if (cajInputUsuario.Text == PLACEHOLDER_USUARIO || string.IsNullOrWhiteSpace(cajInputUsuario.Text))
            {
                MessageBox.Show("El usuario es requerido", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cajInputUsuario.Focus();
                return false;
            }

            if (!isEditMode && (cajInputClave.Text == PLACEHOLDER_CLAVE || string.IsNullOrWhiteSpace(cajInputClave.Text)))
            {
                MessageBox.Show("La clave es requerida", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cajInputClave.Focus();
                return false;
            }

            if (cajInputNombre.Text == PLACEHOLDER_NOMBRE || string.IsNullOrWhiteSpace(cajInputNombre.Text))
            {
                MessageBox.Show("El nombre es requerido", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cajInputNombre.Focus();
                return false;
            }

            return true;
        }

        private void LimpiarFormulario()
        {
            // Restaurar placeholders
            ConfigurarPlaceholders();

            // Resetear campos
            cajComboNivel.SelectedIndex = 0;
            isEditMode = false;
            editingCajeroId = null;
            cajLabelTitulo.Text = "Nuevo Cajero";
            cajInputUsuario.Focus();
        }
        private void ConfigurarPlaceholders()
        {
            // Configurar placeholders
            ConfigurarPlaceholder(cajInputUsuario, PLACEHOLDER_USUARIO);
            ConfigurarPlaceholder(cajInputClave, PLACEHOLDER_CLAVE, true);
            ConfigurarPlaceholder(cajInputNombre, PLACEHOLDER_NOMBRE);
        }

        private void ConfigurarPlaceholder(TextBox textBox, string placeholder, bool isPassword = false)
        {
            textBox.Text = placeholder;
            textBox.ForeColor = Color.Gray;
            if (isPassword) textBox.PasswordChar = '\0';

            textBox.Enter += (s, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                    if (isPassword) textBox.PasswordChar = '*';
                }
            };

            textBox.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Gray;
                    if (isPassword) textBox.PasswordChar = '\0';
                }
            };
        }
    }
}