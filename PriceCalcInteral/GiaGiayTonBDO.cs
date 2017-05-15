using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PriceCalcInternal
{
    public class GiaGiayTonBDO
    {
        public  string MA_GIAY {get;set;}
        public string TEN_GIAY	{get;set;}
        public int GIA_GIAY_TON_KHO{get;set;}
        public int MUC_LOI_NHUAN { get; set; }
        //------
        public static List<GiaGiayTonBDO> LayTatCa()
        {
            var parameters = new Dictionary<string, object>();

            var tblGIAY_IN_RUOT = CalcDbExecutor.ExecuteQuery("SELECT * FROM GIA_GIAY_TON");

            List <GiaGiayTonBDO> lst = new List<GiaGiayTonBDO>();

            foreach (DataRow row in tblGIAY_IN_RUOT.Rows)
            {
                lst.Add(new GiaGiayTonBDO
                {
                    MA_GIAY = (string)row["MA_GIAY"],
                    TEN_GIAY = (string)row["TEN_GIAY"],                 
                    GIA_GIAY_TON_KHO = (int)row["GIA_GIAY_TON_KHO"],
                    MUC_LOI_NHUAN = (int)row["MUC_LOI_NHUAN"]
                    
                });
            }

            return lst;
        }
        public static GiaGiayTonBDO LayTheoID(string maGiayTon)
        {
            var nguon = GiaGiayTonBDO.LayTatCa().Where(x => x.MA_GIAY == maGiayTon).Select(x => new GiaGiayTonBDO
            {
                MA_GIAY = x.MA_GIAY,
                    TEN_GIAY = x.TEN_GIAY,                 
                    GIA_GIAY_TON_KHO = x.GIA_GIAY_TON_KHO,
                    MUC_LOI_NHUAN = x.MUC_LOI_NHUAN
            }
                ).SingleOrDefault();

            return nguon;
        }

    }

    
}
