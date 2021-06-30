using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace number_1
{   
    public partial class Form1 : Form
    {   
        private double usd_buy;
        private double usd_sell;
        private double eur_buy;
        private double eur_sell;
        private double rub_buy;
        private double rub_sell;
        public void DownloadInfo()
        {   
            string uri = "https://myfin.by/currency/minsk";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string str = " ", str1 = "";
            while (true)
            {   str = reader.ReadLine();

                if (str.Contains("Доллар США</a></td>"))
                {   
                    str1 = reader.ReadLine();
                    str1 = str1.Replace(" ", string.Empty);    // удаляем все пробелы
                    str1 = str1.Replace("<td>", string.Empty).Replace("</td>", string.Empty).Replace(".", ",");
                    usd_buy = double.Parse(str1);
                    textBox14.Text = str1;

                    str1 = reader.ReadLine();
                    str1 = str1.Replace(" ", string.Empty);    // удаляем все пробелы
                    str1 = str1.Replace("<td>", string.Empty).Replace("</td>", string.Empty).Replace(".", ",");
                    usd_sell = double.Parse(str1);
                    textBox15.Text = str1;
                }

                if (str.Contains(">Евро</a></td>"))
                {   
                    str1 = reader.ReadLine();
                    str1 = str1.Replace(" ", string.Empty);    // удаляем все пробелы
                    str1 = str1.Replace("<td>", string.Empty).Replace("</td>", string.Empty).Replace(".", ",");
                    eur_buy = double.Parse(str1);
                    textBox16.Text = str1;

                    str1 = reader.ReadLine();
                    str1 = str1.Replace(" ", string.Empty);    // удаляем все пробелы
                    str1 = str1.Replace("<td>", string.Empty).Replace("</td>", string.Empty).Replace(".", ",");
                    eur_sell = double.Parse(str1);
                    textBox17.Text = str1;
                }

                if (str.Contains(">Российский рубль<sup>100</sup></a></td>"))
                {   
                    str1 = reader.ReadLine();
                    str1 = str1.Replace(" ", string.Empty);    // удаляем все пробелы
                    str1 = str1.Replace("<td>", string.Empty).Replace("</td>", string.Empty).Replace(".", ",");
                    rub_buy = double.Parse(str1);
                    textBox18.Text = str1;

                    str1 = reader.ReadLine();
                    str1 = str1.Replace(" ", string.Empty);    // удаляем все пробелы
                    str1 = str1.Replace("<td>", string.Empty).Replace("</td>", string.Empty).Replace(".", ",");
                    rub_sell = double.Parse(str1);
                    textBox19.Text = str1;
                    textBox13.Text = DateTime.Now.ToLongDateString() + "   " + DateTime.Now.ToLongTimeString();

                    MessageBox.Show(" Данные по курсам валют успешно загружены !");
                    break;
                }
            }

        }

        public Form1()
        {   
            InitializeComponent();
            DownloadInfo();      
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {   // если НЕ ЧИСЛО            и  НЕ управляющий орган
            if (!char.IsDigit(e.KeyChar) && !(Char.IsControl(e.KeyChar)))
            {  // если НЕ запятая                   и
                if (!((e.KeyChar.ToString() == ",") &&
                    // ЕСТЬ запятая в тексте 
                    (textBox1.Text.IndexOf(",") == -1)))
                { // символ не обрабатывается
                    e.Handled = true; }
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {   // если НЕ ЧИСЛО            и  НЕ управляющий орган
            if (!char.IsDigit(e.KeyChar) && !(Char.IsControl(e.KeyChar)))
            {  // если НЕ запятая                  и
                if (!((e.KeyChar.ToString() == ",") &&
                    // ЕСТЬ запятая в тексте 
                    (textBox2.Text.IndexOf(",") == -1)))
                { // символ не обрабатывается
                    e.Handled = true;
                }
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {   // если НЕ ЧИСЛО            и  НЕ управляющий орган
            if (!char.IsDigit(e.KeyChar) && !(Char.IsControl(e.KeyChar)))
            {  // если НЕ запятая                  и
                if (!((e.KeyChar.ToString() == ",") &&
                    // ЕСТЬ запятая в тексте 
                    (textBox3.Text.IndexOf(",") == -1)))
                { // символ не обрабатывается
                    e.Handled = true;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        { 
            DownloadInfo();  
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            double now;
            if (textBox1.Text == "" || textBox1.Text == ",") now = 0;
            else now = double.Parse(textBox1.Text);

            if (textBox7.Text == "BYN") { textBox4.Text = Math.Round(now / usd_sell, 2).ToString(); }
            else { textBox4.Text = Math.Round(now * usd_buy, 2).ToString(); }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            double now;
            if (textBox2.Text == "" || textBox2.Text == ",") now = 0;
            else now = double.Parse(textBox2.Text);

            if (textBox8.Text == "BYN") { textBox5.Text = Math.Round(now / eur_sell, 2).ToString(); }
            else { textBox5.Text = Math.Round(now * eur_buy, 2).ToString(); }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            double now;
            if (textBox3.Text == "" || textBox6.Text == ",") now = 0;
            else now = double.Parse(textBox3.Text);

            if (textBox9.Text == "BYN") { textBox6.Text = Math.Round(now *100 / rub_sell, 2).ToString(); }
            else { textBox6.Text = Math.Round(now * rub_buy / 100, 2).ToString(); }
        }

        // обмен usd и byn
        private void button1_Click(object sender, EventArgs e)
        {
            string str1 = textBox7.Text,
                str2 = textBox10.Text;
            Color left = textBox7.BackColor,
                right = textBox10.BackColor;
            textBox7.Text = str2; textBox10.Text = str1;
            textBox7.BackColor = right;
            textBox10.BackColor = left;
            str2 = textBox4.Text; textBox1.Text = str2;
        }

        // обмен eur и byn
        private void button2_Click(object sender, EventArgs e)
        {
            string str1 = textBox8.Text,
                str2 = textBox11.Text;
            Color left = textBox8.BackColor,
                right = textBox11.BackColor;
            textBox8.Text = str2; textBox11.Text = str1;
            textBox8.BackColor = right;
            textBox11.BackColor = left;
            str2 = textBox5.Text; textBox2.Text = str2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string str1 = textBox9.Text,
                str2 = textBox12.Text;
            Color left = textBox9.BackColor,
                right = textBox12.BackColor;
            textBox9.Text = str2; textBox12.Text = str1;
            textBox9.BackColor = right;
            textBox12.BackColor = left;
            str2 = textBox6.Text; textBox3.Text = str2;
        }
    }
}