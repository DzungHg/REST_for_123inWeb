using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriceCalcInternal;
using HoTroTinhGiaPhang;
using HoTroTinhGiaCuon;
namespace TestDzung
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("tổng record test: {0}", KhoCuonBDO.LayTatCa("MENU-LO-XO").Count());//OK
            //Console.WriteLine("tổng record test: {0}", KhoCuonBDO.LayTheoID(1, "MENU-LO-XO").TEN_KHO);//OK
            CauHinhTinhGiaCuon cHGiaCuon = new CauHinhTinhGiaCuon("MENU-LO-XO", 1, 1, 2, 1);
            Console.WriteLine("Tính giá in cuốn: {0}", TinhToanCuon.GiaIn(cHGiaCuon));//OK
            //Console.WriteLine("Tính giá cán màng: {0}", TinhPhi.GiaCanMang("MENU-LO-XO", 1, 1, 2, 1));//OK
            //var cauHinhTGCuon = new CauHinhTinhGiaCuon("CATALO-KIM",7, 6, 9, 3);
            //Console.WriteLine("Tính giá in: {0}", TinhToanCuon.GiaIn(cauHinhTGCuon));//OK
            //Console.WriteLine("Tính giá Giấy: {0}", TinhPhi.GiaGiayRuot(cauHinhTGCuon("MENU-LO-XO", 1, 1, 2, 1));
            //var cauHinhTGPHang = new CauHinhTinhGiaPhang("DANH-THIEP-THUONG", 1, 1, 1, 1); 
            //Test tính giá Phẳng
            //Console.WriteLine("Tính giá in phẳng: {0}", TinhToanPhang.GiaIn(cauHinhTGPHang));//OK
            //Console.WriteLine("Tính cán màng phẳng: {0}", TinhToanPhang.GiaCanMang(cauHinhTGPHang, "CM-1"));//OK
            Console.ReadLine();
        }
    }
}
