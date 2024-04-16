namespace ScheduleWithAppointments.View
{
    partial class UserControlDay
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txt = new System.Windows.Forms.Label();
            this.eventContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // txt
            // 
            this.txt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.txt.Font = new System.Drawing.Font("#9Slide03 Arima Madurai", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt.ForeColor = System.Drawing.Color.Black;
            this.txt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.txt.Location = new System.Drawing.Point(31, 10);
            this.txt.Name = "txt";
            this.txt.Size = new System.Drawing.Size(94, 28);
            this.txt.TabIndex = 1;
            this.txt.Text = "Temp text";
            this.txt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // eventContainer
            // 
            this.eventContainer.Location = new System.Drawing.Point(3, 41);
            this.eventContainer.Name = "eventContainer";
            this.eventContainer.Size = new System.Drawing.Size(122, 56);
            this.eventContainer.TabIndex = 2;
            // 
            // UserControlDay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.eventContainer);
            this.Controls.Add(this.txt);
            this.Name = "UserControlDay";
            this.Size = new System.Drawing.Size(128, 100);
            this.Load += new System.EventHandler(this.UserControlDay_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label txt;
        private System.Windows.Forms.FlowLayoutPanel eventContainer;
    }
}
