//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyDaiLy.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class BaoCaoCongNo
    {
        public int ID { get; set; }
        public decimal NoDau { get; set; }
        public decimal PhatSinh { get; set; }
    
        public virtual BangBaoCaoThang BangBaoCaoThang { get; set; }
    }
}