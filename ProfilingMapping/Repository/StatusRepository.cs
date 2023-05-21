using ProfilingMapping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProfilingMapping.Repository
{
    public class StatusRepository
    {
        public static IEnumerable<TBL_STATUS> getAllStatus()
        {
            PROFILINGMAPPINGDBEntities entities = new PROFILINGMAPPINGDBEntities();
            var status = from entstatus in entities.TBL_STATUS select entstatus;

            return (IEnumerable<TBL_STATUS>)status;
        }
    }
}