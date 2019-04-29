using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Карякин_2_1_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(dataGridView1.RowCount==0)
            {
                MessageBox.Show("Заполните таблицу данными!");
                return;
            }
            double H = 0;
            double[] probability = new double[dataGridView1.RowCount];
            int n = dataGridView1.RowCount;
            double sumProbabilities = 0;
            string letters1;
            string letters2;
            int y = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for(int j = 0; j < 2; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value == null)
                    {
                        MessageBox.Show("Не все поля заполнены!");
                        return;
                    }
                }
                if (!double.TryParse(dataGridView1[1, i].Value.ToString(), out probability[i]))
                {
                    MessageBox.Show("Должны быть введены только числа.");
                    return;
                }
                if (probability[i] < 0 || probability[i] > 1)
                {
                    MessageBox.Show("Вероятность события должна быть ни меньше нуля и ни больше единицы.");
                    return;
                }
                sumProbabilities += probability[i];
                if (sumProbabilities == 0)
                {
                    textBox1.Text = H.ToString();
                }
            }
            for (int j = 0; j < dataGridView1.RowCount;j++)
            {
               letters1 = dataGridView1[0,j].Value.ToString();
                y++;
                for(int w=y; w<dataGridView1.RowCount;w++)
                {
                    letters2 = dataGridView1[0,w].Value.ToString();
                    if (letters1 == letters2)
                    {
                        MessageBox.Show("Названия переменных должны быть отличны друг от друга!");
                        return; 
                    }
                } 
            }
            if (sumProbabilities == 1)
            {
                if (n == 1)
                {
                    H = 0;
                    textBox1.Text = Math.Round(H, 4).ToString();
                }
                else for (int i = 0; i < probability.Length; i++)
                {

                    if (probability[i] != 0)
                    {
                        H += -probability[i] * Math.Log(probability[i], n);
                        textBox1.Text = Math.Round(H, 4).ToString();
                    }

                }
            }
            else
            {
                MessageBox.Show("Сумма вероятностей должна быть равна единице.");
            }
        }
    }   
}
