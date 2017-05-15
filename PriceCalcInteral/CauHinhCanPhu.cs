using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalcInternal
{
    public class CauHinhCanPhu
    {//mặc định cán khổ A3
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
