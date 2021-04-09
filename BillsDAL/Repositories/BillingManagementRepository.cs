using BillsDAL.Repositories.Interfaces;
using BillsEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BillsDAL.Repositories
{
   public class BillingManagementRepository : IBillingManagementRepository
    {
         DTSAssignmentContext DB = new DTSAssignmentContext();

        public IEnumerable<BILHDR> GetAllBills()
        {
            return DB.BILHDRs.Include(b => b.VNDCODNavigation).Where(b=>b.BILPRC != null);
        }

        public IEnumerable<VNDDTL> GetAllVendors()
        {
            return DB.VNDDTLs;
        }

        public IEnumerable<ITMDTL> GetAllItems()
        {
            return DB.ITMDTLs;
        }

        public decimal? GetItemPriceByCode(int itemCode)
        {
            return DB.ITMDTLs.FirstOrDefault(i => i.ITMCOD == itemCode)?.ITMPRC;
        }

        public BILHDR GetBillheaderByCode(int billCode)
        {
            return DB.BILHDRs.Include(b => b.BILDTLs).FirstOrDefault(i => i.BILCOD == billCode);
        }

        public IEnumerable<BILDTL> GetBillDetailByBillCode(int billCode)
        {
            return DB.BILDTLs.Include(b => b.BILCODNavigation).Include(b => b.ITMCODNavigation).Where(b => b.BILCOD == billCode);
        }

        public bool DeleteBillByBillCode(int billCode)
        {
            try
            {
                var bill =  DB.BILHDRs.Include(b => b.BILDTLs).FirstOrDefault(b => b.BILCOD == billCode);
                foreach (var item in bill.BILDTLs)
                {
                    DB.BILDTLs.Remove(item);
                }
                DB.BILHDRs.Remove(bill);
                DB.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
          
        }

        public BILHDR InsertBillHeader(BILHDR billHeader)
        {
            try
            {
                if (billHeader != null)
                {
                    var insertedBillHeader = DB.Add(billHeader);
                    DB.SaveChanges();
                    return insertedBillHeader.Entity;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public BILDTL InsertBillDetail(BILDTL billDetail)
        {
            try
            {
                if (billDetail != null)
                {
                    var insertedBilldetail = DB.Add(billDetail);
                    DB.SaveChanges();
                    return insertedBilldetail.Entity;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool UpdateBillHeader(BILHDR billHeader)
        {
            try
            {
                DB.Update(billHeader);
                DB.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
