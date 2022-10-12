using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;

namespace Receiver1
{
    public partial class Form1 : Form
    {
        private MessageQueue queue;
        public Form1()
        {
            InitializeComponent();
            init();
        }
        private void init()
        {
            Console.WriteLine("receiving message. Enter to exit.");
            //tạo connection factory
            IConnectionFactory factory = new
           ConnectionFactory("tcp://localhost:61616");
            //tạo connection
            IConnection con = factory.CreateConnection("admin", "admin");
            con.Start();//nối tới MOM
                        //tạo session
            ISession session = con.CreateSession(AcknowledgementMode.AutoAcknowledge);
            //tạo consumer
            ActiveMQQueue destination = new ActiveMQQueue("thanthidet");
            IMessageConsumer consumer = session.CreateConsumer(destination);
            //nhận mesage - lắng nghe

            consumer.Listener += Consumer_Listener;
        }

        private void Consumer_Listener(IMessage message)
        {
            if (message is IObjectMessage)
            {
                IObjectMessage obj = message as IObjectMessage;
                BenhNhan bn = (BenhNhan)obj.Body;
                Console.WriteLine(bn);
                SetText(bn);
            }
        }

       /* private void Queue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
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

        delegate void SetTextCallback(string text);*/
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
