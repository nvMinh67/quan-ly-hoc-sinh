using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    [DataContract]
    public  class TaiKhoanHocSinhDTO
    {
        [DataMember] public string MaHocSinh { get; set; }
        [DataMember] public string MatKhau { get; set; }

        public TaiKhoanHocSinhDTO(string maHocSinh, string matKhau)
        {
            MaHocSinh = maHocSinh;
            MatKhau = matKhau;
        }
    }
}
