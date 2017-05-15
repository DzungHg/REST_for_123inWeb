using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriceCalcInternal;


namespace HoTroTinhGiaCuon
{
    public class TinhToanCuon
    {
        public static decimal GiaIn(CauHinhTinhGiaCuon cauHinhTGCuon)
        {
            decimal ketQua = 0;
            
            var chiPhiIn = TinhToan.PhiIn(cauHinhTGCuon.CHinhIn, cauHinhTGCuon.SoToChayTong);

            //var mucLoiNhuanIn_TP = (double)cauHinhTGCuon.MucLoiNhuanInThanhPham_PCT / 100; //đã cũ theo số cuốn
            var mucLoiNhuanIn = (double)cauHinhTGCuon.MucLoiNhuanIn_TheoSoTrang_PCT / 100;

            //Tinh theo muc loi nhuan gop /doanh thu
            ketQua = chiPhiIn + chiPhiIn * (decimal)mucLoiNhuanIn / (decimal)(1 - mucLoiNhuanIn); 
            
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

            var chiPhiCanPhu = TinhToan.PhiCanMang(cauHinhCan1, soToChay);
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
       
        
    }
}
