    using DTO;
    using System;
    using System.Data;

    namespace DAO
    {
        public class DiemDAO
        {
            private static DiemDAO instance;

            private DiemDAO() { }

            public static DiemDAO Instance
            {
                get
                {
                    if (instance == null) instance = new DiemDAO();
                    return instance;
                }
                private set => instance = value;
            }

            public DataTable LayDanhSachDiem(string maHocSinh, string maMonHoc, string maHocKy, string maNamHoc, string maLop)
            {
                string query = "EXEC LayDanhSachDiem @maHocSinh , @maMonHoc , @maHocKy , @maNamHoc , @maLop";
                object[] parameters = new object[] { maHocSinh, maMonHoc, maHocKy, maNamHoc, maLop };
                return DataProvider.Instance.ExecuteQuery(query, parameters);
            }

            public DataTable LayDanhSachDiemHocSinh(string maHocSinh, string maMonHoc, string maHocKy, string maNamHoc, string maLop)
            {
                string query = "EXEC LayDanhSachDiemHocSinh @maHocSinh , @maMonHoc , @maHocKy , @maNamHoc , @maLop";
                object[] parameters = new object[] { maHocSinh, maMonHoc, maHocKy, maNamHoc, maLop };
                return DataProvider.Instance.ExecuteQuery(query, parameters);
            }

            public void ThemDiem(DiemDTO diem)
            {
                string query = "EXEC ThemDiem @maHocSinh , @maMonHoc , @maHocKy , @maNamHoc , @maLop , @maLoaiDiem , @diemSo";
                object[] parameters = new object[] { 
                    diem.MaHocSinh, diem.MaMonHoc, diem.MaHocKy, diem.MaNamHoc, diem.MaLop, diem.MaLoaiDiem, diem.DiemSo 
                };
                DataProvider.Instance.ExecuteNonQuery(query, parameters);
            }
        public void xoaDiemDaLuu(DiemDTO diem)
        {
            string query = @"
        DELETE FROM Diem
        WHERE MaHocSinh = @maHocSinh
          AND MaMonHoc = @maMonHoc
          AND MaHocKy = @maHocKy
          AND MaNamHoc = @maNamHoc
          AND MaLop = @maLop";

            object[] parameters = new object[]
            {
        diem.maHocSinh,
        diem.maMonHoc,
        diem.maHocKy,
        diem.maNamHoc,
        diem.maLop,
        diem.maLoaiDiem
            };

            // Thực thi truy vấn xóa
            DataProvider.Instance.ExecuteNonQueryNew(query, parameters);
        }
        public void CapNhatDiem(DiemDTO diem)
        {
            string query = @"
        DELETE FROM Diem
        WHERE MaHocSinh = @maHocSinh
          AND MaMonHoc = @maMonHoc
          AND MaHocKy = @maHocKy
          AND MaNamHoc = @maNamHoc
          AND MaLop = @maLop
          AND MaLoai = @maLoaiDiem";

            object[] parameters = new object[]
            {
        diem.maHocSinh,
        diem.maMonHoc,
        diem.maHocKy,
        diem.maNamHoc,
        diem.maLop,
        diem.maLoaiDiem
            };

            // Thực thi truy vấn xóa
            DataProvider.Instance.ExecuteNonQueryNew(query, parameters);
        }

        public void XoaDiem(int stt)
            {
                string query = "EXEC XoaDiem @STT";
                object[] parameters = new object[] { stt };
                DataProvider.Instance.ExecuteNonQuery(query, parameters);
            }
        }
    }
