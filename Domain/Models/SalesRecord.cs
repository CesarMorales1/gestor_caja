using System;

namespace SalesBookApp.Models
{
    public class SalesRecord
    {
        public string CashierNumber { get; set; } = string.Empty;     // d1
        public decimal Subtotal { get; set; }                         // Sum of d11
        public decimal TaxableBase { get; set; }                      // Sum of d16
        public decimal Vat16 { get; set; }                           // Sum of d17
        public decimal TotalInvoice { get; set; }                    // Sum of d24
        public string StartInvoice { get; set; } = string.Empty;     // Min d6
        public string EndInvoice { get; set; } = string.Empty;       // Max d6
        public decimal ExemptAmount { get; set; }                    // Sum of d15
        public string PrinterSerial { get; set; } = string.Empty;    // d30
        public DateTime Date { get; set; }                           // d33
        public string DocumentType { get; set; } = "Factura";        // Tipo de documento (siempre "Factura" por ahora)
        public decimal IgtfBase { get; set; }
        public decimal IgtfAmount { get; set; }

        public string NroRif { get; set; } = string.Empty;

        public string BusinessName { get; set; } = "Resumen Diario de Ventas";
        public string ReportZ { get; set; } = string.Empty;
    }
}