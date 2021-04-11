using Microsoft.AspNetCore.Mvc;
using BillsBLL.Services.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using BillsEntity.Models;
using BillsManagementSystem.Reports;
using DevExpress.DataAccess.ObjectBinding;
using BillsManagementSystem.Models;
using Microsoft.Extensions.Primitives;
using System.Linq;

namespace BillsManagementSystem.Controllers
{
   
    public class BillManagmentController : Controller
    {
        private readonly IBillingManagementService _billingManagementService;
        public BillManagmentController(IBillingManagementService billingManagementService)
        {
            _billingManagementService = billingManagementService;
        }
        
        public IActionResult Index()
        {
            var bills = _billingManagementService.GetAllBills();
            return View(bills.Data);
        }

        public IActionResult Test()
        {
            return View();
        }

        public IActionResult formTest(IEnumerable<formViewModel> model)
        { 
            var gh = Request.Query["itemPrice"];
            var ghs = Request.Query["itemPrice[]"];
            var dict = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());
            var v = dict["itemPrice"];
            var X = dict["itemPrice[]"];
            return View();
        }

        public IActionResult ReportViewer()
        {
            var Source = _billingManagementService.GetBillDetailByBillCode(88);
            BillReport rep = new BillReport();
            rep.DataSource = Source.Data;

            BillReport1 report = new BillReport1();
            Parameter parameter = new Parameter()
            {

            };
            report.Parameters.Add(new DevExpress.XtraReports.Parameters.Parameter { Name = "BillId",
                Type = typeof(System.DateTime),
                Value = 88 });

            return View(rep);
        }

        [HttpGet("BillManagment/UpdateBillsList")]
        public PartialViewResult UpdateBillsList()
        {
            var bills = _billingManagementService.GetAllBills();
            return PartialView("PV_BillingList", bills.Data);
        }

        [HttpGet("BillManagment/EditBill/{billCode}")]
        public PartialViewResult EditBill(int billCode)
        {
            var bills = _billingManagementService.GetBillByBillCode(billCode);
            return PartialView("PV_EditBill", bills.Data);
        }

        [HttpDelete("BillManagment/deleteBillByBillCode/{billCode}")]
        public JsonResult DeleteBillByBillCode(int billCode)
        {
            var result = _billingManagementService.DeleteBillByBillCode(billCode);
           return result == true ? Json("Success") :  Json("Fail");
        }

        public ActionResult<IEnumerable<SelectListItem>> GetAllItems()
        {
            var items = _billingManagementService.GetAllItems();
            if (items.IsSuccess)
            {
                List<SelectListItem> list = new List<SelectListItem>();
                foreach (var item in items.Data)
                {
                    list.Add(new SelectListItem()
                    { Text = item.ITMNAM, Value = item.ITMCOD.ToString() });
                }

                return list;
            }
            return null;
        }

        [HttpGet("BillManagment/GetBillItemByItemCode/{itemCode}")]
        public JsonResult GetBillItemByItemCode(int itemCode)
        {
            var items = _billingManagementService.GetBillItemByItemCode(itemCode);
            if (items.IsSuccess && items.Data != null)
            {
                return Json(items.Data);
            }
            return null;
        }

        public ActionResult<IEnumerable<SelectListItem>> GetAllVendors()
        {
            var items = _billingManagementService.GetAllVendors();
            if (items.IsSuccess)
            {
                List<SelectListItem> list = new List<SelectListItem>();
                foreach (var item in items.Data)
                {
                    list.Add(new SelectListItem()
                    { Text = item.VNDNAM, Value = item.VNDCOD.ToString() });
                }
                return list;
            }
            return null;
        }

        [HttpGet("BillManagment/GetItemPriceByCode/{itemCode}")]
        public ActionResult<decimal?> GetItemPriceByCode(int itemCode)
        {
            var items = _billingManagementService.GetItemPriceByCode(itemCode);
            if (items.IsSuccess && items.Data != null)
            {
                return items.Data;
            }
            return null;
        }

        [HttpGet("BillManagment/GetBillDetailByBillCode/{billCode}")]
        public JsonResult GetBillDetailByBillCode(int billCode)
        {
            var items = _billingManagementService.GetBillDetailByBillCode(billCode);
            if (items.IsSuccess && items.Data != null)
            {
                return Json(items.Data);
            }
            return null;
        }

        [HttpPost("BillManagment/InsertBillHeader")]
        public JsonResult InsertBillHeader(BILHDR billHeader)
        {
            var insertedBill = _billingManagementService.InsertBillHeader(billHeader);
            if (insertedBill.IsSuccess && insertedBill.Data != null)
            {
                
                return Json(insertedBill.Data.BILCOD);
            }
            return null;
        }

        [HttpPost("BillManagment/InsertBillDetails/{billCode}")]
        public JsonResult InsertBillDetails(int billCode, BILDTL billDetail)
        {
            var insertedBilldetail = _billingManagementService.InsertBillDetail(billDetail, billCode);
            if (insertedBilldetail.IsSuccess && insertedBilldetail.Data != null)
            {
                return Json("The bill detail inserted Successfully");
            }
            return null;
        }
    }
}
