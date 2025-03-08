namespace ChatBook.UI.Forms
{
    partial class ChatForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListBox listBoxMessages;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnSend;

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
            this.listBoxMessages = new System.Windows.Forms.ListBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // listBoxMessages
            // 
            this.listBoxMessages.FormattingEnabled = true;
            this.listBoxMessages.ItemHeight = 16;
            this.listBoxMessages.Location = new System.Drawing.Point(12, 12);
            this.listBoxMessages.Size = new System.Drawing.Size(776, 340);
            this.listBoxMessages.TabIndex = 0;

            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(12, 370);
            this.txtMessage.Size = new System.Drawing.Size(680, 22);
            this.txtMessage.TabIndex = 1;

            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(700, 368);
            this.btnSend.Size = new System.Drawing.Size(88, 26);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Отправить";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);

            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listBoxMessages);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.btnSend);
            this.Name = "ChatForm";
            this.ShowIcon = false;
            this.Text = "Чат";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
