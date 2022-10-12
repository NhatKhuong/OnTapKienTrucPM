using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Model;
using System.Messaging;

namespace Receiver
{
    public partial class frmKhamBenh : Form
    {
        private MessageQueue queue;


        public frmKhamBenh()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            string path = @".\private$\khambenh";
            queue = new MessageQueue(path);
            queue.BeginReceive();
            queue.ReceiveCompleted += Queue_ReceiveCompleted;
        }

        private void Queue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var msg = e.Message;
            int type = msg.BodyType;
            XmlMessageFormatter fmt = new XmlMessageFormatter(
                new System.Type[]
                {
                    typeof(string),typeof(BenhNhan)
                });
            msg.Formatter = fmt;
            var result = msg.Body;
            var t = result.GetType();
            if (t.Equals(typeof(BenhNhan)))
            {
                BenhNhan benhNhan = (BenhNhan)result;
                SetText(benhNhan);
            }
            queue.BeginReceive();
        }

        delegate void SetTextCallback(string text);
        private void SetText(BenhNhan benhNhan)
        {
            if (listBox.InvokeRequired)
            {
                txtMaSoBenhNhan.Invoke(new MethodInvoker(delegate { listBox.Items.Add(benhNhan); }));
            }
        }

        private void btnGoiKham_Click(object sender, EventArgs e)
        {
            BenhNhan benhNhan = (BenhNhan)listBox.SelectedItem;
            txtMaSoBenhNhan.Text = benhNhan.msbn;
            txtHoTen.Text = benhNhan.hoten;
            txtCMND.Text = benhNhan.soCMND;
            txtDiaChi.Text = benhNhan.diaChi;
            listBox.Items.RemoveAt(listBox.SelectedIndex);
        }
    }
}
