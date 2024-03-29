﻿using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace HelloWorldEmguCVProject
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        //Create image
        private void button1_Click(object sender, EventArgs e)
        {
            Image<Bgr, Byte> img1 = new Image<Bgr, Byte>(320, 240, new Bgr(255, 0, 0));
            pictureBox1.Image = img1.ToBitmap();


        }
        //Add Existing Image
        private void button2_Click(object sender, EventArgs e)
        {
            string strFileName = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image<Bgr, Byte> img1 = new Image<Bgr, Byte>(ofd.FileName);
                pictureBox1.Image = img1.ToBitmap();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Creating an image
            Image<Bgr, Byte> img1 = new Image<Bgr, Byte>(320, 240, new Bgr(255, 0, 0));
            //yellow(0,255,255)
            Byte b1 = 255;
            Bgr yellow = new Bgr(0, 255, 255);
            //Change the color by iterating Data
         /*   for (int i = 20; i < 60; i++)
            {
                for (int j = 20; j < 60; j++)
                {
                    img1.Data[i, j, 0] = 0;
                    img1.Data[i, j, 1] = b1;
                    img1.Data[i, j, 2] = b1;
                }
            }*/
            //Change the color by setting an Bgr color
           /* for (int i = 120; i < 160; i++)
            {
                for (int j = 20; j < 60; j++)
                {
                    img1[i, j] = yellow;
                }
            }*/


            //The best practice to reduce performance penalties
            byte[,,] data = img1.Data;
            for (int i = 30; i < 160; i++)
            {
                for (int j = 100; j < 190; j++)
                {
                    //Avoid using c# property inside a loop can have a huge performance boost
                    data[i, j, 0] = 0;
                    data[i, j, 1] = b1;
                    data[i, j, 2] = b1;
                }
            }
            //Show the result
            pictureBox1.Image = img1.ToBitmap();
        }

        //Using operator overload
        private void button4_Click(object sender, EventArgs e)
        {
            Image<Bgr, Byte> imgBlue = new Image<Bgr, Byte>(320, 240, new Bgr(255, 0, 0));
            Image<Bgr, Byte> imgGreen = new Image<Bgr, Byte>(320, 240, new Bgr(0, 0, 255));
            Image<Bgr, Byte> imgRed = new Image<Bgr, byte>(320, 240, new Bgr(0, 0, 255));
            // Blue + green + red + white
            //Operatirs Overload.
            Image<Bgr, Byte> Img1 = imgBlue + imgGreen + imgRed;
            pictureBox1.Image = Img1.ToBitmap();
        }

        //Generic operations support
        private void button5_Click(object sender, EventArgs e)
        {
            Image<Gray, Byte> imgGray = new Image<Gray, Byte>(@"C:\Users\anamarquez\source\Repass\emguCVOpenCV\HelloWorldEmguCVProject\HelloWorldEmguCVProject\imgprogram.jpg");
            Image<Gray, Single> img1 = imgGray.Convert<Single>(delegate (Byte b) {
                return (Single)Math.Sin(b * b / 255.0); }
            );
            pictureBox1.Image = imgGray.ToBitmap();
        }
        //XML serialization
        private void button6_Click(object sender, EventArgs e)
        {
            string strFileName = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                Image<Bgr, Byte> img1 = new Image<Bgr, Byte>(ofd.FileName);
                pictureBox1.Image = img1.ToBitmap();

                //Convert an Image to XLMDocument
                StringBuilder sb1 = new StringBuilder();
                (new XmlSerializer(typeof(Image<Bgr, Byte>))).Serialize(new StringWriter(sb1), img1);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(sb1.ToString());
                //save the XML file
                xmlDoc.Save("image.xml");

            }
        }
        //DesXML serialization
        private void button7_Click(object sender, EventArgs e)
        {

            string strFileName = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            XmlDocument xmlDoc = new XmlDocument();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                xmlDoc.Load(ofd.FileName);
                Image<Bgr, Byte> img1 = (Image<Bgr, Byte>)
                (new XmlSerializer(typeof(Image<Bgr, Byte>))).Deserialize(new XmlNodeReader(xmlDoc));
                pictureBox1.Image = img1.ToBitmap();
            }
        }
    }
    
}
