using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using GMap.NET.MapProviders;
using System.Windows.Forms.DataVisualization.Charting;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace Lorayeni
{
    public partial class Form1 : Form
    {

        string[] ports = SerialPort.GetPortNames();

        long max = 100;
        long min = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            timer1.Start();

            if (serialPort1.IsOpen == false)
            {
                if (comboBox1.Text == "")
                    return;
                serialPort1.PortName = comboBox1.Text;
                serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text);
                try
                {
                    serialPort1.Open();
                    label6.Text = "Baglanti Kuruldu.";
                }
                catch (Exception hata)
                {

                    MessageBox.Show("Hata" + hata.Message);
                }
            }
            else
            {
                label6.Text = "Baglanti kuruldu.";
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PointLatLng point = new PointLatLng(39.792242, 30.510151);
            GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.red_pushpin);

            GMapOverlay markers = new GMapOverlay("markers");

            markers.Markers.Add(marker);
            map.Overlays.Add(markers);


            foreach (string port in ports)
            {
                comboBox1.Items.Add(port);
                comboBox1.SelectedIndex = 0;
            }
            comboBox2.Items.Add("4800");
            comboBox2.Items.Add("9600");
            comboBox2.Items.Add("115200");
            comboBox2.SelectedIndex = 1;
            label2.Text = "Bağlantı Kapalı";

        }

        private void button2_Click(object sender, EventArgs e)
        {

            timer1.Stop();
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Close();
                label6.Text = "Bağlantı Kapalı";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            map.DragButton = MouseButtons.Left;
            map.MapProvider = GMapProviders.GoogleMap;

            map1.DragButton = MouseButtons.Left;
            map1.MapProvider = GMapProviders.GoogleMap;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            map.MinZoom = 5;
            map.MaxZoom = 100;
            map.Zoom = 15;

            map1.MinZoom = 5;
            map1.MaxZoom = 100;
            map1.Zoom = 15;
            string sonuc;
            sonuc = serialPort1.ReadLine();
            string[] degisken = sonuc.Split('*');
            textBox5.Text = sonuc + "";

            if (sonuc.Split('*').Length > 2)
            {
                textBox3.Text = degisken[0].ToString();
                double lat = Convert.ToDouble(degisken[1].ToString().Replace(".", ","));
                double lng = Convert.ToDouble(degisken[2].ToString().Replace(".", ","));
                textBox1.Text = degisken[1].ToString();
                textBox2.Text = degisken[2].ToString();
                textBox6.Text = degisken[3].ToString();
                textBox5.Text = degisken[4].ToString();


                double hız = Convert.ToDouble(degisken[0]);

                textBox9.Text = degisken[3].ToString();
                // label17.Text = degisken[2].ToString();
                double lat1 = Convert.ToDouble(degisken[4].ToString().Replace(".", ","));
                double lng1 = Convert.ToDouble(degisken[5].ToString().Replace(".", ","));
                textBox7.Text = degisken[4].ToString();
                textBox8.Text = degisken[5].ToString();
                //  textBox6.Text = degisken[3].ToString();
                // textBox5.Text = degisken[4].ToString();


                double hız1 = Convert.ToDouble(degisken[5]);




                //   GMapOverlay polygons = new GMapOverlay("polygons");


                // GMapPolygon polygon = new GMapPolygon(points, "Jardin des Tuileries");
                //  polygons.Polygons.Add(polygon);
                //  map.Overlays.Add(polygons);

                //GMapOverlay polygons = new GMapOverlay("polygons");
                //List<PointLatLng> points = new List<PointLatLng>();

                //GMapPolygon polygon = new GMapPolygon(points, "Jardin des Tuileries");
                //polygons.Polygons.Add(polygon);
                //map.Overlays.Add(polygons);
                map.Position = new PointLatLng(lat, lng);

                // map1.Position = new PointLatLng(lat1, lng1);
                map1.Position = new PointLatLng(39.785997, 30.512952);
                //map.SetCurrentPositionByKeywords("Maputo, Mozambique");
                //map.Position = new PointLatLng(lat,lng);
                 
                    fartifagr.ChartAreas[0].AxisX.Minimum = min;
                    fartifagr.ChartAreas[0].AxisX.Maximum = max;    
                    fartifagr.ChartAreas[0].AxisY.Minimum = 0;
                    fartifagr.ChartAreas[0].AxisY.Maximum = 1000;
                    fartifagr.ChartAreas[0].AxisX.ScaleView.Zoom(min, max);
                    fartifagr.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0; //X ekseni için grid kaldır
                    fartifagr.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Number;
                    DateTime myDateValue = DateTime.Now;
                    textBox4.Text = myDateValue.ToString();

                    this.fartifagr.Series[0].Points.AddXY(myDateValue.ToString("d/M/yyyy HH:mm:ss"), hız);//HIZ DEĞİŞECEK.7


                   airtifagr.ChartAreas[0].AxisX.Minimum = min;
                   airtifagr.ChartAreas[0].AxisX.Maximum = max;
                   airtifagr.ChartAreas[0].AxisY.Minimum = 0;
                   airtifagr.ChartAreas[0].AxisY.Maximum = 350;
                   airtifagr.ChartAreas[0].AxisX.ScaleView.Zoom(min, max);
                   airtifagr.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0; //X ekseni için grid kaldır
                   airtifagr.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Number;


                    this.airtifagr.Series[0].Points.AddXY(myDateValue.ToString("d/M/yyyy HH:mm:ss"), hız);//HIZ DEĞİŞECEK


                    float yyy = (float)hız;
                    aGauge1.Value = yyy;

            }
            serialPort1.DiscardOutBuffer();
            serialPort1.DiscardInBuffer();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void map_MouseEnter(object sender, EventArgs e)
        {

            map.Size = new Size(1144, 743);
        }

        private void map_MouseLeave(object sender, EventArgs e)
        {
            map.Size = new Size(450, 311);
        }

        private void map1_MouseEnter(object sender, EventArgs e)
        {
            map1.Size = new Size(1144, 743);
        }

        private void map1_MouseLeave(object sender, EventArgs e)
        {
            map1.Size = new Size(450, 311);
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }


        /*  private void map2_MouseEnter(object sender, EventArgs e)
           {
               map2.Size = new Size(1144, 743);
               map2.Location = new Point(992, 101);*/



        /*  private void map2_MouseLeave(object sender, EventArgs e)
          {
              map2.Size = new Size(450, 311);
              map2.Location = new Point(1452, 101);*/

        /*     private void aGauge3_ValueInRangeChanged(object sender, ValueInRangeChangedEventArgs e)
             {

             }*/

        private void timer2_Tick(object sender, EventArgs e)
        {
            DateTime myDateValue = DateTime.Now;
            textBox4.Text = myDateValue.ToString();
        }
    }
}

