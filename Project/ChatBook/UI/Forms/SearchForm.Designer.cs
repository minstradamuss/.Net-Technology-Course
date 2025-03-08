using System;
using System.Windows.Forms;

namespace ChatBook.UI.Forms
{
    partial class SearchForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ListBox listBoxResults;

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
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.listBoxResults = new System.Windows.Forms.ListBox();
            this.SuspendLayout();

            // txtSearch
            this.txtSearch.Location = new System.Drawing.Point(20, 20);
            this.txtSearch.Size = new System.Drawing.Size(200, 22);

            // btnSearch
            this.btnSearch.Location = new System.Drawing.Point(230, 20);
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.Text = "Поиск";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            // listBoxResults
            this.listBoxResults.Location = new System.Drawing.Point(20, 60);
            this.listBoxResults.Size = new System.Drawing.Size(280, 150);

            // SearchForm
            this.ClientSize = new System.Drawing.Size(320, 230);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.listBoxResults);
            this.Text = "Поиск книг";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
