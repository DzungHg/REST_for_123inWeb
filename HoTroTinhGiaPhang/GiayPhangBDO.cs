using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PriceCalcInternal;

namespace HoTroTinhGiaPhang
{
    public class GiayPhangBDO
    {
         public long ID {get;set;}
        public string TEN_GIAY_IN{get;set;}
        public string SKU_SAN_PHAM	{get;set;}
        public long THU_TU { get; set; }
        public string MA_GIAY_AP { get; set; }
        public int MUC_LOI_NHUAN_GIAY {get;set;}
        //------
        public static List<GiayPhangBDO> LayTatCa(string productSku)
        {
            var parameters = new Dictionary<string, object>();

            parameters.Add("SKU_SAN_PHAM", productSku);


             var tblGIAY_IN_RUOT = CalcDbExecutor.ExecuteQuery("SELECT * FROM GIAY_IN_SPP WHERE SKU_SAN_PHAM = @SKU_SAN_PHAM ORDER BY THU_TU ASC", parameters);

            List <GiayPhangBDO> lst = new List<GiayPhangBDO>();

            foreach (DataRow row in tblGIAY_IN_RUOT.Rows)
            {
                lst.Add(new GiayPhangBDO
                {
                    ID = (long)row["ID"],
                    TEN_GIAY_IN = (string)row["TEN_GIAY_IN"],
                    THU_TU = (long)row["THU_TU"],
                    SKU_SAN_PHAM = (string)row["SKU_SAN_PHAM"],
                    MA_GIAY_AP = (string)row["MA_GIAY_AP"],
                    MUC_LOI_NHUAN_GIAY = (int)row ["MUC_LOI_NHUAN_GIAY"]
                });
            }

            return lst;
        }
        public static GiayPhangBDO LayTheoID(int idGiayRuot, string sKUSanPham)
        {
            var nguon = GiayPhangBDO.LayTatCa(sKUSanPham).Where(x => x.ID == idGiayRuot).Select(x => new GiayPhangBDO
            {
               ID = x.ID,
                    TEN_GIAY_IN = x.TEN_GIAY_IN,
                    THU_TU = x.THU_TU,
                    SKU_SAN_PHAM = x.SKU_SAN_PHAM,
                    MA_GIAY_AP = x.MA_GIAY_AP,
                    MUC_LOI_NHUAN_GIAY = x.MUC_LOI_NHUAN_GIAY
            }
                ).SingleOrDefault();

            return nguon;
        }

    
    }
}
