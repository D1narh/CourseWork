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
    
    public partial class PersonDataInRequest
    {
        public int Id { get; set; }
        public Nullable<int> RequestId { get; set; }
        public bool Who { get; set; }
        public bool Document { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int TelNumber { get; set; }
        public string Mail { get; set; }
        public int SerialNumber { get; set; }
        public string HotelAdress { get; set; }
        public string HotelName { get; set; }
    
        public virtual Request Request { get; set; }
    }
}
