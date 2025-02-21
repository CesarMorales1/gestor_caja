using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SalesBookApp.Models;
using MyApp.Infrastructure.Database;

namespace SalesBookApp.Repositories
{
    public class SalesRepository
    {
        private readonly SqlHelper _sqlHelper;

        public SalesRepository()
        {
            _sqlHelper = new SqlHelper();
        }

        public async Task<List<SalesRecord>> GetSalesRecordsAsync(DateTime startDate, DateTime endDate)
        {
            var salesRecords = new List<SalesRecord>();
            string query = @"
    SELECT 
        d1 as CashierNumber,
        d30 as PrinterSerial,
        DATE(STR_TO_DATE(d33, '%d/%m/%Y')) as SaleDate,
        MIN(d6) as StartInvoice,
        MAX(d6) as EndInvoice,
        SUM(CAST(REPLACE(d11, ',', '.') AS DECIMAL(10,2))) as Subtotal,
        SUM(CAST(REPLACE(d16, ',', '.') AS DECIMAL(10,2))) as TaxableBase,
        SUM(CAST(REPLACE(d17, ',', '.') AS DECIMAL(10,2))) as Vat16,
        SUM(CAST(REPLACE(d15, ',', '.') AS DECIMAL(10,2))) as ExemptAmount,
        SUM(CAST(REPLACE(d24, ',', '.') AS DECIMAL(10,2))) as TotalInvoice,
        ROUND(SUM(CAST(REPLACE(d13, ',', '.') AS DECIMAL(10,2))) / 0.03, 2) as IgtfBase, -- Base del IGTF (monto en USD convertido)
        SUM(CAST(REPLACE(d13, ',', '.') AS DECIMAL(10,2))) as IgtfAmount -- IGTF real
    FROM apos08 
    WHERE STR_TO_DATE(d33, '%d/%m/%Y') BETWEEN @startDate AND @endDate
    GROUP BY DATE(STR_TO_DATE(d33, '%d/%m/%Y')), d30
    ORDER BY SaleDate, PrinterSerial";

            try
            {
                using (var connection = _sqlHelper.ObtenerConexion())
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@startDate", startDate.Date);
                    command.Parameters.AddWithValue("@endDate", endDate.Date);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            salesRecords.Add(new SalesRecord
                            {
                                CashierNumber = reader["CashierNumber"].ToString(),
                                PrinterSerial = reader["PrinterSerial"].ToString(),
                                Date = Convert.ToDateTime(reader["SaleDate"]),
                                StartInvoice = reader["StartInvoice"].ToString(),
                                EndInvoice = reader["EndInvoice"].ToString(),
                                Subtotal = Convert.ToDecimal(reader["Subtotal"]),
                                TaxableBase = Convert.ToDecimal(reader["TaxableBase"]),
                                Vat16 = Convert.ToDecimal(reader["Vat16"]),
                                ExemptAmount = Convert.ToDecimal(reader["ExemptAmount"]),
                                TotalInvoice = Convert.ToDecimal(reader["TotalInvoice"]),
                                IgtfBase = Convert.ToDecimal(reader["IgtfBase"]),
                                IgtfAmount = Convert.ToDecimal(reader["IgtfAmount"]),
                                DocumentType = "Factura",
                                BusinessName = "Resumen Diario de Ventas",
                                ReportZ = string.Empty
                            });
                        }
                    }
                }
                return salesRecords;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving sales records: {ex.Message}", ex);
            }
        }
    }
}