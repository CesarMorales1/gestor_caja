using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using SalesBookApp.Repositories;
using SalesBookApp.Services;
using SalesBookApp.Models;
using System.IO;
using System.Collections.Generic;

namespace SalesBookApp
{
    public partial class SalesBookForm : Form
    {
        private readonly SalesRepository _salesRepository;
        private readonly ExcelService _excelService;
        private List<SalesRecord> _currentRecords;

        public SalesBookForm()
        {
            InitializeComponent();
            _salesRepository = new SalesRepository();
            _excelService = new ExcelService();

            // Set default dates
            datePickerStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            datePickerEnd.Value = DateTime.Now;

            // Configure DataGridView
            ConfigureDataGridView();

            // Initially hide the export button
            btnExport.Visible = false;
        }

        private void ConfigureDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = true;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView1.MultiSelect = false;

            // Columna 1: Fecha
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Date",
                DataPropertyName = "Date",
                HeaderText = "Fecha",
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "dd/MM/yyyy",
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            // Columna 2: Nro de RIF (vacío)
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "BusinessName",
                DataPropertyName = "BusinessName",
                HeaderText = "Nro de RIF",
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            // Columna 3: Nro Máquina Fiscal (sin cambios)
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PrinterSerial",
                DataPropertyName = "PrinterSerial",
                HeaderText = "Nro Máquina Fiscal",
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            // Columna 4: Resumen Diario de Ventas (nueva columna)
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SummaryDailySales",
                HeaderText = "Nombre o razon social del cliente",
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            // Columna 5: Tipo Documento (desplazada de 4 a 5)
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DocumentType",
                DataPropertyName = "DocumentType",
                HeaderText = "Tipo Documento",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            // Columna 6: Factura Inicial (desplazada de 5 a 6)
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StartInvoice",
                DataPropertyName = "StartInvoice",
                HeaderText = "Factura Inicial",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            // Columna 7: Factura Final (desplazada de 6 a 7)
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EndInvoice",
                DataPropertyName = "EndInvoice",
                HeaderText = "Factura Final",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            // Columna 8: N Reporte Z (desplazada de 7 a 8)
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ReportZ",
                DataPropertyName = "ReportZ",
                HeaderText = "N Reporte Z",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            // Columna 9: Base Imponible (desplazada de 8 a 9)
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TaxableBase",
                DataPropertyName = "TaxableBase",
                HeaderText = "Base del 16%",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            // Columna 10: IVA 16% (desplazada de 9 a 10)
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Vat16",
                DataPropertyName = "Vat16",
                HeaderText = "IVA 16%",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            // Columna 11: Base IGTF (desplazada de 10 a 11)
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IgtfBase",
                DataPropertyName = "IgtfBase",
                HeaderText = "Base IGTF",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            // Columna 12: IGTF 3% (desplazada de 11 a 12)
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IgtfAmount",
                DataPropertyName = "IgtfAmount",
                HeaderText = "IGTF 3%",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            // Columna 13: Exentas/Exoneradas (desplazada de 12 a 13)
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ExemptAmount",
                DataPropertyName = "ExemptAmount",
                HeaderText = "Exentas/Exoneradas",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            // Columna 14: Total Ventas (desplazada de 13 a 14)
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalInvoice",
                DataPropertyName = "TotalInvoice",
                HeaderText = "Total Ventas",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            // Estilo general
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridView1.ColumnHeadersHeight = 40;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridView1.GridColor = System.Drawing.Color.LightGray;

            // Validación de celdas numéricas (ajustada a nuevas posiciones: 8 a 13)
            dataGridView1.CellValidating += (s, e) =>
            {
                if (e.ColumnIndex >= 8 && e.ColumnIndex <= 13) // Columnas numéricas
                {
                    if (!string.IsNullOrEmpty(e.FormattedValue.ToString()))
                    {
                        if (!decimal.TryParse(e.FormattedValue.ToString(), out _))
                        {
                            e.Cancel = true;
                            MessageBox.Show("Por favor ingrese un valor numérico válido.",
                                "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            };

            // Evento para formatear celdas
            dataGridView1.CellFormatting += (s, e) =>
            {
                // Formatear la columna "Nro de RIF" para que esté vacía
                if (dataGridView1.Columns[e.ColumnIndex].Name == "BusinessName")
                {
                    e.Value = "";
                    e.FormattingApplied = true;
                }
                // Formatear la columna "Resumen Diario de Ventas" para mostrar texto fijo
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "SummaryDailySales")
                {
                    e.Value = "Resumen Diario de Ventas";
                    e.FormattingApplied = true;
                }
            };
        }

        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            if (datePickerEnd.Value < datePickerStart.Value)
            {
                MessageBox.Show("La fecha final debe ser mayor o igual a la fecha inicial",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                btnGenerate.Enabled = false;
                btnExport.Enabled = false;
                Cursor = Cursors.WaitCursor;

                _currentRecords = await _salesRepository.GetSalesRecordsAsync(
                    datePickerStart.Value, datePickerEnd.Value);

                if (_currentRecords.Count == 0)
                {
                    MessageBox.Show("No se encontraron registros para el período seleccionado",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = _currentRecords;
                btnExport.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener los registros de ventas: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnGenerate.Enabled = true;
                btnExport.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentRecords == null || _currentRecords.Count == 0)
                {
                    MessageBox.Show("No hay datos para exportar",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var excelData = _excelService.GenerateSalesBookExcel(
                    _currentRecords, datePickerStart.Value, datePickerEnd.Value);

                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Excel Files|*.xlsx";
                    saveDialog.FileName = $"Libro_Ventas_{datePickerStart.Value:yyyyMMdd}_{datePickerEnd.Value:yyyyMMdd}.xlsx";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllBytes(saveDialog.FileName, excelData);
                        MessageBox.Show("Libro de ventas generado exitosamente",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el libro de ventas: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            // Evento de entrada para el grupo, si se requiere
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}