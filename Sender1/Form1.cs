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
using System.Xml.Serialization;
using System.IO;

namespace Sender1
{
    public partial class Form1 : Form
    {
        MessageQueue quece = null;
        public Form1()
        {
            InitializeComponent();
            init();
        }
        private void init()
        {
            IConnectionFactory factory = new
ConnectionFactory("tcp://localhost:61616");
            //tạo connection
            IConnection con = factory.CreateConnection("admin", "admin");
            con.Start();//nối tới MOM
                        //tạo session
            ISession session = con.CreateSession(AcknowledgementMode.AutoAcknowledge);
            //tạo producer
            ActiveMQQueue destination = new ActiveMQQueue("thanthidet");
            IMessageProducer producer = session.CreateProducer(destination);

        }

        public class XMLObjectConverter<T>
        {
            public string object2XML(T p)
            {
                string xml = "";
                XmlSerializer ser = new XmlSerializer(typeof(T));
                using (MemoryStream ms = new MemoryStream())
                {
                    ser.Serialize(ms, p);
                    ms.Position = 0;
                    xml = new StreamReader(ms).ReadToEnd();
                }
                return xml;
            }
        }

        private void txtLuuThongTin_Click(object sender, EventArgs e)
        {
            string msbn = txtMaSoBenhNhan.Text;
            string socmnd = txtCMND.Text;
            string hoten = txtHoTen.Text;
            string diaChi = txtDiaChi.Text;

            BenhNhan benhNhan = new BenhNhan(msbn, socmnd, hoten, diaChi);
            // BenhNhan benhNhan = new BenhNhan("df", "df", "gdf", "fdf");

            IConnectionFactory factory = new
                 ConnectionFactory("tcp://localhost:61616");
            //tạo connection
            IConnection con = factory.CreateConnection("admin", "admin");
            con.Start();//nối tới MOM
                        //tạo session
            ISession session = con.CreateSession(AcknowledgementMode.AutoAcknowledge);
            //tạo producer
            IObjectMessage xml = session.CreateObjectMessage(benhNhan);
            ActiveMQQueue destination = new ActiveMQQueue("thanthidet");
            IMessageProducer producer = session.CreateProducer(destination);
            producer.Send(xml);

            //shutdown
            session.Close();
            con.Close();
            Console.WriteLine(xml);
        }
    }
}
