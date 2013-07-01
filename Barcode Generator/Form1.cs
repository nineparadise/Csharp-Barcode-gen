using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OnBarcode.Barcode.WinForms;
using System.Configuration;

namespace Barcode_Generator
{
    public partial class Form1 : Form
    {
        Image image2;
        Image image1;
        int initbar=0;
        int initqr = 0;
        String Codedata ="";
        bool flag = true;
        public Form1()  
        {
            InitializeComponent();
            radioButton1.Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            image1 = null;
            image2 = null;
            pictureBox1.Image = image1;
            pictureBox2.Image = image2;
            
            generatebar();
            generateqr();
           
        }
        private void generatebar()
        {
            try
            {
                
                    
                    image2 = null;
                   
                    pictureBox2.Image = image2;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    OnBarcode.Barcode.Linear barcode = new OnBarcode.Barcode.Linear();
                    barcode.Type = OnBarcode.Barcode.BarcodeType.CODE39;
                    barcode.X = 1;
                    barcode.Y = 80;




                    if (textBox1.Enabled)
                    {
                        Codedata = textBox1.Text.ToString();
                        barcode.Data = Codedata;
                        barcode.drawBarcode("C://barcodefolder//Bar" + Codedata + ".png");
                       
                    }
                    else
                    {
                        Codedata = "000000000000" + initbar;
                        barcode.Data = Codedata;
                        barcode.drawBarcode("C://barcodefolder//Bar" + Codedata + ".png");
                       
                    }

                   
                    image2 = Image.FromFile("C://barcodefolder//Bar" + Codedata + ".png");
                    pictureBox2.Image = image2;
                    initbar++;
                    checkwatermark2();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    label4.ForeColor = Color.Green;
                    label4.Text = "status OK";

                  
          

            }
            catch(Exception ex)
            {
                label4.ForeColor = Color.Red;
                label4.Text = ex.ToString();
            }
            
        }

        private void generateqr()
        {
            try
            {

                image1 = null;
               
                pictureBox1.Image = image1;
             
                GC.Collect();
                GC.WaitForPendingFinalizers();
                OnBarcode.Barcode.QRCode qrCode = new OnBarcode.Barcode.QRCode();
                qrCode.X = 4;
                if (textBox1.Enabled)
                {
                    Codedata = textBox1.Text.ToString();
                    qrCode.Data = Codedata;
                    qrCode.drawBarcode("C://barcodefolder//QR" + Codedata + ".jpg");
                }
                else
                {
                    Codedata = "000000000000" + initqr;
                    qrCode.Data = Codedata;
                      qrCode.drawBarcode("C://barcodefolder//QR" + Codedata + ".jpg");

                }

                image1 = Image.FromFile("C://barcodefolder//QR" + Codedata + ".jpg");
                initqr++;
                checkwatermark1();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                label4.ForeColor = Color.Green;
                label4.Text = "status OK";
                


            }
            catch (Exception ex)
            {
                label4.ForeColor = Color.Red;
                label4.Text = ex.ToString();
            }

        }

        private void checkwatermark1()
        {  
            GC.Collect();
            GC.WaitForPendingFinalizers();
            image1 = Image.FromFile("C://barcodefolder//QR" + Codedata + ".jpg");
            Bitmap bmp = new Bitmap(image1);
          
            try
            {
                for (int i = 0; i < bmp.Height; i++)
                {
                    for (int j = 0; j < bmp.Width; j++)
                    {
                        Color now_color = bmp.GetPixel(j, i);
                        Color Match = Color.FromArgb(-65536);
                        //Find Watermark
                        if (now_color == Match)
                        {
                            label5.Text = "match";
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                            flag = false;
                            image1 = null;
                            pictureBox1.Image = null;
                            System.IO.File.Delete("C://barcodefolder//QR" + Codedata + ".jpg");
                            i = bmp.Height;
                            j = bmp.Width;

                        }
                        else {
                            flag = true;
                            label5.Text = "No Match"; 
                        }
                    }


                }
                if (flag == false)
                {
                    initqr--;

                    generateqr();
                }
                else
                {
                    image1 = Image.FromFile("C://barcodefolder//QR" + Codedata + ".jpg");
                    pictureBox1.Image = image1;
                
                }
            }


            catch (Exception ex)
            {
                label4.ForeColor = Color.Red;
                label4.Text = "The lastest process still working \nPlease wait a moment and try to generate again\n" + ex.ToString();
                // flag = false;
                // break;
            }

        }
        private void checkwatermark2()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers(); 
            image2 = Image.FromFile("C://barcodefolder//Bar" + Codedata + ".png");
            Bitmap bmp = new Bitmap( image2 );
           
            try
            {
                for (int i = 0; i < bmp.Height; i++)
                {
                    for (int j = 0; j < bmp.Width; j++)
                    {
                        Color now_color = bmp.GetPixel(j, i);
                        Color Match = Color.FromArgb(-65536);
                        //find watermark
                        if (now_color == Match)
                        {
                            label5.Text = "match";
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                            image2 = null;
                            flag = false;
                            System.IO.File.Delete("C://barcodefolder//BAR" + Codedata + ".jpg");
                            pictureBox2.Image = image2;
                            i = bmp.Height;
                            j = bmp.Width;
                        }
                        else
                        {
                            flag = true;
                            label5.Text = "No Match";
                        }

                    }

                }
                if (flag == false)
                {
                    initbar--;
                    generatebar();
                }
                else
                {
                    image2 = Image.FromFile("C://barcodefolder//Bar" + Codedata + ".png");
                    pictureBox2.Image = image2;
                }
            }
            catch (Exception ex)
            {
                label4.ForeColor = Color.Red;
                label4.Text = "The lastest process still working \nPlease wait a moment and try to generate again\n" + ex.ToString();
                // flag = false;
                // break;
            }
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            this.Text = folderBrowserDialog1.SelectedPath;
        }

        private void label3_Click(object sender, EventArgs e)
        {
         
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
          
            //backgroundWorker1.Dispose();
        }
    }
}
