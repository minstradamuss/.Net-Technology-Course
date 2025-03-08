using System.Windows.Forms;

namespace ChatBook.UI.Forms
{
    partial class EditProfileForm
    {
        private System.ComponentModel.IContainer components = null;
        private PictureBox pictureBoxAvatar;
        private Button btnUploadAvatar;
        private TextBox txtFirstName;
        private TextBox txtLastName;
        private TextBox txtPhone;
        private Button btnSave;
        private Label lblFirstName;
        private Label lblLastName;
        private Label lblPhone;

        private void InitializeComponent()
        {
            this.pictureBoxAvatar = new PictureBox();
            this.btnUploadAvatar = new Button();
            this.txtFirstName = new TextBox();
            this.txtLastName = new TextBox();
            this.txtPhone = new TextBox();
            this.btnSave = new Button();
            this.lblFirstName = new Label();
            this.lblLastName = new Label();
            this.lblPhone = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAvatar)).BeginInit();
            this.SuspendLayout();

            // Аватар
            this.pictureBoxAvatar.BorderStyle = BorderStyle.FixedSingle;
            this.pictureBoxAvatar.Location = new System.Drawing.Point(20, 20);
            this.pictureBoxAvatar.Size = new System.Drawing.Size(100, 100);
            this.pictureBoxAvatar.SizeMode = PictureBoxSizeMode.Zoom;

            // Кнопка загрузки аватара
            this.btnUploadAvatar.Location = new System.Drawing.Point(140, 50);
            this.btnUploadAvatar.Size = new System.Drawing.Size(120, 30);
            this.btnUploadAvatar.Text = "Загрузить фото";
            this.btnUploadAvatar.Click += new System.EventHandler(this.btnUploadAvatar_Click);

            // Имя
            this.lblFirstName.Text = "Имя:";
            this.lblFirstName.Location = new System.Drawing.Point(20, 140);
            this.txtFirstName.Location = new System.Drawing.Point(140, 140);
            this.txtFirstName.Size = new System.Drawing.Size(200, 22);

            // Фамилия
            this.lblLastName.Text = "Фамилия:";
            this.lblLastName.Location = new System.Drawing.Point(20, 180);
            this.txtLastName.Location = new System.Drawing.Point(140, 180);
            this.txtLastName.Size = new System.Drawing.Size(200, 22);

            // Телефон
            this.lblPhone.Text = "Телефон:";
            this.lblPhone.Location = new System.Drawing.Point(20, 220);
            this.txtPhone.Location = new System.Drawing.Point(140, 220);
            this.txtPhone.Size = new System.Drawing.Size(200, 22);

            // Кнопка сохранения
            this.btnSave.Location = new System.Drawing.Point(20, 260);
            this.btnSave.Size = new System.Drawing.Size(320, 30);
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // Добавление элементов на форму
            this.ClientSize = new System.Drawing.Size(380, 320);
            this.Controls.Add(this.pictureBoxAvatar);
            this.Controls.Add(this.btnUploadAvatar);
            this.Controls.Add(this.lblFirstName);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.lblLastName);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.btnSave);
            this.Text = "Редактирование профиля";

            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAvatar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
