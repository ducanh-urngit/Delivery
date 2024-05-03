using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace QLGiaoHangEntity.BLayer
{
    internal class BLEntity_formMain
    {
        DeliveryEntities YeuCauFrmMain = new DeliveryEntities();
        public List<object> LayDonHangDaNhan(int maNV)
        {
            List<object> list = new List<object>();
            string trangthai = "DangGiao";
            if (TonTaiNV(maNV))
            {
                //DeliveryEntities layDon = new DeliveryEntities();
                var query = YeuCauFrmMain.CHITIETDONHANGs
                    .Join(YeuCauFrmMain.DONHANGs,
                            ct => ct.MaDonHang, dh => dh.MaDonHang, (ct, dh) => new { ct, dh })
                    .Join(YeuCauFrmMain.KHACHHANGs,
                            ct => ct.ct.MaKH, kh => kh.MaKH, (ct, kh) => new { ct.ct, ct.dh, kh })
                    .Where(ct => ct.ct.MaNV == maNV && ct.ct.TrangThai == trangthai)
                    .Select(ct => new 
                    { 
                        ct.ct.MaDonHang, 
                        ct.dh.TenDonHang, 
                        ct.dh.NgayDat, 
                        ct.dh.NgayGiaoDuKien, 
                        ct.kh.DiaChi, 
                        ct.kh.SDT, 
                        ct.ct.TrangThai 
                    });
                
                foreach (var item in query)
                    list.Add(item);
                //layDon.Dispose();
            }
            return list;
        }
        public List<object> LayDonDangTrongKho()
        {
            //DeliveryEntities DonTrongKho = new DeliveryEntities();
            string TrangThai = "DangTrongKho";
            var query = YeuCauFrmMain.CHITIETDONHANGs
                    .Join(YeuCauFrmMain.DONHANGs,
                            ct => ct.MaDonHang, dh => dh.MaDonHang, (ct, dh) => new { ct, dh })
                    .Join(YeuCauFrmMain.KHACHHANGs,
                            ct => ct.ct.MaKH, kh => kh.MaKH, (ct, kh) => new { ct.ct, ct.dh, kh })
                    .Where(ct => ct.ct.TrangThai == TrangThai)
                    .Select(ct => new
                    {
                        ct.ct.MaDonHang,
                        ct.dh.TenDonHang,
                        ct.dh.NgayDat,
                        ct.dh.NgayGiaoDuKien,
                        ct.kh.DiaChi,
                        ct.kh.SDT,
                        ct.ct.TrangThai
                    });
            List<object> list = new List<object>();
            foreach(var item in query)
                list.Add(item);
            //DonTrongKho.Dispose();
            return list;
        }
        public List<object> TraCuu(int makhachhang, string err)
        {
            //DeliveryEntities qlThongTin = new DeliveryEntities();
            var query = YeuCauFrmMain.CHITIETDONHANGs
                .Join(YeuCauFrmMain.KHACHHANGs,
                        ct => ct.MaKH, kh => kh.MaKH, (ct, kh) => new { ct, kh })
                .Join(YeuCauFrmMain.DONHANGs,
                        ct => ct.ct.MaDonHang, dh => dh.MaDonHang, (ct, dh) => new { ct.ct, ct.kh, dh })
                .Where(ct => ct.ct.MaKH == makhachhang)
                .Select(ct => new
                {
                    ct.kh.TenKH,
                    ct.ct.MaDonHang,
                    ct.dh.TenDonHang,
                    ct.ct.TrangThai,
                    ct.dh.GiaTriDonHang,
                    ct.dh.NgayGiaoDuKien,
                    ct.ct.NgayGiaoThucTe
                });



            List<object> list = new List<object>();
            foreach (var item in query)
                list.Add(item);
            //qlThongTin.Dispose();
            return list;
        }

        public bool Login(int maQL, string matkhau, string err)
        {
            bool check = false;
            //DeliveryEntities qlLogin = new DeliveryEntities();
            try
            {
                QuanLy QL = YeuCauFrmMain.QuanLies.Find(maQL);
                if (QL != null)
                    if (QL.MatKhau == matkhau)
                        check = true;
            }
            catch
            {
                check = false;  
            }
            //qlLogin.Dispose();
            return check;
        }
        public bool TonTaiNV(int MaNV)
        {
            bool f = false;
            //DeliveryEntities nv = new DeliveryEntities();
            try
            {
                NHANVIEN a = YeuCauFrmMain.NHANVIENs.Find(MaNV);
                if (a != null)
                    f = true;
            }
            catch
            {
                f = false;
            }
            //nv.Dispose();
            return f;
        }
        public bool NhanDonGiaoHang(int maDonHang, int maNV)
        {
            bool f = false;
            string trangthai = "DangTrongKho";
            if (TonTaiNV(maNV))
            {
                //DeliveryEntities gh = new DeliveryEntities();
                try
                {
                    CHITIETDONHANG donNhan = YeuCauFrmMain.CHITIETDONHANGs.Find(maDonHang);
                    if (donNhan != null)
                    {
                        if(donNhan.TrangThai == trangthai)
                        {
                            donNhan.MaNV = maNV;
                            donNhan.TrangThai = "DangGiao";
                            YeuCauFrmMain.SaveChanges();
                            f = true;
                        }
                        
                    }
                }
                catch
                {
                    f = false;
                }
                //gh.Dispose();
            }
            return f;
        }
        public bool CheckLaDonCuaNhanVien(int maDonHang, int MaNV, string trangThai)
        {
            bool check = false;
            //DeliveryEntities donHang = new DeliveryEntities();
            try
            {
                CHITIETDONHANG ct = YeuCauFrmMain.CHITIETDONHANGs.Find(maDonHang);
                if (ct != null)
                    if (ct.MaNV == MaNV && ct.TrangThai == trangThai)
                        check = true;
            }
            catch
            {
                check = false;
            }
            //donHang.Dispose();
            return check;
        }

        public bool Capnhat_nhanvien(int madonhang, DateTime ngaygiao, string trangthai, string err)
        {
            bool check = false;
            //DeliveryEntities qlGH = new DeliveryEntities();
            try
            {
                CHITIETDONHANG ct = YeuCauFrmMain.CHITIETDONHANGs.Find(madonhang);
                if (ct != null)
                {
                    ct.NgayGiaoThucTe = ngaygiao;
                    ct.TrangThai = trangthai;
                    YeuCauFrmMain.SaveChanges();
                    check = true;
                }
                else
                {
                    check = false;
                }
            }
            catch
            {
                check=false;
            }
            //qlGH.Dispose();
            return check;
        }
        public bool HuyDonDaNhan(int madh, int manv)
        {
            string trangThai = "DangGiao";
            if (CheckLaDonCuaNhanVien(madh, manv, trangThai))
            {
                //DeliveryEntities qlGH = new DeliveryEntities();
                CHITIETDONHANG ct = YeuCauFrmMain.CHITIETDONHANGs.Find(madh);
                ct.MaNV = null;
                ct.TrangThai = "DangTrongKho";
                YeuCauFrmMain.SaveChanges();
                //qlGH.Dispose();
                return true;
            }
            else
                return false; 
        }
        ~BLEntity_formMain() { }
    }
    
}
