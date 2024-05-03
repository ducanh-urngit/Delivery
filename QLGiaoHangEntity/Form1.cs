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
    public partial class Form1 : Form
    {
        BLEntity_formMain QlForm = new BLEntity_formMain();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rdbTrangThai_NhanVien_DaGiao.Checked = true;
            LoadData_dgvLayHang();
        }
        
        public void LoadData_dgvLayHang()
        {
            //BLEntity_formMain LayTT = new BLEntity_formMain();
            dgv_LayHang.DataSource = QlForm.LayDonDangTrongKho();
            dgv_LayHang.AutoResizeColumns();
        }
        public void LoadData_DonDaNhan(int maNV)
        {
            //BLEntity_formMain layDon = new BLEntity_formMain();
            dgvDonDaNhan_NhanVien.DataSource = QlForm.LayDonHangDaNhan(maNV);
        }
        private void dgv_LayHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgv_LayHang.CurrentCell.RowIndex;
            txtMaDonHang_LayHang.Text = dgv_LayHang.Rows[r].Cells[0].Value.ToString();
        }
        private void txtMatKhau_Login_TextChanged(object sender, EventArgs e)
        {
            txtMatKhau_Login.PasswordChar = '*';
        }
        private void btnTraCuu_Click(object sender, EventArgs e)
        {

            try
            {
                int maKhachhang = int.Parse(txtMaKhachHang_KH.Text);

                //BLEntity_formMain blTraCuu = new BLEntity_formMain();

                dgv_TraCuu.DataSource = QlForm.TraCuu(maKhachhang, "");
                dgv_TraCuu.AutoResizeColumns();
            }
            catch (FormatException)
            {
                MessageBox.Show("Mã khách hàng không hợp lệ. Vui lòng nhập lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            int manv = int.Parse(txtMaNV_Login.Text);
            string matkhau = txtMatKhau_Login.Text.ToString();
            //BLEntity_formMain blLogin = new BLEntity_formMain();
            bool check = QlForm.Login(manv, matkhau, "");
            if (check == true)
            {
                MessageBox.Show("Dang nhap thanh cong", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                using (Form2 form2 = new Form2())
                {
                    this.Visible = false;
                    form2.ShowDialog();
                    txtMaNV_Login.Clear();
                    txtMatKhau_Login.Clear();
                    this.Visible = true;
                    LoadData_dgvLayHang();
                }
            }
            else
            {
                MessageBox.Show("Sai mã nhân viên hoặc mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btn_CapNhat_Click(object sender, EventArgs e)
        {
            int madh = int.Parse(txt_Madonhang_nhanvien.Text);
            int maNV = int.Parse(txtMaNV_NhanVien.Text.ToString());
            string trangThai = "DangGiao";
            DateTime ngaygiao = date_nhanvien.Value;
            //BLEntity_formMain blCapNhat = new BLEntity_formMain();
            if (QlForm.CheckLaDonCuaNhanVien(madh, maNV, trangThai) == true)
            {
                if (rdbTrangThai_NhanVien_DaGiao.Checked)
                    trangThai = "DaGiao";
                if (rdbTrangThai_NhanVien_TraVe.Checked)
                    trangThai = "TraHang";
                if (rdbTrangThai_NhanVien_Huy.Checked)
                    trangThai = "Huy";
                bool check = QlForm.Capnhat_nhanvien(madh, ngaygiao, trangThai, "");
                if (check == true)
                {
                    MessageBox.Show("Đã cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật trạng thái ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                txt_Madonhang_nhanvien.Clear();
                txtMaNV_NhanVien.Clear();
            }
            else
                MessageBox.Show("Đơn hàng này không phải của bạn hoặc bạn chưa nhận giao đơn này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        private void btnNhanGiaoHang_LayHang_Click(object sender, EventArgs e)
        {
            try
            {
                int madh = int.Parse(txtMaDonHang_LayHang.Text.ToString());
                int maNV = int.Parse(txtMaNV_LayHang.Text.ToString());
                //BLEntity_formMain nhanDon = new BLEntity_formMain();
                if(QlForm.NhanDonGiaoHang(madh,maNV))
                    MessageBox.Show("Nhận đơn giao hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Lỗi không nhận được đơn hàng này", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            catch 
            {
                MessageBox.Show("Lỗi mã không thể nhận đơn hàng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadData_dgvLayHang();
            txtMaDonHang_LayHang.Clear();
        }
        private void btnDonDaNhan_NhanVien_Click(object sender, EventArgs e)
        {
            try
            {
                int maNV = int.Parse(txtMaNV_DonDaNhan_NhanVien.Text.ToString());
                LoadData_DonDaNhan(maNV);
            }
            catch
            {
                MessageBox.Show("Mã nhân viên không hợp lệ", "lỗi", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnHuyDonDaNhan_NhanVien_Click(object sender, EventArgs e)
        {
            try
            {
                int maNV = int.Parse(txtMaNV_DonDaNhan_NhanVien.Text.ToString());
                int madh = int.Parse(txtMaDH_DonDaNhan_NhanVien.Text.ToString());
                //BLEntity_formMain huyDon = new BLEntity_formMain();
                if (QlForm.HuyDonDaNhan(madh, maNV))
                    MessageBox.Show("Huỷ thành công", "thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                else
                    MessageBox.Show("Đơn hàng này không phải của bạn hoặc bạn chưa nhận giao đơn này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaDH_DonDaNhan_NhanVien.Clear();
                LoadData_DonDaNhan(maNV);
                LoadData_dgvLayHang();
            }
            catch
            {
                MessageBox.Show("lỗi không thể huỷ", "lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        ~Form1() { }

    }
}
