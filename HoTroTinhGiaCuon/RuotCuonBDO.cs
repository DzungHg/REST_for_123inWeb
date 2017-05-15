using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PriceCalcInternal;

namespace HoTroTinhGiaCuon
{
    public class RuotCuonBDO
    {
        public long ID { get; set; }
        public string TEN_RUOT{get;set;}
        public long THU_TU { get; set; }
        public string SKU_SAN_PHAM{get;set;}
        public int SO_TRANG { get; set; }
        //-------
        public static List<RuotCuonBDO> LayTatCa(string productSku)
        {
            var parameters = new Dictionary<string, object>();

            parameters.Add("SKU_SAN_PHAM", productSku);


            var tblRUOT = CalcDbExecutor.ExecuteQuery("SELECT * FROM RUOT_SPC WHERE SKU_SAN_PHAM = @SKU_SAN_PHAM ORDER BY THU_TU ASC", parameters);

            List <RuotCuonBDO > lst = new List<RuotCuonBDO>();

            foreach (DataRow row in tblRUOT.Rows)
            {
                lst.Add(new RuotCuonBDO
                {
                    ID = (long)row["ID"],
                    TEN_RUOT = (string)row["TEN_RUOT"],
                    THU_TU = (long)row["THU_TU"],
                    SKU_SAN_PHAM = (string)row["SKU_SAN_PHAM"],
                    SO_TRANG = (int)row["SO_TRANG"]
                    
                });
            }

            return lst;
        }
        public static RuotCuonBDO LayTheoID(int idRuotCuon, string sKUSanPham)
        {
            var nguon = RuotCuonBDO.LayTatCa(sKUSanPham).Where(x => x.ID == idRuotCuon).Select(x => new RuotCuonBDO
            {
                ID = x.ID,
                TEN_RUOT = x.TEN_RUOT,
                THU_TU = x.THU_TU,
                SKU_SAN_PHAM = x.SKU_SAN_PHAM,
                SO_TRANG = x.SO_TRANG
            }
                ).SingleOrDefault();

            return nguon;
        }

    }
}
