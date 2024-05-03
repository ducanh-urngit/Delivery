using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLGiaoHangEntity.BLayer
{
    internal class BL_Edit
    {
        DeliveryEntities YeuCauEdit = new DeliveryEntities();
        
        public List<object> LayThongTinChiTiet()
        {
            //DeliveryEntities qlThongTin = new DeliveryEntities();
            var query = YeuCauEdit.DONHANGs
                .Select(dh => new
                {
                    dh.MaDonHang,
                    dh.TenDonHang,
                    dh.NgayDat,
                    dh.NgayGiaoDuKien,
                    dh.GiaTriDonHang,
                    dh.ThuHo
                });
            List<object> list = new List<object>();
            foreach (var item in query)
                list.Add(item);
            //qlThongTin.Dispose();
            return list;
            
        }
        public bool ThemKhachHang( string tenKH, string DiaChi, string sdt)
        {
            //DeliveryEntities qlGH = new DeliveryEntities();
            try
            {
                KHACHHANG kh = new KHACHHANG();
                int maKH = YeuCauEdit.KHACHHANGs
                    .Select(KH => KH.MaKH).Max();
                maKH++;
                kh.MaKH = maKH;
                kh.TenKH = tenKH;
                kh.DiaChi = DiaChi;
                kh.SDT = sdt;
                YeuCauEdit.KHACHHANGs.Add(kh);
                YeuCauEdit.SaveChanges();
                //qlGH.Dispose();
                return true;
            }
            catch
            {
                //qlGH.Dispose();
                return false;
            }
        }
        public bool ThemNhanVien(string tenNV, string sdt)
        {
            //DeliveryEntities qlGH = new DeliveryEntities();
            try
            {
                NHANVIEN nv = new NHANVIEN();
                int maNV = YeuCauEdit.NHANVIENs.Select(NV => NV.MaNV).Max();
                maNV++;
                nv.MaNV = maNV;
                nv.TenNV = tenNV;
                nv.SDTNV = sdt;
                YeuCauEdit.NHANVIENs.Add(nv);
                YeuCauEdit.SaveChanges();
                //qlGH.Dispose();
                return true;
            }
            catch
            {
                //qlGH.Dispose();
                return false;
            }
        }
        public bool SuaDonHang(int madh, string tenDH, int giatri, int thuho, DateTime ngaydat, DateTime ngaydukien)
        {
            //DeliveryEntities qlGH = new DeliveryEntities();
            DONHANG dh = YeuCauEdit.DONHANGs.Find(madh);
            if (dh != null)
            {
                dh.TenDonHang = tenDH;
                dh.GiaTriDonHang = giatri;
                dh.ThuHo = thuho;
                dh.NgayDat = ngaydat;
                dh.NgayGiaoDuKien = ngaydukien;
                YeuCauEdit.SaveChanges();
            }
            //qlGH.Dispose();
            return true;
        }
        public bool XoaDonHang(int madh)
        {
            //DeliveryEntities qlGH = new DeliveryEntities();
            try
            {
                CHITIETDONHANG chiTiet = new CHITIETDONHANG();
                chiTiet.MaDonHang = madh;
                YeuCauEdit.CHITIETDONHANGs.Attach(chiTiet);
                YeuCauEdit.CHITIETDONHANGs.Remove(chiTiet);
                YeuCauEdit.SaveChanges();

                DONHANG don = new DONHANG();
                don.MaDonHang = madh;
                YeuCauEdit.DONHANGs.Attach(don);
                YeuCauEdit.DONHANGs.Remove(don);
                YeuCauEdit.SaveChanges();

                //qlGH.Dispose();

                return true;
            }
            catch 
            {
                //qlGH.Dispose();
                return false;
            }
        }
        public List<string> LayMaKH()
        {
            //DeliveryEntities qlGH = new DeliveryEntities();
            List<string> items = new List<string>();
            foreach (KHACHHANG kh in YeuCauEdit.KHACHHANGs)
            {
                items.Add(kh.MaKH.ToString());
            }
            //qlGH.Dispose();
            return items;
        }
        public List<string> LayMaNV()
        {
            //DeliveryEntities qlGH = new DeliveryEntities();
            List<string> items = new List<string>();
            foreach (NHANVIEN nv in YeuCauEdit.NHANVIENs)
            {
                items.Add(nv.MaNV.ToString());
            }
            //qlGH.Dispose();
            return items;
        }
        public bool ThemDonHang(int makh/*, int manv*/, string tenDH, DateTime ngaydat, DateTime ngaygiaodukien, int giatri, int thuho)
        {
            //DeliveryEntities qlGH = new DeliveryEntities();
            try
            {
                /*int madh = (from DH in YeuCauEdit.DONHANGs
                            select DH.MaDonHang).Max();*/
                int madh = YeuCauEdit.DONHANGs.Select(DH => DH.MaDonHang).Max();
                madh++;

                DONHANG dh = new DONHANG();
                dh.MaDonHang = madh;
                dh.TenDonHang = tenDH;
                dh.NgayDat = ngaydat;
                dh.NgayGiaoDuKien = ngaygiaodukien;
                dh.GiaTriDonHang = giatri;
                dh.ThuHo = thuho;
                YeuCauEdit.DONHANGs.Add(dh);
                YeuCauEdit.SaveChanges();

                CHITIETDONHANG ct = new CHITIETDONHANG();
                ct.MaDonHang = madh;
                ct.MaKH = makh;
                ct.MaNV = null;
                ct.NgayGiaoThucTe = null;
                ct.TrangThai = "DangTrongKho";
                YeuCauEdit.CHITIETDONHANGs.Add(ct);
                YeuCauEdit.SaveChanges();

                //qlGH.Dispose();

                return true;
            }
            catch
            {
                //qlGH.Dispose();
                return false;   
            }
        }
        ~BL_Edit() { }
    }
}
