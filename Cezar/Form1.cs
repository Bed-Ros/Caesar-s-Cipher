using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cezar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Предварительные приготовления
            textBox3.Text = "";
            //Проверка на отсутствия текста
            if (textBox1.Text != " ")
            {
                //Проверка на отсутствия ключа
                if (textBox2.Text != " ")
                {
                    double[] rus_tablica = new double[]  {0.062, 0.014, 0.038, 0.013,
                    0.025, 0.072, 0.007, 0.016, 0.062, 0.01, 0.028, 0.035, 0.026, 0.053,
                    0.09, 0.023, 0.04, 0.045, 0.053, 0.021, 0.002, 0.009, 0.003, 0.012,
                    0.006, 0.003, 0.014, 0.016, 0.014, 0.003, 0.006, 0.018 };

                    double[] frequency = new double[32];
                    int[] kolvo_bukv = new int[32];

                    int x, b, key, alph_max, sum_kolvo_bukv;
                    char ascii_min, ascii_max;
                    StringBuilder text = new StringBuilder();

                    //Выбор языка
                    //Русский без Ё (по умолчанию)            
                    ascii_min = 'А';
                    ascii_max = 'Я';
                    alph_max = 32;
                    //Английский
                    if (radioButton5.Checked)
                    {
                        ascii_min = 'A';
                        ascii_max = 'Z';
                        alph_max = 26;
                    }

                    //Ввод данных                    
                    text.Append(textBox1.Text.ToUpper());
                    key = Convert.ToInt32(textBox2.Text) % alph_max;
                    //Замена Ё на Е
                    for (int i = 0; i < text.Length; i++)
                    {
                        if (text[i] == 'Ё')
                        {
                            text[i] = 'Е';
                        }
                    }

                    //Расшифровка
                    b = 0;
                    if (radioButton1.Checked)
                    {
                        for (int i = 0; i < text.Length; i++)
                        {
                            if (((int)text[i] >= (int)ascii_min) && ((int)text[i] <= (int)ascii_max))
                            {
                                //Разделение по 5
                                if (b % 5 == 0 && b !=0)
                                {
                                    textBox3.Text += " ";
                                }
                                b++;
                                
                                x = (int)text[i] - key;
                                if (x < (int)ascii_min) x += (int)alph_max;
                                textBox3.Text += (char)x;
                            }
                            

                        }
                    }
                    //Шифрование 
                    b = 0;
                    if (radioButton2.Checked)
                    {
                        for (int i = 0; i < text.Length; i++)
                        {
                            if (((int)text[i] >= (int)ascii_min) && ((int)text[i] <= (int)ascii_max))
                            {
                                //Разделение по 5
                                if (b % 5 == 0 && b != 0)
                                {
                                    textBox3.Text += " ";
                                }
                                b++;

                                x = (int)text[i] + key;
                                if (x > (int)ascii_max) x -= (int)alph_max;
                                textBox3.Text += (char)x;
                            }
                            

                        }
                    }
                    //Взлом
                    if (radioButton3.Checked)
                    {
                        //Русский без Ё (по умолчанию)            
                        ascii_min = 'А';
                        ascii_max = 'Я';
                        alph_max = 32;
                        //Обнуление массива
                        for (int i = 0; i < alph_max; i++)
                        {
                            kolvo_bukv[i] = 0;
                        }
                        //Считаем количество каждой буквы
                        for (int i = 0; i < text.Length; i++)
                        {
                            if (((int)text[i] >= (int)ascii_min) && ((int)text[i] <= (int)ascii_max))
                            {
                                kolvo_bukv[(int)text[i] - (int)ascii_min]++;
                            }
                        }
                        //Считаем общее количество букв
                        sum_kolvo_bukv = 0;
                        for (int i = 0; i < alph_max; i++)
                        {
                            sum_kolvo_bukv += kolvo_bukv[i];
                        }
                        //Считаем относительную вероятность каждой буквы
                        for (int i = 0; i < alph_max; i++)
                        {
                            frequency[i] = (double)kolvo_bukv[i] / (double)sum_kolvo_bukv;
                        }
                        double min = 1000;
                        double z = 0;

                        for (int j = 0; j < alph_max; j++)
                        {
                            for (int i = 0; i < alph_max; i++)
                            {
                                int q = i + j;
                                if (q >= alph_max) { q -= alph_max; }
                                z += (frequency[q] - rus_tablica[i]) * (frequency[q] - rus_tablica[i]);
                            }
                            if (z <= min)
                            {
                                min = z;
                                key = j;
                            }
                            z = 0;
                        }
                        textBox2.Text = Convert.ToString(key);
                        //Расшифровка
                        b = 0;
                        for (int i = 0; i < text.Length; i++)
                        {                            
                            if (((int)text[i] >= (int)ascii_min) && ((int)text[i] <= (int)ascii_max))
                            {
                                //Разделение по 5
                                if (b % 5 == 0 && b != 0)
                                {
                                    textBox3.Text += " ";
                                }
                                b++;

                                x = (int)text[i] - key;
                                if (x < (int)ascii_min) x = x + (int)alph_max;
                                textBox3.Text += (char)x;
                            }
                            

                        }
                    }

                }
                else
                {
                    textBox3.Text += "Введите ключ";
                }
            }
            else
            {
                textBox3.Text += "Введите текст";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Очистить все поля
            textBox1.Text = " ";
            textBox2.Text = " ";
            textBox3.Text = " ";
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Ввод только 5 цифр и Backspace в поле ключа
            if ((!Char.IsDigit(e.KeyChar) || textBox2.Text.Length >= 5) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        //Изменение интерфейса, при нажатии на radioButton'ы
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            button1.Text = "Зашифровать";
            panel2.Visible = true;
            label6.Visible = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            button1.Text = "Расшифровать";
            panel2.Visible = true;
            label6.Visible = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            button1.Text = "Взломать";
            panel2.Visible = false;
            label6.Visible = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Блокировка печати в вывод
            e.Handled = true;
        }
    }
}
