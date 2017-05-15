using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PriceCalcInternal;

namespace HoTroTinhGiaPhang
{
    public class KhoPhangBDO
    {
        public long ID { get; set; }
        public string TEN_KHO { get; set; }
        public long THU_TU { get; set; }
        public string SKU_SAN_PHAM { get; set; }
        public string MA_TO_IN_AP { get; set; }
        public int SO_CON_TR_TO_CHAY { get; set; }

        public static List<KhoPhangBDO> LayTatCa(string productSku)
        {
            var parameters = new Dictionary<string, object>();

            parameters.Add("SKU_SAN_PHAM", productSku);


            var tblKHO_SAN_PHAM = CalcDbExecutor.ExecuteQuery("SELECT * FROM KHO_SAN_PHAM_SPP WHERE SKU_SAN_PHAM = @SKU_SAN_PHAM ORDER BY THU_TU ASC", parameters);

            List<KhoPhangBDO> lst = new List<KhoPhangBDO>();

            foreach (DataRow row in tblKHO_SAN_PHAM.Rows)
            {
                lst.Add(new KhoPhangBDO
                {
                    ID = (long)row["ID"],
                    TEN_KHO = (string)row["TEN_KHO"],
                    THU_TU = (long)row["THU_TU"],
                    SKU_SAN_PHAM = (string)row["SKU_SAN_PHAM"],
                    MA_TO_IN_AP = (string)row["MA_TO_IN_AP"],
                    SO_CON_TR_TO_CHAY = (int)row["SO_CON_TR_TO_CHAY"]
                });
            }

            return lst;
        }
        public static KhoPhangBDO LayTheoID(int idKhoCuon, string sKUSanPham)
        {
            var nguon = KhoPhangBDO.LayTatCa(sKUSanPham).Where(x => x.ID == idKhoCuon).Select(x => new KhoPhangBDO
            {
                ID = x.ID,
                TEN_KHO = x.TEN_KHO,
                THU_TU = x.THU_TU,
                SKU_SAN_PHAM = x.SKU_SAN_PHAM,
                MA_TO_IN_AP = x.MA_TO_IN_AP,
                SO_CON_TR_TO_CHAY = x.SO_CON_TR_TO_CHAY
            }
                ).SingleOrDefault();

            return nguon;
        }
    }
}
