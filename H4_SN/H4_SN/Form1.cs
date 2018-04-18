/*type number and select template .txt will generate specified SN*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace H4_SN
{
    public partial class Form1 : Form
    {
        String lineline;
        String fmt = "00000.##";
        String filename;
        int SN = 1;
        int line_count;
        int time = 0;
        int data = 0;
        public Form1()
        {
            InitializeComponent();
        }

        String auto_path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        String path = @"C:\Users\gan\Desktop\H4_SN";
        

        private void button1_Click(object sender, EventArgs e)
        {
            String number = textBox3.Text;
            filename = textBox4.Text;
            SN = int.Parse(number);
            DirectoryInfo di = Directory.CreateDirectory(path);
            //Console.Write("start\r\n=====\r\n");
            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                label1.Text = openFileDialog1.FileName;
                
                //lineline = sr_txt.ReadLine();
                for (time = 1; time <= SN; time++)
                {
                    StreamReader sr_txt = new StreamReader(openFileDialog1.FileName);
                    line_count = 0;
                    int data_temp = 0;

                    while ((lineline = sr_txt.ReadLine()) != null)
                    {
                        //Console.WriteLine(lineline);
                        line_count++;
                        textBox1.Text += lineline;
                        textBox1.Text += "\r\n";
                        if (line_count == 75 || line_count == 77 || line_count == 79 || line_count == 81)
                        {
                            String addr = "";

                            switch (line_count)
                            {
                                case 75:    //千
                                    addr = "48"; data_temp = time / 1000;
                                    break;
                                case 77:    //百
                                    addr = "4A"; data_temp = (time / 100) % 10;
                                    break;
                                case 79:    //十
                                    addr = "4C"; data_temp = (time / 10) % 10;
                                    break;
                                case 81:    //個 
                                    addr = "4E"; data_temp = time % 10;
                                    break;
                            }
                            textBox2.Text += addr + "\t";
                            textBox2.Text += (data_temp + 30).ToString();
                            textBox2.Text += "\t00000000";
                            textBox2.Text += "\r\n";
                        }
                        else
                        {
                            textBox2.Text += lineline;
                            textBox2.Text += "\r\n";
                        }

                    }
                    System.IO.File.WriteAllText(@"C:\Users\gan\Desktop\H4_SN\" + filename+ time.ToString(fmt) + ".txt", textBox2.Text);
                    textBox2.Clear();
                }
                Console.WriteLine("There are {0} lines.",line_count);
                MessageBox.Show("DONE");
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String auto_path = Environment.GetFolderPath( Environment.SpecialFolder.DesktopDirectory);
            String path = @"C:\Users\gan\Desktop\testfolder";
            DirectoryInfo di = Directory.CreateDirectory(path);
            for (time = 0; time < 2; time++)
            {
                System.IO.File.WriteAllText(@"C:\Users\gan\Desktop\testfolder\test" + time + ".txt", textBox2.Text);
            }
        }
            
    }
}
