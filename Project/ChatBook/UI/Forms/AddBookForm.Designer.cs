using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChatBook.UI.Forms
{
    partial class AddBookForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtBookTitle;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.TextBox txtReview;
        private System.Windows.Forms.Button btnSaveBook;
        private System.Windows.Forms.PictureBox pictureBoxCover;
        private System.Windows.Forms.Button btnUploadCover;
        private System.Windows.Forms.Button buttonDelete;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtBookTitle = new System.Windows.Forms.TextBox();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.txtReview = new System.Windows.Forms.TextBox();
            this.btnSaveBook = new System.Windows.Forms.Button();
            this.pictureBoxCover = new System.Windows.Forms.PictureBox();
            this.btnUploadCover = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCover)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBookTitle
            // 
            this.txtBookTitle.ForeColor = System.Drawing.Color.Black;
            this.txtBookTitle.Location = new System.Drawing.Point(20, 20);
            this.txtBookTitle.Name = "txtBookTitle";
            this.txtBookTitle.Size = new System.Drawing.Size(352, 22);
            this.txtBookTitle.TabIndex = 1;
            this.txtBookTitle.Text = "Введите название книги";
            this.txtBookTitle.Click += new System.EventHandler(this.txtBookTitle_Click);
            // 
            // txtAuthor
            // 
            this.txtAuthor.ForeColor = System.Drawing.Color.Black;
            this.txtAuthor.Location = new System.Drawing.Point(20, 50);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.Size = new System.Drawing.Size(352, 22);
            this.txtAuthor.TabIndex = 2;
            this.txtAuthor.Text = "Введите автора";
            this.txtAuthor.Click += new System.EventHandler(this.txtAuthor_Click);
            // 
            // cmbStatus
            // 
            this.cmbStatus.Items.AddRange(new object[] {
            "Читаю",
            "Прочитано",
            "В планах"});
            this.cmbStatus.Location = new System.Drawing.Point(20, 80);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(120, 24);
            this.cmbStatus.TabIndex = 3;
            // 
            // txtReview
            // 
            this.txtReview.ForeColor = System.Drawing.Color.Black;
            this.txtReview.Location = new System.Drawing.Point(20, 149);
            this.txtReview.Multiline = true;
            this.txtReview.Name = "txtReview";
            this.txtReview.Size = new System.Drawing.Size(352, 71);
            this.txtReview.TabIndex = 4;
            this.txtReview.Text = "Введите отзыв (если прочитано)";
            this.txtReview.Click += new System.EventHandler(this.txtReview_Click);
            // 
            // btnSaveBook
            // 
            this.btnSaveBook.BackColor = System.Drawing.Color.Moccasin;
            this.btnSaveBook.Location = new System.Drawing.Point(69, 400);
            this.btnSaveBook.Name = "btnSaveBook";
            this.btnSaveBook.Size = new System.Drawing.Size(100, 30);
            this.btnSaveBook.TabIndex = 7;
            this.btnSaveBook.Text = "Добавить";
            this.btnSaveBook.UseVisualStyleBackColor = false;
            this.btnSaveBook.Click += new System.EventHandler(this.btnSaveBook_Click);
            // 
            // pictureBoxCover
            // 
            this.pictureBoxCover.BackColor = System.Drawing.Color.Moccasin;
            this.pictureBoxCover.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxCover.Location = new System.Drawing.Point(20, 230);
            this.pictureBoxCover.Name = "pictureBoxCover";
            this.pictureBoxCover.Size = new System.Drawing.Size(127, 150);
            this.pictureBoxCover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxCover.TabIndex = 5;
            this.pictureBoxCover.TabStop = false;
            // 
            // btnUploadCover
            // 
            this.btnUploadCover.BackColor = System.Drawing.Color.Moccasin;
            this.btnUploadCover.Location = new System.Drawing.Point(185, 230);
            this.btnUploadCover.Name = "btnUploadCover";
            this.btnUploadCover.Size = new System.Drawing.Size(140, 30);
            this.btnUploadCover.TabIndex = 6;
            this.btnUploadCover.Text = "Загрузить обложку";
            this.btnUploadCover.UseVisualStyleBackColor = false;
            this.btnUploadCover.Click += new System.EventHandler(this.btnUploadCover_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.BackColor = System.Drawing.Color.Moccasin;
            this.buttonDelete.Location = new System.Drawing.Point(185, 400);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(100, 30);
            this.buttonDelete.TabIndex = 9;
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // AddBookForm
            // 
            this.BackColor = System.Drawing.Color.OldLace;
            this.ClientSize = new System.Drawing.Size(384, 450);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.txtBookTitle);
            this.Controls.Add(this.txtAuthor);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.txtReview);
            this.Controls.Add(this.pictureBoxCover);
            this.Controls.Add(this.btnUploadCover);
            this.Controls.Add(this.btnSaveBook);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(316, 497);
            this.Name = "AddBookForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление книги";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCover)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
