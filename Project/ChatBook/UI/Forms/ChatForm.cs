using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ChatBook.UI.Forms
{
    public partial class ChatForm : Form
    {
        private Dictionary<string, List<string>> chatMessages = new Dictionary<string, List<string>>();
        private string selectedChat = null;

        public ChatForm()
        {
            InitializeComponent();
            listBoxChats.KeyDown += listBoxChats_KeyDown; // Отслеживание нажатий клавиш
            listBoxChats.Click += listBoxChats_Click; // Один клик — выделение чата
            listBoxChats.DoubleClick += listBoxChats_DoubleClick; // Двойной клик — открытие чата
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMessage.Text) && selectedChat != null)
            {
                if (!chatMessages.ContainsKey(selectedChat))
                {
                    chatMessages[selectedChat] = new List<string>();
                }

                chatMessages[selectedChat].Add($"Вы: {txtMessage.Text}");
                listBoxMessages.Items.Add($"Вы: {txtMessage.Text}");
                txtMessage.Clear();
            }
        }

        private void txtMessage_TextChanged(object sender, EventArgs e)
        {
            const int maxHeight = 100; // Максимальная высота поля
            const int minHeight = 22;  // Минимальная высота поля

            using (Graphics g = txtMessage.CreateGraphics())
            {
                SizeF size = g.MeasureString(txtMessage.Text, txtMessage.Font, txtMessage.Width);
                int newHeight = Math.Min(maxHeight, Math.Max(minHeight, (int)size.Height + 10));

                txtMessage.Height = newHeight;
            }

            txtMessage.ScrollBars = txtMessage.Height >= maxHeight ? ScrollBars.Vertical : ScrollBars.None;
        }


        private void listBoxChats_Click(object sender, EventArgs e)
        {
            if (listBoxChats.SelectedItem != null)
            {
                selectedChat = listBoxChats.SelectedItem.ToString(); // Выделяем чат
            }
        }

        private void listBoxChats_DoubleClick(object sender, EventArgs e)
        {
            if (selectedChat != null && chatMessages.ContainsKey(selectedChat))
            {
                listBoxMessages.Items.Clear();
                foreach (var msg in chatMessages[selectedChat])
                {
                    listBoxMessages.Items.Add(msg);
                }
            }
        }

        private void listBoxChats_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && selectedChat != null)
            {
                chatMessages.Remove(selectedChat);
                listBoxChats.Items.Remove(selectedChat);
                selectedChat = null;
                listBoxMessages.Items.Clear();
            }
        }

        public void AddChat(string chatName)
        {
            if (!listBoxChats.Items.Contains(chatName))
            {
                listBoxChats.Items.Add(chatName);
                chatMessages[chatName] = new List<string>();
            }
        }
    }
}
