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
        String file_name;
        String desktop_path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        String folder_path;
        String number;
        int SN = 1;
        int line_count;
        int loop = 0;

        public Form1()
        {
            InitializeComponent();
        }
        
        


        private void button1_Click(object sender, EventArgs e)
        {
            number = textBox3.Text;
            folder_path = desktop_path + @"\H4_SN";
            file_name = textBox4.Text;
            SN = int.Parse(number);

            DirectoryInfo di = Directory.CreateDirectory(folder_path);

            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                label1.Text = openFileDialog1.FileName;
                
                //lineline = sr_txt.ReadLine();
                for (loop = 1; loop <= SN; loop++)
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
                                    addr = "48"; data_temp = loop / 1000;
                                    break;
                                case 77:    //百
                                    addr = "4A"; data_temp = (loop / 100) % 10;
                                    break;
                                case 79:    //十
                                    addr = "4C"; data_temp = (loop / 10) % 10;
                                    break;
                                case 81:    //個 
                                    addr = "4E"; data_temp = loop % 10;
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
                    System.IO.File.WriteAllText(folder_path + @"\" + file_name + loop.ToString(fmt) + ".txt", textBox2.Text);
                    textBox2.Clear();
                }
                Console.WriteLine("There are {0} lines.",line_count);
                MessageBox.Show("DONE");
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1. Edit filename\n2. Edit times\n3. Press SELECT TEMPLATE");
        }
    }
}
