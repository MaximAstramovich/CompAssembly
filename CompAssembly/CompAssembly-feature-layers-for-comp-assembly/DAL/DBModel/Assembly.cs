//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.DBModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Assembly
    {
        public Nullable<int> IDCUS { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public Nullable<int> Num { get; set; }
        public Nullable<int> Summ { get; set; }
        public Nullable<int> Corpus { get; set; }
        public Nullable<int> Board { get; set; }
        public Nullable<int> CPU { get; set; }
        public Nullable<int> Graphic { get; set; }
        public Nullable<int> OZU { get; set; }
        public Nullable<int> HDD { get; set; }
        public Nullable<int> SSD { get; set; }
        public Nullable<int> Power { get; set; }
        public Nullable<int> DVD { get; set; }
        public Nullable<int> Audio { get; set; }
        public Nullable<int> Ice { get; set; }
        public int Код { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> DateOfPayment { get; set; }
    
        public virtual Customers Customers { get; set; }
    }
}
