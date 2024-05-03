using QLGiaoHangEntity.DataSet1TableAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLGiaoHangEntity.BLayer
{
    internal class BL_Report
    {
        public List<object> TaoReportDonDangGiao(int maNV, string trangthai)
        {
            List<object> list = new List<object>();
            DataSet1TableAdapters.DonHangDangGiaoTableAdapter adapter = new DataSet1TableAdapters.DonHangDangGiaoTableAdapter();
            var a = adapter.GetData(maNV, trangthai);
            foreach (var item in a)
                list.Add(item);
            adapter.Dispose();  
            return list;
        }

        public List<object> TaoReportDonDaGiao(int maNV, string trangthai)
        {
            List<object> list = new List<object>();
            DataSet1TableAdapters.DonHangDaGiaoTableAdapter adapter = new DataSet1TableAdapters.DonHangDaGiaoTableAdapter();
            var a = adapter.GetData(maNV, trangthai);
            foreach (var item in a)
                list.Add(item);
            adapter.Dispose();
            return list;
        }

        ~BL_Report() { }
    }
}
