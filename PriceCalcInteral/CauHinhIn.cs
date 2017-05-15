using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalcInternal
{
    public class CauHinhIn
    {
        public int BHR { get; set; }
        public int TocDo { get; set; }
        public int SoMatIn { get; set; }
        public int PhiClick { get; set; }
        public double ThoiGianChuanBi { get; set; }

        public CauHinhIn(int bHR, int tocDo, int soMatIn, double thoiGianChuanBi, int phiClick)
        {
            this.BHR = bHR;
            this.TocDo = tocDo;
            this.SoMatIn = soMatIn;
            this.ThoiGianChuanBi = thoiGianChuanBi;
            this.PhiClick = phiClick;

        }
    }
}
