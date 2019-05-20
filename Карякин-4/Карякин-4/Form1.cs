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
            if (n == 0)
            {
                MessageBox.Show("Длина слова должна быть больше нуля!");
                return;
            }
            char[] alp = new char[dataGridView2.RowCount];
            string[] alphabet = new string[dataGridView2.RowCount];
            int p = 1;
            double combinCount = Math.Pow(alp.Length, n);
            int co = Convert.ToInt32(combinCount);
            double H = 0;
            double[] probability = new double[dataGridView2.RowCount];
            double sumProbabilities = 0;
            string letters1;
            string letters2;
            int y = 0;
            string str = "";
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
            if (combinCount == 1)
            {
                H = 0;
                textBox3.Text = Math.Round(H, 4).ToString();
            }
            AllCombinationsOfSymbols(alphabet, n,str,comboLetters);// где alphabet-кол-во строк; n-длина комбинаций;str-строка с будущей комбинацией;comboLetters-лист
            AllCombinationsOfProbabl(probability, n,p,comboP);
            for (int r = 0; r < comboLetters.Count; r++)
            {
                dataGridView1.Rows.Add(comboLetters.ElementAt(r), comboP.ElementAt(r));
                H += -comboP.ElementAt(r) * Math.Log(comboP.ElementAt(r), combinCount);
            }
            textBox3.Text = Math.Round(H, 4).ToString();                                                                                                                                                                                         // dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
        }
        private void AllCombinationsOfSymbols(string[] alphabet, int n, string str, List<string> comboLetters)
        {
            string[] interCombin = new string[n];
            CombinationsOfSymbols(interCombin, alphabet, 0, n, str, comboLetters);
        }
        private void CombinationsOfSymbols(string[] interCombin, string[] alphabet, int index, int n, string str,List<string> comboLetters)
        {
             if (index == n)
            {
                for (int i = 0; i < n; i++)
                {
                    str += interCombin[i];
                }
                comboLetters.Add(str);
                return;
            }
            for (int i = 0; i < alphabet.Length; i++)
            {
                interCombin[index] = alphabet[i];
                CombinationsOfSymbols(interCombin, alphabet, index+1 , n, str,comboLetters);
            }
        }
        private void AllCombinationsOfProbabl(double[] probability, int n, double p, List<double> comboP)
        {
            double[] interProbabl = new double[n];
            CombinationsOfProbabl(interProbabl, probability, 0, n, p, comboP);
        }
        private void CombinationsOfProbabl(double[] interProbabl, double[] probability, int index, int n, double p, List<double> comboP)
        {
            if (index == n)
            {
                for (int i = 0; i < n; i++)
                {
                    p *= interProbabl[i];
                }
                comboP.Add(p);
                return;
            }
            for (int i = 0; i < probability.Length; i++)
            {
                interProbabl[index] = probability[i];
                CombinationsOfProbabl(interProbabl, probability, index + 1, n, p,comboP);
            }
            return;
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
