using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Карякин_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            textBox1.Clear();
            if (textBox2.Text == "" || textBox3.Text == "" || textBox3.Text == "" ||
               textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("Не все поля заполнены!");
                return;
            }
            textBox2.CharacterCasing = CharacterCasing.Lower;
            string firstLetter = textBox2.Text;
            string secondLetter = textBox4.Text;
            double firstProbability = double.Parse(textBox3.Text.ToString());
            double secondProbability = double.Parse(textBox5.Text.ToString());
            HashSet<string> s = new HashSet<string>();
            double[] combinationsP = { firstProbability, secondProbability };
            string[] combinationsL = { firstLetter, secondLetter };
            double H = 0;
            double[] q = new double[4];
            int j = 0;
            IList<double> comboP = new List<double>();
            IList<string> comboLetters = new List<string>();
            for (int i = 0; i < combinationsP.Length; i++)
            {
                if (firstProbability < 0 || secondProbability > 1 || 
                    firstProbability > 1 || secondProbability < 0)
                {
                    textBox1.Text = "";
                    MessageBox.Show("Вероятность события должна быть больше или равна нулю и не должна быть больше единицы.");
                    return;
                }
                if (firstLetter == secondLetter)
                {
                    MessageBox.Show("Названия переменных должны быть отличны друг от друга ");
                    textBox1.Text = "";
                    return;
                }
                if (firstProbability + secondProbability != 1)
                {
                    MessageBox.Show("Сумма вероятностей должна быть равна единице.");
                    textBox1.Text = "";
                    return;
                }
                for (int k = 0; k < combinationsP.Length; k++)
                {
                    q[j] = combinationsP[i] * combinationsP[k];
                    comboP.Add(q[j]);
                    j++;
                }
                j = 0;
                
            }
            for (int i = 0; i < combinationsL.Length; i++)
            {
                for (int l = 0; l < combinationsL.Length; l++)
                {
                    string combination3 =  combinationsL[i] + combinationsL[l];
                    comboLetters.Add(combination3);
                    H += -q[j] * Math.Log(q[j], 4);
                }
            }
            if (firstProbability == 0 || secondProbability == 0)
            {
                textBox1.Text = H.ToString();
                for (int w = 0; w < comboP.Count; w++)
                {
                    dataGridView1.Rows.Add(comboLetters.ElementAt(w), comboP.ElementAt(w));
                }
                H = 0;
                textBox1.Text = H.ToString();
                return;
            }
            else
            {
                for (int w = 0; w < comboP.Count; w++)
                {
                    dataGridView1.Rows.Add(comboLetters.ElementAt(w), comboP.ElementAt(w));
                }
                textBox1.Text = Math.Round(H, 4).ToString();
            }
            
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if((e.KeyChar <=47 || e.KeyChar >=58) && number !=8 && number !=44)
            {
                e.Handled = true;
            }
        }
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44)
            {
                e.Handled = true;
            }
        }










   

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

  
    }

       
        
    
}
