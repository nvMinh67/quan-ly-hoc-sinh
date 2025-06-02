using System.Runtime.Serialization;

namespace DTO
{

    [DataContract]
    public class LoaiNguoiDungDTO
    {
        [DataMember]
        private string maLoai;

        [DataMember]
        private string tenLoai;

        public LoaiNguoiDungDTO() { }
        public string MaLoai { get => maLoai; set => maLoai = value; }
        public string TenLoai { get => tenLoai; set => tenLoai = value; }
    }
}
