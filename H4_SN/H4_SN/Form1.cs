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
        String number; //  loop times
        String SN_str;
        
        int line_count;
        int loop = 0;

        public Form1()
        {
            InitializeComponent();
        }
        
        


        private void button1_Click(object sender, EventArgs e)
        {
            number = textBox3.Text;
            number = "1";
            folder_path = desktop_path + @"\H4_SN";
            file_name = textBox4.Text;
            SN_str = textBox_SN_1.Text;


            //String[] SN_split_16 = new String[16];
            //for (int i = 0; i < 16; i++)
            //{
            //    SN_split_16[i] = SN_str.Substring(i, 1);
            //    Console.WriteLine(SN_split_16[i]);
            //}


            byte[] bytes = Encoding.ASCII.GetBytes(SN_str); // string to dec
            String hexVal = bytes[0].ToString("X");         // dec to hex

            String[] SN_split_16 = new String[16];
            for (int i = 0; i < 16; i++)
            {
                SN_split_16[i] = bytes[i].ToString("X");
                Console.Write((SN_split_16[i])+"\t");
            }

            //Console.WriteLine(bytes[0] + "\t" + SN_str + "\t" + Int32.Parse(hexVal));

            
            DirectoryInfo di = Directory.CreateDirectory(folder_path);

            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                label1.Text = openFileDialog1.FileName;
                
                //lineline = sr_txt.ReadLine();
                for (loop = 1; loop <= int.Parse(number); loop++)
                {
                    StreamReader sr_txt = new StreamReader(openFileDialog1.FileName);
                    line_count = 0;
                    

                    while ((lineline = sr_txt.ReadLine()) != null)
                    {
                        //Console.WriteLine(lineline);
                        line_count++;
                        textBox1.Text += lineline;
                        textBox1.Text += "\r\n";
                        if (line_count == 51 || line_count == 53 || line_count == 55 || line_count == 57 ||
                            line_count == 59 || line_count == 61 || line_count == 63 || line_count == 65 ||
                            line_count == 67 || line_count == 69 || line_count == 71 || line_count == 73 ||
                            line_count == 75 || line_count == 77 || line_count == 79 || line_count == 81)
                        {
                            String addr = "";
                            String data_str = "";
                     

                            switch (line_count)
                            {
                                //year
                                case 51:
                                    addr = "30"; data_str = SN_split_16[0];
                                    break;
                                    
                                //week
                                case 53:
                                    addr = "32"; data_str = SN_split_16[1];
                                    break;
                                case 55:
                                    addr = "34"; data_str = SN_split_16[2];
                                    break;

                                //foxlink
                                case 57:
                                    addr = "36"; data_str = SN_split_16[3];
                                    break;
                                //Arena Model Number
                                case 59:
                                    addr = "38"; data_str = SN_split_16[4];
                                    break;
                                case 61:
                                    addr = "3A"; data_str = SN_split_16[5];
                                    break;
                                case 63:
                                    addr = "3C"; data_str = SN_split_16[6];
                                    break;
                                case 65:
                                    addr = "3E"; data_str = SN_split_16[7];
                                    break;

                                //Arena
                                case 67:
                                    addr = "40"; data_str = SN_split_16[8];
                                    break;
                                case 69:
                                    addr = "42"; data_str = SN_split_16[9];
                                    break;
                                //flag

                                case 71:
                                    addr = "44"; data_str = SN_split_16[10];
                                    break;

                                //serial number
                                case 73:    //萬
                                    addr = "46"; data_str = SN_split_16[11];
                                    break;
                                case 75:    //千
                                    addr = "48"; data_str = SN_split_16[12];
                                    break;
                                case 77:    //百
                                    addr = "4A"; data_str = SN_split_16[13];
                                    break;
                                case 79:    //十
                                    addr = "4C"; data_str = SN_split_16[14];
                                    break;
                                case 81:    //個 
                                    addr = "4E"; data_str = SN_split_16[15];
                                    break;
                            }
                            textBox2.Text += addr + "\t";
                            textBox2.Text += data_str;
                            textBox2.Text += "\t00000000";
                            textBox2.Text += "\r\n";
                        }
                        else
                        {
                            textBox2.Text += lineline;
                            textBox2.Text += "\r\n";
                        }

                    }
                    //System.IO.File.WriteAllText(folder_path + @"\" + file_name + loop.ToString(fmt) + ".txt", textBox2.Text);
                    System.IO.File.WriteAllText(folder_path + @"\" + SN_str + ".txt", textBox2.Text);
                    textBox2.Clear();
                }
                Console.WriteLine("There are {0} lines.",line_count);
                //MessageBox.Show("DONE");
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
