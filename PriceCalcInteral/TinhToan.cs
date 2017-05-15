using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalcInternal
{
    public class TinhToan
    {
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
