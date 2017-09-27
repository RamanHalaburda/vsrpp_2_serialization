using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.IO;

namespace vsrpp_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<Vehicle> vehicles = new List<Vehicle>();

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        // add car
        private void button1_Click(object sender, EventArgs e)
        {
            Car car = new Car(tb1.Text, tb2.Text, Convert.ToUInt16(tb3.Text), Convert.ToUInt32(tb4.Text), Convert.ToUInt16(tb5.Text), tb6.Text);
            vehicles.Add(car);
            PrintVehicles();
        }
        
        // add ship
        private void button2_Click(object sender, EventArgs e)
        {
            Ship ship = new Ship(tb7.Text, tb8.Text, Convert.ToUInt16(tb9.Text), Convert.ToUInt32(tb10.Text), Convert.ToUInt16(tb11.Text), tb12.Text, tb13.Text, Convert.ToUInt16(tb14.Text));
            vehicles.Add(ship);
            PrintVehicles();
        }

        // add plane
        private void button3_Click(object sender, EventArgs e)
        {
            Plane plane = new Plane(tb15.Text, tb16.Text, Convert.ToUInt16(tb17.Text), Convert.ToUInt32(tb18.Text), Convert.ToUInt16(tb19.Text), tb20.Text, Convert.ToUInt16(tb21.Text), Convert.ToUInt16(tb22.Text));
            vehicles.Add(plane);
            PrintVehicles();
        }

        private void PrintVehicles()
        {
            PrintList.Text = String.Empty;

            foreach (Vehicle v in vehicles)
            {
                PrintList.Text += v.Print();
                PrintList.Text += System.Environment.NewLine;
            }
        }

        // binary serialize
        private void button4_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream("BinaryList.dat", FileMode.OpenOrCreate))
            {
                foreach (Vehicle v in vehicles)
                {
                    formatter.Serialize(fs, v);
                    Console.WriteLine("Объект сериализован");
                }                                
            }
        }

        // binary deserialize
        private void button5_Click(object sender, EventArgs e)
        {
            vehicles.Clear();
            BinaryFormatter formatter = new BinaryFormatter();
            // десериализация из файла
            using (FileStream fs = new FileStream("BinaryList.dat", FileMode.Open))
            {
                while (fs.Position != fs.Length)
                {
                    vehicles.Add((Vehicle)formatter.Deserialize(fs));
                } 
                PrintVehicles();
                Console.WriteLine("объект десериализован");
            }
        }

        // XML serialize
        private void button7_Click(object sender, EventArgs e)
        {
            SoapFormatter sp = new SoapFormatter();
            using (FileStream fs = new FileStream("XMLList.soap", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                foreach (Vehicle v in vehicles)
                {
                    sp.Serialize(fs, v);
                }
            }
        }

        // XML deserialize
        private void button6_Click(object sender, EventArgs e)
        {
            vehicles.Clear();
            SoapFormatter sf = new SoapFormatter();
            // десериализация из файла
            using (FileStream fs = new FileStream("XMLList.soap", FileMode.Open))
            {
                while (fs.Position != fs.Length)
                {
                    vehicles.Add((Vehicle)sf.Deserialize(fs));
                }
                
                //vehicles = (List<Vehicle>)sp.Deserialize(fs);

                //var veh = (List<Vehicle>)sp.Deserialize(fs);

                //Vehicle[] veh = (Vehicle[])sf.Deserialize(fs);
                //foreach (Vehicle v in veh)
                //{
                //    vehicles.Add(v);
                //}

                PrintVehicles();
                Console.WriteLine("объект десериализован");
            }
        }
    }
}
