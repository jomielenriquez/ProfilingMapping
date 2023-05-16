using ProfilingMapping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProfilingMapping.Repository
{
    public class LoginRepository
    {
        public static Guid HasAccess(string username, string password)
        {
            PROFILINGMAPPINGDBEntities entities = new PROFILINGMAPPINGDBEntities();

            var admin = (from entadmin in entities.TBL_ADMIN.Where(ad => ad.USERNAME == username && ad.PASSWORD == password) select entadmin).FirstOrDefault();

            if (admin != null)
            {
                return admin.ADMINID;
            }

            return new Guid();
        }

    }
}