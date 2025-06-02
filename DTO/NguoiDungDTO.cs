using System;
using System.Runtime.Serialization;

namespace DTO
{
    [DataContract]
    public class NguoiDungDTO
    {
        // KHÔNG đánh dấu field
        [DataMember]
        private string maNguoiDung;

        [DataMember]
        private LoaiNguoiDungDTO loaiNguoiDung;

        [DataMember]
        private string tenNguoiDung;

        [DataMember]
        private string tenDangNhap;

        [DataMember]
        private string matKhau;

        // Constructor
        public NguoiDungDTO() { }

        public NguoiDungDTO(string ma, LoaiNguoiDungDTO loai, string ten, string user, string pass)
        {
            MaNguoiDung = ma;
            LoaiNguoiDung = loai;
            TenNguoiDung = ten;
            TenDangNhap = user;
            MatKhau = pass;
        }

        // Đặt DataMember lên PROPERTY PUBLIC
        [DataMember]
        public string MaNguoiDung { get => maNguoiDung; set => maNguoiDung = value; }

        [DataMember]
        public LoaiNguoiDungDTO LoaiNguoiDung { get => loaiNguoiDung; set => loaiNguoiDung = value; }

        [DataMember]
        public string TenNguoiDung { get => tenNguoiDung; set => tenNguoiDung = value; }

        [DataMember]
        public string TenDangNhap { get => tenDangNhap; set => tenDangNhap = value; }

        [DataMember]
        public string MatKhau { get => matKhau; set => matKhau = value; }
    }
}
