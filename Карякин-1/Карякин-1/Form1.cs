using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Карякин_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Rows.Add(2);
        }
        private void result_Click(object sender, EventArgs e)
        {
           double H = 0;
           double[] probability = new double[dataGridView1.RowCount];
           double sumProbabilities = 0;
           string x1; 
           string x2;
           for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < 2; j++)
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
                if (probability[i] == 0)
                {
                    textBox1.Text = H.ToString();
                    MessageBox.Show("Энтропия равна 0");
                    return;
                }
                sumProbabilities += probability[i];
            }
            x1 = dataGridView1[0, 0].Value.ToString();
            x2 = dataGridView1[0, 1].Value.ToString();
            if (x1 == x2)
            {
                MessageBox.Show("Названия переменных должны быть отличны друг от друга");
                return;
            }
            if (sumProbabilities == 1)
                for (int i = 0; i < dataGridView1.RowCount ; i++)
                {
                    H += -probability[i] * Math.Log(probability[i], 2);
                    textBox1.Text = Math.Round(H, 4).ToString();
                }
            else
            {
                MessageBox.Show("Сумма вероятностей должна быть равна единице.");
            }
        }
    }
}
