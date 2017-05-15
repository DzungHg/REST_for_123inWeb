using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PriceCalcInternal;

namespace HoTroTinhGiaCuon
{
    class SoLuongCuonBDO
    {
        public long ID { get; set; }
        public string TEN_SO_LUONG { get; set; }
        public long THU_TU { get; set; }
        public string SKU_SAN_PHAM { get; set; }
        public int SO_LUONG {get;set;}
        public int MUC_LOI_NHUAN_IN_TP	{ get; set; }
        public int  SO_TO_CHAY_BU_HAO	{ get; set; }
        //
        public static List<SoLuongCuonBDO> LayTatCa(string productSku)
        {
            var parameters = new Dictionary<string, object>();

            parameters.Add("SKU_SAN_PHAM", productSku);

                       
            var tblSO_LUONG_CUON = CalcDbExecutor.ExecuteQuery("SELECT * FROM SO_LUONG_CUON_SPC WHERE SKU_SAN_PHAM = @SKU_SAN_PHAM ORDER BY THU_TU ASC", parameters);

            List<SoLuongCuonBDO> lst = new List<SoLuongCuonBDO>();
           
            
            foreach (DataRow row in tblSO_LUONG_CUON.Rows)
            {
                lst.Add(new SoLuongCuonBDO
                {
                    ID = (long)row["ID"],
                    TEN_SO_LUONG = (string)row["TEN_SO_LUONG"],
                    THU_TU = (long)row["THU_TU"],
                    SKU_SAN_PHAM = (string)row["SKU_SAN_PHAM"],
                    SO_LUONG = (int)row["SO_LUONG"],
                    MUC_LOI_NHUAN_IN_TP = (int)row["MUC_LOI_NHUAN_IN_TP"],
                    SO_TO_CHAY_BU_HAO = (int)row["SO_TO_CHAY_BU_HAO"]

                });
            }

            return lst;
        }
        public static SoLuongCuonBDO LayTheoID (int idSoLuongCuon, string sKUSanPham)
        {
            var nguon = SoLuongCuonBDO.LayTatCa(sKUSanPham).Where(x => x.ID == idSoLuongCuon).Select(x => new SoLuongCuonBDO
                {
                    ID = x.ID,
                    TEN_SO_LUONG = x.TEN_SO_LUONG,
                    THU_TU = x.THU_TU,
                    SKU_SAN_PHAM = x.SKU_SAN_PHAM,
                    SO_LUONG = x.SO_LUONG,
                    MUC_LOI_NHUAN_IN_TP = x.MUC_LOI_NHUAN_IN_TP,
                    SO_TO_CHAY_BU_HAO = x.SO_TO_CHAY_BU_HAO
                }).SingleOrDefault();
            return nguon;
        }
    }
}
