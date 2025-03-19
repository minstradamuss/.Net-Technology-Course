using System;
using System.Windows.Forms;

namespace ChatBook.UI.Forms
{
    partial class AddBookForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtBookTitle;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.TextBox txtReview;
        private System.Windows.Forms.Button btnSaveBook;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.PictureBox pictureBoxCover;
        private System.Windows.Forms.Button btnUploadCover;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtBookTitle = new System.Windows.Forms.TextBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.txtReview = new System.Windows.Forms.TextBox();
            this.btnSaveBook = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pictureBoxCover = new System.Windows.Forms.PictureBox();
            this.btnUploadCover = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCover)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBookTitle
            // 
            this.txtBookTitle.ForeColor = System.Drawing.Color.Gray;
            this.txtBookTitle.Location = new System.Drawing.Point(20, 20);
            this.txtBookTitle.Name = "txtBookTitle";
            this.txtBookTitle.Size = new System.Drawing.Size(352, 22);
            this.txtBookTitle.TabIndex = 1;
            this.txtBookTitle.Text = "Введите название книги";
            // 
            // cmbStatus
            // 
            this.cmbStatus.Items.AddRange(new object[] {
            "Читаю",
            "Прочитано",
            "В планах"});
            this.cmbStatus.Location = new System.Drawing.Point(20, 60);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(120, 24);
            this.cmbStatus.TabIndex = 2;
            // 
            // txtReview
            // 
            this.txtReview.ForeColor = System.Drawing.Color.Gray;
            this.txtReview.Location = new System.Drawing.Point(20, 140);
            this.txtReview.Multiline = true;
            this.txtReview.Name = "txtReview";
            this.txtReview.Size = new System.Drawing.Size(352, 80);
            this.txtReview.TabIndex = 4;
            this.txtReview.Text = "Введите отзыв (если прочитано)";
            // 
            // btnSaveBook
            // 
            this.btnSaveBook.BackColor = System.Drawing.Color.Moccasin;
            this.btnSaveBook.Location = new System.Drawing.Point(20, 400);
            this.btnSaveBook.Name = "btnSaveBook";
            this.btnSaveBook.Size = new System.Drawing.Size(100, 30);
            this.btnSaveBook.TabIndex = 7;
            this.btnSaveBook.Text = "Добавить";
            this.btnSaveBook.UseVisualStyleBackColor = false;
            this.btnSaveBook.Click += new System.EventHandler(this.btnSaveBook_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Moccasin;
            this.btnCancel.Location = new System.Drawing.Point(140, 400);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = false;
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
            this.buttonDelete.Location = new System.Drawing.Point(262, 400);
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
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.txtReview);
            this.Controls.Add(this.pictureBoxCover);
            this.Controls.Add(this.btnUploadCover);
            this.Controls.Add(this.btnSaveBook);
            this.Controls.Add(this.btnCancel);
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

        private Button buttonDelete;
    }
}