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
    
    public partial class TBL_STATUS
    {
        public TBL_STATUS()
        {
            this.TBL_REQUEST = new HashSet<TBL_REQUEST>();
        }
    
        public System.Guid STATUSID { get; set; }
        public string NAME { get; set; }
        public string CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public System.DateTime CREATEDDATE { get; set; }
        public string CREATEDBY { get; set; }
        public Nullable<System.DateTime> UPDATEDDATE { get; set; }
        public string UPDATEDBY { get; set; }
    
        public virtual ICollection<TBL_REQUEST> TBL_REQUEST { get; set; }
    }
}
