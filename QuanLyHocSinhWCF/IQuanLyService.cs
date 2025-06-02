using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
namespace QuanLyHocSinhWCF
{
    [ServiceContract]
    public interface IQuanLyService
    {
        [OperationContract]
        bool DangNhap(string username, string password);

        [OperationContract]
        NguoiDungDTO GetThongTinNguoiDung(string username, string password);

        [OperationContract]
        bool ThemLopHoc(LopDTO lop);

        [OperationContract]
        List<LopDTO> TimLopTheoMa(string maLop);

        [OperationContract]
        List<LopDTO> TimLopTheoTen(string tenLop);

        [OperationContract]
        bool ThemHocSinh(HocSinhDTO hocSinh);

        [OperationContract]
        List<HocSinhDTO> TimHocSinhTheoMa(string maHS);

        [OperationContract]
        List<HocSinhDTO> TimHocSinhTheoTen(string tenHS);

        [OperationContract]
        bool ThemGiaoVien(GiaoVienDTO giaoVien);

        [OperationContract]
        List<GiaoVienDTO> TimGiaoVienTheoMa(string maGV);

        [OperationContract]
        List<GiaoVienDTO> TimGiaoVienTheoTen(string tenGV);

        [OperationContract]
        bool DangNhapHocSinh(string maHS, string matKhau);

        [OperationContract]
        TaiKhoanHocSinhDTO LayTaiKhoanHocSinh(string maHS);

        [OperationContract]
        [ServiceKnownType(typeof(List<int>))]  // Cho phép WCF hiểu List<int>
        void LuuDiem(List<DiemDTO> danhSachDiem,
                 string maLop, string maNamHoc, string maMonHoc, string maHocKy);

        [OperationContract]
        bool LuuPhanCong(DataTable dtPhanCong);
        [OperationContract]
        bool ThemPhanCong(PhanCongDTO phanCong);

        [OperationContract]
        string ThemPhanCongCheck(PhanCongDTO phanCong);
    }

}
