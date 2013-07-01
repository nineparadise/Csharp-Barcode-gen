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
        int count = 0;
        String Codedata ="";
        int i = 0;
        public Form1()
        {
            InitializeComponent();
            radioButton1.Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            generate();
            count = 0;

          

        }
        private void generate()
        {
            try
            {
                backgroundWorker1.WorkerSupportsCancellation = true;
                backgroundWorker1.RunWorkerAsync();

            }
            catch
            {}
            
        }

        private void checkwatermark1()
        {



            GC.Collect();
            GC.WaitForPendingFinalizers();
            image1 = Image.FromFile("C://barcodefolder//Qr" + Codedata + ".jpg");
            Bitmap bmp = new Bitmap(image1);

            Boolean flag = true;

            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {

                    Color now_color = bmp.GetPixel(j, i);

                    Color Match = Color.FromArgb(-65536);


                    //here  you  will find more practical 
                    if (now_color == Match)
                    {
                       
                        label5.Text = "match";
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        flag = false;
                        image1 = null;
                        pictureBox1.Image = image1;
                        try
                        {
                            System.IO.File.Delete("C://barcodefolder//Qr" + Codedata + ".jpg");
                        }
                        catch
                        { button1.PerformClick(); }
                        break;
                       
                    }
                    else { label5.Text = "No Match"; }
                   

                }

            }
            if (flag == false)
            {

                try
                {
                   

                    GC.Collect();
                    GC.WaitForPendingFinalizers();


                    OnBarcode.Barcode.Linear barcode = new OnBarcode.Barcode.Linear();
                    barcode.Type = OnBarcode.Barcode.BarcodeType.CODE39;
                    barcode.X = 1;
                    barcode.Y = 80;



                    OnBarcode.Barcode.QRCode qrCode = new OnBarcode.Barcode.QRCode();

                    qrCode.X = 4;


                    if (textBox1.Enabled)
                    {
                        Codedata = textBox1.Text.ToString();
                        qrCode.Data = Codedata;
                        // barcode.Data = Codedata;
                        //  System.IO.File.Delete("C://barcodefolder//Bar" + Codedata + ".png");
                        System.IO.File.Delete("C://barcodefolder//QR" + Codedata + ".jpg");
                        // barcode.drawBarcode("C://barcodefolder//Bar" + Codedata + ".png");
                        qrCode.drawBarcode("C://barcodefolder//QR" + Codedata + ".jpg");
                    }
                    else
                    {
                        Codedata = "000000000000" + i;
                        qrCode.Data = Codedata;
                        // barcode.Data = Codedata;
                        // System.IO.File.Delete("C://barcodefolder//Bar" + Codedata + ".png");
                        System.IO.File.Delete("C://barcodefolder//QR" + Codedata + ".jpg");
                        // barcode.drawBarcode("C://barcodefolder//Bar" + Codedata + ".png");
                        qrCode.drawBarcode("C://barcodefolder//QR" + Codedata + ".jpg");

                    }

                    image1 = Image.FromFile("C://barcodefolder//Qr" + Codedata + ".jpg");
                    image2 = Image.FromFile("C://barcodefolder//Bar" + Codedata + ".png");
                    pictureBox1.Image = image1;
                    pictureBox2.Image = image2;
                    checkwatermark1();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();



                    label4.ForeColor = Color.Green;
                    label4.Text = "status OK";

                }
                catch (Exception ex)
                {
                    label4.ForeColor = Color.Red;
                    label4.Text = "The lastest process still working \nPlease wait a moment and try to generate again\n" + ex.ToString();


                    button1.PerformClick();


                }

            }

            image1 = Image.FromFile("C://barcodefolder//Qr" + Codedata + ".jpg");
            image2 = Image.FromFile("C://barcodefolder//Bar" + Codedata + ".png");
            pictureBox1.Image = image1;
            pictureBox2.Image = image2;
           

        }
        private void checkwatermark2()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers(); 
            
            image2 = Image.FromFile("C://barcodefolder//Bar" + Codedata + ".png");
            Bitmap bmp = new Bitmap( image2 );
            bool flag = true;

            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {

                    Color now_color = bmp.GetPixel(j, i);

                    Color Match = Color.FromArgb(-65536);


                    //here  you  will find more practical 
                    if (now_color == Match)
                    {
                        label5.Text = "match";
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        image2 = null;
                        pictureBox2.Image = image2;
                        try
                        {
                            System.IO.File.Delete("C://barcodefolder//Bar" + Codedata + ".png");
                        }
                        catch { button1.PerformClick(); }
                        flag = false;
                        break;
                    }
                    else {
                       
                        label5.Text = "No Match";
                       
                    }

                }

            }
            if (flag == false)
            {
                try
                {
                    image1 = null;
                    image2 = null;
                    pictureBox1.Image = image1;
                    pictureBox2.Image = image2;

                    GC.Collect();
                    GC.WaitForPendingFinalizers();


                    OnBarcode.Barcode.Linear barcode = new OnBarcode.Barcode.Linear();
                    barcode.Type = OnBarcode.Barcode.BarcodeType.CODE39;
                    barcode.X = 1;
                    barcode.Y = 80;



                    OnBarcode.Barcode.QRCode qrCode = new OnBarcode.Barcode.QRCode();

                    qrCode.X = 4;


                    if (textBox1.Enabled)
                    {
                        Codedata = textBox1.Text.ToString();
                        qrCode.Data = Codedata;
                        barcode.Data = Codedata;
                        System.IO.File.Delete("C://barcodefolder//Bar" + Codedata + ".png");
                        //   System.IO.File.Delete("C://barcodefolder//QR" + Codedata + ".jpg");
                        barcode.drawBarcode("C://barcodefolder//Bar" + Codedata + ".png");
                        // qrCode.drawBarcode("C://barcodefolder//QR" + Codedata + ".jpg");
                    }
                    else
                    {
                        Codedata = "000000000000" + i;
                        qrCode.Data = Codedata;
                        barcode.Data = Codedata;
                        System.IO.File.Delete("C://barcodefolder//Bar" + Codedata + ".png");
                        // System.IO.File.Delete("C://barcodefolder//QR" + Codedata + ".jpg");
                        barcode.drawBarcode("C://barcodefolder//Bar" + Codedata + ".png");
                        //  qrCode.drawBarcode("C://barcodefolder//QR" + Codedata + ".jpg");

                    }

                    image1 = Image.FromFile("C://barcodefolder//Qr" + Codedata + ".jpg");
                    image2 = Image.FromFile("C://barcodefolder//Bar" + Codedata + ".png");
                    pictureBox1.Image = image1;
                    pictureBox2.Image = image2;
                    checkwatermark2();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();



                    label4.ForeColor = Color.Green;
                    label4.Text = "status OK";

                }
                catch (Exception ex)
                {
                    label4.ForeColor = Color.Red;
                    label4.Text = "The lastest process still working \nPlease wait a moment and try to generate again\n" + ex.ToString();

                    button1.PerformClick();

                }
            }
            image1 = Image.FromFile("C://barcodefolder//Qr" + Codedata + ".jpg");
            image2 = Image.FromFile("C://barcodefolder//Bar" + Codedata + ".png");
            pictureBox1.Image = image1;
            pictureBox2.Image = image2;
          
           
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
            try
            {
                image1 = null;
                image2 = null;
                pictureBox1.Image = image1;
                pictureBox2.Image = image2;

                GC.Collect();
                GC.WaitForPendingFinalizers();
              

                OnBarcode.Barcode.Linear barcode = new OnBarcode.Barcode.Linear();
                barcode.Type = OnBarcode.Barcode.BarcodeType.CODE39;
                barcode.X = 1;
                barcode.Y = 80;
            


                OnBarcode.Barcode.QRCode qrCode = new OnBarcode.Barcode.QRCode();

                qrCode.X = 4;


                if (textBox1.Enabled)
                {
                    Codedata = textBox1.Text.ToString();
                    qrCode.Data = Codedata;
                    barcode.Data = Codedata;
                    System.IO.File.Delete("C://barcodefolder//Bar" + Codedata + ".png");
                    System.IO.File.Delete("C://barcodefolder//QR" + Codedata + ".jpg");
                    barcode.drawBarcode("C://barcodefolder//Bar" + Codedata + ".png");
                    qrCode.drawBarcode("C://barcodefolder//QR" + Codedata + ".jpg");
                }
                else
                {
                    Codedata = "000000000000" + i;
                    qrCode.Data = Codedata;
                    barcode.Data = Codedata;
                    System.IO.File.Delete("C://barcodefolder//Bar" + Codedata + ".png");
                    System.IO.File.Delete("C://barcodefolder//QR" + Codedata + ".jpg");
                    barcode.drawBarcode("C://barcodefolder//Bar" + Codedata + ".png");
                    qrCode.drawBarcode("C://barcodefolder//QR" + Codedata + ".jpg");

                }

                image1 = Image.FromFile("C://barcodefolder//Qr" + Codedata + ".jpg");
                image2 = Image.FromFile("C://barcodefolder//Bar" + Codedata + ".png");
                pictureBox1.Image = image1;
                pictureBox2.Image = image2;
                checkwatermark1(); 
                checkwatermark2();
                GC.Collect();
                GC.WaitForPendingFinalizers();
              


                label4.ForeColor = Color.Green;
                label4.Text = "status OK";
                i++;
            }
            catch (Exception ex)
            {
                label4.ForeColor = Color.Red;
                label4.Text = "The lastest process still working \nPlease wait a moment and try to generate again\n" + ex.ToString();
              
                    
                    button1.PerformClick();
                    backgroundWorker1.CancelAsync();
              

            }
            backgroundWorker1.Dispose();
        }
    }
}
