using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalcInteral
{
    public class TinhPhi
    {
        public static decimal GiaIn(CauHinhTinhGiaCuon cauHinhTGCuon)
        {
            decimal ketQua = 0;
            
            var chiPhiIn = TinhPhi.PhiIn(cauHinhTGCuon.CHinhIn, cauHinhTGCuon.SoToChayTong);

            var mucLoiNhuanIn_TP = (double)cauHinhTGCuon.MucLoiNhuanInThanhPham_PCT / 100;
            //Tinh theo muc loi nhuan gop /doanh thu
            ketQua = chiPhiIn + chiPhiIn * (decimal)mucLoiNhuanIn_TP / (decimal)(1 - mucLoiNhuanIn_TP); 
            
            return ketQua;
        }
        public static decimal GiaCanMang(CauHinhTinhGiaCuon cauHinhTGCuon)
        {
            decimal ketQua = 0;

            
            var maGiayRuot = cauHinhTGCuon.MaGiayAp;
            
            //Xác định có màng cán không bằng cách kiểm tra 2 item cuối của mã giấy áp
            
            string[] maGiays = maGiayRuot.Split('-');
            if (maGiays[maGiays.Length -1] != "CM") //mục cuối dùng
                return 0;
            //Có thì tìm mã tờ cán phủ
            string maToCanMang = "";
            foreach (var tcp in ToCanPhuBDO.LayTatCa())
            {
                if (tcp.MA_TO_CAN.Substring(0,2)== "CM")
                {
                    maToCanMang = tcp.MA_TO_CAN;
                    break;
                }
 
            }


            var soToChay = cauHinhTGCuon.SoToChayTong;

            var toCanPhuBDO = ToCanPhuBDO.LayTheoID(maToCanMang);

            var cauHinhCan1 = new CauHinhCanPhu(toCanPhuBDO.BHR, toCanPhuBDO.TOC_DO_M2, 2, toCanPhuBDO.THOI_GIAN_CHUAN_BI);

            var chiPhiCanPhu = TinhPhi.PhiCanMang(cauHinhCan1, soToChay);
            //Tinh theo muc loi nhuan gop /doanh thu
            double mucLoiNhuanIn_TP = (double)cauHinhTGCuon.MucLoiNhuanInThanhPham_PCT / 100;

            ketQua = chiPhiCanPhu + (decimal)mucLoiNhuanIn_TP * chiPhiCanPhu / (1 - (decimal)mucLoiNhuanIn_TP);

            return ketQua;
        }
        public static decimal GiaGiayRuot(CauHinhTinhGiaCuon cauHinhTGCuon)
        {
            decimal ketQua = 0;

            var soToChay = cauHinhTGCuon.SoToChayTong;
           

            var giaGiayTonBDO = GiaGiayTonBDO.LayTheoID(cauHinhTGCuon.MaGiayAp);
            double mucLoiNhuanGiay = (double)giaGiayTonBDO.MUC_LOI_NHUAN / 100;                                    
            //Tinh theo muc loi nhuan gop /doanh thu
            ketQua = giaGiayTonBDO.GIA_GIAY_TON_KHO + giaGiayTonBDO.GIA_GIAY_TON_KHO * (decimal)mucLoiNhuanGiay / (1 - (decimal)mucLoiNhuanGiay);
            ketQua *= soToChay;

            return ketQua;
        }
        public static decimal PhiIn (CauHinhIn cauHinhIn, int soToChay)
        {
            decimal result = 0;
            var thoiGianIn = (double)(soToChay * cauHinhIn.SoMatIn) / cauHinhIn.TocDo; //To chay /gio
            var thoiGianChuanBi_H = (double)cauHinhIn.ThoiGianChuanBi / 60;
            var phiVanHanh = cauHinhIn.BHR * (thoiGianChuanBi_H + thoiGianIn); //Thoi gian chuan bi la Phut
            var phiNgLieu = cauHinhIn.PhiClick * cauHinhIn.SoMatIn * soToChay;
            result = (decimal)phiVanHanh + phiNgLieu;

            return result;
        }
        public static decimal PhiCanMang(CauHinhCanPhu cauHinhCan, int soToChay)
        {//la
            decimal result = 0;
            double dienTichToChay = (0.32d * 0.48d) * soToChay * cauHinhCan.SoMatCan;//lấy cơ bản 32x48
            var thoiGianCan = (double)dienTichToChay / cauHinhCan.TocDoM2; //M2/gio
            double thoiGianChuanBiTheoGio = cauHinhCan.ThoiGianChuanBi / 60;
            var phiVanHanh = cauHinhCan.BHR * (thoiGianChuanBiTheoGio + thoiGianCan); //ThoiGianChuanBi la PHut
            var phiNgLieu = cauHinhCan.PhiNguyenLieuM2 * dienTichToChay;

            result = (decimal)phiVanHanh + (decimal)phiNgLieu;

            return result;
        }
    }
}
