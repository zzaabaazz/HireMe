using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HireMe
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            button1.Text = "Задание 1";
            button2.Text = "Задание 2";
            button3.Text = "Задание 3";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var myForm = new FormTask1();
            myForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var myForm = new FormTask2();
            myForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Используем фиксированные пути к файлам
            string inputFile = "input.log";
            string outputFile = "output.log";

            var task3 = new ClassTask3();
            task3.Run(inputFile, outputFile);
        }
    }
}
