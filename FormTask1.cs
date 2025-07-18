using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace HireMe
{
    public partial class FormTask1 : Form
    {
        public FormTask1()
        {
            InitializeComponent();

            label1.Text = "Развернутая строка";
            buttonCompress.Text = "Компрессия";

            label2.Text = "Свернутая строка";
            buttonDeCompress.Text = "Декомпрессия";
        }

        private void RemovePlaceholderText(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "Развернутая строка")
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
            }
        }

        private void buttonCompress_Click(object sender, EventArgs e)
        {
            textBox2.Text = CompressString(textBox1.Text);
        }

        private void buttonDeCompress_Click(object sender, EventArgs e)
        {
            textBox1.Text = DecompressString(textBox2.Text);
        }

        public string CompressString(string input)
        {
            if (string.IsNullOrEmpty(input)) // Проверка на пустую строку
            {
                return string.Empty;
            }

            int countChar = 1; // счётчик одинаковых подряд идущих символов
            char previousChar = input[0]; // предыдущий символ для сравнения
            StringBuilder output = new StringBuilder();
            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] == previousChar)
                {
                    countChar++;
                }
                else
                {
                    output.Append(previousChar);
                    if (countChar > 1)
                    {
                        output.Append(countChar);
                    }
                    previousChar = input[i];
                    countChar = 1; // сброс счётчика
                }
            }
                output.Append(previousChar);
                if (countChar > 1)
                {
                    output.Append(countChar);
                }

                return output.ToString();
        }

        public string DecompressString(string input)
        {
            if (string.IsNullOrEmpty(input)) // Проверка на пустую строку
            {
                return string.Empty;
            }
            StringBuilder output = new StringBuilder();
            int i = 0; // индекс текущего символа
            while (i < input.Length)
            {
                char currentChar = input[i];
                output.Append(currentChar);
                i++;
                if (i < input.Length && char.IsDigit(input[i]))
                {
                    int count = int.Parse(input[i].ToString());
                    for (int j = 1; j < count; j++)
                    {
                        output.Append(currentChar);
                    }
                    i++; // пропустить цифру
                }
            }
            return output.ToString();
        }


    }
}
