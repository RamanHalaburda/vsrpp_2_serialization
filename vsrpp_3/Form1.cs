using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

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
            if (vehicles == null) { return; }

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(typeof(Vehicle));
                using (MemoryStream stream = new MemoryStream())
                {
                    foreach (Vehicle v in vehicles)
                    {
                        serializer.Serialize(stream, v);
                    }
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save("xmllist.xml");
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                //Log exception here
            }
            /*
            XmlSerializer formatter = new XmlSerializer(typeof(Vehicle));

            using (FileStream fs = new FileStream("XMLList.xml", FileMode.OpenOrCreate))
            {
                foreach (Vehicle v in vehicles)
                {
                    formatter.Serialize(fs, v);
                }
            }
            */

            //SoapFormatter sp = new SoapFormatter();
            //using (FileStream fs = new FileStream("SoapList.soap", FileMode.OpenOrCreate))
            //{
            //    
            //}
        }

        // XML deserialize
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load("xmllist.xml");
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(Vehicle);

                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        while (reader.EOF)
                        {
                            vehicles.Add((Vehicle)serializer.Deserialize(reader));
                        }
                        reader.Close();
                    }

                    read.Close();
                }
            }
            catch (Exception ex)
            {
                //Log exception here
            }

            PrintVehicles();
            /*
            XmlSerializer formatter = new XmlSerializer(typeof(Vehicle));

            using (FileStream fs = new FileStream("XMLList.xml", FileMode.Open))
            {
                while (fs.Position != fs.Length)
                {
                    vehicles.Add((Vehicle)formatter.Deserialize(fs));
                }
            }
            */

            /*
            vehicles.Clear();
            SoapFormatter sf = new SoapFormatter();
            // десериализация из файла
            using (FileStream fs = new FileStream("SoapList.soap", FileMode.Open))
            {
                fs.Position = 0;

                while (fs.Position != fs.Length)
                {
                    vehicles.Add((Vehicle)sf.Deserialize(fs));
                }

                //vehicles = (List<Vehicle>)sf.Deserialize(fs);

                //var veh = (List<Vehicle>)sf.Deserialize(fs);

                //Vehicle[] veh = (Vehicle[])sf.Deserialize(fs);
                //foreach (Vehicle v in veh)
                //{
                //    vehicles.Add(v);
                //}

                PrintVehicles();
                Console.WriteLine("объект десериализован"); 
            }
                */

            //vehicles.Clear();
            //    SoapFormatter formatter = new SoapFormatter();
            //    // десериализация из файла
            //    using (FileStream fs = new FileStream("SoapList.soap", FileMode.Open))
            //    {
            //        while (fs.Position != fs.Length)
            //        {
            //            vehicles.Add((Vehicle)formatter.Deserialize(fs));
            //        }
            //        PrintVehicles();
            //        Console.WriteLine("объект десериализован");
            //    }

        }

        public void SerializeObject<T>(T serializableObject, string fileName)
        {
            if (serializableObject == null) { return; }

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(serializableObject.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, serializableObject);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(fileName);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                //Log exception here
            }
        }

        public T DeserializeObject<T>(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) { return default(T); }

            T objectOut = default(T);

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(T);

                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        objectOut = (T)serializer.Deserialize(reader);
                        reader.Close();
                    }

                    read.Close();
                }
            }
            catch (Exception ex)
            {
                //Log exception here
            }

            return objectOut;
        }
    }
}
