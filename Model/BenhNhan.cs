using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class BenhNhan
    {

        public string msbn { get; set; }
        public string soCMND { get; set; }
        public string hoten { get; set; }
        public string diaChi { get; set; }

        public BenhNhan(string msbn, string cmnd, string hoTen, string diaChi)
        {
            this.msbn = msbn;
            this.soCMND = cmnd;
            this.hoten = hoTen;
            this.diaChi = diaChi;
        }

        public BenhNhan()
        {

        }

        public override string ToString()
        {
            return msbn + "\t" + hoten;
        }

    }
}
