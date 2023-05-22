using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaimApp.Class.Approval
{
    public class ApprovalRequest
    {
        public int UserId { get; set; }
        public string FIO { get; set; }
        public string Status { get; set; }
        public int ApprovalRequestID { get; set; }
        public DateTime DateToApproval { get; set; }

        public ApprovalRequest(int userId, string fio, DateTime dateToApprovar, int approvalRequestID)
        {
            UserId = userId;
            FIO = fio;
            DateToApproval = dateToApprovar;
            Status = "В ожидании";
            ApprovalRequestID = approvalRequestID;
        }
    }
}
