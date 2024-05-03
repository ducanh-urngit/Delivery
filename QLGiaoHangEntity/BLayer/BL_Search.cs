using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace QLGiaoHangEntity.BLayer
{
    internal class BL_Search
    {
        DeliveryEntities YeuCauSearch = new DeliveryEntities();

        public List<object> TimKH_HCM(string tp)
        {
            List<object> list = new List<object>();
            var query = YeuCauSearch.KHACHHANGs
                .Where(kh => kh.DiaChi.Contains(tp) && kh.DiaChi.EndsWith(tp))
                .Select(kh => new
                {
                    kh.MaKH,
                    kh.TenKH,
                    kh.DiaChi,
                    kh.SDT
                });
            foreach (var item in query)
                list.Add(item);
            return list;
        }
        ~BL_Search() { }
    }
}
