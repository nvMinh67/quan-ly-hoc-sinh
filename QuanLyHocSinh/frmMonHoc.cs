using BUS;
using DevComponents.DotNetBar;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyHocSinh
{
    public partial class frmMonHoc : Office2007Form
    {
        public frmMonHoc()
        {
            InitializeComponent();
        }

        private void frmMonHoc_Load(object sender, EventArgs e)
        {
            MonHocBUS.Instance.HienThi(dgvMonHoc, bindingNavigatorMonHoc);
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            if (dgvMonHoc.RowCount == 0) bindingNavigatorDeleteItem.Enabled = true;

            BindingSource bindingSource = bindingNavigatorMonHoc.BindingSource;
            DataTable dataTable = (DataTable)bindingSource.DataSource;
            DataRow dataRow = dataTable.NewRow();

            string stt = Utilities.LaySTT(dgvMonHoc.Rows.Count + 1);
            dataRow["MaMonHoc"] = "MH" + stt;
            dataRow["TenMonHoc"] = "";
            //dataRow["SoTiet"] = 0;
            //dataRow["HeSo"] = 0;

            dataTable.Rows.Add(dataRow);
            bindingSource.MoveLast();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (dgvMonHoc.RowCount == 0) bindingNavigatorDeleteItem.Enabled = false;
            else if (
                MessageBox.Show(
                    "Bạn có chắc chắn xóa dòng này không?",
                    "Xóa lớp học",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question
                ) == DialogResult.OK
            ) bindingNavigatorMonHoc.BindingSource.RemoveCurrent();
        }

        //private void bindingNavigatorSaveItem_Click(object sender, EventArgs e)
        //{
        //    string[] colNames = { "colMaMonHoc", "colTenMonHoc"};
        //    if (KiemTraTruocKhiLuu.KiemTraDataGridView(dgvMonHoc, colNames)
        //        //&&
        //        //KiemTraTruocKhiLuu.KiemTraHeSo(dgvMonHoc, "colHeSo")
        //        )
        //    {
        //        bindingNavigatorPositionItem.Focus();
        //        BindingSource bindingSource = bindingNavigatorMonHoc.BindingSource;
        //        DataTable input = bindingSource.DataSource;
        //        MonHocBUS.Instance.CapNhatMonHoc(input);

        //        MessageBox.Show(
        //            "Dữ liệu đã được lưu vào CSDL",
        //            "Cập nhật thành công",
        //            MessageBoxButtons.OK,
        //            MessageBoxIcon.Information
        //        );
        //    }
        //}
        private void bindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            string[] colNames = { "colMaMonHoc", "colTenMonHoc" };

            if (KiemTraTruocKhiLuu.KiemTraDataGridView(dgvMonHoc, colNames)
                //&& KiemTraTruocKhiLuu.KiemTraHeSo(dgvMonHoc, "colHeSo")
                )
            {
                bindingNavigatorPositionItem.Focus();

                BindingSource bindingSource = bindingNavigatorMonHoc.BindingSource;
                DataTable input = bindingSource.DataSource as DataTable;
                input.Columns.Add("SoTiet", typeof(int));     // thêm dòng này
                input.Columns.Add("HeSo", typeof(int));       // và dòng này nếu dùng

                if (input != null)
                {
                    MonHocBUS.Instance.CapNhatMonHoc(input);

                    MessageBox.Show(
                        "Dữ liệu đã được lưu vào CSDL",
                        "Cập nhật thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
        }


        private void bindingNavigatorExitItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvMonHoc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
