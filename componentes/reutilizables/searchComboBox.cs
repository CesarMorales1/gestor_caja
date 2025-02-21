using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace apos_gestor_caja.componentes.reutilizables
{
    [DefaultEvent("SelectedIndexChanged")]
    public class RegexFilterComboBox : ComboBox
    {
        private List<string> _originalItems; // Lista original de ítems
        private bool _useRawRegex; // Opción para usar regex crudo o escapado

        public RegexFilterComboBox()
        {
            // Configuración inicial
            this.DropDownStyle = ComboBoxStyle.DropDown; // Editable
            this.AutoCompleteMode = AutoCompleteMode.None; // Desactivar autocompletado nativo
            _originalItems = new List<string>();
            _useRawRegex = false; // Por defecto, escapar el texto
            this.TextChanged += RegexFilterComboBox_TextChanged;
            this.Height = 30;
        }

        // Propiedad para configurar si se usa regex crudo o escapado
        [Category("Behavior")]
        [Description("Indica si el texto ingresado se interpreta como una expresión regular cruda o se escapa para coincidencia literal.")]
        [DefaultValue(false)]
        public bool UseRawRegex
        {
            get => _useRawRegex;
            set => _useRawRegex = value;
        }

        // Método público para establecer los ítems
        public void SetItems(IEnumerable<string> items)
        {
            _originalItems.Clear();
            _originalItems.AddRange(items);
            ResetItems();
        }

        // Método público para agregar un ítem
        public void AddItem(string item)
        {
            _originalItems.Add(item);
            ResetItems();
        }

        // Método público para limpiar los ítems
        public void ClearItems()
        {
            _originalItems.Clear();
            this.Items.Clear();
            this.Text = string.Empty;
        }

        // Restaurar ítems originales
        private void ResetItems()
        {
            this.Items.Clear();
            this.Items.AddRange(_originalItems.ToArray());
        }

        // Evento de filtrado
        private void RegexFilterComboBox_TextChanged(object sender, EventArgs e)
        {
            string input = this.Text.Trim();

            // Si el texto está vacío, mostrar todos los ítems originales
            if (string.IsNullOrEmpty(input))
            {
                ResetItems();
                this.DroppedDown = true;
                Cursor.Current = Cursors.Default;
                return;
            }

            try
            {
                // Crear la expresión regular
                string pattern = _useRawRegex ? input : Regex.Escape(input);
                Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

                // Filtrar ítems
                var filteredItems = _originalItems.FindAll(item => regex.IsMatch(item));

                // Actualizar el ComboBox
                this.Items.Clear();
                this.Items.AddRange(filteredItems.ToArray());
                this.DroppedDown = true;

                // Mantener el cursor al final del texto
                this.SelectionStart = input.Length;
                this.SelectionLength = 0;
                Cursor.Current = Cursors.Default;
            }
            catch (ArgumentException)
            {
                // Si el regex es inválido (por ejemplo, patrón mal formado), restaurar ítems originales
                ResetItems();
                this.DroppedDown = true;
            }
        }

        // Sobrescribir el comportamiento al presionar Enter para seleccionar el primer ítem filtrado
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Enter && this.Items.Count > 0)
            {
                this.SelectedIndex = 0; // Seleccionar el primer ítem filtrado
                e.Handled = true;
            }
        }
    }
}