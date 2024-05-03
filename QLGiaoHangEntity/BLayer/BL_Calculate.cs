using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLGiaoHangEntity.BLayer
{
    internal class BL_Calculate
    {
        DeliveryEntities YeuCauCalculate = new DeliveryEntities();
        public List<object> LayNV_TinhSoDH(int Manv)
        {
            List<object> list = new List<object>();
            var query = YeuCauCalculate.CHITIETDONHANGs
                .Join(YeuCauCalculate.DONHANGs,
                        ct => ct.MaDonHang, dh => dh.MaDonHang, (ct, dh) => new { ct, dh })
                .Where(ct => ct.ct.MaNV == Manv && (ct.ct.TrangThai == "DaGiao" || ct.ct.TrangThai == "DangGiao"))
                .Select(ct => new
                {
                    ct.ct.MaDonHang,
                    ct.dh.TenDonHang,
                    ct.ct.MaKH,
                    ct.ct.MaNV,
                    ct.dh.GiaTriDonHang,
                    ct.dh.NgayDat,
                    ct.ct.NgayGiaoThucTe,
                    ct.ct.TrangThai
                });
            foreach (var item in query)
                list.Add(item);
            return list;
        }
        public List<object> LayKH_TongTien(int maKH)
        {
            List<object> list = new List<object>();
            var query = YeuCauCalculate.CHITIETDONHANGs
                .Join(YeuCauCalculate.DONHANGs,
                        ct => ct.MaDonHang, dh => dh.MaDonHang, (ct, dh) => new { ct, dh })
                .Where(ct => ct.ct.MaKH == maKH && ct.ct.TrangThai != "Huy" && ct.ct.TrangThai != "TraHang")
                .Select(ct => new
                {
                    ct.ct.MaDonHang,
                    ct.dh.TenDonHang,
                    ct.ct.MaNV,
                    ct.dh.GiaTriDonHang,
                    ct.dh.NgayDat,
                    ct.ct.NgayGiaoThucTe,
                    ct.ct.TrangThai
                });
            foreach (var item in query)
                list.Add(item);
            return list;
        }    
        public int DemDonDangGiao(int maNV)
        {
            int soDon = 0;
            string trangthai = "DangGiao";
            soDon = YeuCauCalculate.CHITIETDONHANGs
                .Where(ct => ct.MaNV == maNV && ct.TrangThai == trangthai)
                .Select(ct => ct.MaDonHang)
                .Count();
            return soDon;
        }

        public int DemDonDaGiao(int maNV)
        {
            int soDon = 0;
            string trangthai = "DaGiao";
            soDon = YeuCauCalculate.CHITIETDONHANGs
                .Where(ct => ct.MaNV == maNV && ct.TrangThai == trangthai)
                .Select(ct => ct.MaDonHang)
                .Count();
            return soDon;
        }
        public int TinhTienKH(int maKH)
        {
            int tong = 0;
            var d = YeuCauCalculate.CHITIETDONHANGs
                .Join(YeuCauCalculate.DONHANGs,
                        ct => ct.MaDonHang, dh => dh.MaDonHang, (ct, dh) => new { ct, dh })
                .Where(ct => ct.ct.MaKH == maKH && ct.ct.TrangThai != "Huy" && ct.ct.TrangThai != "TraHang")
                .Select(ct => ct.dh.GiaTriDonHang)
                .Sum();
            tong = d.Value;
            return tong;
        }
        ~BL_Calculate() { }
    }
}
