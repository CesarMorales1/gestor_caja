using System;
using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;
using SalesBookApp.Models;

namespace SalesBookApp.Services
{
    public class ExcelService
    {
        public byte[] GenerateSalesBookExcel(List<SalesRecord> salesRecords, DateTime startDate, DateTime endDate)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Libro de Ventas");

                // Title styling
                worksheet.Cell(1, 1).Value = "LIBRO DE VENTAS";
                var titleRange = worksheet.Range(1, 1, 1, 14); // 14 columnas
                titleRange.Merge();
                titleRange.Style
                    .Font.SetBold(true)
                    .Font.SetFontSize(16)
                    .Font.SetFontColor(XLColor.White)
                    .Fill.SetBackgroundColor(XLColor.FromArgb(0, 51, 102)) // Dark blue
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                    .Border.SetOutsideBorder(XLBorderStyleValues.Medium);

                // Period styling
                worksheet.Cell(2, 1).Value = $"Período: {startDate:dd/MM/yyyy} - {endDate:dd/MM/yyyy}";
                var periodRange = worksheet.Range(2, 1, 2, 14); // 14 columnas
                periodRange.Merge();
                periodRange.Style
                    .Font.SetBold(true)
                    .Font.SetFontSize(12)
                    .Font.SetFontColor(XLColor.FromArgb(51, 51, 51)) // Dark gray
                    .Fill.SetBackgroundColor(XLColor.FromArgb(230, 230, 230)) // Light gray
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                    .Border.SetOutsideBorder(XLBorderStyleValues.Thin);

                // Group headers (nueva fila para cabeceras agrupadas)
                worksheet.Cell(3, 1).Value = "Factura";
                var facturaRange = worksheet.Range(3, 1, 3, 8); // Desde Fecha hasta Factura Final
                facturaRange.Merge();
                facturaRange.Style
                    .Font.SetBold(true)
                    .Font.SetFontColor(XLColor.White)
                    .Fill.SetBackgroundColor(XLColor.FromArgb(0, 102, 204))
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                    .Border.SetOutsideBorder(XLBorderStyleValues.Medium);

                worksheet.Cell(3, 9).Value = "Total Ventas";
                var totalVentasRange = worksheet.Range(3, 9, 3, 12); // Desde Base IGTF hasta Exentas/Exoneradas
                totalVentasRange.Merge();
                totalVentasRange.Style
                    .Font.SetBold(true)
                    .Font.SetFontColor(XLColor.White)
                    .Fill.SetBackgroundColor(XLColor.FromArgb(0, 102, 204))
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                    .Border.SetOutsideBorder(XLBorderStyleValues.Medium);

                // Headers (orden ajustado)
                string[] headers = new string[]
                {
                    "Fecha",
                    "Nro de RIF",
                    "Nombre o Razón Social del Cliente",
                    "Nro Máquina Fiscal",
                    "N Reporte Z",
                    "Tipo Documento",
                    "Factura Inicial",
                    "Factura Final",
                    "Base IGTF",
                    "IGTF 3%",
                    "Total Ventas",
                    "Exentas/Exoneradas no Sujetas",
                    "Base del 16%",
                    "IVA 16%"
                };

                worksheet.Row(4).Height = 25; // Increase header row height
                for (int i = 0; i < headers.Length; i++)
                {
                    var cell = worksheet.Cell(4, i + 1);
                    cell.Value = headers[i];
                    cell.Style
                        .Font.SetBold(true)
                        .Font.SetFontColor(XLColor.White)
                        .Fill.SetBackgroundColor(XLColor.FromArgb(0, 102, 204)) // Medium blue
                        .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                        .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                        .Border.SetOutsideBorder(XLBorderStyleValues.Medium)
                        .Alignment.SetWrapText(true);
                }

                // Data
                int row = 5;
                bool alternate = false;
                foreach (var record in salesRecords)
                {
                    worksheet.Row(row).Height = 20; // Consistent row height

                    worksheet.Cell(row, 1).Value = record.Date.ToString("dd/MM/yyyy");
                    worksheet.Cell(row, 2).Value = ""; // Nro de RIF (vacío)
                    worksheet.Cell(row, 3).Value = "Resumen Diario de Ventas"; // Nombre o Razón Social del Cliente
                    worksheet.Cell(row, 4).Value = record.PrinterSerial; // Nro Máquina Fiscal
                    worksheet.Cell(row, 5).Value = record.ReportZ; // N Reporte Z
                    worksheet.Cell(row, 6).Value = record.DocumentType; // Tipo Documento
                    worksheet.Cell(row, 7).Value = record.StartInvoice; // Factura Inicial
                    worksheet.Cell(row, 8).Value = record.EndInvoice; // Factura Final
                    worksheet.Cell(row, 9).Value = record.IgtfBase; // Base IGTF
                    worksheet.Cell(row, 10).Value = record.IgtfAmount; // IGTF 3%
                    worksheet.Cell(row, 11).Value = record.TotalInvoice; // Total Ventas
                    worksheet.Cell(row, 12).Value = record.ExemptAmount; // Exentas/Exoneradas no Sujetas
                    worksheet.Cell(row, 13).Value = record.TaxableBase; // Base del 16%
                    worksheet.Cell(row, 14).Value = record.Vat16; // IVA 16%

                    // Alternate row colors
                    var rowRange = worksheet.Range(row, 1, row, 14);
                    rowRange.Style
                        .Fill.SetBackgroundColor(alternate ? XLColor.FromArgb(245, 245, 245) : XLColor.White)
                        .Border.SetOutsideBorder(XLBorderStyleValues.Thin)
                        .Border.SetInsideBorder(XLBorderStyleValues.Thin);

                    // Currency formatting (sin $)
                    var currencyColumns = new[] { 9, 10, 11, 12, 13, 14 }; // Columnas numéricas
                    foreach (var col in currencyColumns)
                    {
                        worksheet.Cell(row, col).Style
                            .NumberFormat.SetFormat("#,##0.00") // Sin $
                            .Font.SetFontColor(XLColor.FromArgb(0, 102, 0)); // Green for money
                    }

                    alternate = !alternate;
                    row++;
                }

                // Totals styling
                worksheet.Row(row).Height = 25;
                worksheet.Cell(row, 8).Value = "TOTALES:"; // Ajustado a la columna 8 (antes de valores numéricos)
                worksheet.Cell(row, 8).Style
                    .Font.SetBold(true)
                    .Font.SetFontSize(12)
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right)
                    .Fill.SetBackgroundColor(XLColor.FromArgb(230, 230, 230));

                var sumColumns = new[] { 9, 10, 11, 12, 13, 14 }; // Columnas numéricas
                foreach (int col in sumColumns)
                {
                    var cell = worksheet.Cell(row, col);
                    cell.FormulaA1 = $"SUM({worksheet.Cell(5, col).Address}:{worksheet.Cell(row - 1, col).Address})";
                    cell.Style
                        .Font.SetBold(true)
                        .Font.SetFontColor(XLColor.FromArgb(0, 102, 0))
                        .NumberFormat.SetFormat("#,##0.00") // Sin $
                        .Fill.SetBackgroundColor(XLColor.FromArgb(230, 230, 230))
                        .Border.SetOutsideBorder(XLBorderStyleValues.Medium);
                }

                // Column widths
                worksheet.Column(1).Width = 12;   // Fecha
                worksheet.Column(2).Width = 15;   // Nro de RIF
                worksheet.Column(3).Width = 35;   // Nombre o Razón Social del Cliente
                worksheet.Column(4).Width = 20;   // Nro Máquina Fiscal
                worksheet.Column(5).Width = 15;   // N Reporte Z
                worksheet.Column(6).Width = 15;   // Tipo Documento
                worksheet.Column(7).Width = 15;   // Factura Inicial
                worksheet.Column(8).Width = 15;   // Factura Final
                worksheet.Column(9).Width = 15;   // Base IGTF
                worksheet.Column(10).Width = 15;  // IGTF 3%
                worksheet.Column(11).Width = 15;  // Total Ventas
                worksheet.Column(12).Width = 20;  // Exentas/Exoneradas no Sujetas
                worksheet.Column(13).Width = 15;  // Base del 16%
                worksheet.Column(14).Width = 15;  // IVA 16%

                // Freeze top 4 rows (para incluir las cabeceras agrupadas)
                worksheet.SheetView.FreezeRows(4);

                // Add some padding
                worksheet.Rows().Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);

                // Convert to byte array
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}