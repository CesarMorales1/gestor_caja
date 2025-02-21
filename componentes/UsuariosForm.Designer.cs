namespace apos_gestor_caja.componentes
{
    partial class UsuariosForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.TextBox();
            this.Nombre = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(118, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Registro de usuario";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(89, 149);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(279, 26);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(85, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Usuario:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(84, 223);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "Contraseña:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(90, 251);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(279, 26);
            this.password.TabIndex = 4;
            this.password.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // Nombre
            // 
            this.Nombre.AutoSize = true;
            this.Nombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Nombre.Location = new System.Drawing.Point(84, 328);
            this.Nombre.Name = "Nombre";
            this.Nombre.Size = new System.Drawing.Size(87, 25);
            this.Nombre.TabIndex = 5;
            this.Nombre.Text = "Nombre:";
            this.Nombre.Click += new System.EventHandler(this.Nombre_Click);
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(90, 356);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(279, 26);
            this.name.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(53)))), ((int)(((byte)(84)))));
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button2.Location = new System.Drawing.Point(65, 487);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(105, 45);
            this.button2.TabIndex = 8;
            this.button2.Text = "Guardar";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(264, 487);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(105, 45);
            this.button3.TabIndex = 9;
            this.button3.Text = "Cancelar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // UsuariosForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 579);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.name);
            this.Controls.Add(this.Nombre);
            this.Controls.Add(this.password);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(486, 635);
            this.MinimumSize = new System.Drawing.Size(372, 635);
            this.Name = "UsuariosForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.UsuariosForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label Nombre;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;

        private void ApplyModernStyle()
        {
            // Configuración del formulario
            this.BackColor = System.Drawing.Color.FromArgb(240, 244, 248);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            // Estilo para los Labels
            label1.Font = new System.Drawing.Font("Segoe UI", 18, System.Drawing.FontStyle.Bold);
            label1.ForeColor = System.Drawing.Color.FromArgb(51, 51, 51);
            label2.Font = label3.Font = Nombre.Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Regular);
            label2.ForeColor = label3.ForeColor = Nombre.ForeColor = System.Drawing.Color.FromArgb(75, 75, 75);

            // Estilo para los TextBox
            textBox1.BackColor = password.BackColor = name.BackColor = System.Drawing.Color.White;
            textBox1.ForeColor = password.ForeColor = name.ForeColor = System.Drawing.Color.FromArgb(51, 51, 51);
            textBox1.BorderStyle = password.BorderStyle = name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox1.Font = password.Font = name.Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Regular);

            // Agregar sombreado simulado a los TextBox
            textBox1.Paint += (sender, e) => DrawBorder(sender, e, System.Drawing.Color.FromArgb(204, 204, 204));
            password.Paint += (sender, e) => DrawBorder(sender, e, System.Drawing.Color.FromArgb(204, 204, 204));
            name.Paint += (sender, e) => DrawBorder(sender, e, System.Drawing.Color.FromArgb(204, 204, 204));

            // Estilo para los botones
            button2.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            button2.ForeColor = System.Drawing.Color.White;
            button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button2.Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold);
            button2.Cursor = System.Windows.Forms.Cursors.Hand;

            button3.BackColor = System.Drawing.Color.FromArgb(204, 204, 204);
            button3.ForeColor = System.Drawing.Color.Black;
            button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;
            button3.Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold);
            button3.Cursor = System.Windows.Forms.Cursors.Hand;
        }

        private void DrawBorder(object sender, System.Windows.Forms.PaintEventArgs e, System.Drawing.Color borderColor)
        {
            System.Windows.Forms.Control control = sender as System.Windows.Forms.Control;
            if (control != null)
            {
                using (System.Drawing.Pen pen = new System.Drawing.Pen(borderColor, 2))
                {
                    e.Graphics.DrawRectangle(pen, new System.Drawing.Rectangle(0, 0, control.Width - 1, control.Height - 1));
                }
            }
        }
    }
}