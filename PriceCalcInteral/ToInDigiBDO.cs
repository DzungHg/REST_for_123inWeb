using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PriceCalcInternal
{
    public class ToInDigiBDO
    {
        public string  MA_TO_IN {get;set;}
        public string TEN_TO_IN{get;set;}
        public int BHR{get;set;}
        public int THOI_GIAN_CHUAN_BI{get;set;}
        public int TOC_DO{get;set;}
        public int PHI_CLICK { get; set; }
        //========
        public static List<ToInDigiBDO> LayTatCa()
        {
            var parameters = new Dictionary<string, object>();

            var tblGIAY_IN_RUOT = CalcDbExecutor.ExecuteQuery("SELECT * FROM TO_IN_DIGI");

            List<ToInDigiBDO> lst = new List<ToInDigiBDO>();

            foreach (DataRow row in tblGIAY_IN_RUOT.Rows)
            {
                lst.Add(new ToInDigiBDO
                {
                    MA_TO_IN = (string)row["MA_TO_IN"],
                    TEN_TO_IN = (string)row["TEN_TO_IN"],
                    BHR = (int)row["BHR"],
                    THOI_GIAN_CHUAN_BI = (int)row["THOI_GIAN_CHUAN_BI"],
                    TOC_DO = (int)row["TOC_DO"],
                    PHI_CLICK = (int)row["PHI_CLICK"]
                    
                });
            }

            return lst;
        }
        public static ToInDigiBDO LayTheoID(string maToIn)
        {
            var nguon = ToInDigiBDO.LayTatCa().Where(x => x.MA_TO_IN == maToIn).Select(x => new ToInDigiBDO
            {
                MA_TO_IN = x.MA_TO_IN,
                TEN_TO_IN = x.TEN_TO_IN,
                BHR = x.BHR,
                THOI_GIAN_CHUAN_BI = x.THOI_GIAN_CHUAN_BI,
                TOC_DO = x.TOC_DO,
                PHI_CLICK = x.PHI_CLICK
            }
                ).SingleOrDefault();

            return nguon;
        }

    
    }
}
