using System;
using System.Windows.Forms;

namespace ChatBook.UI.Forms
{
    public partial class SearchForm : Form
    {
        public SearchForm()
        {
            InitializeComponent();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchQuery = txtSearch.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                MessageBox.Show("Введите название книги для поиска.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            listBoxResults.Items.Clear();
            listBoxResults.Items.Add($"Результат 1 для \"{searchQuery}\"");
            listBoxResults.Items.Add($"Результат 2 для \"{searchQuery}\"");
            listBoxResults.Items.Add($"Результат 3 для \"{searchQuery}\"");
        }
    }
}
