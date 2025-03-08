using System;
using System.Windows.Forms;

namespace ChatBook.UI.Forms
{
    public partial class ChatForm : Form
    {
        public ChatForm()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMessage.Text))
            {
                MessageBox.Show("Введите сообщение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            listBoxMessages.Items.Add($"Вы: {txtMessage.Text}");
            txtMessage.Clear();
        }
    }
}