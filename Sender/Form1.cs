using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Model;
using System.Collections;
using System.Messaging;

namespace Sender
{
    public partial class frmNhanBenh : Form
    {
        MessageQueue quece = null;

        public frmNhanBenh()
        {
            InitializeComponent();

            init(); 
        }

        private void init()
        {
            string path = @".\private$\khambenh";

            if (MessageQueue.Exists(path))
            {
                quece = new MessageQueue(path, QueueAccessMode.Send);
            }
            else
            {
                quece = new MessageQueue(path, true);
            }

        }

        private void txtLuuThongTin_Click(object sender, EventArgs e)
        {
            string msbn = txtMaSoBenhNhan.Text;
            string socmnd = txtCMND.Text;
            string hoten = txtHoTen.Text;
            string diaChi = txtDiaChi.Text;

            BenhNhan benhNhan = new BenhNhan(msbn, socmnd, hoten, diaChi);

            MessageQueueTransaction transaction = new MessageQueueTransaction();
            transaction.Begin();
            quece.Send(benhNhan, transaction);
            transaction.Commit();

        }
    }
}
