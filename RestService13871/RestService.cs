using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using PriceCalcInternal; //Dz them
using HoTroTinhGiaPhang;
using HoTroTinhGiaCuon;

namespace RestService13871
{
    public class RestService : IRestService
    {
        #region REST APIs

        /// <summary>
        /// Lấy thông tin chi tiết của sản phẩm phẳng thông qua SKU
        /// </summary>
        /// <param name="productSku"></param>
        /// <returns></returns>
        public FlatProductDetails GetFlatProductDetails(string productSku)
        {
            var parameters = new Dictionary<string, object>();

            parameters.Add("SKU_SAN_PHAM", productSku);

            var productDetails = new FlatProductDetails();

            var tblKHO_SAN_PHAM = RestDbExecutor.ExecuteQuery("SELECT * FROM KHO_SAN_PHAM_SPP WHERE SKU_SAN_PHAM = @SKU_SAN_PHAM ORDER BY THU_TU ASC", parameters);
            var tblSO_LUONG_THANH_PHAM = RestDbExecutor.ExecuteQuery("SELECT * FROM SO_LUONG_THANH_PHAM_SPP WHERE SKU_SAN_PHAM = @SKU_SAN_PHAM ORDER BY THU_TU ASC", parameters);
            var tblCACH_IN = RestDbExecutor.ExecuteQuery("SELECT * FROM CACH_IN_SPP WHERE SKU_SAN_PHAM = @SKU_SAN_PHAM ORDER BY THU_TU ASC", parameters);
            var tblGIAY_IN = RestDbExecutor.ExecuteQuery("SELECT * FROM GIAY_IN_SPP WHERE SKU_SAN_PHAM = @SKU_SAN_PHAM ORDER BY THU_TU ASC", parameters);
            var tblNHOM_THANH_PHAM = RestDbExecutor.ExecuteQuery("SELECT * FROM NHOM_THANH_PHAM_SPP WHERE SKU_SAN_PHAM = @SKU_SAN_PHAM ORDER BY THU_TU ASC", parameters);
            var tblTHOI_GIAN_GIAO = RestDbExecutor.ExecuteQuery("SELECT * FROM THOI_GIAN_GIAO_SPP WHERE SKU_SAN_PHAM = @SKU_SAN_PHAM ORDER BY THU_TU ASC", parameters);

            productDetails.DANH_SACH_KHO_SAN_PHAM = new List<KHO_SAN_PHAM_SPP>();
            foreach (DataRow row in tblKHO_SAN_PHAM.Rows)
            {
                productDetails.DANH_SACH_KHO_SAN_PHAM.Add(new KHO_SAN_PHAM_SPP
                {
                    ID = (long)row["ID"],
                    TEN_KHO = (string)row["TEN_KHO"],
                    THU_TU = (long)row["THU_TU"],
                    SKU_SAN_PHAM = (string)row["SKU_SAN_PHAM"]
                });
            }

            productDetails.DANH_SACH_SO_LUONG_THANH_PHAM = new List<SO_LUONG_THANH_PHAM_SPP>();
            foreach (DataRow row in tblSO_LUONG_THANH_PHAM.Rows)
            {
                productDetails.DANH_SACH_SO_LUONG_THANH_PHAM.Add(new SO_LUONG_THANH_PHAM_SPP
                {
                    ID = (long)row["ID"],
                    TEN_SO_LUONG = (string)row["TEN_SO_LUONG"],
                    THU_TU = (long)row["THU_TU"],
                    SKU_SAN_PHAM = (string)row["SKU_SAN_PHAM"]
                });
            }

            productDetails.DANH_SACH_CACH_IN = new List<CACH_IN_SPP>();
            foreach (DataRow row in tblCACH_IN.Rows)
            {
                productDetails.DANH_SACH_CACH_IN.Add(new CACH_IN_SPP
                {
                    ID = (long)row["ID"],
                    TEN_CACH_IN = (string)row["TEN_CACH_IN"],
                    THU_TU = (long)row["THU_TU"],
                    SKU_SAN_PHAM = (string)row["SKU_SAN_PHAM"]
                });
            }

            productDetails.DANH_SACH_GIAY_IN = new List<GIAY_IN_SPP>();
            foreach (DataRow row in tblGIAY_IN.Rows)
            {
                productDetails.DANH_SACH_GIAY_IN.Add(new GIAY_IN_SPP
                {
                    ID = (long)row["ID"],
                    TEN_GIAY_IN = (string)row["TEN_GIAY_IN"],
                    THU_TU = (long)row["THU_TU"],
                    SKU_SAN_PHAM = (string)row["SKU_SAN_PHAM"]
                });
            }

            productDetails.DANH_SACH_NHOM_THANH_PHAM = new List<NHOM_THANH_PHAM_SPP>();
            foreach (DataRow row in tblNHOM_THANH_PHAM.Rows)
            {
                var NHOM_THANH_PHAM = new NHOM_THANH_PHAM_SPP
                {
                    ID = (long)row["ID"],
                    TEN_NHOM = (string)row["TEN_NHOM"],
                    THU_TU = (long)row["THU_TU"],
                    SKU_SAN_PHAM = (string)row["SKU_SAN_PHAM"]
                };

                var tblTHANH_PHAM = RestDbExecutor.ExecuteQuery("SELECT * FROM THANH_PHAM_SPP WHERE ID_NHOM_THANH_PHAM = " + NHOM_THANH_PHAM.ID + " ORDER BY THU_TU ASC ", null);

                NHOM_THANH_PHAM.DANH_SACH_THANH_PHAM = new List<THANH_PHAM_SPP>();
                foreach (DataRow rowTHANH_PHAM in tblTHANH_PHAM.Rows)
                {
                    NHOM_THANH_PHAM.DANH_SACH_THANH_PHAM.Add(new THANH_PHAM_SPP
                    {
                        ID = (long)rowTHANH_PHAM["ID"],
                        TEN_THANH_PHAM = (string)rowTHANH_PHAM["TEN_THANH_PHAM"],
                        THU_TU = (long)rowTHANH_PHAM["THU_TU"],
                        ID_NHOM_THANH_PHAM = (long)rowTHANH_PHAM["ID_NHOM_THANH_PHAM"]
                    });
                }

                productDetails.DANH_SACH_NHOM_THANH_PHAM.Add(NHOM_THANH_PHAM);
            }

            productDetails.DANH_SACH_THOI_GIAN_GIAO = new List<THOI_GIAN_GIAO_SPP>();
            foreach (DataRow row in tblTHOI_GIAN_GIAO.Rows)
            {
                productDetails.DANH_SACH_THOI_GIAN_GIAO.Add(new THOI_GIAN_GIAO_SPP
                {
                    ID = (long)row["ID"],
                    TEN_THOI_GIAN_GIAO = (string)row["TEN_THOI_GIAN_GIAO"],
                    THU_TU = (long)row["THU_TU"],
                    SKU_SAN_PHAM = (string)row["SKU_SAN_PHAM"]
                });
            }

            return productDetails;
        }

        /// <summary>
        /// Lấy giá của sản phẩm phẳng
        /// </summary>
        /// <param name="sizeId"></param>
        /// <param name="quantityId"></param>
        /// <param name="paperId"></param>
        /// <param name="printingStyleId"></param>
        /// <param name="shipTimeId"></param>
        /// <param name="finishedProductGroupId"></param>
        /// <param name="finishedProductIds"></param>
        /// <returns></returns>
        //public decimal? GetPriceOfFlatProduct(string sizeId, string quantityId, string paperId, string printingStyleId, string shipTimeId, string finishedProductGroupId, string finishedProductIds)
        //{
        //    /**
        //     * Kiểm tra chứng thực
        //     */

        //    if (!AuthenticateViaAuthorizationHeader())
        //    {
        //        return null;
        //    }

        //    /**
        //     * Tính giá
        //     */

        //    var parameters = new Dictionary<string, object>();

        //    parameters.Add("ID_KHO_SAN_PHAM", sizeId);
        //    parameters.Add("ID_SO_LUONG_THANH_PHAM", quantityId);
        //    parameters.Add("ID_GIAY_IN", paperId);
        //    parameters.Add("ID_CACH_IN", printingStyleId);
        //    parameters.Add("ID_THOI_GIAN_GIAO", shipTimeId);
        //    parameters.Add("ID_NHOM_THANH_PHAM", finishedProductGroupId);
        //    parameters.Add("ID_THANH_PHAM", finishedProductIds);

        //    var GIA_GIAY_IN_RAW = RestDbExecutor.ExecuteScalar("SELECT DON_GIA FROM GIA_GIAY_IN_SPP WHERE ID_KHO_SAN_PHAM = @ID_KHO_SAN_PHAM AND ID_SO_LUONG_THANH_PHAM = @ID_SO_LUONG_THANH_PHAM AND ID_GIAY_IN = @ID_GIAY_IN", parameters);
        //    var GIA_CACH_IN_RAW = RestDbExecutor.ExecuteScalar("SELECT DON_GIA FROM GIA_CACH_IN_SPP WHERE ID_KHO_SAN_PHAM = @ID_KHO_SAN_PHAM AND ID_SO_LUONG_THANH_PHAM = @ID_SO_LUONG_THANH_PHAM AND ID_CACH_IN = @ID_CACH_IN", parameters);
        //    var GIA_THANH_PHAM_RAW = RestDbExecutor.ExecuteScalar(string.Format("SELECT SUM(DON_GIA) AS DON_GIA FROM GIA_THANH_PHAM_SPP WHERE ID_KHO_SAN_PHAM = @ID_KHO_SAN_PHAM AND ID_SO_LUONG_THANH_PHAM = @ID_SO_LUONG_THANH_PHAM AND ID_NHOM_THANH_PHAM = @ID_NHOM_THANH_PHAM AND ID_THANH_PHAM IN({0})", parameters["ID_THANH_PHAM"]), parameters);
        //    var PHAN_TRAM_GIA_RAW = RestDbExecutor.ExecuteScalar("SELECT PHAN_TRAM_GIA FROM GIA_THOI_GIAN_GIAO_SPP WHERE ID_KHO_SAN_PHAM = @ID_KHO_SAN_PHAM AND ID_SO_LUONG_THANH_PHAM = @ID_SO_LUONG_THANH_PHAM AND ID_THOI_GIAN_GIAO = @ID_THOI_GIAN_GIAO", parameters);

        //    var GIA_GIAY_IN = GIA_GIAY_IN_RAW != DBNull.Value ? Convert.ToDecimal(GIA_GIAY_IN_RAW) : 0;
        //    var GIA_CACH_IN = GIA_CACH_IN_RAW != DBNull.Value ? Convert.ToDecimal(GIA_CACH_IN_RAW) : 0;
        //    var GIA_THANH_PHAM = GIA_THANH_PHAM_RAW != DBNull.Value ? Convert.ToDecimal(GIA_THANH_PHAM_RAW) : 0;
        //    var PHAN_TRAM_GIA = PHAN_TRAM_GIA_RAW != DBNull.Value ? Convert.ToDecimal(PHAN_TRAM_GIA_RAW) : 0;

        //    var GIA_TONG1 = GIA_GIAY_IN + GIA_CACH_IN + GIA_THANH_PHAM;
        //    var GIA_THOI_GIAN = GIA_TONG1 * (PHAN_TRAM_GIA / 100);

        //    var GIA_TONG = GIA_TONG1 + GIA_THOI_GIAN;

        //    /**
        //     * Hỗ trợ CORS
        //     */

        //    AllowCORS();

        //    return GIA_TONG;
        //}

        public decimal? GetPriceOfFlatProduct(string sizeId, string quantityId, string paperId, string printingStyleId, string shipTimeId, string finishedProducts)
        {
            /**
             * Kiểm tra chứng thực
             */

            if (!AuthenticateViaAuthorizationHeader())
            {
                return null;
            }

            /**
             * Tính giá
             */

            var parameters = new Dictionary<string, object>();

            parameters.Add("ID_KHO_SAN_PHAM", sizeId);
            parameters.Add("ID_SO_LUONG_THANH_PHAM", quantityId);
            parameters.Add("ID_GIAY_IN", paperId);
            parameters.Add("ID_CACH_IN", printingStyleId);
            parameters.Add("ID_THOI_GIAN_GIAO", shipTimeId);
            parameters.Add("ID_NHOM_THANH_PHAM", finishedProducts);
            parameters.Add("ID_THANH_PHAM", finishedProducts);

            var GIA_GIAY_IN_RAW = RestDbExecutor.ExecuteScalar("SELECT DON_GIA FROM GIA_GIAY_IN_SPP WHERE ID_KHO_SAN_PHAM = @ID_KHO_SAN_PHAM AND ID_SO_LUONG_THANH_PHAM = @ID_SO_LUONG_THANH_PHAM AND ID_GIAY_IN = @ID_GIAY_IN", parameters);
            var GIA_CACH_IN_RAW = RestDbExecutor.ExecuteScalar("SELECT DON_GIA FROM GIA_CACH_IN_SPP WHERE ID_KHO_SAN_PHAM = @ID_KHO_SAN_PHAM AND ID_SO_LUONG_THANH_PHAM = @ID_SO_LUONG_THANH_PHAM AND ID_CACH_IN = @ID_CACH_IN", parameters);
            //--Dz- lay SKU SP
            var sku_SanPhamP = RestDbExecutor.ExecuteScalar("SELECT SKU_SAN_PHAM FROM KHO_SAN_PHAM_SPP WHERE ID = @ID_KHO_SAN_PHAM", parameters);
            //--dz-end
            var GIA_THANH_PHAM = (decimal)0;
            foreach (var finishedProductPair in finishedProducts.Split(','))
            {
                var pair = finishedProductPair.Split('~');
                var ID_NHOM_THANH_PHAM = pair[0];
                var ID_THANH_PHAM = pair[1];

                parameters["ID_NHOM_THANH_PHAM"] = ID_NHOM_THANH_PHAM;
                parameters["ID_THANH_PHAM"] = ID_THANH_PHAM;

                var GIA_THANH_PHAM_RAW = RestDbExecutor.ExecuteScalar("SELECT SUM(DON_GIA) AS DON_GIA FROM GIA_THANH_PHAM_SPP WHERE ID_KHO_SAN_PHAM = @ID_KHO_SAN_PHAM AND ID_SO_LUONG_THANH_PHAM = @ID_SO_LUONG_THANH_PHAM AND ID_NHOM_THANH_PHAM = @ID_NHOM_THANH_PHAM AND ID_THANH_PHAM = @ID_THANH_PHAM", parameters);
                GIA_THANH_PHAM += GIA_THANH_PHAM_RAW != DBNull.Value ? Convert.ToDecimal(GIA_THANH_PHAM_RAW) : 0;
            }

            var PHAN_TRAM_GIA_RAW = RestDbExecutor.ExecuteScalar("SELECT PHAN_TRAM_GIA FROM GIA_THOI_GIAN_GIAO_SPP WHERE ID_KHO_SAN_PHAM = @ID_KHO_SAN_PHAM AND ID_SO_LUONG_THANH_PHAM = @ID_SO_LUONG_THANH_PHAM AND ID_THOI_GIAN_GIAO = @ID_THOI_GIAN_GIAO", parameters);

            var GIA_GIAY_IN = GIA_GIAY_IN_RAW != DBNull.Value ? Convert.ToDecimal(GIA_GIAY_IN_RAW) : 0;
            var GIA_CACH_IN = GIA_CACH_IN_RAW != DBNull.Value ? Convert.ToDecimal(GIA_CACH_IN_RAW) : 0;
            var PHAN_TRAM_GIA = PHAN_TRAM_GIA_RAW != DBNull.Value ? Convert.ToDecimal(PHAN_TRAM_GIA_RAW) : 0;
            //Dũng tìm CM trong thành phẩm phẳng gắp ra
            var maCM = "";
            foreach (var finishedProductPair in finishedProducts.Split(','))
            {
                var pair = finishedProductPair.Split('~');
                //var ID_NHOM_THANH_PHAM = pair[0];
                var ID_THANH_PHAM = pair[1];
                
                //parameters["ID_NHOM_THANH_PHAM"] = ID_NHOM_THANH_PHAM;
                //1). Tìm tất cả ID nếu Id nào có MA_THANH_PHAM = bắt đầu 2 chữ = CM thì ngưng để tính cán màng
                
                parameters["ID_THANH_PHAM"] = ID_THANH_PHAM;
                var maThanhPham= (string)RestDbExecutor.ExecuteScalar("SELECT MA_THANH_PHAM FROM THANH_PHAM_SPP WHERE ID = @ID_THANH_PHAM", parameters);
                //var GIA_THANH_PHAM_RAW = RestDbExecutor.ExecuteScalar("SELECT SUM(DON_GIA) AS DON_GIA FROM GIA_THANH_PHAM_SPP WHERE ID_KHO_SAN_PHAM = @ID_KHO_SAN_PHAM AND ID_SO_LUONG_THANH_PHAM = @ID_SO_LUONG_THANH_PHAM AND ID_NHOM_THANH_PHAM = @ID_NHOM_THANH_PHAM AND ID_THANH_PHAM = @ID_THANH_PHAM", parameters);
                if (!string.IsNullOrEmpty(maThanhPham))
                {
                    if (maThanhPham.Substring(0,2) =="CM")
                    {
                        maCM = maThanhPham; //lấy
                        break;//Thoát khỏi foreach
                    }
                }                
            }
            if (!string.IsNullOrEmpty(maCM))//có cán màng thì tính tiền cán màng
            {               
                var cauhinhInPhang = new CauHinhTinhGiaPhang((string)sku_SanPhamP, int.Parse(sizeId), int.Parse(quantityId), int.Parse(paperId), int.Parse(printingStyleId));
                var giaCanMang = TinhToanPhang.GiaCanMang(cauhinhInPhang, maCM);
                GIA_THANH_PHAM += giaCanMang; //thêm vô
            }
            //Dzung kết thúc cán màng
            //-Dzung-Tinh toan gia. chỉ sửa đoạn này để lồng vô
            CauHinhTinhGiaPhang cauHinhInPhang = null;
            if (GIA_CACH_IN <= 0 || GIA_GIAY_IN <= 0)
                cauHinhInPhang = new CauHinhTinhGiaPhang((string)sku_SanPhamP, int.Parse(sizeId), int.Parse(quantityId), int.Parse(paperId), int.Parse(printingStyleId));           
            
            decimal giaIn = 0;
            decimal giaGiay = 0;
            ///A). Tính theo Dzung: xử lý giá in và giá giấy
            ///Mánh: trường hợp cài -1 thì chỗ dó 0 tính cả theo số lượng sẵn lẫn hàng loạt
            ///nếu để 0 thì tính số hàng loạt chứ không tính
            ///Trường hợp >0 thì dùng giá cài dặt
            int tmpIntCheck = (int)GIA_CACH_IN;
            switch (tmpIntCheck)
            {
                case - 1:
                    giaIn = 0;
                    break;
                case 0:
                    giaIn = TinhToanPhang.GiaIn(cauHinhInPhang);
                    break;
                default:
                    giaIn = GIA_CACH_IN;
                    break;

            }
            //Tiếp giấy dùng tmpGiaCachIn chung
            tmpIntCheck = (int)GIA_GIAY_IN;
            switch (tmpIntCheck)
            {
                case -1:
                    giaGiay = 0;
                    break;
                case 0:
                    giaGiay = TinhToanPhang.GiaGiay(cauHinhInPhang);
                    break;
                default:
                    giaGiay = GIA_GIAY_IN;
                    break;

            }
           ///Hết xử lý giá giấy và in thành công
           ///2. Xử lý cán màng (chưa nghĩ ra)
            var GIA_TONG1 = giaIn + giaGiay + GIA_THANH_PHAM;
            //--Dzung--end
            //var GIA_TONG1 = GIA_GIAY_IN + GIA_CACH_IN + GIA_THANH_PHAM;
            var GIA_THOI_GIAN = GIA_TONG1 * (PHAN_TRAM_GIA / 100);

            var GIA_TONG = GIA_TONG1 + GIA_THOI_GIAN;

            /**
             * Hỗ trợ CORS
             */

            AllowCORS();

            return GIA_TONG;
        }

        /// <summary>
        /// Lấy thông tin chi tiết của sản phẩm cuốn thông qua SKU
        /// </summary>
        /// <param name="productSku"></param>
        /// <returns></returns>
        public BookProductDetails GetBookProductDetails(string productSku)
        {
            var parameters = new Dictionary<string, object>();

            parameters.Add("SKU_SAN_PHAM", productSku);

            var productDetails = new BookProductDetails();

            var tblKHO_SAN_PHAM = RestDbExecutor.ExecuteQuery("SELECT * FROM KHO_SAN_PHAM_SPC WHERE SKU_SAN_PHAM = @SKU_SAN_PHAM ORDER BY THU_TU ASC", parameters);
            var tblBIA = RestDbExecutor.ExecuteQuery("SELECT * FROM BIA_SPC WHERE SKU_SAN_PHAM = @SKU_SAN_PHAM ORDER BY THU_TU ASC", parameters);
            var tblDONG_CUON = RestDbExecutor.ExecuteQuery("SELECT * FROM DONG_CUON_SPC WHERE SKU_SAN_PHAM = @SKU_SAN_PHAM ORDER BY THU_TU ASC", parameters);
            var tblSO_LUONG_CUON = RestDbExecutor.ExecuteQuery("SELECT * FROM SO_LUONG_CUON_SPC WHERE SKU_SAN_PHAM = @SKU_SAN_PHAM ORDER BY THU_TU ASC", parameters);
            var tblRUOT = RestDbExecutor.ExecuteQuery("SELECT * FROM RUOT_SPC WHERE SKU_SAN_PHAM = @SKU_SAN_PHAM ORDER BY THU_TU ASC", parameters);
            var tblGIAY_IN_RUOT = RestDbExecutor.ExecuteQuery("SELECT * FROM GIAY_IN_RUOT_SPC WHERE SKU_SAN_PHAM = @SKU_SAN_PHAM ORDER BY THU_TU ASC", parameters);
            var tblTHOI_GIAN_GIAO = RestDbExecutor.ExecuteQuery("SELECT * FROM THOI_GIAN_GIAO_SPC WHERE SKU_SAN_PHAM = @SKU_SAN_PHAM ORDER BY THU_TU ASC", parameters);

            productDetails.DANH_SACH_KHO_SAN_PHAM = new List<KHO_SAN_PHAM_SPC>();
            foreach (DataRow row in tblKHO_SAN_PHAM.Rows)
            {
                productDetails.DANH_SACH_KHO_SAN_PHAM.Add(new KHO_SAN_PHAM_SPC
                {
                    ID = (long)row["ID"],
                    TEN_KHO = (string)row["TEN_KHO"],
                    THU_TU = (long)row["THU_TU"],
                    SKU_SAN_PHAM = (string)row["SKU_SAN_PHAM"]
                });
            }

            productDetails.DANH_SACH_BIA = new List<BIA_SPC>();
            foreach (DataRow row in tblBIA.Rows)
            {
                productDetails.DANH_SACH_BIA.Add(new BIA_SPC
                {
                    ID = (long)row["ID"],
                    TEN_BIA = (string)row["TEN_BIA"],
                    THU_TU = (long)row["THU_TU"],
                    SKU_SAN_PHAM = (string)row["SKU_SAN_PHAM"]
                });
            }

            productDetails.DANH_SACH_DONG_CUON = new List<DONG_CUON_SPC>();
            foreach (DataRow row in tblDONG_CUON.Rows)
            {
                productDetails.DANH_SACH_DONG_CUON.Add(new DONG_CUON_SPC
                {
                    ID = (long)row["ID"],
                    TEN_DONG_CUON = (string)row["TEN_DONG_CUON"],
                    THU_TU = (long)row["THU_TU"],
                    SKU_SAN_PHAM = (string)row["SKU_SAN_PHAM"]
                });
            }

            productDetails.DANH_SACH_SO_LUONG_CUON = new List<SO_LUONG_CUON_SPC>();
            foreach (DataRow row in tblSO_LUONG_CUON.Rows)
            {
                productDetails.DANH_SACH_SO_LUONG_CUON.Add(new SO_LUONG_CUON_SPC
                {
                    ID = (long)row["ID"],
                    TEN_SO_LUONG = (string)row["TEN_SO_LUONG"],
                    THU_TU = (long)row["THU_TU"],
                    SKU_SAN_PHAM = (string)row["SKU_SAN_PHAM"]
                });
            }

            productDetails.DANH_SACH_RUOT = new List<RUOT_SPC>();
            foreach (DataRow row in tblRUOT.Rows)
            {
                productDetails.DANH_SACH_RUOT.Add(new RUOT_SPC
                {
                    ID = (long)row["ID"],
                    TEN_RUOT = (string)row["TEN_RUOT"],
                    THU_TU = (long)row["THU_TU"],
                    SKU_SAN_PHAM = (string)row["SKU_SAN_PHAM"]
                });
            }

            productDetails.DANH_SACH_GIAY_IN_RUOT = new List<GIAY_IN_RUOT_SPC>();
            foreach (DataRow row in tblGIAY_IN_RUOT.Rows)
            {
                productDetails.DANH_SACH_GIAY_IN_RUOT.Add(new GIAY_IN_RUOT_SPC
                {
                    ID = (long)row["ID"],
                    TEN_GIAY_IN = (string)row["TEN_GIAY_IN"],
                    THU_TU = (long)row["THU_TU"],
                    SKU_SAN_PHAM = (string)row["SKU_SAN_PHAM"]
                });
            }

            productDetails.DANH_SACH_THOI_GIAN_GIAO = new List<THOI_GIAN_GIAO_SPC>();
            foreach (DataRow row in tblTHOI_GIAN_GIAO.Rows)
            {
                productDetails.DANH_SACH_THOI_GIAN_GIAO.Add(new THOI_GIAN_GIAO_SPC
                {
                    ID = (long)row["ID"],
                    TEN_THOI_GIAN_GIAO = (string)row["TEN_THOI_GIAN_GIAO"],
                    THU_TU = (long)row["THU_TU"],
                    SKU_SAN_PHAM = (string)row["SKU_SAN_PHAM"]
                });
            }

            return productDetails;
        }

        /// <summary>
        /// Lấy giá sản phẩm cuốn
        /// </summary>
        /// <param name="sizeId">ID của bảng KHỔ SẢN PHẨM CUỐN</param>
        /// <param name="quantityId">ID của bảng SỐ LƯỢNG CUỐN</param>
        /// <param name="coverId">ID của bảng BÌA</param>
        /// <param name="bookStyleId">ID của bảng ĐÓNG CUỐN</param>
        /// <param name="blockId">ID của bảng RUỘT</param>
        /// <param name="paperId">ID của bảng GIẤY IN CUỐN</param>
        /// <param name="shipTimeId">ID của bảng THỜI GIAN GIAO</param>
        /// <returns></returns>
        public decimal? GetPriceOfBookProduct(string sizeId, string quantityId, string coverId, string bookStyleId, string blockId, string paperId, string shipTimeId)
        {
            /**
             * Kiểm tra chứng thực
             */

            if (!AuthenticateViaAuthorizationHeader())
            {
                return null;
            }

            /**
             * Tính giá
             */

            var parameters = new Dictionary<string, object>();

            parameters.Add("ID_KHO_SAN_PHAM", sizeId);
            parameters.Add("ID_SO_LUONG_CUON", quantityId);
            parameters.Add("ID_BIA", coverId);
            parameters.Add("ID_DONG_CUON", bookStyleId);
            parameters.Add("ID_RUOT", blockId);
            parameters.Add("ID_GIAY_IN_RUOT", paperId);
            parameters.Add("ID_THOI_GIAN_GIAO", shipTimeId);
            //--Dz- lay SKU SP
            var sku_SanPham = RestDbExecutor.ExecuteScalar("SELECT SKU_SAN_PHAM FROM BIA_SPC WHERE ID = @ID_BIA", parameters);
            //--dz-end
            var GIA_BIA_RAW = RestDbExecutor.ExecuteScalar("SELECT DON_GIA FROM GIA_BIA_SPC WHERE ID_KHO_SAN_PHAM = @ID_KHO_SAN_PHAM AND ID_SO_LUONG_CUON = @ID_SO_LUONG_CUON AND ID_BIA = @ID_BIA", parameters);
            var GIA_DONG_CUON_RAW = RestDbExecutor.ExecuteScalar("SELECT DON_GIA FROM GIA_DONG_CUON_SPC WHERE ID_KHO_SAN_PHAM = @ID_KHO_SAN_PHAM AND ID_SO_LUONG_CUON = @ID_SO_LUONG_CUON AND ID_DONG_CUON = @ID_DONG_CUON", parameters);
            var GIA_IN_RUOT_RAW = RestDbExecutor.ExecuteScalar("SELECT DON_GIA FROM GIA_IN_RUOT_SPC WHERE ID_KHO_SAN_PHAM = @ID_KHO_SAN_PHAM AND ID_SO_LUONG_CUON = @ID_SO_LUONG_CUON AND ID_RUOT = @ID_RUOT", parameters);
            var GIAY_GIAY_RUOT_RAW = RestDbExecutor.ExecuteScalar("SELECT DON_GIA FROM GIA_GIAY_RUOT_SPC WHERE ID_KHO_SAN_PHAM = @ID_KHO_SAN_PHAM AND ID_SO_LUONG_CUON = @ID_SO_LUONG_CUON AND ID_RUOT = @ID_RUOT AND ID_GIAY_IN_RUOT = @ID_GIAY_IN_RUOT", parameters);
            var PHAN_TRAM_GIA_RAW = RestDbExecutor.ExecuteScalar("SELECT PHAN_TRAM_GIA FROM GIA_THOI_GIAN_GIAO_SPC WHERE ID_KHO_SAN_PHAM = @ID_KHO_SAN_PHAM AND ID_SO_LUONG_CUON = @ID_SO_LUONG_CUON AND ID_THOI_GIAN_GIAO = @ID_THOI_GIAN_GIAO", parameters);

            var GIA_BIA = GIA_BIA_RAW != DBNull.Value ? Convert.ToDecimal(GIA_BIA_RAW) : 0;
            var GIA_DONG_CUON = GIA_IN_RUOT_RAW != DBNull.Value ? Convert.ToDecimal(GIA_DONG_CUON_RAW) : 0;
            var GIA_IN_RUOT = GIA_IN_RUOT_RAW != DBNull.Value ? Convert.ToDecimal(GIA_IN_RUOT_RAW) : 0;
            var GIAY_GIAY_RUOT = GIAY_GIAY_RUOT_RAW != DBNull.Value ? Convert.ToDecimal(GIAY_GIAY_RUOT_RAW) : 0;
            var PHAN_TRAM_GIA = PHAN_TRAM_GIA_RAW != DBNull.Value ? Convert.ToDecimal(PHAN_TRAM_GIA_RAW) : 0;

            //-Dzung-Tinh toan gia. chỉ sửa đoạn này để lồng vô
            var cauHinhInCuon = new CauHinhTinhGiaCuon((string)sku_SanPham, int.Parse(sizeId), int.Parse(quantityId), int.Parse(blockId), int.Parse(paperId));
            var giaInGiayCanMang = TinhToanCuon.GiaIn(cauHinhInCuon) + TinhToanCuon.GiaGiayRuot(cauHinhInCuon) + TinhToanCuon.GiaCanMang(cauHinhInCuon);
            //--Dzung--end

            //var GIA_TONG1 = GIA_BIA + GIA_DONG_CUON + GIA_IN_RUOT + GIAY_GIAY_RUOT;
            var GIA_TONG1 = GIA_BIA + GIA_DONG_CUON + giaInGiayCanMang;
            var GIA_THOI_GIAN = GIA_TONG1 * (PHAN_TRAM_GIA / 100);

            var GIA_TONG = GIA_TONG1 + GIA_THOI_GIAN;
          

            /**
             * Hỗ trợ CORS
             */

            AllowCORS();

            return GIA_TONG;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Kiểm tra thông tin chứng thực từ client thông qua HTTP HEADER Authorization
        /// </summary>
        /// <returns>true nếu thông tin chứng thực đúng, false nếu sai</returns>
        private bool AuthenticateViaAuthorizationHeader()
        {
            var authToken = ConfigurationManager.AppSettings.Get("AuthToken");
            var headers = WebOperationContext.Current.IncomingRequest.Headers;
            var authHeaderName = "Authorization";

            if (!headers.AllKeys.Contains(authHeaderName) || headers[authHeaderName] != authToken)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Hỗ trợ CORS cho client từ domain khác
        /// </summary>
        private void AllowCORS()
        {
            //WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            //if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            //{
            //    WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Methods", "OPTIONS, GET, HEAD, POST");
            //    WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Headers", "access-control-allow-headers, authorization, content-type, mode");
            //}
        }

        #endregion
    }
}
