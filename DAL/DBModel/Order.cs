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
    
    public partial class Order
    {
        public int Код { get; set; }
        public Nullable<int> NumberAssembly { get; set; }
        public Nullable<System.DateTime> DateOfPayment { get; set; }
    }
}