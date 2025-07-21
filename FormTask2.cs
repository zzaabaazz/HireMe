using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace HireMe
{
    public partial class FormTask2 : Form
    {
        public static class Server
        {
            private static int count = 0;
            public static readonly ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();

            public static int GetCount()
            {
                rwLock.EnterReadLock();
                try
                {
                    return count;
                }
                finally
                {
                    rwLock.ExitReadLock();
                }
            }

            public static void AddToCount(int value)
            {
                rwLock.EnterWriteLock();
                try
                {
                    count += value;
                }
                finally
                {
                    rwLock.ExitWriteLock();
                }
            }
        }

        public FormTask2()
        {
            InitializeComponent();
            buttonRead.Text = "GetCount";
            buttonWrite.Text = "AddToCount";
        }

        private async void buttonRead_Click(object sender, EventArgs e)
        {
            // Выполняем чтение в фоновом потоке, чтобы не блокировать UI
            int currentCount = await Task.Run(() => Server.GetCount());

            // Обновляем UI в основном потоке
            listBox1.Items.Insert(0, $"Read: {currentCount}");
        }

        private void buttonWrite_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int value))
            {
                // Выполняем запись в фоновом потоке
                Task.Run(() =>
                {
                    Server.AddToCount(value);
                    // Обновляем UI в основном потоке
                    this.Invoke(new Action(() =>
                    {
                        listBox1.Items.Insert(0, $"Write: {value}");
                    }));
                });
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректное число.",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
