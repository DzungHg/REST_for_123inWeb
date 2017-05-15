using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalcInteral
{
    public class TO_IN_DIGI
    {
        public string MA_TO_IN { get; set; }
        public string TEN_TO_IN { get; set; }
        public int BHR { get; set; }
        public int TOC_DO { get; set; }
        public int PHI_CLICK { get; set; }
    }
    public class GIA_GIAY_TON
    {
        public string MA_GIAY { get; set; }
        public string TEN_GIAY { get; set; }
        public int GIA_GIAY_TON_KHO { get; set; }
        public int MUC_LOI_NHUAN { get; set; }
    }
    public struct CauHinhIn
    {
        public int BHR { get; set; }
        public int TocDo { get; set; }
        public int SoMatIn { get; set; }
        public int PhiClick { get; set; }
        public double ThoiGianChuanBi { get; set; }

        public CauHinhIn(int bHR, int tocDo, int soMatIn, double thoiGianChuanBi)
        {
            this.BHR = bHR;
            this.TocDo = tocDo;
            this.SoMatIn = soMatIn;
            this.ThoiGianChuanBi = thoiGianChuanBi;

        }
    }
    public struct CauHinhCanPhu
    {
        public int BHR { get; set; }
        public int TocDoM2 { get; set; }
        public int SoMatCan { get; set; }
        public int PhiNguyenLieuM2 { get; set; }
        public double ThoiGianChuanBi { get; set; }

        public CauHinhCanPhu(int bHR, int tocDo, int soMatIn, double thoiGianChuanBi)
        {
            this.BHR = bHR;
            this.TocDoM2 = tocDo;
            this.SoMatCan = soMatIn;
            this.ThoiGianChuanBi = thoiGianChuanBi;

        }
    }
}
