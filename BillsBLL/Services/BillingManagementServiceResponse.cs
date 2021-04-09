using System;
using System.Collections.Generic;
using System.Text;

namespace BillsBLL.Services
{
    public class BillingManagementServiceResponse<T>
    {
        public T Data { get; set; }
        public string Messeage { get; set; }
        public bool IsSuccess { get; set; }
    }
}
