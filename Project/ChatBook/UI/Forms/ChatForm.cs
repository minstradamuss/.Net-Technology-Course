using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ChatBook.Domain.Interfaces;
using ChatBook.Entities;
using ChatBook.Services;
using ChatBook.ViewModels;
using ChatService.Domain;

namespace ChatBook.UI.Forms
{
    public partial class ChatForm : Form
    {
        private readonly string _currentUserNickname;
        private Dictionary<string, List<Entities.Message>> chatMessages = new Dictionary<string, List<Entities.Message>>();
        private string selectedChat = null;
        private readonly string _chatPartnerNickname;
        private readonly IChatService _chatService;
        private readonly ChatViewModel _viewModel;

        public ChatForm(string currentUserNickname, ChatViewModel userService, IChatService chatService, string chatPartnerNickname = null)
        {
            InitializeComponent();
            _viewModel = userService ?? throw new ArgumentNullException(nameof(userService));
            _chatService = chatService ?? throw new ArgumentNullException(nameof(chatService));
            _currentUserNickname = currentUserNickname ?? throw new ArgumentNullException(nameof(currentUserNickname));

            LoadFriendsAndChats();

            if (chatPartnerNickname != null)
            {
                selectedChat = chatPartnerNickname;
                LoadChatMessages(selectedChat);
            }

            listBoxChats.KeyDown += listBoxChats_KeyDown;
            listBoxChats.Click += listBoxChats_Click;
            listBoxChats.DoubleClick += listBoxChats_DoubleClick;
        }

        private List<string> WrapMessage(string message, int maxLineLength)
        {
            var words = message.Split(' ');
            var lines = new List<string>();
            string currentLine = "";

            foreach (var word in words)
            {
                if ((currentLine + word).Length > maxLineLength)
                {
                    lines.Add(currentLine.TrimEnd());
                    currentLine = "";
                }
                currentLine += word + " ";
            }

            if (!string.IsNullOrWhiteSpace(currentLine))
                lines.Add(currentLine.TrimEnd());

            return lines;
        }


        public void LoadChatMessages(string chatPartnerNickname)
        {
            listBoxMessages.Items.Clear();
            var chatMessagesFromDb = _viewModel.GetChatMessages(_currentUserNickname, chatPartnerNickname);

            chatMessages[chatPartnerNickname] = chatMessagesFromDb;

            foreach (var msg in chatMessagesFromDb)
            {
                var user = _viewModel.GetUserByNickname(_currentUserNickname) as User;
                if (user == null) return; // или обработка ошибки

                bool isOwnMessage = msg.SenderId == user.Id;
                string senderLabel = isOwnMessage ? "Вы" : chatPartnerNickname;

                var wrappedLines = WrapMessage(msg.Content, 60); // 60 — количество символов в строке

                for (int i = 0; i < wrappedLines.Count; i++)
                {
                    string prefix = (i == 0) ? $"{senderLabel}: " : "        "; // отступ только на первых строках
                    listBoxMessages.Items.Add(prefix + wrappedLines[i]);
                }
            }
        }



        private void LoadFriendsAndChats()
        {
            listBoxChats.Items.Clear();
            var friends = _viewModel.GetFriends(_currentUserNickname);
            var chatPartners = _viewModel.GetAllChatPartners(_currentUserNickname);

            HashSet<string> uniqueUsers = new HashSet<string>();

            foreach (var friend in friends)
            {
                string displayName = $"{friend.Nickname} - {friend.FirstName} {friend.LastName}";
                if (uniqueUsers.Add(friend.Nickname))
                {
                    listBoxChats.Items.Add(displayName);
                }
            }

            foreach (var chatPartner in chatPartners)
            {
                string displayName = $"{chatPartner.Nickname} - {chatPartner.FirstName} {chatPartner.LastName}";
                if (uniqueUsers.Add(chatPartner.Nickname))
                {
                    listBoxChats.Items.Add(displayName);
                }
            }
        }


        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMessage.Text) && selectedChat != null)
            {
                string receiverNickname = selectedChat.Split('-')[0].Trim();

                var receiver = _viewModel.GetUserByNickname(receiverNickname);
                if (receiver == null)
                {
                    MessageBox.Show("Ошибка: получатель не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var newMessage = new Entities.Message
                {
                    SenderId = _viewModel.GetUserByNickname(_currentUserNickname).Id,
                    ReceiverId = receiver.Id,
                    Content = txtMessage.Text,
                    Timestamp = DateTime.UtcNow
                };

                _viewModel.SaveMessage(newMessage);

                if (!chatMessages.ContainsKey(selectedChat))
                {
                    chatMessages[selectedChat] = new List<Entities.Message>();

                }

                chatMessages[selectedChat].Add(newMessage);
                var lines = WrapMessage(newMessage.Content, 60); // 60 символов максимум в строке
                for (int i = 0; i < lines.Count; i++)
                {
                    string prefix = i == 0 ? "Вы: " : "     ";
                    listBoxMessages.Items.Add(prefix + lines[i]);
                }

                txtMessage.Clear();
            }
        }


        private void listBoxChats_Click(object sender, EventArgs e)
        {
            if (listBoxChats.SelectedItem != null)
            {
                selectedChat = listBoxChats.SelectedItem.ToString().Split('-')[0].Trim();
            }
        }


        private void listBoxChats_DoubleClick(object sender, EventArgs e)
        {
            if (selectedChat != null)
            {
                string chatPartnerNickname = selectedChat.Split('-')[0].Trim();
                LoadChatMessages(chatPartnerNickname);
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

        private void txtMessage_TextChanged(object sender, EventArgs e)
        {
            const int maxHeight = 100;
            const int minHeight = 22;

            using (Graphics g = txtMessage.CreateGraphics())
            {
                SizeF size = g.MeasureString(txtMessage.Text, txtMessage.Font, txtMessage.Width);
                int newHeight = Math.Min(maxHeight, Math.Max(minHeight, (int)size.Height + 10));

                txtMessage.Height = newHeight;
            }

            txtMessage.ScrollBars = txtMessage.Height >= maxHeight ? ScrollBars.Vertical : ScrollBars.None;
        }
    }
}
