namespace IntelliSpaceISE
{
    partial class ImageDlg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageDlg));
            this.axiSiteEnterprise1 = new AxISITELib.AxiSiteEnterprise();
            ((System.ComponentModel.ISupportInitialize)(this.axiSiteEnterprise1)).BeginInit();
            this.SuspendLayout();
            // 
            // axiSiteEnterprise1
            // 
            this.axiSiteEnterprise1.Enabled = true;
            this.axiSiteEnterprise1.Location = new System.Drawing.Point(5, 2);
            this.axiSiteEnterprise1.Name = "axiSiteEnterprise1";
            this.axiSiteEnterprise1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axiSiteEnterprise1.OcxState")));
            this.axiSiteEnterprise1.Size = new System.Drawing.Size(803, 658);
            this.axiSiteEnterprise1.TabIndex = 0;
            // 
            // ImageDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 662);
            this.Controls.Add(this.axiSiteEnterprise1);
            this.Name = "ImageDlg";
            this.Text = "IntelliSpace Image Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.axiSiteEnterprise1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxISITELib.AxiSiteEnterprise axiSiteEnterprise1;
    }
}