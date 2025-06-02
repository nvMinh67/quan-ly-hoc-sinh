using System.Runtime.Serialization;

namespace DTO
{
    [DataContract]
    public class LopDTO
    {
        [DataMember] public string maLop { get; set; }
        [DataMember] public string tenLop { get; set; }
        [DataMember] public string maKhoiLop { get; set; }
        [DataMember] public string maNamHoc { get; set; }
        [DataMember] public int siSo { get; set; }
        [DataMember] public string maGiaoVien { get; set; }

        public LopDTO() { }
        public LopDTO(string maLop, string tenLop, string maKhoiLop, string maNamHoc, int siSo, string maGiaoVien)
        {
            this.maLop = maLop;
            this.tenLop = tenLop;
            this.maKhoiLop = maKhoiLop;
            this.maNamHoc = maNamHoc;
            this.siSo = siSo;
            this.maGiaoVien = maGiaoVien;
        }

        public string MaLop { get => maLop; set => maLop = value; }
        public string TenLop { get => tenLop; set => tenLop = value; }
        public string MaKhoiLop { get => maKhoiLop; set => maKhoiLop = value; }
        public string MaNamHoc { get => maNamHoc; set => maNamHoc = value; }
        public int SiSo { get => siSo; set => siSo = value; }
        public string MaGiaoVien { get => maGiaoVien; set => maGiaoVien = value; }
    }
}
