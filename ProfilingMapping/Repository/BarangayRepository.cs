using ProfilingMapping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProfilingMapping.Repository
{
    public class BarangayRepository
    {
        public static IEnumerable<TBL_BARANGAY> GetAllBarangays()
        {
            PROFILINGMAPPINGDBEntities entities = new PROFILINGMAPPINGDBEntities();
            var barangay = from entbrgy in entities.TBL_BARANGAY select entbrgy;
            return (IEnumerable<TBL_BARANGAY>)barangay;
        }
    }
}