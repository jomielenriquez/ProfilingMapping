//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProfilingMapping.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBL_BARANGAY
    {
        public TBL_BARANGAY()
        {
            this.TBL_NAMES = new HashSet<TBL_NAMES>();
            this.TBL_ADMIN = new HashSet<TBL_ADMIN>();
        }
    
        public System.Guid BARANGAYID { get; set; }
        public string NAME { get; set; }
        public string CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public System.DateTime CREATEDDATE { get; set; }
        public string CREATEDBY { get; set; }
        public Nullable<System.DateTime> UPDATEDDATE { get; set; }
        public string UPDATEDBY { get; set; }
    
        public virtual ICollection<TBL_NAMES> TBL_NAMES { get; set; }
        public virtual ICollection<TBL_ADMIN> TBL_ADMIN { get; set; }
    }
}
