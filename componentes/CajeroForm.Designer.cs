namespace apos_gestor_caja.Forms
{
    partial class CajeroForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.cajBusquedaCombo = new apos_gestor_caja.componentes.reutilizables.RegexFilterComboBox();
            this.cajListadoGrid = new System.Windows.Forms.DataGridView();
            this.cajPanelEdicion = new System.Windows.Forms.Panel();
            this.cajLabelTitulo = new System.Windows.Forms.Label();
            this.cajInputUsuario = new System.Windows.Forms.TextBox();
            this.cajInputClave = new System.Windows.Forms.TextBox();
            this.cajInputNombre = new System.Windows.Forms.TextBox();
            this.cajComboNivel = new System.Windows.Forms.ComboBox();
            this.cajBotonGuardar = new System.Windows.Forms.Button();
            this.cajBotonCancelar = new System.Windows.Forms.Button();
            this.cajBotonNuevo = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cajListadoGrid)).BeginInit();
            this.SuspendLayout();

            // splitContainer
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Size = new System.Drawing.Size(1200, 700);
            this.splitContainer.SplitterDistance = 800;
            this.splitContainer.TabIndex = 0;

            // Panel1 - Lista de Cajeros
            this.splitContainer.Panel1.Controls.Add(this.cajListadoGrid);
            this.splitContainer.Panel1.Controls.Add(this.cajBusquedaCombo);

            // cajBusquedaCombo
            this.cajBusquedaCombo.Location = new System.Drawing.Point(12, 12);
            this.cajBusquedaCombo.Name = "cajBusquedaCombo";
            this.cajBusquedaCombo.Size = new System.Drawing.Size(776, 30);
            this.cajBusquedaCombo.TabIndex = 0;
            this.cajBusquedaCombo.Text = "Buscar cajero...";
            this.cajBusquedaCombo.Font = new System.Drawing.Font("Segoe UI", 10F);

            // cajListadoGrid
            this.cajListadoGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.cajListadoGrid.Location = new System.Drawing.Point(12, 48);
            this.cajListadoGrid.Name = "cajListadoGrid";
            this.cajListadoGrid.Size = new System.Drawing.Size(776, 640);
            this.cajListadoGrid.TabIndex = 1;
            this.cajListadoGrid.ReadOnly = true;
            this.cajListadoGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.cajListadoGrid.MultiSelect = false;
            this.cajListadoGrid.AllowUserToAddRows = false;
            this.cajListadoGrid.AllowUserToDeleteRows = false;
            this.cajListadoGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // Panel2 - Formulario de Edición
            this.splitContainer.Panel2.Controls.Add(this.cajPanelEdicion);

            // cajPanelEdicion
            this.cajPanelEdicion.Location = new System.Drawing.Point(6, 12);
            this.cajPanelEdicion.Name = "cajPanelEdicion";
            this.cajPanelEdicion.Size = new System.Drawing.Size(382, 676);
            this.cajPanelEdicion.TabIndex = 0;
            this.cajPanelEdicion.Padding = new System.Windows.Forms.Padding(12);

            // cajLabelTitulo
            this.cajLabelTitulo.AutoSize = true;
            this.cajLabelTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.cajLabelTitulo.Location = new System.Drawing.Point(12, 12);
            this.cajLabelTitulo.Name = "cajLabelTitulo";
            this.cajLabelTitulo.Size = new System.Drawing.Size(200, 25);
            this.cajLabelTitulo.TabIndex = 0;
            this.cajLabelTitulo.Text = "Datos del Cajero";

            // cajInputUsuario
            this.cajInputUsuario.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cajInputUsuario.Location = new System.Drawing.Point(12, 72);
            this.cajInputUsuario.Name = "cajInputUsuario";
            this.cajInputUsuario.Size = new System.Drawing.Size(358, 30);
            this.cajInputUsuario.TabIndex = 1;
            this.cajInputUsuario.Text = "Usuario";
            this.cajInputUsuario.ForeColor = System.Drawing.Color.Gray;

            // cajInputClave
            this.cajInputClave.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cajInputClave.Location = new System.Drawing.Point(12, 112);
            this.cajInputClave.Name = "cajInputClave";
            this.cajInputClave.Size = new System.Drawing.Size(358, 30);
            this.cajInputClave.TabIndex = 2;
            this.cajInputClave.PasswordChar = '*';
            this.cajInputClave.Text = "Clave";
            this.cajInputClave.ForeColor = System.Drawing.Color.Gray;

            // cajInputNombre
            this.cajInputNombre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cajInputNombre.Location = new System.Drawing.Point(12, 152);
            this.cajInputNombre.Name = "cajInputNombre";
            this.cajInputNombre.Size = new System.Drawing.Size(358, 30);
            this.cajInputNombre.TabIndex = 3;
            this.cajInputNombre.Text = "Nombre";
            this.cajInputNombre.ForeColor = System.Drawing.Color.Gray;

            // cajComboNivel
            this.cajComboNivel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cajComboNivel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cajComboNivel.FormattingEnabled = true;
            this.cajComboNivel.Location = new System.Drawing.Point(12, 192);
            this.cajComboNivel.Name = "cajComboNivel";
            this.cajComboNivel.Size = new System.Drawing.Size(358, 30);
            this.cajComboNivel.TabIndex = 4;

            // Botones de Acción
            this.cajBotonNuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.cajBotonNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cajBotonNuevo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.cajBotonNuevo.ForeColor = System.Drawing.Color.White;
            this.cajBotonNuevo.Location = new System.Drawing.Point(12, 232);
            this.cajBotonNuevo.Name = "cajBotonNuevo";
            this.cajBotonNuevo.Size = new System.Drawing.Size(116, 35);
            this.cajBotonNuevo.TabIndex = 5;
            this.cajBotonNuevo.Text = "Nuevo";
            this.cajBotonNuevo.UseVisualStyleBackColor = false;

            this.cajBotonGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.cajBotonGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cajBotonGuardar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.cajBotonGuardar.ForeColor = System.Drawing.Color.White;
            this.cajBotonGuardar.Location = new System.Drawing.Point(134, 232);
            this.cajBotonGuardar.Name = "cajBotonGuardar";
            this.cajBotonGuardar.Size = new System.Drawing.Size(116, 35);
            this.cajBotonGuardar.TabIndex = 6;
            this.cajBotonGuardar.Text = "Guardar";
            this.cajBotonGuardar.UseVisualStyleBackColor = false;

            this.cajBotonCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.cajBotonCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cajBotonCancelar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.cajBotonCancelar.ForeColor = System.Drawing.Color.White;
            this.cajBotonCancelar.Location = new System.Drawing.Point(256, 232);
            this.cajBotonCancelar.Name = "cajBotonCancelar";
            this.cajBotonCancelar.Size = new System.Drawing.Size(116, 35);
            this.cajBotonCancelar.TabIndex = 7;
            this.cajBotonCancelar.Text = "Cancelar";
            this.cajBotonCancelar.UseVisualStyleBackColor = false;

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.splitContainer);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "CajeroForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión de Cajeros";

            this.cajPanelEdicion.Controls.Add(this.cajLabelTitulo);
            this.cajPanelEdicion.Controls.Add(this.cajInputUsuario);
            this.cajPanelEdicion.Controls.Add(this.cajInputClave);
            this.cajPanelEdicion.Controls.Add(this.cajInputNombre);
            this.cajPanelEdicion.Controls.Add(this.cajComboNivel);
            this.cajPanelEdicion.Controls.Add(this.cajBotonNuevo);
            this.cajPanelEdicion.Controls.Add(this.cajBotonGuardar);
            this.cajPanelEdicion.Controls.Add(this.cajBotonCancelar);

            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cajListadoGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #region Windows Form Designer generated code

        private System.Windows.Forms.SplitContainer splitContainer;
        private apos_gestor_caja.componentes.reutilizables.RegexFilterComboBox cajBusquedaCombo;
        private System.Windows.Forms.DataGridView cajListadoGrid;
        private System.Windows.Forms.Panel cajPanelEdicion;
        private System.Windows.Forms.Label cajLabelTitulo;
        private System.Windows.Forms.TextBox cajInputUsuario;
        private System.Windows.Forms.TextBox cajInputClave;
        private System.Windows.Forms.TextBox cajInputNombre;
        private System.Windows.Forms.ComboBox cajComboNivel;
        private System.Windows.Forms.Button cajBotonGuardar;
        private System.Windows.Forms.Button cajBotonCancelar;
        private System.Windows.Forms.Button cajBotonNuevo;

        #endregion
    }
}