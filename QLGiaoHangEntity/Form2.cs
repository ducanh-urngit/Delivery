using Microsoft.Reporting.WinForms;
using QLGiaoHangEntity.BLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLGiaoHangEntity
{
    public partial class Form2 : Form
    {
        BL_Edit QLEdit = new BL_Edit();
        BL_Calculate QLCalculate = new BL_Calculate();
        BL_Search QLSearch = new BL_Search();
        BL_Report QLReport = new BL_Report();
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            Clear_panel();
            LoadData_SuaDH();
            LoadData_XoaDH();
            LoadData_ThemDH();
        }
        public void LoadData_XoaDH()
        {
            //BL_Cau1 bl_XoaDH = new BL_Cau1();
            dgv_XoaDH.DataSource = QLEdit.LayThongTinChiTiet();
            dgv_XoaDH.AutoResizeColumns();
        }
        public void LoadData_ThemDH()
        {
            //BL_Cau1 bl_themDH = new BL_Cau1();
            cmb_maKH_ThemDH.DataSource = QLEdit.LayMaKH();
        }
        public void LoadData_SuaDH()
        {
            //BL_Cau1 loadData = new BL_Cau1();
            dgv_SuaDH.DataSource = QLEdit.LayThongTinChiTiet();
            dgv_SuaDH.AutoResizeColumns();
            /*dgv_SuaDH_CellClick(null, null);*/
        }
        private void dgv_SuaDH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgv_SuaDH.CurrentCell.RowIndex;
            txt_TenDH_SuaDH.Text = dgv_SuaDH.Rows[r].Cells[1].Value.ToString();
            txt_GiaTri_SuaDH.Text = dgv_SuaDH.Rows[r].Cells[4].Value.ToString();
            txt_ThuHo_SuaDH.Text = dgv_SuaDH.Rows[r].Cells[5].Value.ToString();
            dtp_NgayDat__SuaDH.Text = dgv_SuaDH.Rows[r].Cells[2].Value.ToString();
            dtp_NgayDuKien_SuaDH.Text = dgv_SuaDH.Rows[r].Cells[3].Value.ToString();
        }
        private void dgv_XoaDH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgv_XoaDH.CurrentCell.RowIndex;
            txt_MaDH_XoaDH.Text = dgv_XoaDH.Rows[r].Cells[0].Value.ToString();
        }
        private void btn_Them_ThemKH_Click(object sender, EventArgs e)
        {
            //BL_Cau1 themKH = new BL_Cau1();
            if (QLEdit.ThemKhachHang(txt_TenKH_ThemKH.Text.ToString(), txt_DiaChi_ThemKH.Text.ToString(), txt_sdt_ThemKH.Text.ToString()) == true)
            {
                MessageBox.Show("Đã thêm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_TenKH_ThemKH.Clear();
                txt_DiaChi_ThemKH.Clear();
                txt_sdt_ThemKH.Clear();
            }
            else
            {
                MessageBox.Show("Không thể thêm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_Them_themNV_Click(object sender, EventArgs e)
        {
            //BL_Cau1 themNV = new BL_Cau1();
            if (QLEdit.ThemNhanVien(txt_tenNV_ThemNV.Text.ToString(), txt_sdt_themNV.Text.ToString()) == true)
            {
                MessageBox.Show("Đã thêm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_tenNV_ThemNV.Clear();
                txt_sdt_themNV.Clear();
            }
            else
            {
                MessageBox.Show("Không thể thêm", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btn_Luu_SuaDH_Click(object sender, EventArgs e)
        {
            int r = dgv_SuaDH.CurrentCell.RowIndex;
            int madh = int.Parse(dgv_SuaDH.Rows[r].Cells[0].Value.ToString());
            string tenDH = txt_TenDH_SuaDH.Text.ToString();
            int giatriDH = int.Parse(txt_GiaTri_SuaDH.Text.ToString());
            int thuho = int.Parse(txt_ThuHo_SuaDH.Text.ToString());
            DateTime ngaydat = dtp_NgayDat__SuaDH.Value;
            DateTime ngaygiaodukien = dtp_NgayDuKien_SuaDH.Value;
            //BL_Cau1 bl_SuaDH = new BL_Cau1();
            if (QLEdit.SuaDonHang(madh, tenDH, giatriDH, thuho, ngaydat, ngaygiaodukien) == true)
            {
                LoadData_SuaDH();
                MessageBox.Show("Đã sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Không thể sửa", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadData_SuaDH();
            LoadData_XoaDH();
            LoadData_ThemDH();
        }
        private void btn_Xoa_XoaDH_Click(object sender, EventArgs e)
        {
            //BL_Cau1 bl_XoaDH = new BL_Cau1();
            if (QLEdit.XoaDonHang(int.Parse(txt_MaDH_XoaDH.Text.ToString())) == true)
            {
                MessageBox.Show("Đã xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData_XoaDH();
            }
            else
            {
                MessageBox.Show("Không thể xóa", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadData_SuaDH();
            LoadData_XoaDH();
            LoadData_ThemDH();
        }
        private void btn_Them_ThemDH_Click(object sender, EventArgs e)
        {
            //BL_Cau1 bl_ThemDH = new BL_Cau1();
            int makh = int.Parse(cmb_maKH_ThemDH.Text.ToString());
            //int manv = int.Parse(cmb_MaNV_themDH.Text.ToString());
            string tenDH = txt_TenDH_ThemDH.Text.ToString();
            DateTime ngaydat = dtp_NgayDat_ThemDH.Value;
            DateTime ngaygiaodukien = dtp_NgayDuKien_ThemDH.Value;
            int giatri = int.Parse(txt_GiaTri_ThemDH.Text.ToString());
            int thuho = int.Parse(txt_ThuHo_ThemDH.Text.ToString());
            if (QLEdit.ThemDonHang(makh, /*manv,*/ tenDH, ngaydat, ngaygiaodukien, giatri, thuho) == true)
            {
                txt_TenDH_ThemDH.Clear();
                txt_GiaTri_ThemDH.Clear();
                txt_ThuHo_ThemDH.Clear();
                MessageBox.Show("Đã thêm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Không thể thêm", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btn_rptDonDangGiao_Click(object sender, EventArgs e)
        {
            try
            {
                //BL_Cau4 taoReport = new BL_Cau4();
                int maNV = int.Parse(txtMaNV_rptDonDangGiao.Text.ToString());
                string trangthai = "DangGiao";
                List<object> a = QLReport.TaoReportDonDangGiao(maNV, trangthai);
                report_rptDonDangGiao.LocalReport.ReportPath = "ReportDonDangGiao.rdlc";
                var source = new ReportDataSource("DataSetDonDangGiao", a);
                report_rptDonDangGiao.LocalReport.DataSources.Clear();
                report_rptDonDangGiao.LocalReport.DataSources.Add(source);
                report_rptDonDangGiao.RefreshReport();
            }
            catch
            {
                MessageBox.Show("Hãy nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btn_rptDonDaGiao_Click(object sender, EventArgs e)
        {
            try
            {
                //BL_Cau4 taoReport = new BL_Cau4();
                int maNV = int.Parse(txtMaNV_rptDonDaGiao.Text.ToString());
                string trangthai = "DaGiao";
                List<object> a = QLReport.TaoReportDonDaGiao(maNV, trangthai);
                report_rptDonDaGiao.LocalReport.ReportPath = "ReportDonDaGiao.rdlc";
                var source = new ReportDataSource("DataSetDonDaGiao", a);
                report_rptDonDaGiao.LocalReport.DataSources.Clear();
                report_rptDonDaGiao.LocalReport.DataSources.Add(source);
                report_rptDonDaGiao.RefreshReport();
            }
            catch
            {
                MessageBox.Show("Hãy nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnReport_rptDonTraVe_Click(object sender, EventArgs e)
        {
            try
            {
                //BL_Cau4 taoReport = new BL_Cau4();
                int maNV = int.Parse(txtMaNV_rptDonTraVe.Text.ToString());
                string trangthai = "TraHang";
                List<object> a = QLReport.TaoReportDonDaGiao(maNV, trangthai);
                report_rptDonTraVe.LocalReport.ReportPath = "ReportDonTraVe.rdlc";
                var source = new ReportDataSource("DataSetDonTraVe", a);
                report_rptDonTraVe.LocalReport.DataSources.Clear();
                report_rptDonTraVe.LocalReport.DataSources.Add(source);
                report_rptDonTraVe.RefreshReport();
            }
            catch
            {
                MessageBox.Show("Hãy nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnReport_rptDonBiHuy_Click(object sender, EventArgs e)
        {
            try
            {
                //BL_Cau4 taoReport = new BL_Cau4();
                int maNV = int.Parse(txtMaNV_rptDonBiHuy.Text.ToString());
                string trangthai = "Huy";
                List<object> a = QLReport.TaoReportDonDangGiao(maNV, trangthai);
                report_rptDonBiHuy.LocalReport.ReportPath = "ReportDonBiHuy.rdlc";
                var source = new ReportDataSource("DataSetDonBiHuy", a);
                report_rptDonBiHuy.LocalReport.DataSources.Clear();
                report_rptDonBiHuy.LocalReport.DataSources.Add(source);
                report_rptDonBiHuy.RefreshReport();
            }
            catch
            {
                MessageBox.Show("Hãy nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnTinh_SoDonCuaNV_Click(object sender, EventArgs e)
        {
            int MaNV = int.Parse(txtMaNV_TinhSoDon.Text.ToString());
            dgv_TinhSoDon.DataSource = QLCalculate.LayNV_TinhSoDH(MaNV);
            lblDonDangGiao.Text = Convert.ToString(QLCalculate.DemDonDangGiao(MaNV));
            lblDonDaGiao.Text = Convert.ToString(QLCalculate.DemDonDaGiao(MaNV));
            dgv_TinhSoDon.AutoResizeColumns();
        }
        private void btnTinh_TongTien_Click(object sender, EventArgs e)
        {
            int MaKH = int.Parse(txtMaKH_TongSoTienKhachMua.Text.ToString());
            dgv_TongTien.DataSource = QLCalculate.LayKH_TongTien(MaKH);
            lblTongTien.Text = Convert.ToString(QLCalculate.TinhTienKH(MaKH));
            dgv_TongTien.AutoResizeColumns();
        }
        private void btn_TimKhachHang_Click(object sender, EventArgs e)
        {
            string tp = txtThanhPho_TimKiemKhachHang.Text.ToString();
            dgvThanhPho_TimKiemKhachHang.DataSource = QLSearch.TimKH_HCM(tp);
            dgvThanhPho_TimKiemKhachHang.AutoResizeColumns();
        }
        public void Clear_panel()
        {
            pnl_themDH.Visible = false;
            pnl_suaDH.Visible = false;
            pnl_XoaDH.Visible = false;
            pnl_ThemKhachHang.Visible = false;
            pnl_themNV.Visible = false;
            pnl_rptDonDangGiao.Visible = false;
            pnl_rptDonDaGiao.Visible = false;
            pnl_rptDonTraVe.Visible = false;
            pnl_rptDonBiHuy.Visible = false;
            pnl_TinhSoDon.Visible = false;
            pnl_TinhTienKhachHang.Visible = false;
            pnl_TimKhachHang.Visible = false;
        }
        private void thêmĐơnHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear_panel();
            pnl_themDH.Visible = true;
        }
        private void sửaĐơnHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear_panel();
            pnl_suaDH.Visible = true;
        }
        private void xóaĐơnHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear_panel();
            pnl_XoaDH.Visible = true;
        }
        private void thêmKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear_panel();
            pnl_ThemKhachHang.Visible = true;
        }
        private void thêmNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear_panel();
            pnl_themNV.Visible = true;
        }

        private void báoCáoĐơnHàngĐangGiaoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear_panel();
            pnl_rptDonDangGiao.Visible = true;
        }
        private void báoCáoĐơnHàngĐãGiaoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear_panel();
            pnl_rptDonDaGiao.Visible = true;
        }

        private void báoCáoĐơnHàngTrảVềToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear_panel();
            pnl_rptDonTraVe.Visible = true;
        }

        private void báoCáoĐơnHàngBịHuỷToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear_panel();
            pnl_rptDonBiHuy.Visible = true;
        }

        private void tínhSốTiền1NgườiKháchĐãMuaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear_panel();
            pnl_TinhSoDon.Visible = true;
        }

        private void tínhTổngCácGiáTrịĐơnHàngMà1NhânViênĐãGiaoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear_panel();
            pnl_TinhTienKhachHang.Visible = true;
        }
        private void tìmKiếmKháchHàngỞTPHCMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear_panel();
            pnl_TimKhachHang.Visible = true;
        }
        ~Form2() { }
    }
}
