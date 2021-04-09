using BillsBLL.Services.Interfaces;
using BillsDAL.Repositories;
using BillsDAL.Repositories.Interfaces;
using BillsEntity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillsBLL.Services
{
    public class BillingManagementService : IBillingManagementService
    {
        private readonly IBillingManagementRepository _billingManagementRepository;

        public BillingManagementService(IBillingManagementRepository billingManagementRepository)
        {
            _billingManagementRepository = billingManagementRepository;
        }

        public BillingManagementServiceResponse<IEnumerable<BILHDR>> GetAllBills()
        {
            BillingManagementServiceResponse<IEnumerable<BILHDR>> response = new Services.BillingManagementServiceResponse<IEnumerable<BILHDR>>();
            try
            {
                response.Data = _billingManagementRepository.GetAllBills();
                response.IsSuccess = true;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messeage = "Can't get the bills list " + Environment.NewLine + ex.Message;
                return response;
            }
        }

        public BillingManagementServiceResponse<IEnumerable<VNDDTL>> GetAllVendors()
        {
            BillingManagementServiceResponse<IEnumerable<VNDDTL>> response = new Services.BillingManagementServiceResponse<IEnumerable<VNDDTL>>();
            try
            {
                response.Data = _billingManagementRepository.GetAllVendors();
                response.IsSuccess = true;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messeage = "Can't get the vendors list " + Environment.NewLine + ex.Message;
                return response;
            }
        }

        public BillingManagementServiceResponse<IEnumerable<ITMDTL>> GetAllItems()
        {
            BillingManagementServiceResponse<IEnumerable<ITMDTL>> response = new Services.BillingManagementServiceResponse<IEnumerable<ITMDTL>>();
            try
            {
                response.Data = _billingManagementRepository.GetAllItems();
                response.IsSuccess = true;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messeage = "Can't get the items list " + Environment.NewLine + ex.Message;
                return response;
            }
        }

        public BillingManagementServiceResponse<decimal?> GetItemPriceByCode(int itemCode)
        {
            BillingManagementServiceResponse<decimal?> response = new Services.BillingManagementServiceResponse<decimal?>();
            try
            {
                response.Data = _billingManagementRepository.GetItemPriceByCode(itemCode);
                response.IsSuccess = true;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messeage = "Can't get the item " + Environment.NewLine + ex.Message;
                return response;
            }
        }

        public bool DeleteBillByBillCode(int billCode)
        {
            try
            {
                var result = _billingManagementRepository.DeleteBillByBillCode(billCode);
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public BillingManagementServiceResponse<IEnumerable<BILDTL>> GetBillDetailByBillCode(int billCode)
        {
            BillingManagementServiceResponse<IEnumerable<BILDTL>> response = new Services.BillingManagementServiceResponse<IEnumerable<BILDTL>>();
            try
            {
                response.Data = _billingManagementRepository.GetBillDetailByBillCode(billCode);
                response.IsSuccess = true;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messeage = "Can't get bill detail " + Environment.NewLine + ex.Message;
                return response;
            }
        }

        public BillingManagementServiceResponse<BILHDR> InsertBillHeader(BILHDR billHeader)
        {
            BillingManagementServiceResponse<BILHDR> response = new Services.BillingManagementServiceResponse<BILHDR>();
            try
            {
                if (billHeader != null)
                {
                    var insertedBillHeader = _billingManagementRepository.InsertBillHeader(billHeader);
                    response.IsSuccess = true;
                    response.Data = insertedBillHeader;
                    return response;
                }
                response.IsSuccess = false;
                response.Messeage = "The Bill header object is null";
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messeage = "The Bill header object Not inserted " + Environment.NewLine + ex.Message;
                return response;
            }

        }

        public BillingManagementServiceResponse<BILDTL> InsertBillDetail(BILDTL billDetail, int billCode)
        {
            BillingManagementServiceResponse<BILDTL> response = new Services.BillingManagementServiceResponse<BILDTL>();
            try
            {
                if (billDetail != null)
                {
                    billDetail.BILCOD = billCode;
                    var insertedBilldetail = _billingManagementRepository.InsertBillDetail(billDetail);
                    var billHeader = _billingManagementRepository.GetBillheaderByCode(billCode);
                    decimal total = 0;
                    foreach (var item in billHeader.BILDTLs)
                    {
                        total += item.ITMPRC * item.ITMQTY;
                    }
                    billHeader.BILPRC = total;
                    _billingManagementRepository.UpdateBillHeader(billHeader);
                    response.Data = insertedBilldetail;
                    response.IsSuccess = true;
                    return response;
                }
                response.IsSuccess = false;
                response.Messeage = "The Bill details object is null";
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messeage = "The Bill details object Not inserted " + Environment.NewLine + ex.Message;
                return response;
            }

        }
    }
}
