using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriceCalcInternal;


namespace HoTroTinhGiaPhang
{
    public class TinhToanPhang
    {
        public static decimal GiaIn(CauHinhTinhGiaPhang cauHinhTGPhang)
        {
            decimal ketQua = 0;

            var chiPhiIn = TinhToanPhang.PhiIn(cauHinhTGPhang.CHinhIn, cauHinhTGPhang.SoToChayTong);

            var mucLoiNhuanIn_TP = (double)cauHinhTGPhang.MucLoiNhuanIn_PCT / 100;
            //Tinh theo muc loi nhuan gop /doanh thu
            ketQua = chiPhiIn + chiPhiIn * (decimal)mucLoiNhuanIn_TP / (decimal)(1 - mucLoiNhuanIn_TP);

            return ketQua;
        }
        public static decimal GiaGiay(CauHinhTinhGiaPhang cauHinhTGPhang)
        {
            decimal ketQua = 0;

            var soToChay = cauHinhTGPhang.SoToChayTong;
            
            double mucLoiNhuanGiay = (double)cauHinhTGPhang.MucLoiNhuanGiay_PCT / 100;
            //Tinh theo muc loi nhuan gop /doanh thu
            
            ketQua = cauHinhTGPhang.GiaGiayTonKho + cauHinhTGPhang.GiaGiayTonKho * (decimal)mucLoiNhuanGiay / (1 - (decimal)mucLoiNhuanGiay);
            ketQua *= soToChay;

            return ketQua;
        }
        public static decimal PhiIn(CauHinhIn cauHinhIn, int soToChay)
        {
            decimal result = 0;
            var thoiGianIn = (double)(soToChay * cauHinhIn.SoMatIn) / cauHinhIn.TocDo; //To chay /gio
            var thoiGianChuanBi_H = (double)cauHinhIn.ThoiGianChuanBi / 60;
            var phiVanHanh = cauHinhIn.BHR * (thoiGianChuanBi_H + thoiGianIn); //Thoi gian chuan bi la Phut
            var phiNgLieu = cauHinhIn.PhiClick * cauHinhIn.SoMatIn * soToChay;
            result = (decimal)phiVanHanh + phiNgLieu;

            return result;
        }
        public static decimal GiaCanMang(CauHinhTinhGiaPhang cauHinhTGPhang, string MA_CAN_MANG)
        {
            ///MA_CAN_MANG dạng: CM-2 đầu là cán màng, cột 2 là số mặt
            decimal ketQua = 0;            

            //Xác định có màng cán không bằng cách kiểm tra 2 item cuối của mã giấy áp

            string[] maCanMangs = MA_CAN_MANG.Split('-');

            string aCM = maCanMangs[0];
            int soMatCan = int.Parse(maCanMangs[1]);//chắc chắn có giá trị
               
            //Có thì tìm mã tờ cán phủ
            var maToCanMang = ToCanPhuBDO.LayTatCa().Where(x => x.MA_TO_CAN.Substring(0,2) == aCM).SingleOrDefault().MA_TO_CAN;
            /*foreach (var tcp in ToCanPhuBDO.LayTatCa())
            {
                if (tcp.MA_TO_CAN.Substring(0, 2) == aCM)
                {
                    maToCanMang = tcp.MA_TO_CAN;
                    break;
                }
                
            }*/
            
            if (string.IsNullOrEmpty(maToCanMang)) 
                return 0; //thoát luôn nghĩa là không có mã

            var soToChay = cauHinhTGPhang.SoToChayTong;

            var toCanPhuBDO = ToCanPhuBDO.LayTheoID(maToCanMang);

            var cauHinhCan1 = new CauHinhCanPhu(toCanPhuBDO.BHR, toCanPhuBDO.TOC_DO_M2, soMatCan, toCanPhuBDO.THOI_GIAN_CHUAN_BI);

            var chiPhiCanPhu = TinhToan.PhiCanMang(cauHinhCan1, soToChay); //Dùng bên tính toán cuốn để khỏi double.
            //Tinh theo muc loi nhuan gop /doanh thu
            double mucLoiNhuanIn_TP = (double)cauHinhTGPhang.MucLoiNhuanIn_PCT / 100;

            ketQua = chiPhiCanPhu + (decimal)mucLoiNhuanIn_TP * chiPhiCanPhu / (1 - (decimal)mucLoiNhuanIn_TP);

            return ketQua;
        }
    }
}
