using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PriceCalcInternal;

namespace HoTroTinhGiaPhang
{
    
    public class CachInPhangBDO
    {
         public long ID {get;set;}
        public string TEN_CACH_IN{get;set;}
        public string SKU_SAN_PHAM	{get;set;}
        public long THU_TU { get; set; }
        public int SO_MAT { get; set; }
      
        //------
        public static List<CachInPhangBDO> LayTatCa(string productSku)
        {
            var parameters = new Dictionary<string, object>();

            parameters.Add("SKU_SAN_PHAM", productSku);


             var tblGIAY_IN_RUOT = CalcDbExecutor.ExecuteQuery("SELECT * FROM CACH_IN_SPP WHERE SKU_SAN_PHAM = @SKU_SAN_PHAM ORDER BY THU_TU ASC", parameters);

             List<CachInPhangBDO> lst = new List<CachInPhangBDO>();

            foreach (DataRow row in tblGIAY_IN_RUOT.Rows)
            {
                lst.Add(new CachInPhangBDO
                {
                    ID = (long)row["ID"],
                    TEN_CACH_IN = (string)row["TEN_CACH_IN"],
                    THU_TU = (long)row["THU_TU"],
                    SKU_SAN_PHAM = (string)row["SKU_SAN_PHAM"],
                    SO_MAT = (int)row ["SO_MAT"]
                });
            }

            return lst;
        }
        public static CachInPhangBDO LayTheoID(int idGiayRuot, string sKUSanPham)
        {
            var nguon = CachInPhangBDO.LayTatCa(sKUSanPham).Where(x => x.ID == idGiayRuot).Select(x => new CachInPhangBDO
            {
               ID = x.ID,
                    TEN_CACH_IN = x.TEN_CACH_IN,
                    THU_TU = x.THU_TU,
                    SKU_SAN_PHAM = x.SKU_SAN_PHAM,
                    SO_MAT = x.SO_MAT
                   
            }
                ).SingleOrDefault();

            return nguon;
        }

    }
    
}
