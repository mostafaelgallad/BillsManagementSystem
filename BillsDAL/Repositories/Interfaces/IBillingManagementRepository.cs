using BillsEntity.Models;
using System.Collections.Generic;

namespace BillsDAL.Repositories.Interfaces
{
    public interface IBillingManagementRepository
    {
        public IEnumerable<BILHDR> GetAllBills();
        public IEnumerable<VNDDTL> GetAllVendors();
        public IEnumerable<ITMDTL> GetAllItems();
        public decimal? GetItemPriceByCode(int itemCode);
        public IEnumerable<BILDTL> GetBillDetailByBillCode(int billCode);
        public BILHDR InsertBillHeader(BILHDR billHeader);
        public BILDTL InsertBillDetail(BILDTL billDetail);
        public BILHDR GetBillheaderByCode(int billCode);
        public bool UpdateBillHeader(BILHDR billHeader);
        public bool DeleteBillByBillCode(int billCode);
        public BILHDR GetBillByBillCode(int billCode);
        public BILDTL GetBillItemByItemCode(int itemCode);
    }
}
