using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RestService13871
{
    /// <summary>
    /// Định nghĩa các Rest APIs lấy dữ liệu
    /// </summary>
    [ServiceContract]
    public interface IRestService
    {
        /// <summary>
        /// Lấy các dữ liệu liên quan về giá của một sản phẩm phẳng dựa vào SKU của nó
        /// </summary>
        /// <param name="productId">ID của sản phẩm</param>
        /// <returns></returns>
        [WebGet(UriTemplate = "Products/Flat/{ProductSku}/Details")]
        FlatProductDetails GetFlatProductDetails(string productSku);

        /// <summary>
        /// Lấy giá sản phẩm phẳng
        /// </summary>
        /// <param name="sizeId"></param>
        /// <param name="quantityId"></param>
        /// <param name="paperId"></param>
        /// <param name="printingStyleId"></param>
        /// <param name="shipTimeId"></param>
        /// <param name="finishedProductGroupId"></param>
        /// <param name="finishedProductIds"></param>
        /// <returns></returns>
        //[WebGet(UriTemplate = "Price/Flat/{SizeId}/{QuantityId}/{PaperId}/{PrintingStyleId}/{ShipTimeId}/{FinishedProductGroupId}/{FinishedProductIds}")]
        //decimal? GetPriceOfFlatProduct(string sizeId, string quantityId, string paperId, string printingStyleId, string shipTimeId, string finishedProductGroupId, string finishedProductIds);

        [WebGet(UriTemplate = "Price/Flat/{SizeId}/{QuantityId}/{PaperId}/{PrintingStyleId}/{ShipTimeId}/{FinishedProducts}")]
        decimal? GetPriceOfFlatProduct(string sizeId, string quantityId, string paperId, string printingStyleId, string shipTimeId, string finishedProducts);

        /// <summary>
        /// Lấy các dữ liệu liên quan về giá của một sản phẩm cuốn dựa vào SKU của nó
        /// </summary>
        /// <param name="productId">ID của sản phẩm</param>
        /// <returns></returns>
        [WebGet(UriTemplate = "Products/Book/{ProductSku}/Details")]
        BookProductDetails GetBookProductDetails(string productSku);

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
        [WebGet(UriTemplate = "Price/Book/{SizeId}/{QuantityId}/{CoverId}/{BookStyleId}/{BlockId}/{PaperId}/{ShipTimeId}")]
        decimal? GetPriceOfBookProduct(string sizeId, string quantityId, string coverId, string bookStyleId, string blockId, string paperId, string shipTimeId);
    }
}
