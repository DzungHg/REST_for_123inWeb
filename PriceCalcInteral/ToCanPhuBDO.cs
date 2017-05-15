using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PriceCalcInternal
{
    public class ToCanPhuBDO
    {
        public string MA_TO_CAN {get;set;}
        public string TEN_TO_CAN{get;set;}
        public int BHR{get;set;}
        public int TOC_DO_M2{get;set;}
        public int PHI_M2{get;set;}
        public int THOI_GIAN_CHUAN_BI { get; set; }//PHut
        //--------
        public static List<ToCanPhuBDO> LayTatCa()
        {
            var parameters = new Dictionary<string, object>();

            var tblGIAY_IN_RUOT = CalcDbExecutor.ExecuteQuery("SELECT * FROM TO_CAN_PHU");

            List<ToCanPhuBDO> lst = new List<ToCanPhuBDO>();

            foreach (DataRow row in tblGIAY_IN_RUOT.Rows)
            {
                lst.Add(new ToCanPhuBDO
                {
                    MA_TO_CAN = (string)row["MA_TO_CAN"],
                    TEN_TO_CAN = (string)row["TEN_TO_CAN"],
                    BHR = (int)row["BHR"],
                    THOI_GIAN_CHUAN_BI = (int)row["THOI_GIAN_CHUAN_BI"],
                    TOC_DO_M2 = (int)row["TOC_DO_M2"],
                    PHI_M2 = (int)row["PHI_M2"]

                });
            }

            return lst;
        }
        public static ToCanPhuBDO LayTheoID(string maToCanPhu)
        {
            var nguon = ToCanPhuBDO.LayTatCa().Where(x => x.MA_TO_CAN == maToCanPhu).Select(x => new ToCanPhuBDO
            {
                MA_TO_CAN = x.MA_TO_CAN,
                TEN_TO_CAN = x.TEN_TO_CAN,
                BHR = x.BHR,
                THOI_GIAN_CHUAN_BI = x.THOI_GIAN_CHUAN_BI,
                TOC_DO_M2 = x.TOC_DO_M2,
                PHI_M2 = x.PHI_M2
            }
                ).SingleOrDefault();

            return nguon;
        }
    }
}
