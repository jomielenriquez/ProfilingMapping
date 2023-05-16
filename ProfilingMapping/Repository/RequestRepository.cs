using ProfilingMapping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProfilingMapping.Repository
{
    public class RequestRepository
    {
        public static IEnumerable<TBL_REQUEST> GetAllRequest()
        {
            PROFILINGMAPPINGDBEntities entities = new PROFILINGMAPPINGDBEntities();
            var request = from entrequest in entities.TBL_REQUEST select entrequest;
            return (IEnumerable<TBL_REQUEST>)request;
        }

        public static TBL_REQUEST GetRequest(Guid? RequestID)
        {
            PROFILINGMAPPINGDBEntities entities = new PROFILINGMAPPINGDBEntities();
            var request = (from entrequest in entities.TBL_REQUEST.Where(rq => rq.REQUESTID == RequestID) select entrequest).FirstOrDefault();
            return (TBL_REQUEST)request;
        }
    }
}