using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ChatBook.Entities;
using ChatBook.Services;


namespace ChatBook.UI.Forms
{
    public partial class ChatForm : Form
    {
        private readonly UserService _userService;
        private readonly string _currentUserNickname;
        private Dictionary<string, List<Entities.Message>> chatMessages = new Dictionary<string, List<Entities.Message>>();
        private string selectedChat = null;
        private readonly string _chatPartnerNickname;

        public ChatForm(string currentUserNickname, UserService userService, string chatPartnerNickname = null)
        {
            InitializeComponent();
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
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

        public void LoadChatMessages(string chatPartnerNickname)
        {
            listBoxMessages.Items.Clear();
            var chatMessagesFromDb = _userService.GetChatMessages(_currentUserNickname, chatPartnerNickname);

            chatMessages[chatPartnerNickname] = chatMessagesFromDb;

            foreach (var msg in chatMessagesFromDb)
            {
                string sender = msg.SenderId == _userService.GetUserByNickname(_currentUserNickname).Id ? "Вы" : chatPartnerNickname;
                listBoxMessages.Items.Add($"({msg.Timestamp:T}) {sender}: {msg.Content}");
            }
        }


        private void LoadFriendsAndChats()
        {
            listBoxChats.Items.Clear();
            var friends = _userService.GetFriends(_currentUserNickname);
            var chatPartners = _userService.GetAllChatPartners(_currentUserNickname);

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

                var receiver = _userService.GetUserByNickname(receiverNickname);
                if (receiver == null)
                {
                    MessageBox.Show("Ошибка: получатель не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var newMessage = new Entities.Message
                {
                    SenderId = _userService.GetUserByNickname(_currentUserNickname).Id,
                    ReceiverId = receiver.Id,
                    Content = txtMessage.Text,
                    Timestamp = DateTime.UtcNow
                };

                _userService.SaveMessage(newMessage);

                if (!chatMessages.ContainsKey(selectedChat))
                {
                    chatMessages[selectedChat] = new List<Entities.Message>();

                }

                chatMessages[selectedChat].Add(newMessage);
                listBoxMessages.Items.Add($"Вы: {newMessage.Content}");
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
