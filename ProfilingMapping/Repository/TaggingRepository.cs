using ProfilingMapping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProfilingMapping.Repository
{
    public class TaggingRepository
    {
        public static IEnumerable<TBL_TAGGING> getTaggings()
        {
            PROFILINGMAPPINGDBEntities entities = new PROFILINGMAPPINGDBEntities();
            var taggings = from enttagging in entities.TBL_TAGGING select enttagging;

            return (IEnumerable<TBL_TAGGING>)taggings;
        }
    }
}