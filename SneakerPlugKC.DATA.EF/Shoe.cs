//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SneakerPlugKC.DATA.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Shoe
    {
        public int ShoeID { get; set; }
        public string ShoeName { get; set; }
        public int SizeId { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public bool IsInStock { get; set; }
        public string ShoePhoto { get; set; }
        public string Condition { get; set; }
    
        public virtual Size Size { get; set; }
    }
}
