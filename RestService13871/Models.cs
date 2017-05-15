using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RestService13871
{
    #region Sản phẩm phẳng

    /// <summary>
    /// Tương ứng với bảng KHỔ SẢN PHẨM, các property trong lớp này mặc định trùng khớp với các cột trong cơ sở dữ liệu. Nếu khác sẽ được giải thích ở các property đó
    /// </summary>
    [DataContract]
    public class KHO_SAN_PHAM_SPP
    {
        [DataMember]
        public long ID { get; set; }

        [DataMember]
        public string TEN_KHO { get; set; }

        [DataMember]
        public long THU_TU { get; set; }

        [DataMember]
        public string SKU_SAN_PHAM { get; set; }
    }

    /// <summary>
    /// Tương ứng với bảng SỐ LƯỢNG THÀNH PHẨM, các property trong lớp này mặc định trùng khớp với các cột trong cơ sở dữ liệu. Nếu khác sẽ được giải thích ở các property đó
    /// </summary>
    [DataContract]
    public class SO_LUONG_THANH_PHAM_SPP
    {
        [DataMember]
        public long ID { get; set; }

        [DataMember]
        public string TEN_SO_LUONG { get; set; }

        [DataMember]
        public long THU_TU { get; set; }

        [DataMember]
        public string SKU_SAN_PHAM { get; set; }
    }

    /// <summary>
    /// Tương ứng với bảng NHÓM THÀNH PHẨM, các property trong lớp này mặc định trùng khớp với các cột trong cơ sở dữ liệu. Nếu khác sẽ được giải thích ở các property đó
    /// </summary>
    [DataContract]
    public class NHOM_THANH_PHAM_SPP
    {
        [DataMember]
        public long ID { get; set; }

        [DataMember]
        public string TEN_NHOM { get; set; }

        [DataMember]
        public long THU_TU { get; set; }

        [DataMember]
        public string SKU_SAN_PHAM { get; set; }

        [DataMember]
        public List<THANH_PHAM_SPP> DANH_SACH_THANH_PHAM;
    }

    /// <summary>
    /// Tương ứng với bảng THÀNH PHẨM, các property trong lớp này mặc định trùng khớp với các cột trong cơ sở dữ liệu. Nếu khác sẽ được giải thích ở các property đó
    /// </summary>
    [DataContract]
    public class THANH_PHAM_SPP
    {
        [DataMember]
        public long ID { get; set; }

        [DataMember]
        public string TEN_THANH_PHAM { get; set; }

        [DataMember]
        public long ID_NHOM_THANH_PHAM { get; set; }

        [DataMember]
        public long THU_TU { get; set; }
    }

    /// <summary>
    /// Tương ứng với bảng GIẤY IN, các property trong lớp này mặc định trùng khớp với các cột trong cơ sở dữ liệu. Nếu khác sẽ được giải thích ở các property đó
    /// </summary>
    [DataContract]
    public class GIAY_IN_SPP
    {
        [DataMember]
        public long ID { get; set; }

        [DataMember]
        public string TEN_GIAY_IN { get; set; }

        [DataMember]
        public long THU_TU { get; set; }

        [DataMember]
        public string SKU_SAN_PHAM { get; set; }
    }

    /// <summary>
    /// Tương ứng với bảng CÁCH IN, các property trong lớp này mặc định trùng khớp với các cột trong cơ sở dữ liệu. Nếu khác sẽ được giải thích ở các property đó
    /// </summary>
    [DataContract]
    public class CACH_IN_SPP
    {
        [DataMember]
        public long ID { get; set; }

        [DataMember]
        public string TEN_CACH_IN { get; set; }

        [DataMember]
        public long THU_TU { get; set; }

        [DataMember]
        public string SKU_SAN_PHAM { get; set; }
    }

    /// <summary>
    /// Tương ứng với bảng THỜI GIAN GIAO, các property trong lớp này mặc định trùng khớp với các cột trong cơ sở dữ liệu. Nếu khác sẽ được giải thích ở các property đó
    /// </summary>
    [DataContract]
    public class THOI_GIAN_GIAO_SPP
    {
        [DataMember]
        public long ID { get; set; }

        [DataMember]
        public string TEN_THOI_GIAN_GIAO { get; set; }

        [DataMember]
        public long THU_TU { get; set; }

        [DataMember]
        public string SKU_SAN_PHAM { get; set; }
    }

    /// <summary>
    /// Thông tin chi tiết về sản phẩm phẳng
    /// </summary>
    [DataContract]
    public class FlatProductDetails
    {
        [DataMember]
        public string SKU_SAN_PHAM { get; set; }

        [DataMember]
        public List<KHO_SAN_PHAM_SPP> DANH_SACH_KHO_SAN_PHAM { get; set; }

        [DataMember]
        public List<SO_LUONG_THANH_PHAM_SPP> DANH_SACH_SO_LUONG_THANH_PHAM { get; set; }

        [DataMember]
        public List<GIAY_IN_SPP> DANH_SACH_GIAY_IN { get; set; }

        [DataMember]
        public List<CACH_IN_SPP> DANH_SACH_CACH_IN { get; set; }

        [DataMember]
        public List<NHOM_THANH_PHAM_SPP> DANH_SACH_NHOM_THANH_PHAM { get; set; }

        [DataMember]
        public List<THOI_GIAN_GIAO_SPP> DANH_SACH_THOI_GIAN_GIAO { get; set; }
    }

    #endregion

    #region Sản phẩm cuốn

    /// <summary>
    /// Tương ứng với bảng KHỔ SẢN PHẨM, các property trong lớp này mặc định trùng khớp với các cột trong cơ sở dữ liệu. Nếu khác sẽ được giải thích ở các property đó
    /// </summary>
    [DataContract]
    public class KHO_SAN_PHAM_SPC
    {
        [DataMember]
        public long ID { get; set; }

        [DataMember]
        public string TEN_KHO { get; set; }

        [DataMember]
        public long THU_TU { get; set; }

        [DataMember]
        public string SKU_SAN_PHAM { get; set; }
    }

    /// <summary>
    /// Tương ứng với bảng SỐ LƯỢNG CUỐN, các property trong lớp này mặc định trùng khớp với các cột trong cơ sở dữ liệu. Nếu khác sẽ được giải thích ở các property đó
    /// </summary>
    [DataContract]
    public class SO_LUONG_CUON_SPC
    {
        [DataMember]
        public long ID { get; set; }

        [DataMember]
        public string TEN_SO_LUONG { get; set; }

        [DataMember]
        public long THU_TU { get; set; }

        [DataMember]
        public string SKU_SAN_PHAM { get; set; }
    }

    /// <summary>
    /// Tương ứng với bảng BÌA, các property trong lớp này mặc định trùng khớp với các cột trong cơ sở dữ liệu. Nếu khác sẽ được giải thích ở các property đó
    /// </summary>
    [DataContract]
    public class BIA_SPC
    {
        [DataMember]
        public long ID { get; set; }

        [DataMember]
        public string TEN_BIA { get; set; }

        [DataMember]
        public long THU_TU { get; set; }

        [DataMember]
        public string SKU_SAN_PHAM { get; set; }
    }

    /// <summary>
    /// Tương ứng với bảng ĐÓNG CUỐN, các property trong lớp này mặc định trùng khớp với các cột trong cơ sở dữ liệu. Nếu khác sẽ được giải thích ở các property đó
    /// </summary>
    [DataContract]
    public class DONG_CUON_SPC
    {
        [DataMember]
        public long ID { get; set; }

        [DataMember]
        public string TEN_DONG_CUON { get; set; }

        [DataMember]
        public long THU_TU { get; set; }

        [DataMember]
        public string SKU_SAN_PHAM { get; set; }
    }

    /// <summary>
    /// Tương ứng với bảng RUỘT, các property trong lớp này mặc định trùng khớp với các cột trong cơ sở dữ liệu. Nếu khác sẽ được giải thích ở các property đó
    /// </summary>
    [DataContract]
    public class RUOT_SPC
    {
        [DataMember]
        public long ID { get; set; }

        [DataMember]
        public string TEN_RUOT { get; set; }

        [DataMember]
        public long THU_TU { get; set; }

        [DataMember]
        public string SKU_SAN_PHAM { get; set; }
    }

    /// <summary>
    /// Tương ứng với bảng GIẤY IN RUỘT, các property trong lớp này mặc định trùng khớp với các cột trong cơ sở dữ liệu. Nếu khác sẽ được giải thích ở các property đó
    /// </summary>
    [DataContract]
    public class GIAY_IN_RUOT_SPC
    {
        [DataMember]
        public long ID { get; set; }

        [DataMember]
        public string TEN_GIAY_IN { get; set; }

        [DataMember]
        public long THU_TU { get; set; }

        [DataMember]
        public string SKU_SAN_PHAM { get; set; }
    }

    /// <summary>
    /// Tương ứng với bảng THỜI GIAN GIAO, các property trong lớp này mặc định trùng khớp với các cột trong cơ sở dữ liệu. Nếu khác sẽ được giải thích ở các property đó
    /// </summary>
    [DataContract]
    public class THOI_GIAN_GIAO_SPC
    {
        [DataMember]
        public long ID { get; set; }

        [DataMember]
        public string TEN_THOI_GIAN_GIAO { get; set; }

        [DataMember]
        public long THU_TU { get; set; }

        [DataMember]
        public string SKU_SAN_PHAM { get; set; }
    }

    /// <summary>
    /// Thông tin chi tiết về sản phẩm cuốn
    /// </summary>
    [DataContract]
    public class BookProductDetails
    {
        [DataMember]
        public List<KHO_SAN_PHAM_SPC> DANH_SACH_KHO_SAN_PHAM { get; set; }

        [DataMember]
        public List<BIA_SPC> DANH_SACH_BIA { get; set; }

        [DataMember]
        public List<DONG_CUON_SPC> DANH_SACH_DONG_CUON { get; set; }

        [DataMember]
        public List<SO_LUONG_CUON_SPC> DANH_SACH_SO_LUONG_CUON { get; set; }

        [DataMember]
        public List<RUOT_SPC> DANH_SACH_RUOT { get; set; }

        [DataMember]
        public List<GIAY_IN_RUOT_SPC> DANH_SACH_GIAY_IN_RUOT { get; set; }

        [DataMember]
        public List<THOI_GIAN_GIAO_SPC> DANH_SACH_THOI_GIAN_GIAO { get; set; }
    }

    #endregion
}
