//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace XMLfromDatabase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Stock
    {
        public int StockID { get; set; }
        public int OnHand { get; set; }
        public int OnOrder { get; set; }
        public Nullable<int> Item_Code { get; set; }
        public Nullable<int> Store_StoreID { get; set; }
    
        public virtual Book Book { get; set; }
        public virtual Store Store { get; set; }
    }
}
