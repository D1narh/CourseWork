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
    
    public partial class BascetLine
    {
        public int Id { get; set; }
        public int BasketId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
    
        public virtual Basket Basket { get; set; }
        public virtual Product Product { get; set; }
    }
}
