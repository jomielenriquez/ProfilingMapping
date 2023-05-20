using ProfilingMapping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProfilingMapping.Repository
{
    public class SettingsRepository
    {
        public static TBL_ADMIN getCurrentUserInfo(Guid? adminID)
        {
            PROFILINGMAPPINGDBEntities entities = new PROFILINGMAPPINGDBEntities();
            var profile = (from currentprofile in entities.TBL_ADMIN.Where(admin => admin.ADMINID == adminID) select currentprofile).FirstOrDefault();

            return (TBL_ADMIN)profile;
        }
        public static IEnumerable<TBL_ADMIN> getAdmins()
        {
            PROFILINGMAPPINGDBEntities entities = new PROFILINGMAPPINGDBEntities();
            var profile = from currentprofile in entities.TBL_ADMIN select currentprofile;

            return (IEnumerable<TBL_ADMIN>)profile;
        }
    }
}