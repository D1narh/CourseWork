//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MaimApp.DataModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class Approval
    {
        public int Id { get; set; }
        public int IsOk { get; set; }
        public int Approval_request_id { get; set; }
    
        public virtual Approval_request Approval_request { get; set; }
    }
}
