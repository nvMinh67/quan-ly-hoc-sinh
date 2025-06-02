using BUS;
using DevComponents.DotNetBar;
using DTO;
using QuanLyHocSinh.WCFProxy1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions; // cần import thêm
using System.Windows.Forms;
namespace QuanLyHocSinh
{
    public partial class frmDiem : Office2007Form
    {
        private int[,] STT = null;

        public frmDiem()
        {
            InitializeComponent();
        }

        private void frmDiem_Load(object sender, EventArgs e)
        {
            NamHocBUS.Instance.HienThiComboBox(cmbNamHoc);
            HocKyBUS.Instance.HienThiComboBox(cmbHocKy);

            if (cmbNamHoc.SelectedValue != null)
                LopBUS.Instance.HienThiComboBox(cmbNamHoc.SelectedValue.ToString(), cmbLop);

            if (cmbNamHoc.SelectedValue != null && cmbLop.SelectedValue != null)
                MonHocBUS.Instance.HienThiComboBox(
                    cmbNamHoc.SelectedValue.ToString(), 
                    cmbLop.SelectedValue.ToString(), 
                    cmbMonHoc
                );
        }
        private void btnLuuDiem_Click(object sender, EventArgs e)
        {
            List<DiemDTO> danhSachDiem = new List<DiemDTO>();
            string[] colNames = { "colDiemMieng", "colDiem15Phut", "colDiem45Phut", "colDiemThi" };
      
            //if (!KiemTraTruocKhiLuu.KiemTraDiem(dgvDiem, colNames) || STT == null) return;

            string maLop = cmbLop.SelectedValue.ToString();
            string maNamHoc = cmbNamHoc.SelectedValue.ToString();
            string maMonHoc = cmbMonHoc.SelectedValue.ToString();
            string maHocKy = cmbHocKy.SelectedValue.ToString();

            foreach (DataGridViewRow row in dgvDiem.Rows)
            {
                
                string maHocSinh = row.Cells["colMaHocSinh"].Value.ToString();
               

                for (int j = 0; j < colNames.Length; j++)
                {
                    string chuoiDiem = row.Cells[colNames[j]].Value?.ToString() ?? "";
                    string[] diemArray = chuoiDiem.Split(';');

                    foreach (string diemStr in diemArray)
                    {
                        if (!string.IsNullOrWhiteSpace(diemStr))
                        {
                        if (float.TryParse(diemStr, out float diemSo))
                        {
                            var diemDTO = new DiemDTO(maHocSinh, maMonHoc, maHocKy, maNamHoc, maLop, $"LD000{j + 1}", diemSo);
                                danhSachDiem.Add(diemDTO);
                            }
                        }
                    }
                }   
            }
            //List<List<int>> sttList = new List<List<int>>();
            //for (int i = 0; i < dgvDiem.Rows.Count; i++)
            //{
            //    List<int> rowList = new List<int>();
            //    for (int j = 0; j < colNames.Length; j++)
            //    {
            //        rowList.Add(STT[i, j]);
            //    }
            //    sttList.Add(rowList);
            //}

            var client = new QuanLyServiceClient();
            //// Gọi WCF
            DiemDTO[] diemArrayF = danhSachDiem.ToArray();
            //int[][] sttArray = sttList.Select(row => row.ToArray()).ToArray();
            client.LuuDiem(diemArrayF, maLop, maNamHoc, maMonHoc, maHocKy);
            MessageBox.Show("Cập nhật thành công!", "COMPLETED", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnHienThiDanhSach_Click(sender, e);

            frmXemDiem frm = (frmXemDiem)Application.OpenForms["frmXemDiem"];
            if (frm != null) frm.btnHienThiClicked(sender, e);
        }


        //private void btnLuuDiem_Click(object sender, EventArgs e)
        //{
        //    string[] colNames = { "colDiemMieng", "colDiem15Phut", "colDiem45Phut", "colDiemThi" };
        //    if (!KiemTraTruocKhiLuu.KiemTraDiem(dgvDiem, colNames) || STT == null) return;

        //    string maLop = cmbLop.SelectedValue.ToString();
        //    string maNamHoc = cmbNamHoc.SelectedValue.ToString();
        //    string maMonHoc = cmbMonHoc.SelectedValue.ToString();
        //    string maHocKy = cmbHocKy.SelectedValue.ToString();
        //    int rowCount = 0;

        //    foreach (DataGridViewRow row in dgvDiem.Rows)
        //    {
        //        string maHocSinh = row.Cells["colMaHocSinh"].Value.ToString();
        //        rowCount++;

        //        for (int i = 0; i < colNames.Length; i++)
        //        {
        //            if (row.Cells[colNames[i]].Value.ToString() == "") row.Cells[colNames[i]].Value = 0;
        //            string chuoiDiem = row.Cells[colNames[i]].Value.ToString();

        //            if (string.IsNullOrWhiteSpace(chuoiDiem)) continue;
        //            int count = 0;

        //            for (int j = 0; j < chuoiDiem.Length; j++)
        //            {
        //                if (chuoiDiem[j] != ';' && j != chuoiDiem.Length - 1) count++;
        //                else
        //                {
        //                    if (j == chuoiDiem.Length - 1)
        //                    {
        //                        j++;
        //                        count++;
        //                    }

        //                    string diemDaXuLy = chuoiDiem.Substring(j - count, count);
        //                    if (!string.IsNullOrWhiteSpace(diemDaXuLy) && QuyDinhBUS.Instance.KiemTraDiem(diemDaXuLy))
        //                    {
        //                        DiemDTO diem = new DiemDTO(maHocSinh, maMonHoc, maHocKy, maNamHoc, maLop, $"LD000{i + 1}", float.Parse(diemDaXuLy));
        //                        DiemBUS.Instance.ThemDiem(diem);
        //                    }
        //                    count = 0;
        //                }
        //            }
        //        }

        //        #region Xóa các kết quả cũ
        //        for (int i = 1; i < 60; i++)
        //        {
        //            for (int j = 1; j < 20; j++)
        //            {
        //                int id = STT[i, j];
        //                if (id > 0) DiemBUS.Instance.XoaDiem(id);
        //                else break;
        //            }
        //        }
        //        #endregion

        //        #region Lưu vào bảng kết quả
        //        if (rowCount <= dgvDiem.Rows.Count)
        //        {
        //            KQHSMonHocBUS.Instance.LuuKetQua(maHocSinh, maLop, maNamHoc, maMonHoc, maHocKy);
        //            KQHSCaNamBUS.Instance.LuuKetQua(maHocSinh, maLop, maNamHoc);
        //            KQLHMonHocBUS.Instance.LuuKetQua(maLop, maNamHoc, maMonHoc, maHocKy);
        //            KQLHHocKyBUS.Instance.LuuKetQua(maLop, maNamHoc, maHocKy);
        //        }
        //        #endregion
        //    }

        //    MessageBox.Show("Cập nhật thành công!", "COMPLETED", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    btnHienThiDanhSach_Click(sender, e);

        //    frmXemDiem frm = (frmXemDiem)Application.OpenForms["frmXemDiem"];
        //    if (frm != null) frm.btnHienThiClicked(sender, e);
        //}

        private void btnXemDiem_Click(object sender, EventArgs e)
        {
            Utilities.ShowForm("frmXemDiem");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnThemNamHoc_Click(object sender, EventArgs e)
        {
            Utilities.ShowForm("frmNamHoc");
            NamHocBUS.Instance.HienThiComboBox(cmbNamHoc);
        }

        private void btnThemLop_Click(object sender, EventArgs e)
        {
            Utilities.ShowForm("frmLop");
            LopBUS.Instance.HienThiComboBox(cmbNamHoc.SelectedValue.ToString(), cmbLop);
        }

        private void btnThemHocKy_Click(object sender, EventArgs e)
        {
            Utilities.ShowForm("frmHocKy");
            HocKyBUS.Instance.HienThiComboBox(cmbHocKy);
        }

        private void btnThemMonHoc_Click(object sender, EventArgs e)
        {
            Utilities.ShowForm("frmMonHoc");
            MonHocBUS.Instance.HienThiComboBox(
                cmbNamHoc.SelectedValue.ToString(), 
                cmbLop.SelectedValue.ToString(), 
                cmbMonHoc
            );
        }

        // Lấy thông tin lớp theo từng năm học
        private void cmbNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNamHoc.SelectedValue != null)
                LopBUS.Instance.HienThiComboBox(cmbNamHoc.SelectedValue.ToString(), cmbLop);
            cmbLop.DataBindings.Clear();
        }

        // Lấy môn học theo từng lớp
        private void cmbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNamHoc.SelectedValue != null && cmbLop.SelectedValue != null)
                MonHocBUS.Instance.HienThiComboBox(
                    cmbNamHoc.SelectedValue.ToString(), 
                    cmbLop.SelectedValue.ToString(), 
                    cmbMonHoc
                );
            cmbMonHoc.DataBindings.Clear();
        }

        private void btnHienThiDanhSach_Click(object sender, EventArgs e)
        {
            if (cmbNamHoc.SelectedValue != null && 
                cmbLop.SelectedValue != null && 
                cmbHocKy.SelectedValue != null && 
                cmbMonHoc.SelectedValue != null)
                HocSinhBUS.Instance.HienThiHocSinhTheoLop(
                    bindingNavigatorDiem,
                    dgvDiem,
                    cmbNamHoc.SelectedValue.ToString(), 
                    cmbLop.SelectedValue.ToString()
                );
            DiemBUS.Instance.HienThi(dgvDiem, cmbMonHoc, cmbHocKy, cmbNamHoc, cmbLop, ref STT);
        }

        internal void btnHienThiClicked(object sender, EventArgs e)
        {
            btnHienThiDanhSach_Click(sender, e);
        }

        internal void btnLuuDiemClicked(object sender, EventArgs e)
        {
            btnLuuDiem_Click(sender, e);
        }

        private void dgvDiem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void navPaneLeft_Load(object sender, EventArgs e)
        {

        }
    }
}
