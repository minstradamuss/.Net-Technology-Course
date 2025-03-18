using System;
using System.Windows.Forms;

namespace ChatBook.UI.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox pictureBoxAvatar;
        private System.Windows.Forms.Label lblNickname;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Button btnEditProfile;
        private System.Windows.Forms.Button btnSearchBooks;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.NumericUpDown numericRating;
        private System.Windows.Forms.TextBox txtReview;
        private System.Windows.Forms.Button btnSaveReview;
        private System.Windows.Forms.ComboBox cmbStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pictureBoxAvatar = new System.Windows.Forms.PictureBox();
            this.lblNickname = new System.Windows.Forms.Label();
            this.lblFullName = new System.Windows.Forms.Label();
            this.btnEditProfile = new System.Windows.Forms.Button();
            this.btnSearchBooks = new System.Windows.Forms.Button();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.numericRating = new System.Windows.Forms.NumericUpDown();
            this.txtReview = new System.Windows.Forms.TextBox();
            this.btnSaveReview = new System.Windows.Forms.Button();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.buttonChats = new System.Windows.Forms.Button();
            this.buttonSearchFriends = new System.Windows.Forms.Button();
            this.btnAddBook = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAvatar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRating)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxAvatar
            // 
            this.pictureBoxAvatar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxAvatar.Location = new System.Drawing.Point(20, 20);
            this.pictureBoxAvatar.Name = "pictureBoxAvatar";
            this.pictureBoxAvatar.Size = new System.Drawing.Size(128, 117);
            this.pictureBoxAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxAvatar.TabIndex = 0;
            this.pictureBoxAvatar.TabStop = false;
            // 
            // lblNickname
            // 
            this.lblNickname.AutoSize = true;
            this.lblNickname.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblNickname.Location = new System.Drawing.Point(163, 26);
            this.lblNickname.Name = "lblNickname";
            this.lblNickname.Size = new System.Drawing.Size(102, 24);
            this.lblNickname.TabIndex = 1;
            this.lblNickname.Text = "Nickname";
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Font = new System.Drawing.Font("Arial", 10F);
            this.lblFullName.Location = new System.Drawing.Point(163, 63);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(81, 19);
            this.lblFullName.TabIndex = 2;
            this.lblFullName.Text = "Full Name";
            // 
            // btnEditProfile
            // 
            this.btnEditProfile.Location = new System.Drawing.Point(400, 20);
            this.btnEditProfile.Name = "btnEditProfile";
            this.btnEditProfile.Size = new System.Drawing.Size(198, 30);
            this.btnEditProfile.TabIndex = 4;
            this.btnEditProfile.Text = "Редактировать профиль";
            this.btnEditProfile.Click += new System.EventHandler(this.btnEditProfile_Click);
            // 
            // btnSearchBooks
            // 
            this.btnSearchBooks.Location = new System.Drawing.Point(400, 79);
            this.btnSearchBooks.Name = "btnSearchBooks";
            this.btnSearchBooks.Size = new System.Drawing.Size(120, 30);
            this.btnSearchBooks.TabIndex = 6;
            this.btnSearchBooks.Text = "Поиск книг";
            this.btnSearchBooks.Click += new System.EventHandler(this.btnSearchBooks_Click);
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.Location = new System.Drawing.Point(0, 0);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(121, 24);
            this.comboBoxStatus.TabIndex = 0;
            // 
            // numericRating
            // 
            this.numericRating.Location = new System.Drawing.Point(0, 0);
            this.numericRating.Name = "numericRating";
            this.numericRating.Size = new System.Drawing.Size(120, 22);
            this.numericRating.TabIndex = 0;
            // 
            // txtReview
            // 
            this.txtReview.Location = new System.Drawing.Point(0, 0);
            this.txtReview.Name = "txtReview";
            this.txtReview.Size = new System.Drawing.Size(100, 22);
            this.txtReview.TabIndex = 0;
            // 
            // btnSaveReview
            // 
            this.btnSaveReview.Location = new System.Drawing.Point(0, 0);
            this.btnSaveReview.Name = "btnSaveReview";
            this.btnSaveReview.Size = new System.Drawing.Size(75, 23);
            this.btnSaveReview.TabIndex = 0;
            // 
            // cmbStatus
            // 
            this.cmbStatus.Location = new System.Drawing.Point(0, 0);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(121, 24);
            this.cmbStatus.TabIndex = 0;
            // 
            // buttonChats
            // 
            this.buttonChats.Location = new System.Drawing.Point(526, 79);
            this.buttonChats.Name = "buttonChats";
            this.buttonChats.Size = new System.Drawing.Size(120, 30);
            this.buttonChats.TabIndex = 7;
            this.buttonChats.Text = "Чаты";
            this.buttonChats.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonSearchFriends
            // 
            this.buttonSearchFriends.Location = new System.Drawing.Point(652, 79);
            this.buttonSearchFriends.Name = "buttonSearchFriends";
            this.buttonSearchFriends.Size = new System.Drawing.Size(120, 30);
            this.buttonSearchFriends.TabIndex = 8;
            this.buttonSearchFriends.Text = "Друзья";
            this.buttonSearchFriends.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnAddBook
            // 
            this.btnAddBook.Location = new System.Drawing.Point(12, 166);
            this.btnAddBook.Name = "btnAddBook";
            this.btnAddBook.Size = new System.Drawing.Size(198, 30);
            this.btnAddBook.TabIndex = 9;
            this.btnAddBook.Text = "Добавить книгу";
            // 
            // MainForm
            // 
            this.BackColor = System.Drawing.Color.AntiqueWhite;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(782, 553);
            this.Controls.Add(this.btnAddBook);
            this.Controls.Add(this.buttonSearchFriends);
            this.Controls.Add(this.buttonChats);
            this.Controls.Add(this.pictureBoxAvatar);
            this.Controls.Add(this.lblNickname);
            this.Controls.Add(this.lblFullName);
            this.Controls.Add(this.btnEditProfile);
            this.Controls.Add(this.btnSearchBooks);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Профиль пользователя";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAvatar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRating)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private Button buttonChats;
        private Button buttonSearchFriends;
        private Button btnAddBook;
    }
}
