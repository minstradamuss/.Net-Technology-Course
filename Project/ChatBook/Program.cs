using System;
using System.Data.SQLite;
using System.Windows.Forms;
using ChatBook.UI.Forms;

namespace ChatBook
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Указываем строку подключения для SQLite
            string connectionString = "Data Source =C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\Project\\ChatBook\\DB\\ChatBook.db";

            // Создаем SQLite-соединение
            using (var connection = new SQLiteConnection(connectionString))
            {
                // Инициализируем контекст с этим соединением
                using (var db = new ChatBookDbContext(connection))
                {
                    // Инициализируем базу данных
                    db.Database.Initialize(force: false);
                }
            }

            // Запуск формы входа в приложение
            Application.Run(new LoginForm());
        }
    }
}
