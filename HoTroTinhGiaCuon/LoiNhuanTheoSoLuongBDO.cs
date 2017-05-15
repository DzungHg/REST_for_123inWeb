using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PriceCalcInternal;

namespace HoTroTinhGiaCuon
{
    public class LoiNhuanTheoSoLuongBDO
    {
        public string MA_SO { get; set; }
        public string TEN { get; set; }
        public string DAY_SO_LUONG { get; set; } //phân cách dấu ;
        public string DAY_LOI_NHUAN { get; set; } //phân cách dấu ;
        public string DON_VI_SO_LUONG { get; set; }
        public int THU_TU { get; set; }
        //====
        public static List<LoiNhuanTheoSoLuongBDO> LayTatCa()
        {
            var parameters = new Dictionary<string, object>();

            var tblGIAY_IN_RUOT = CalcDbExecutor.ExecuteQuery("SELECT * FROM TO_IN_DIGI");

            List<LoiNhuanTheoSoLuongBDO> lst = new List<LoiNhuanTheoSoLuongBDO>();

            foreach (DataRow row in tblGIAY_IN_RUOT.Rows)
            {
                lst.Add(new LoiNhuanTheoSoLuongBDO
                {
                    MA_SO = (string)row["MA_SO"],
                    TEN = (string)row["TEN"],
                    DAY_SO_LUONG = (string)row["DAY_SO_LUONG"],
                    DAY_LOI_NHUAN = (string)row["DAY_LOI_NHUAN"],
                    DON_VI_SO_LUONG = (string)row["DON_VI_SO_LUONG"],
                    THU_TU = (int)row["THU_TU"]

                });
            }

            return lst;
        }
        public static LoiNhuanTheoSoLuongBDO LayTheoID(string maSo)
        {
            var nguon = LoiNhuanTheoSoLuongBDO.LayTatCa().Where(x => x.MA_SO == maSo).Select(x => new LoiNhuanTheoSoLuongBDO
            {
                MA_SO = x.MA_SO,
                TEN = x.TEN,
                DAY_SO_LUONG = x.DAY_SO_LUONG,
                DAY_LOI_NHUAN = x.DAY_LOI_NHUAN,
                DON_VI_SO_LUONG = x.DON_VI_SO_LUONG,
                THU_TU = x.THU_TU
            }
                ).SingleOrDefault();

            return nguon;
        }
        public static int MucLNhuanTheoSLuong(string maSo, int soLuong = 0)
        {
            int result = 0; //tỉ lệ lợi nhuận trên doanh thu
            if (soLuong == 0 || string.IsNullOrEmpty(maSo))
                return 0;
            
            ///dãy số lượng: 9;19;29;39 .. là các giới hạn khi đó số lượng rơi vào các khoảng
            ///giới hạn này ví dụ 1 rơi vô 1-> 9; 10/11/12 rơi vô 10 ->19; để đạt mục tiêu
            ///thiết lập 10 trang là LN này, 20 trang là lợi nhuận; b; 30 trang là lợi nhuận z

            var daySoLuongS = LoiNhuanTheoSoLuongBDO.LayTheoID(maSo).DAY_SO_LUONG.Split(';'); //string
            var dayLoiNhuanS = LoiNhuanTheoSoLuongBDO.LayTheoID(maSo).DAY_LOI_NHUAN.Split(';'); //string
            //
            if (daySoLuongS == null || dayLoiNhuanS == null)
                return 0;
            //
            var soLuongMax = int.Parse(daySoLuongS[daySoLuongS.Length - 1]); //Xử lý item cuối vì nó không có khoảng thêm
            
            var soLuongCheck = 0;//Dùng để lấy tỉ lệ mà thôi
            
            if (soLuong > soLuongMax)
            {
                soLuongCheck = soLuongMax; //Để giới hạn việc rôi vào khoảng lớn nhất
            }
            else
                soLuongCheck = soLuong;


            //Xem khoảng số lượng rớt vô đâu rồi lấy giá ở đó tính
            var tmpI = 0;
            var soBatDau = 1;
            for (int i = 0; i < daySoLuongS.Length; i++)
            {
                if (soLuongCheck >= soBatDau && soLuongCheck <= int.Parse(daySoLuongS[i]))
                {
                    tmpI = i;
                    break;
                }
                soBatDau = int.Parse(daySoLuongS[i]) + 1;
            }
            result = int.Parse(dayLoiNhuanS[tmpI]);            

            return result;
        }
    }
}
