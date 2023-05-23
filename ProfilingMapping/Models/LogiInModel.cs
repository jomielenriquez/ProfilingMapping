using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProfilingMapping.Models
{
    public class LogiInModel
    {
        public static Guid adminID { get; set; }
        public static string FullName {
            get { 
                PROFILINGMAPPINGDBEntities entities = new PROFILINGMAPPINGDBEntities();
                var name = (from names in entities.TBL_ADMIN.Where(admin => admin.ADMINID == adminID) select names).FirstOrDefault();
                return name.FIRSTNAME + " " + name.LASTNAME; 
            }
        }
        public string Role {
            get {
                PROFILINGMAPPINGDBEntities entities = new PROFILINGMAPPINGDBEntities();
                var name = (from names in entities.TBL_ADMIN.Where(admin => admin.ADMINID == adminID) select names).FirstOrDefault();
                return name.TBL_TAGGING.NAME;
            } 
        }
        public Guid Barangay {
            get {
                PROFILINGMAPPINGDBEntities entities = new PROFILINGMAPPINGDBEntities();
                var name = (from names in entities.TBL_ADMIN.Where(admin => admin.ADMINID == adminID) select names).FirstOrDefault();
                return name.BARANGAYID;
            } 
        }
        public string LAT
        {
            get
            {
                PROFILINGMAPPINGDBEntities entities = new PROFILINGMAPPINGDBEntities();
                var name = (from names in entities.TBL_ADMIN.Where(admin => admin.ADMINID == adminID) select names).FirstOrDefault();
                return name.LAT;
            }
        }
        public string LONG
        {
            get
            {
                PROFILINGMAPPINGDBEntities entities = new PROFILINGMAPPINGDBEntities();
                var name = (from names in entities.TBL_ADMIN.Where(admin => admin.ADMINID == adminID) select names).FirstOrDefault();
                return name.LONG;
            }
        }
        public TBL_ADMIN CurrenUserProfile { get; set; }
        public IEnumerable<TBL_NAMES> ListOfNames { get; set; }
        public TBL_NAMES SelectedNames { get; set; }
        public IEnumerable<TBL_REQUEST> ListOfRequest { get; set; }
        public TBL_REQUEST SelectedRequest { get; set; }
        public string ErrorMessage { get; set; }
        public List<string> AreChecked { get; set; }
        public IEnumerable<TBL_BARANGAY> BarangayList { get; set; }
        public IEnumerable<TBL_ADMIN> ListOfAdmins { get; set; }
        public TBL_ADMIN SelectedAdmin { get; set; }
        public IEnumerable<TBL_TAGGING> ListOfTaggings { get; set; }
        public Guid SelectedRoute { get; set; }
        public string selectedName { get; set; }
        public IEnumerable<TBL_STATUS> ListOfStatus { get; set; }
        public bool Minor { get; set; }
        public bool YoungAdults { get; set; }
        public bool MiddleAge { get; set; }
        public bool Senior { get; set; }
        public string GenderFilter { get; set; }
        public int cntMinor { get; set; }
        public int cntYoungAdults { get; set; }
        public int cntMiddleAge { get; set; }
        public int cntSenior { get; set; }
        public int cntMale { get; set; }
        public int cntFemale { get; set; }
        public int PWDCount { get; set; }
        public int UserCount { get; set; }
    }
}