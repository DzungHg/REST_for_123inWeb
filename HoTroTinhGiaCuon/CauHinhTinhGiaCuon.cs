using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriceCalcInternal;


namespace HoTroTinhGiaCuon
{
    public class CauHinhTinhGiaCuon
    {
        public string SKU_SanPham { get; set; }
        public int SoToChayTong { get; set; }
        public int SoTrangTong { get; set; }
        public string MaGiayAp { get; set; }
        public string MaToInAp { get; set; }
        public int MucLoiNhuanInThanhPham_PCT { get; set; }
        public int MucLoiNhuanIn_TheoSoTrang_PCT { get; set; }

        private CauHinhIn _cauHinhIn;
        public CauHinhIn CHinhIn
        {
            get { return _cauHinhIn; }
            set { _cauHinhIn = value; }
        }
       

        public CauHinhTinhGiaCuon(string sku_SanPham, int idKhoCuon, int idSoLuongCuon, int idRuot, int idGiayRuot)
        {
            this.SKU_SanPham = sku_SanPham;

            var khoCuonBDO = KhoCuonBDO.LayTheoID(idKhoCuon, this.SKU_SanPham);            
            var soConTrenTo = khoCuonBDO.SO_CON_TR_TO_CHAY;
            this.MaToInAp = khoCuonBDO.MA_TO_IN_AP;
            //mức lợi nhuận in theo số trang
            this.MucLoiNhuanIn_TheoSoTrang_PCT = LoiNhuanTheoSoLuongBDO.MucLNhuanTheoSLuong(khoCuonBDO.MA_LOI_NHUAN_THEO_TRANG);
            //
            var giayRuotBDO = GiayRuotBDO.LayTheoID(idGiayRuot, this.SKU_SanPham);
            this.MaGiayAp = giayRuotBDO.MA_GIAY_AP;
                        
            //tính tổng số tờ chạy và số trang
            var soLuongCuonBDO = SoLuongCuonBDO.LayTheoID(idSoLuongCuon, this.SKU_SanPham);
            var soCuon = soLuongCuonBDO.SO_LUONG;
            var soToChayBuHao = soLuongCuonBDO.SO_TO_CHAY_BU_HAO;

            this.MucLoiNhuanInThanhPham_PCT = soLuongCuonBDO.MUC_LOI_NHUAN_IN_TP;
            

            var ruotCuonBDO = RuotCuonBDO.LayTheoID(idRuot, this.SKU_SanPham);
            //so trang;
            this.SoTrangTong = ruotCuonBDO.SO_TRANG * soCuon;
            //end sotrang
            int soToTheoSoCuon = (ruotCuonBDO.SO_TRANG * soCuon) / 2; //Chan            
            //Tính tiếp tổng tờ chạy
            int soToChay = 0;
            if (soToTheoSoCuon % soConTrenTo > 0)//Chia bị dư
                soToChay = soToTheoSoCuon / soConTrenTo + 1 + soToChayBuHao;
            else
                soToChay = soToTheoSoCuon / soConTrenTo + soToChayBuHao;
            this.SoToChayTong = soToChay;
            //---
            var toInDigiBDO = ToInDigiBDO.LayTheoID(khoCuonBDO.MA_TO_IN_AP);
            _cauHinhIn = new CauHinhIn(toInDigiBDO.BHR, toInDigiBDO.TOC_DO, 2, toInDigiBDO.THOI_GIAN_CHUAN_BI, toInDigiBDO.PHI_CLICK);
          
        }
    }
}
