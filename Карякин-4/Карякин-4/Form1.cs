using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Карякин_4
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
            textBox3.Clear();
            if (dataGridView2.RowCount == 0)
            {
                MessageBox.Show("Заполните таблицу исходных данных!");
                return;
            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("Заполните поле 'Длина слов'!");
                return;
            }
            int n = int.Parse(textBox2.Text.ToString());
            char[] alp = new char[dataGridView2.RowCount];
            string[] alphabet = new string[dataGridView2.RowCount];
            double combinCount = Math.Pow(alp.Length, n);
            double H = 0;
            double[] probability = new double[dataGridView2.RowCount];
            double sumProbabilities = 0;
            string letters1;
            string letters2;
            int y = 0;
            List<double> comboP = new List<double>();
            List<string> comboLetters = new List<string>();
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (dataGridView2.Rows[i].Cells[j].Value == null)
                    {
                        MessageBox.Show("Таблица исходных данных заполнена не полностью!");
                        return;
                    }
                }
                alphabet[i] = dataGridView2.Rows[i].Cells[0].Value.ToString();
                if (!double.TryParse(dataGridView2[1, i].Value.ToString(), out probability[i]))
                {
                    MessageBox.Show("Должны быть введены только числа.");
                    return;
                }
                if (probability[i] < 0 || probability[i] > 1)
                {
                    MessageBox.Show("Вероятность события должна быть ни меньше нуля и ни больше единицы!");
                    return;
                }
                sumProbabilities += probability[i];
                Math.Round(sumProbabilities, 1);
            }
            for (int m = 0; m < dataGridView2.RowCount; m++)
            {
                letters1 = dataGridView2[0, m].Value.ToString();
                y++;
                for (int w = y; w < dataGridView2.RowCount; w++)
                {
                    letters2 = dataGridView2[0, w].Value.ToString();
                    if (letters1 == letters2)
                    {
                        MessageBox.Show("Назания переменных должны быть отличны друг от друга!");
                        return;
                    }
                }
            }
            if (sumProbabilities != 1)
            {
                MessageBox.Show("Сумма вероятностей должна быть равна единице.");
                return;
            }
            for (int c = 0; c < combinCount; c++)
            {
                string combLetters = "";
                int ic = c;
                double multiP = 1;
                for (int j = n - 1; j >= 0; j--)
                {
                    int index = ic % probability.Length;
                    ic /= alp.Length;
                    combLetters += alphabet[index];
                    multiP *= probability[index];
                    
                }
                string combLetter = new string(combLetters.Reverse().ToArray());
                comboLetters.Add(combLetter);
               
                comboP.Add(multiP);
                if (multiP != 0)
                {
                    if (combinCount == 1)
                    {
                        H = 0;
                        textBox3.Text = Math.Round(H, 4).ToString();
                    }
                    else
                    {
                        H += -multiP * Math.Log(multiP, combinCount);
                    }
                }
            }
            for (int r = 0; r < comboLetters.Count; r++)
            {
                dataGridView1.Rows.Add(comboLetters.ElementAt(r), comboP.ElementAt(r));
            }
            
            textBox3.Text = Math.Round(H, 4).ToString();
            //dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
        }
        
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) & e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
        private void numericUpDown1_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow rows = new DataGridViewRow();
                int a = (int)numericUpDown1.Value;
                dataGridView2.RowCount = (int)a;

            }
            catch
            {
            }
        }
       
    }

}
