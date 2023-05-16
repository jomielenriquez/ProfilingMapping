using ProfilingMapping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProfilingMapping.Repository
{
    public class NamesRepository
    {
        public static IEnumerable<TBL_NAMES> GetAllNames()
        {
            PROFILINGMAPPINGDBEntities entities = new PROFILINGMAPPINGDBEntities();
            var names = from entnames in entities.TBL_NAMES select entnames;
            return (IEnumerable<TBL_NAMES>)names;
        }
    }
}