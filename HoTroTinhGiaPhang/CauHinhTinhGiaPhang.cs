using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriceCalcInternal;

namespace HoTroTinhGiaPhang
{
    public class CauHinhTinhGiaPhang
    {
         public string SKU_SanPham { get; set; }
        public int SoToChayTong { get; set; }
        public string MaGiayAp { get; set; }
        public string MaToInAp { get; set; }
        public int MucLoiNhuanIn_PCT { get; set; }
        public int MucLoiNhuanGiay_PCT { get; set; }
        public int GiaGiayTonKho { get; set; }

        private CauHinhIn _cauHinhIn;
        public CauHinhIn CHinhIn
        {
            get { return _cauHinhIn; }
            set { _cauHinhIn = value; }
        }
       

        public CauHinhTinhGiaPhang(string sku_SanPham, int idKho, int idSoLuong, int idGiay, int idCachIn)
        {
            this.SKU_SanPham = sku_SanPham;

            var khoPhangBDO = KhoPhangBDO.LayTheoID(idKho, this.SKU_SanPham);
            var soConTrenTo = khoPhangBDO.SO_CON_TR_TO_CHAY;
            this.MaToInAp = khoPhangBDO.MA_TO_IN_AP;

            var giayPhangBDO = GiayPhangBDO.LayTheoID(idGiay, this.SKU_SanPham);
            this.MaGiayAp = giayPhangBDO.MA_GIAY_AP;
            // số mặt in
            var soMatIn = CachInPhangBDO.LayTheoID(idCachIn, sku_SanPham).SO_MAT;
            //Xác định mức lợi nhuận giấy
            int mucLoiNhuanGiay = 0;
            if (giayPhangBDO.MUC_LOI_NHUAN_GIAY > 0)
                mucLoiNhuanGiay = giayPhangBDO.MUC_LOI_NHUAN_GIAY;
            else
                mucLoiNhuanGiay = GiaGiayTonBDO.LayTheoID(giayPhangBDO.MA_GIAY_AP).MUC_LOI_NHUAN;
            this.MucLoiNhuanGiay_PCT = mucLoiNhuanGiay;
            //Giá giấy
            this.GiaGiayTonKho = GiaGiayTonBDO.LayTheoID(giayPhangBDO.MA_GIAY_AP).GIA_GIAY_TON_KHO;

            //tính tổng số tờ chạy
            var soLuongPhangBDO = SoLuongPhangBDO.LayTheoID(idSoLuong, this.SKU_SanPham);
            var soSanPham = soLuongPhangBDO.SO_LUONG;
            var soToChayBuHao = soLuongPhangBDO.SO_TO_CHAY_BU_HAO;
            //Mức lợi nhuận in
            this.MucLoiNhuanIn_PCT = soLuongPhangBDO.MUC_LOI_NHUAN_IN_TP;                      
           
            //Tính tiếp tổng tờ chạy
            int soToChay = 0;
            if (soSanPham % soConTrenTo > 0)//Chia bị dư
                soToChay = soSanPham / soConTrenTo + 1 + soToChayBuHao;
            else
                soToChay = soSanPham / soConTrenTo + soToChayBuHao;
            this.SoToChayTong = soToChay;
            //---dùng BDO Tờ in chung
            var toInDigiBDO = ToInDigiBDO.LayTheoID(khoPhangBDO.MA_TO_IN_AP);
            _cauHinhIn = new CauHinhIn(toInDigiBDO.BHR, toInDigiBDO.TOC_DO, soMatIn, toInDigiBDO.THOI_GIAN_CHUAN_BI, toInDigiBDO.PHI_CLICK);
          
        }
    }
}
