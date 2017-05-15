using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PriceCalcInteral
{
    public class GiayRuotBDO
    {
        public int ID {get;set;}
        public string TEN_GIAY_IN{get;set;}
        public string SKU_SAN_PHAM	{get;set;}
        public int THU_TU{get;set;}
        public string MA_GIAY_AP { get; set; }
        //------
         public static List<GiayRuotBDO> LayTatCa(string productSku)
        {
            var parameters = new Dictionary<string, object>();

            parameters.Add("SKU_SAN_PHAM", productSku);


             var tblGIAY_IN_RUOT = RestDbExecutor.ExecuteQuery("SELECT * FROM GIAY_IN_RUOT_SPC WHERE SKU_SAN_PHAM = @SKU_SAN_PHAM ORDER BY THU_TU ASC", parameters);

            List <GiayRuotBDO> lst = new List<GiayRuotBDO>();

            foreach (DataRow row in tblGIAY_IN_RUOT.Rows)
            {
                lst.Add(new GiayRuotBDO
                {
                    ID = (int)row["ID"],
                    TEN_GIAY_IN = (string)row["TEN_GIAY_IN"],
                    THU_TU = (int)row["THU_TU"],
                    SKU_SAN_PHAM = (string)row["SKU_SAN_PHAM"],
                    MA_GIAY_AP = (string)row["MA_GIAY_AP"]
                    
                });
            }

            return lst;
        }
        public static GiayRuotBDO LayTheoID(int idGiayRuot, string sKUSanPham)
        {
            var nguon = GiayRuotBDO.LayTatCa(sKUSanPham).Where(x => x.ID == idGiayRuot).Select(x => new GiayRuotBDO
            {
               ID = x.ID,
                    TEN_GIAY_IN = x.TEN_GIAY_IN,
                    THU_TU = x.THU_TU,
                    SKU_SAN_PHAM = x.SKU_SAN_PHAM,
                    MA_GIAY_AP = x.MA_GIAY_AP
            }
                ).SingleOrDefault();

            return nguon;
        }

    }

    }
}
