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
using System.Drawing.Printing;

namespace Barcode_Generator
{
    public partial class Form1 : Form
    {
       // Image image2;
       // Image image1;
        int state = 0;
        int initbar=0;
        String dirp = null;
        String Codedata ="";
        OnBarcode.Barcode.QRCode qrCode = new OnBarcode.Barcode.QRCode();
        OnBarcode.Barcode.Linear barcode = new OnBarcode.Barcode.Linear();
        bool qrflag = true;
        bool barflag = true;
        Bitmap bitmap;
        public Form1()  
        {
            InitializeComponent();
            barcode.Type = OnBarcode.Barcode.BarcodeType.CODE39;
            barcode.X = 2;
            barcode.Y = 80;
            qrCode.X = 4;
            radioButton1.Select();
            backgroundWorker1.WorkerReportsProgress = true;
            comboBox1.SelectedIndex = 0;
        }
        private void delqr()
        {
        


        }
        private void delbar()
        {
        
        


        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.SelectedPath.ToString() == "")
            {
                if (state == 0)
                {
                    System.Windows.Forms.MessageBox.Show("Please select folder to export file.", "warning",
    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else 
                {
                    System.Windows.Forms.MessageBox.Show("กรุณาเลือกตำแหน่งสำหรับบันทึกข้อมูล.", "warning",
    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                int num;
                dirp = folderBrowserDialog1.SelectedPath.ToString();
                if (!textBox1.Enabled)
                {
                    if (int.TryParse(textBox3.Text, out num) && int.TryParse(comboBox1.SelectedItem.ToString(), out num))
                    {
                        initbar = int.Parse(comboBox1.SelectedIndex.ToString());
                        backgroundWorker1.RunWorkerAsync();
                    }
                    else
                    {
                        if (state == 0)
                        {
                            System.Windows.Forms.MessageBox.Show("please type Combobox and Amount in Number only.", "warning",
    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("กรุณาระบุในกล่องเลือก และค่าจำนวนเป็นตัวเลขเท่านั้น.", "warning",
    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                      
                    }
                }
                else if (!textBox2.Enabled)
                {

                    backgroundWorker1.RunWorkerAsync();
                }
            }
        }

        private void delqrfile()
        {
         System.IO.File.Delete(@dirp+"\\QR-" + Codedata + ".jpg");
        }

        private void delbarfile()
        {
            System.IO.File.Delete(@dirp + "\\BAR-" + Codedata + ".jpg");
        }

        private void delm()
        {
            System.IO.File.Delete(@dirp + "\\CODE-" + Codedata + ".jpg");
        
        }
        private void mergebar()
        {
            int sizeofpic = 0;
            int qrpos = 0;
            int barpos = 0;
            if (int.Parse(Codedata.Length.ToString()) < 20)
            {
                sizeofpic = 750;
                qrpos = 325;
                barpos = 375 - int.Parse(Codedata.Length.ToString())*22;
            }
            else 
            {
                qrpos = 0;
                sizeofpic = (395/10) * int.Parse(Codedata.Length.ToString());
                qrpos = sizeofpic / 2 - 25;

            }
            Image image1 = Image.FromFile(@dirp + "\\QR-" + Codedata + ".jpg");
            Image image2 = Image.FromFile(@dirp + "\\BAR-" + Codedata + ".jpg");
             bitmap = new Bitmap(sizeofpic, 400);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                SolidBrush blueBrush = new SolidBrush(Color.White);
              
                float x = 0.0F;
                float y = 0.0F;
                
                float height = 400.0f;
                // Fill rectangle to screen.
                g.FillRectangle(blueBrush, x, y, sizeofpic, height);
                g.DrawImage(image1, qrpos, 10);
                g.DrawImage(image2, barpos, 250);
               
                String drawString = Codedata;
                
                Font drawFont = new Font("Arial", 10);
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                // Create point for upper-left corner of drawing.
                
                 y = 180.0F;
                 x = sizeofpic/2- int.Parse(Codedata.Length.ToString()) *2.8F;
                StringFormat drawFormat = new StringFormat();
                drawFormat.FormatFlags = StringFormatFlags.DisplayFormatControl;
              
                g.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
                bitmap.Save(@dirp + "\\CODE-"+Codedata+".jpg",System.Drawing.Imaging.ImageFormat.Jpeg);
                
            }

            pictureBox1.Image = bitmap;
            image1.Dispose();
            image2.Dispose();
        
        }

        private void generatebar()
        {
            try
            {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    if (!textBox1.Enabled)
                    {
                        Codedata = textBox2.Text + initbar;
                        barcode.Data = Codedata;
                        barcode.drawBarcode(@dirp + "\\BAR-" + Codedata + ".jpg");
                       
                       
                    }
                    else
                    {
                        Codedata = textBox1.Text.ToString();
                        barcode.Data = Codedata;
                        barcode.drawBarcode(@dirp + "\\BAR-" + Codedata + ".jpg");
                    }
                   
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

            
           

            if (!barflag)
            {
                
                label4.Text = "Please wait for processing";
                for (int i = 0; i < 10000; i++) ;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                System.IO.File.Delete(@dirp + "\\BAR-" + Codedata + ".jpg");
                generatebar();
            }
            label4.Text = "Status ok";
        }

        private void generateqr()
        {
            try
            {
               if (!textBox1.Enabled)
                {

                    Codedata = textBox2.Text + initbar;
                    qrCode.Data = Codedata;
                    qrCode.drawBarcode(@dirp + "\\QR-" + Codedata + ".jpg");
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
                else
                {
                    Codedata = textBox1.Text.ToString();
                    qrCode.Data = Codedata;
                    qrCode.drawBarcode(@dirp + "\\QR-" + Codedata + ".jpg");

                      GC.Collect();
                      GC.WaitForPendingFinalizers();

                }
                
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
            try
            {
                if (!qrflag)
                {

                    label4.Text = "Please wait for processing";
                    for (int i = 0; i < 10000; i++) ;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    System.IO.File.Delete(@dirp + "\\QR-" + Codedata + ".jpg");
                    generateqr();

                }
            }
            catch (Exception ex)
            {
                label4.ForeColor = Color.Red;
                label4.Text = ex.ToString();
            }
           // label4.Text = "Status ok";

        }
        private void checkwatermark1()
        {

            Bitmap bmp = new Bitmap(Image.FromFile(@dirp + "\\QR-" + Codedata + ".jpg"));
            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    Color now_color = bmp.GetPixel(j, i);
                    Color Match = Color.FromArgb(-65536);
                    if (now_color == Match)
                    {

                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        qrflag = false;
                        
                        i = bmp.Height;
                        j = bmp.Width;
                       
                        break;
                    }
                    else
                    {
                        qrflag = true;
                        
                    }

                }

            }
            bmp = null;
            if (qrflag == false)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
               // System.IO.File.Delete("C://barcodefolder//QR" + Codedata + ".jpg");
            }


        }
        private void checkwatermark2()
        {
           
                Bitmap bmp = new Bitmap(Image.FromFile(@dirp + "\\BAR-" + Codedata + ".jpg"));
                for (int i = 0; i < bmp.Height; i++)
                {
                    for (int j = 0; j < bmp.Width; j++)
                    {
                        Color now_color = bmp.GetPixel(j, i);
                        Color Match = Color.FromArgb(-65536);
                      if (now_color == Match)
                        {
                           
                            barflag = false;
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                          
                            i = bmp.Height;
                            j = bmp.Width;
                          
                            break;
                        }
                        else
                        {
                           barflag = true;
                         
                        }
                        
                    }

                }
                bmp = null;
                if (barflag == false)
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                  //  System.IO.File.Delete("C://barcodefolder//BAR" + Codedata + ".jpg");
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
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            comboBox1.Enabled = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            comboBox1.Enabled = false;
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
           label3.Text = folderBrowserDialog1.SelectedPath.ToString();
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
           
            if (!textBox1.Enabled)
            {
                int total = int.Parse(textBox3.Text);
                for (int i = 0; i < total; i++)
                {

                    int percents = (i * 100) / total;
                    backgroundWorker1.ReportProgress(percents);
                    generateqr();
                    generatebar();

                    mergebar();
                    if (!checkBox1.Checked)
                    { delqrfile(); }

                    if (!checkBox2.Checked)
                    { delbarfile(); }

                    if (!checkBox3.Checked)
                    { delm(); }
                    initbar++;

                }
            }
            else
            {
               
                generateqr();
                generatebar();

                mergebar();
                if (!checkBox1.Checked)
                { delqrfile(); }

                if (!checkBox2.Checked)
                { delbarfile(); }

                if (!checkBox3.Checked)
                { delm(); }
                initbar++;
            }
          
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
           progressBar1.Value = e.ProgressPercentage;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
        }

        private void Form1_ResizeBegin(object sender, EventArgs e)
        {
           
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(Form1.ActiveForm.Size.Width.ToString()) < 560)
                {
                    panel1.Location = new System.Drawing.Point(100,30);
                }
                else
                {
                    panel1.Location = new System.Drawing.Point((int.Parse(Form1.ActiveForm.Size.Width.ToString()) / 2) - (700 / 2), 30);
                }
            }
            catch (Exception ex)
            { }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (bitmap == null)
            {
                if (state == 0)
                    System.Windows.Forms.MessageBox.Show( "The code is not generate!","warning",
    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    System.Windows.Forms.MessageBox.Show("กรุณาสร้างโค้ดก่อนทำการพิมพ์", "คำเตือน",
    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                printDialog1.ShowDialog();
                printDocument1.PrintPage += new PrintPageEventHandler(pqr);
                printDocument1.DefaultPageSettings.Landscape = true;
                printDocument1.Print();
            }
        
        }
       

        void pqr(object o, PrintPageEventArgs e)
        {
            System.Drawing.Image i = bitmap;
            Point p = new Point(0, 100);
            e.Graphics.DrawImage(i, p);
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }

        private void aboutUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("This is Barcode Generator Version 1.0\n developed by Polasin Intaladchum");
        }

        private void howToUseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (state == 0)
            {
                System.Windows.Forms.MessageBox.Show("Auto mode > use for generate alot of code in one click.\n\n" +

                "Initial value > this value determine the constant text what would you like to include in your code. " +
                "\n\nAmount > Amount of auto increment number." +
                "\n\nCombobox > Using with Amount.\n e.g. Combobox->3 Amount->3 you will get barcode number 3 , 4 and 5"
                + "\n\n Code > Using whit Custom mode to set your barcode & qrcode value. "
                + "\n\nBrowse > Select your destination folder."
                + "\n\nCheckbox > Check for generate file in type."
                + "\n\nPrint this code > Print the current barcode & qrcode in Picture box.\n\nLanguage > to select language");
            }
            else 
            {
                System.Windows.Forms.MessageBox.Show("โหมด อัตโนมัติ > ใช้สำหรับสร้างโค้ดจำนวนมากใน 1 ครั้ง.\n\n" +

                "ค่าเริ่มต้น > เป็นค่าคงที่ที่จะรวมอยู่ใน Code ของคุณ. " +
                "\n\nจำนวน > จำนวนของการสร้าง Code โดยค่าของ Code จะเพิ่มขึ้นทีละ 1 ." +
                "\n\nกล่องหลายตัวเลือก > ใช้สิ่งนี้ร่วมกับจำนวน.\n เช่น กล่องหลายตัวเลือก->3 จำนวน->3 คุณจะได้โค้ด 3 อัน คือ 3 , 4 และ 5"
                + "\n\n โค้ด > ใช้สำหรับโหมดธรรมดาเพื่อตั้งค่าให้กับ บาร์โค้ดและคิวอาโค้ด. "
                + "\n\nเลือก > เลือกตำแหน่งเป้าหมายที่จะทำการบันทึกรูป."
                + "\n\nกล่องตัวเลือกทางขวามือ > เลือกเพื่อทำการบันทึกไฟล์แต่ละชนิดลงในตำแหน่งเป้าหมาย."
                + "\n\nพิมพ์ > จัดพิมพ์โค้ดที่ปรากฏอยู่ในหน้าต่างแสดงโค้ด.\n\nภาษา > เลือกภาษา");
            }
        }

        private void thaiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            state = 1;
            radioButton1.Text = "โหมด อัตโนมัติ";
            radioButton2.Text = "โหมด ธรรมดา";
            label5.Text = "ค่าเริ่มต้น";
            label6.Text = "จำนวน";
            label2.Text = "ตำแหน่งปลายทาง";
            button2.Text = "เลือกตำแหน่ง";
            button1.Text = "สร้าง";
            button3.Text = "พิมพ์หน้านี้";
            label1.Text = "รหัส";
            languageToolStripMenuItem.Text = "เลือกภาษา";
            helpToolStripMenuItem.Text = "ช่วยเหลือ";
            aboutUsToolStripMenuItem.Text = "เกี่ยวกับเรา";
            howToUseToolStripMenuItem.Text = "วิธีการใช้เบื้องต้น";
        }

        private void engToolStripMenuItem_Click(object sender, EventArgs e)
        {
            state = 0;
            radioButton1.Text = "Auto mode";
            radioButton2.Text = "Custom mode";
            label5.Text = "Initial value";
            label6.Text = "Amount";
            label2.Text = "Destination to export";
            button2.Text = "Borwse";
            button1.Text = "Generate";
            button3.Text = "Print this code";
            label1.Text = "Code";
            languageToolStripMenuItem.Text = "Language";
            helpToolStripMenuItem.Text = "Help";
            aboutUsToolStripMenuItem.Text = "About us";
            howToUseToolStripMenuItem.Text = "How to use";
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
