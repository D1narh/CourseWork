using DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaimApp.Class.Approval
{
    public class ApprovalLoader
    {
        ObservableCollection<ApprovalRequest> ApplovalList = new ObservableCollection<ApprovalRequest>();
        public async Task<ObservableCollection<ApprovalRequest>> Load()
        {
            using (var db = new DbA99dc4MaimfDB())
            {
                var ApprovalData = db.Approvals.Where(x => x.IsOk == 0);
                var ApprovalRequestData = db.ApprovalRequests;
                var UserData = db.UserPrData;

                var Data = from u in db.UserPrData
                           join ar in db.ApprovalRequests on u.UserId equals ar.UserId
                           join a in db.Approvals on ar.Id equals a.ApprovalRequestId
                           where a.IsOk == 0
                           select new ApprovalRequest(u.UserId, u.Name + " " + u.Surname + " " + u.LastName, ar.Date,ar.Id)
                           {
                               UserId = u.UserId,
                               FIO = u.Name + " " + u.Surname + " " + u.LastName,
                               DateToApproval = ar.Date,
                               ApprovalRequestID = ar.Id
                           };

                foreach (var i in Data)
                {
                    ApplovalList.Add(new ApprovalRequest(i.UserId, i.FIO, i.DateToApproval,i.ApprovalRequestID)
                    {
                        UserId = i.UserId,
                        FIO = i.FIO,
                        DateToApproval = i.DateToApproval,
                        ApprovalRequestID = i.ApprovalRequestID
                    });
                }
            }
            return ApplovalList;
        }
    }
}
