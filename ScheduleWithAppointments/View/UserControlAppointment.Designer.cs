namespace ScheduleWithAppointments.View
{
    partial class UserControlAppointment
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
            this.content = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // content
            // 
            this.content.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.content.Font = new System.Drawing.Font("#9Slide03 Arima Madurai", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.content.Location = new System.Drawing.Point(3, 0);
            this.content.Name = "content";
            this.content.Size = new System.Drawing.Size(116, 23);
            this.content.TabIndex = 0;
            this.content.Text = "Cuộc họp đột xuất với xếp";
            this.content.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UserControlAppointment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.content);
            this.Name = "UserControlAppointment";
            this.Size = new System.Drawing.Size(119, 23);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label content;
    }
}
