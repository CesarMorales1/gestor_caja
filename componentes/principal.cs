using apos_gestor_caja.applicationLayer.interfaces;
using apos_gestor_caja.ApplicationCapa.Services;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using apos_gestor_caja.Infrastructure.Repositories;
using apos_gestor_caja.Forms;
using SalesBookApp;

namespace apos_gestor_caja.componentes
{
    public partial class principal : Form
    {
        private Panel permissionsPanel;
        private bool isPanelExpanded = false;
        private DataGridView dataGridViewSales;  // Declaración del DataGridView
        private readonly BancoService _bancoService;

        public principal()
        {
            InitializeComponent();
            this.BackColor = Color.WhiteSmoke;
            StyleNavbarElements();
            _bancoService = new BancoService(new BancoRepository());

            // Creación del DataGridView programáticamente
            this.dataGridViewSales = new DataGridView();
            this.dataGridViewSales.ReadOnly = true;
            this.dataGridViewSales.Dock = DockStyle.Fill;
            this.dataGridViewSales.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dataGridViewSales.Location = new Point(0, navbar.Bottom);
            this.dataGridViewSales.Name = "dataGridViewSales";
            this.dataGridViewSales.Size = new Size(ClientSize.Width, ClientSize.Height - navbar.Bottom);
            this.dataGridViewSales.TabIndex = 0;
            this.dataGridViewSales.Visible = false; // Inicialmente oculto
            this.Controls.Add(this.dataGridViewSales);

            // Añadiendo estilos al DataGridView
            dataGridViewSales.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dataGridViewSales.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridViewSales.DefaultCellStyle.ForeColor = Color.FromArgb(73, 80, 87);
            dataGridViewSales.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 123, 255);
            dataGridViewSales.DefaultCellStyle.SelectionForeColor = Color.White;

            dataGridViewSales.BorderStyle = BorderStyle.None;
            dataGridViewSales.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewSales.EnableHeadersVisualStyles = false;
            dataGridViewSales.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewSales.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            dataGridViewSales.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 123, 255);
            dataGridViewSales.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewSales.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridViewSales.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            dataGridViewSales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewSales.BackgroundColor = Color.WhiteSmoke;

            // Resaltado de filas al pasar el ratón
            dataGridViewSales.CellMouseEnter += (sender, e) =>
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    dataGridViewSales.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(220, 220, 220);
                }
            };
            dataGridViewSales.CellMouseLeave += (sender, e) =>
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    dataGridViewSales.Rows[e.RowIndex].DefaultCellStyle.BackColor = dataGridViewSales.DefaultCellStyle.BackColor;
                }
            };
            this.Controls.Add(this.dataGridViewSales);
        }

        private void StyleNavbarElements()
        {
            // Style the permissions button
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 1;
            button1.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);
            button1.BackColor = Color.White;
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            button1.Cursor = Cursors.Hand;

            // Add hover effect
            button1.MouseEnter += (s, e) => {
                button1.BackColor = Color.FromArgb(240, 240, 240);
            };
            button1.MouseLeave += (s, e) => {
                button1.BackColor = Color.White;
            };

            // Style ComboBoxes
            StyleComboBox(comboBox1);
            StyleComboBox(comboBox5);
        }

        private void StyleComboBox(ComboBox comboBox)
        {
            comboBox.FlatStyle = FlatStyle.Flat;
            comboBox.Font = new Font("Segoe UI", 10F);
            comboBox.BackColor = Color.White;
            comboBox.ForeColor = Color.FromArgb(73, 80, 87);
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void principal_Load(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null && comboBox.SelectedIndex == 2) // "Generar libro de venta" option
            {
                if (permissionsPanel != null && isPanelExpanded)
                {
                    isPanelExpanded = false;
                    AnimatePanel();
                }

                // Usar el nuevo SalesBookForm
                using (var salesBookForm = new SalesBookForm())
                {
                    salesBookForm.ShowDialog();
                }

                comboBox1.SelectedIndex = 0; // Reset the combobox selection after closing the dialog
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox == null)
            {
                return;
            }
            int selectedText = comboBox.SelectedIndex;

            if (selectedText == 1)
            {
                Console.WriteLine(comboBox.SelectedIndex);
                BancoForm formBanco = new BancoForm(_bancoService);
                formBanco.ShowDialog();
            }
            else if (selectedText == 4)
            {
                EmisorForm formEmisor = new EmisorForm();
                formEmisor.ShowDialog();
            }
            else if (selectedText == 3)
            {
                UsuariosForm usuariosForm = new UsuariosForm();
                usuariosForm.ShowDialog();
            }
            else if (selectedText == 5)
            {
                CajeroForm cajeroForm = new CajeroForm();
                cajeroForm.ShowDialog();
            }
            comboBox5.SelectedIndex = 0;
        }

        private Panel CreateModulePanel(string moduleName, string[] permissions)
        {
            Panel modulePanel = new Panel
            {
                Width = 280,
                Height = 200,
                Margin = new Padding(50, 30, 50, 30),
                BackColor = Color.White
            };

            // Add shadow effect
            modulePanel.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, modulePanel.ClientRectangle,
                    Color.FromArgb(200, 200, 200), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(200, 200, 200), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(200, 200, 200), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(200, 200, 200), 1, ButtonBorderStyle.Solid);
            };

            // Module Title
            Label titleLabel = new Label
            {
                Text = moduleName,
                Font = new Font("Segoe UI Semibold", 12),
                ForeColor = Color.FromArgb(64, 64, 64),
                Dock = DockStyle.Top,
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(248, 249, 250)
            };
            modulePanel.Controls.Add(titleLabel);

            // Permissions Container
            FlowLayoutPanel permissionsContainer = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                Padding = new Padding(15, 10, 15, 10),
                AutoScroll = true
            };

            foreach (string permission in permissions)
            {
                Panel permissionRow = new Panel
                {
                    Width = 240,
                    Height = 35,
                    Margin = new Padding(0, 2, 0, 2)
                };

                CheckBox checkbox = new CheckBox
                {
                    Text = permission,
                    Font = new Font("Segoe UI", 10),
                    ForeColor = Color.FromArgb(64, 64, 64),
                    Location = new Point(5, 8),
                    AutoSize = true,
                    Cursor = Cursors.Hand
                };

                // Add hover effect
                permissionRow.MouseEnter += (s, e) =>
                {
                    permissionRow.BackColor = Color.FromArgb(248, 249, 250);
                };
                permissionRow.MouseLeave += (s, e) =>
                {
                    permissionRow.BackColor = Color.Transparent;
                };

                permissionRow.Controls.Add(checkbox);
                permissionsContainer.Controls.Add(permissionRow);
            }

            modulePanel.Controls.Add(permissionsContainer);
            return modulePanel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (permissionsPanel != null)
            {
                isPanelExpanded = !isPanelExpanded;
                AnimatePanel();
                return;
            }

            // Panel principal de permisos
            permissionsPanel = new Panel
            {
                Location = new Point(0, navbar.Bottom),
                Width = ClientRectangle.Width,
                Height = 0,
                BackColor = Color.FromArgb(248, 249, 250)
            };

            // creando panel en el header
            Panel headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.White,
                Padding = new Padding(20, 0, 20, 0)
            };

            // agregando borde al header
            headerPanel.Paint += (s, pe) =>
            {
                pe.Graphics.DrawLine(new Pen(Color.FromArgb(230, 230, 230)),
                    0, headerPanel.Height - 1,
                    headerPanel.Width, headerPanel.Height - 1);
            };

            Label headerLabel = new Label
            {
                Text = "Gestión de Permisos",
                Font = new Font("Segoe UI Semibold", 14),
                ForeColor = Color.FromArgb(52, 58, 64),
                AutoSize = true,
                Location = new Point(20, 17)
            };

            Button saveButton = new Button
            {
                Text = "Guardar Cambios",
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                Size = new Size(130, 35),
                Location = new Point(permissionsPanel.Width - 150, 12),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            // Estilo boton de guardado
            saveButton.FlatAppearance.BorderSize = 0;
            saveButton.MouseEnter += (s, ev) =>
            {
                saveButton.BackColor = Color.FromArgb(0, 105, 217);
            };
            saveButton.MouseLeave += (s, ev) =>
            {
                saveButton.BackColor = Color.FromArgb(0, 123, 255);
            };

            saveButton.Click += (s, ev) =>
            {
                using (var successDialog = new Form())
                {
                    successDialog.Text = "Éxito";
                    successDialog.Size = new Size(300, 150);
                    successDialog.StartPosition = FormStartPosition.CenterParent;
                    successDialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                    successDialog.MaximizeBox = false;
                    successDialog.MinimizeBox = false;

                    var messageLabel = new Label
                    {
                        Text = "Cambios guardados exitosamente",
                        AutoSize = true,
                        Location = new Point(20, 20),
                        Font = new Font("Segoe UI", 10)
                    };

                    var okButton = new Button
                    {
                        Text = "Aceptar",
                        DialogResult = DialogResult.OK,
                        Location = new Point(100, 70),
                        Size = new Size(100, 30),
                        BackColor = Color.FromArgb(0, 123, 255),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat
                    };
                    okButton.FlatAppearance.BorderSize = 0;

                    successDialog.Controls.Add(messageLabel);
                    successDialog.Controls.Add(okButton);
                    successDialog.ShowDialog(this);
                }
            };

            headerPanel.Controls.Add(headerLabel);
            headerPanel.Controls.Add(saveButton);
            permissionsPanel.Controls.Add(headerPanel);

            // Create modules container
            FlowLayoutPanel modulesContainer = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(20),
                BackColor = Color.FromArgb(248, 249, 250)
            };

            // Define only Banco and Emisor modules and their permissions
            var moduleDefinitions = new Dictionary<string, string[]>
            {
                { "Banco", new[] { "Ver", "Editar", "Eliminar" } },
                { "Emisor", new[] { "Ver", "Editar", "Eliminar" } }
            };

            foreach (var module in moduleDefinitions)
            {
                modulesContainer.Controls.Add(CreateModulePanel(module.Key, module.Value));
            }

            permissionsPanel.Controls.Add(modulesContainer);
            Controls.Add(permissionsPanel);

            isPanelExpanded = true;
            AnimatePanel();
        }

        private async void AnimatePanel()
        {
            const int targetHeight = 500;
            const int animationSteps = 20;
            const int animationDelay = 10;

            if (isPanelExpanded)
            {
                for (int i = 0; i <= animationSteps; i++)
                {
                    permissionsPanel.Height = (i * targetHeight) / animationSteps;
                    await Task.Delay(animationDelay);
                }
            }
            else
            {
                for (int i = animationSteps; i >= 0; i--)
                {
                    permissionsPanel.Height = (i * targetHeight) / animationSteps;
                    await Task.Delay(animationDelay);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}