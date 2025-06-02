using BUS;
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
    public class QuanLyService : IQuanLyService
    {
        public bool DangNhap(string username, string password)
        {
            return NguoiDungBUS.Instance.DangNhap(username, password);
        }

        public NguoiDungDTO GetThongTinNguoiDung(string username, string password)
        {
            NguoiDungBUS.Instance.DangNhap(username, password);
            return NguoiDungBUS.Instance.NguoiDung;
        }

        public bool ThemLopHoc(LopDTO lop)
        {
            // Lấy giới hạn sĩ số từ CSDL
            var dt = QuyDinhDAO.Instance.LaySiSoQuyDinh();
            int siSoMin = Convert.ToInt32(dt.Rows[0]["SiSoCanDuoi"]);
            int siSoMax = Convert.ToInt32(dt.Rows[0]["SiSoCanTren"]);

            if (lop.siSo < siSoMin || lop.siSo > siSoMax)
                return false;

            LopBUS.Instance.ThemLop(lop);
            return true;
        }

        public List<LopDTO> TimLopTheoMa(string maLop)
        {
            DataTable dt = LopDAO.Instance.TimTheoMa(maLop);
            return ConvertToList(dt);
        }

        private List<LopDTO> ConvertToList(DataTable dt)
        {
            return dt.AsEnumerable().Select(row => new LopDTO
            {
                MaLop = row["MaLop"].ToString(),
                TenLop = row["TenLop"].ToString(),
                MaKhoiLop = row["MaKhoiLop"].ToString(),
                MaNamHoc = row["MaNamHoc"].ToString(),
                SiSo = Convert.ToInt32(row["SiSo"]),
                MaGiaoVien = row["MaGiaoVien"].ToString()
            }).ToList();
        }


        public List<LopDTO> TimLopTheoTen(string maLop)
        {
            DataTable dt = LopDAO.Instance.TimTheoTen(maLop);
            return ConvertToList(dt);
        }
        public bool ThemHocSinh(HocSinhDTO hocSinh)
        {
            
            if (!QuyDinhBUS.Instance.KiemTraDoTuoi(hocSinh.NgaySinh))
            {
                return false; // Không hợp lệ
            }

            // Thêm học sinh vào DB nếu hợp lệ
            HocSinhBUS.Instance.ThemHocSinh(hocSinh);
            return true;
        }

        public List<HocSinhDTO> TimHocSinhTheoMa(string maHS)
        {
            var dt = HocSinhDAO.Instance.TimTheoMa(maHS);
            return ConvertToListHocSinh(dt);
        }

        public List<HocSinhDTO> TimHocSinhTheoTen(string tenHS)
        {
            var dt = HocSinhDAO.Instance.TimTheoTen(tenHS);
            return ConvertToListHocSinh(dt);
        }

        // Helper method để chuyển từ DataTable sang List<HocSinhDTO>
        private List<HocSinhDTO> ConvertToListHocSinh(DataTable dt)
        {
            return dt.AsEnumerable().Select(row => new HocSinhDTO
            {
                MaHocSinh = row["MaHocSinh"].ToString(),
                hoTen = row["HoTen"].ToString(),
                GioiTinh = Convert.ToBoolean(row["GioiTinh"]),
                NgaySinh = Convert.ToDateTime(row["NgaySinh"]),
                DiaChi = row["DiaChi"].ToString(),
                maDanToc = row["MaDanToc"].ToString(),
                maTonGiao = row["MaTonGiao"].ToString(),
                HoTenCha = row["HoTenCha"].ToString(),
                maNgheCha = row["MaNgheCha"].ToString(),
                HoTenMe = row["HoTenMe"].ToString(),
                maNgheMe = row["MaNgheMe"].ToString(),
                Email = row["Email"].ToString()
            }).ToList();
        }
        public List<GiaoVienDTO> TimGiaoVienTheoMa(string maGV)
        {

            var dt = GiaoVienDAO.Instance.TimTheoMa(maGV);
            return ConvertToListGiaoVien(dt);
        }
        private List<GiaoVienDTO> ConvertToListGiaoVien(DataTable dt)
        {
            return dt.AsEnumerable().Select(row => new GiaoVienDTO(
                row["MaGiaoVien"].ToString(),
                row["TenGiaoVien"].ToString(),
                row["DiaChi"].ToString(),
                row["DienThoai"].ToString(),
                row["ChuyenMon"].ToString()
            )).ToList();
        }


        public List<GiaoVienDTO> TimGiaoVienTheoTen(string tenGV)
        {
            var dt = GiaoVienDAO.Instance.TimTheoTen(tenGV);
            return ConvertToListGiaoVien(dt);
        }
        public bool ThemGiaoVien(GiaoVienDTO giaoVien)
        {
            // Kiểm tra các trường bắt buộc
            if (string.IsNullOrWhiteSpace(giaoVien.TenGiaoVien) ||
                string.IsNullOrWhiteSpace(giaoVien.DiaChi) ||
                string.IsNullOrWhiteSpace(giaoVien.DienThoai) ||
                string.IsNullOrWhiteSpace(giaoVien.chuyenMon))
            {
                return false;
            }

            GiaoVienBUS.Instance.ThemGiaoVien(giaoVien);
            return true;
        }

        public bool DangNhapHocSinh(string maHS, string matKhau)
        {
            var dataTable = TaiKhoanHocSinhBUS.Instance.DangNhap(maHS, matKhau);
            if (dataTable.Rows.Count == 0) return false;
            return true;
        }

        public TaiKhoanHocSinhDTO LayTaiKhoanHocSinh(string maHS)
        {
            return TaiKhoanHocSinhBUS.Instance.LayTaiKhoan(maHS);
        }


        public void LuuDiem(List<DiemDTO> danhSachDiem,
             string maLop, string maNamHoc, string maMonHoc, string maHocKy)
        {
            // Lấy danh sách học sinh duy nhất
            var hocSinhIds = danhSachDiem.Select(d => d.MaHocSinh).Distinct().ToList();

            for (int index = 0; index < hocSinhIds.Count; index++)
            {
                string maHocSinh = hocSinhIds[index];

                // 1. Xóa điểm cũ theo STT tương ứng
               
                var diemCuaHS = danhSachDiem.Where(d => d.MaHocSinh == maHocSinh);
                foreach (var diemreset in diemCuaHS)
                {
                    DiemBUS.Instance.XoaDiemDaLuu(diemreset);

                }
                // 2. Thêm điểm mới của học sinh này
                foreach (var diem in diemCuaHS)
                {
                    if (!string.IsNullOrWhiteSpace(diem.DiemSo.ToString()) &&
                        QuyDinhBUS.Instance.KiemTraDiem(diem.DiemSo.ToString()))
                    {
                        DiemBUS.Instance.ThemDiem(diem);
                    }
                }

                // 3. Lưu kết quả cho học sinh này
                KQHSMonHocBUS.Instance.LuuKetQua(maHocSinh, maLop, maNamHoc, maMonHoc, maHocKy);
                KQHSCaNamBUS.Instance.LuuKetQua(maHocSinh, maLop, maNamHoc);
                KQLHMonHocBUS.Instance.LuuKetQua(maLop, maNamHoc, maMonHoc, maHocKy);
                KQLHHocKyBUS.Instance.LuuKetQua(maLop, maNamHoc, maHocKy);
            }
        }
        public bool LuuPhanCong(DataTable dtPhanCong)
        {
            string[] colNames = { "colMaNamHoc", "colMaLop", "colMaMonHoc", "colMaGiaoVien" };

            PhanCongBUS.Instance.CapNhatPhanCong(dtPhanCong);
            return true;
        }
        public bool ThemPhanCong(PhanCongDTO phanCong)
        {
            if (phanCong == null ||
                string.IsNullOrWhiteSpace(phanCong.MaNamHoc) ||
                string.IsNullOrWhiteSpace(phanCong.MaLop) ||
                string.IsNullOrWhiteSpace(phanCong.MaMonHoc) ||
                string.IsNullOrWhiteSpace(phanCong.MaGiaoVien))
            {
                return false; // dữ liệu không hợp lệ
            }

            PhanCongBUS.Instance.ThemPhanCong(phanCong);
            return true;
        }
        public string ThemPhanCongCheck(PhanCongDTO phanCong)
        {
            if (phanCong == null ||
                string.IsNullOrWhiteSpace(phanCong.MaNamHoc) ||
                string.IsNullOrWhiteSpace(phanCong.MaLop) ||
                string.IsNullOrWhiteSpace(phanCong.MaMonHoc) ||
                string.IsNullOrWhiteSpace(phanCong.MaGiaoVien))
            {
                return "Dữ liệu không hợp lệ!";
            }

            return PhanCongBUS.Instance.ThemPhanCongCheck(phanCong);
        }
        public static class KiemTraTruocKhiLuu
        {
            public static bool KiemTraDataTable(DataTable dt, string[] requiredCols)
            {
                foreach (DataRow row in dt.Rows)
                {
                    foreach (string col in requiredCols)
                    {
                        if (row[col] == DBNull.Value || string.IsNullOrWhiteSpace(row[col]?.ToString()))
                            return false;
                    }
                }
                return true;
            }
        }

    }
}