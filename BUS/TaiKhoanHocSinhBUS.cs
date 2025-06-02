using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class TaiKhoanHocSinhBUS
    {
        private static TaiKhoanHocSinhBUS instance;
        public static TaiKhoanHocSinhBUS Instance => instance ?? (instance = new TaiKhoanHocSinhBUS());

        public DataTable DangNhap(string maHS, string matKhau)
        {
            return TaiKhoanHocSinhDAO.Instance.KiemTraDangNhap(maHS, matKhau);
        }

        public TaiKhoanHocSinhDTO LayTaiKhoan(string maHS)
        {
            return TaiKhoanHocSinhDAO.Instance.LayTaiKhoan(maHS);
        }
    }
}
