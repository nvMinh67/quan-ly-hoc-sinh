using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DAO
{
    public class TaiKhoanHocSinhDAO
    {
        private static TaiKhoanHocSinhDAO instance;
        public static TaiKhoanHocSinhDAO Instance => instance ?? (instance = new TaiKhoanHocSinhDAO());

        public DataTable KiemTraDangNhap(string maHS, string matKhau)
        {
            string query = "SELECT COUNT(*) FROM TaiKhoanHocSinh WHERE Email = @maHS AND password = @matKhau";
            object[] parameters = new object[] { maHS, matKhau };
            return DataProvider.Instance.ExecuteQuery(query, parameters);
        }

        public TaiKhoanHocSinhDTO LayTaiKhoan(string maHS)
        {
            string query = "SELECT * FROM TaiKhoanHocSinh WHERE MaHocSinh = @maHS"; 
            SqlParameter[] parameters = { new SqlParameter("@maHS", maHS) };
            DataTable dt = DataProvider.Instance.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new TaiKhoanHocSinhDTO(row["MaHocSinh"].ToString(), row["MatKhau"].ToString());
            }
            return null;
        }
    }


}
