using System;
using System.Collections.Generic;

namespace Tien_C4_B1
{
    public interface IRepoInventory<T>
    {
        void AddStockProduct(ImportInventory import);
        void AddOutOfStock(ExportInventory export);
        void AddRemainingProduct(RemainingProduct remaining);
        void RepairReceipt(Receipt receipt, ImportInventory import);
        void RepairInvoice(Invoice invoice, ExportInventory export);
        void RepairRemaining(RemainingProduct remainingProduct);
        void AddFoodReceiptDetail(RemainingProduct remaining, ReceiptDetail receiptDetail);
        void ReduceRemainingProduct(RemainingProduct remaining, InvoiceDetail invoiceDetail);
        T Get();
    }
}
