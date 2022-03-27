using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SheduleManagement.Models
{
    public class ResponseViewModel
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public ResponseViewModel(int status, string mes )
        {
            Status = status;
            Message = mes;
        }
        public ResponseViewModel(int status, string mes, object data)
        {
            Status = status;
            Message = mes;
            Data = data;
        }
    }
}
