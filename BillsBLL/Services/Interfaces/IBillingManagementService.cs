using BillsEntity.Models;
using System.Collections.Generic;

namespace BillsBLL.Services.Interfaces
{
    public interface IBillingManagementService
    {
        public BillingManagementServiceResponse<IEnumerable<BILHDR>> GetAllBills();
        public BillingManagementServiceResponse<IEnumerable<VNDDTL>> GetAllVendors();
        public BillingManagementServiceResponse<IEnumerable<ITMDTL>> GetAllItems();
        public BillingManagementServiceResponse<decimal?> GetItemPriceByCode(int itemCode);
        public BillingManagementServiceResponse<IEnumerable<BILDTL>> GetBillDetailByBillCode(int billCode);
        public BillingManagementServiceResponse<BILHDR> InsertBillHeader(BILHDR billHeader);
        public BillingManagementServiceResponse<BILDTL> InsertBillDetail(BILDTL billDetail, int billCode);
        public bool DeleteBillByBillCode(int billCode);
    }
}
