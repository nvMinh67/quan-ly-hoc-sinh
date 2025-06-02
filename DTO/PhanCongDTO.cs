using System.Runtime.Serialization;

namespace DTO
{
    [DataContract]
    public class PhanCongDTO
    {
        private string maNamHoc;
        private string maLop;
        private string maMonHoc;
        private string maGiaoVien;

        public PhanCongDTO(string maNamHoc, string maLop, string maMonHoc, string maGiaoVien)
        {
            this.maNamHoc = maNamHoc;
            this.maLop = maLop;
            this.maMonHoc = maMonHoc;
            this.maGiaoVien = maGiaoVien;
        }

        [DataMember]
        public string MaNamHoc { get => maNamHoc; set => maNamHoc = value; }
        [DataMember]
        public string MaLop { get => maLop; set => maLop = value; }
        [DataMember]
        public string MaMonHoc { get => maMonHoc; set => maMonHoc = value; }
        [DataMember]
        public string MaGiaoVien { get => maGiaoVien; set => maGiaoVien = value; }
    }
}
