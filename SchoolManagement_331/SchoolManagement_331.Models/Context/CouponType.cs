//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchoolManagement_331.Models.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class CouponType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CouponType()
        {
            this.TableCouponCodeMaster = new HashSet<TableCouponCodeMaster>();
        }
    
        public int CouponID { get; set; }
        public string Coupon_Type { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TableCouponCodeMaster> TableCouponCodeMaster { get; set; }
    }
}
