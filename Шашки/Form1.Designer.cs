namespace Шашки
{

    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.Port = new System.Windows.Forms.TextBox();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.OnServer = new Bunifu.Framework.UI.BunifuiOSSwitch();
            this.Min = new Bunifu.Framework.UI.BunifuImageButton();
            this.bunifuImageButton1 = new Bunifu.Framework.UI.BunifuImageButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LeftPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).BeginInit();
            this.SuspendLayout();
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this.LeftPanel;
            this.bunifuDragControl1.Vertical = true;
            // 
            // LeftPanel
            // 
            this.LeftPanel.BackColor = System.Drawing.Color.White;
            this.LeftPanel.Controls.Add(this.txtMessage);
            this.LeftPanel.Controls.Add(this.txtUser);
            this.LeftPanel.Controls.Add(this.Port);
            this.LeftPanel.Controls.Add(this.txtIp);
            this.LeftPanel.Controls.Add(this.btnConnect);
            this.LeftPanel.Controls.Add(this.btnSend);
            this.LeftPanel.Controls.Add(this.txtLog);
            this.LeftPanel.Controls.Add(this.OnServer);
            this.LeftPanel.Controls.Add(this.Min);
            this.LeftPanel.Controls.Add(this.bunifuImageButton1);
            this.LeftPanel.Controls.Add(this.panel4);
            this.LeftPanel.Controls.Add(this.panel3);
            this.LeftPanel.Controls.Add(this.panel1);
            this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.LeftPanel.Location = new System.Drawing.Point(480, 0);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(469, 480);
            this.LeftPanel.TabIndex = 1;
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(43, 380);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(296, 46);
            this.txtMessage.TabIndex = 16;
            this.txtMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMessage_KeyPress_1);
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(44, 117);
            this.txtUser.Multiline = true;
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(265, 27);
            this.txtUser.TabIndex = 15;
            // 
            // Port
            // 
            this.Port.Location = new System.Drawing.Point(235, 79);
            this.Port.Multiline = true;
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(74, 27);
            this.Port.TabIndex = 14;
            this.Port.Text = "1987";
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(44, 79);
            this.txtIp.Multiline = true;
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(185, 27);
            this.txtIp.TabIndex = 13;
            this.txtIp.Text = "192.168.1.101";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(317, 76);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(127, 68);
            this.btnConnect.TabIndex = 12;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(347, 380);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(97, 46);
            this.btnSend.TabIndex = 11;
            this.btnSend.Text = "Enter";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click_1);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(43, 151);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(401, 222);
            this.txtLog.TabIndex = 9;
            // 
            // OnServer
            // 
            this.OnServer.BackColor = System.Drawing.Color.Transparent;
            this.OnServer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("OnServer.BackgroundImage")));
            this.OnServer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.OnServer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OnServer.Location = new System.Drawing.Point(409, 448);
            this.OnServer.Name = "OnServer";
            this.OnServer.OffColor = System.Drawing.Color.Gray;
            this.OnServer.OnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(202)))), ((int)(((byte)(94)))));
            this.OnServer.Size = new System.Drawing.Size(35, 20);
            this.OnServer.TabIndex = 5;
            this.OnServer.Value = false;
            this.OnServer.OnValueChange += new System.EventHandler(this.OnServer_OnValueChange);
            // 
            // Min
            // 
            this.Min.BackColor = System.Drawing.Color.White;
            this.Min.Image = global::Шашки.Properties.Resources.subtract_64pxBlue;
            this.Min.ImageActive = null;
            this.Min.Location = new System.Drawing.Point(341, 13);
            this.Min.Name = "Min";
            this.Min.Size = new System.Drawing.Size(53, 51);
            this.Min.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Min.TabIndex = 4;
            this.Min.TabStop = false;
            this.Min.Zoom = 15;
            this.Min.Click += new System.EventHandler(this.Min_Click);
            // 
            // bunifuImageButton1
            // 
            this.bunifuImageButton1.BackColor = System.Drawing.Color.White;
            this.bunifuImageButton1.Image = global::Шашки.Properties.Resources.delete_64pxBlue;
            this.bunifuImageButton1.ImageActive = null;
            this.bunifuImageButton1.Location = new System.Drawing.Point(391, 12);
            this.bunifuImageButton1.Name = "bunifuImageButton1";
            this.bunifuImageButton1.Size = new System.Drawing.Size(53, 51);
            this.bunifuImageButton1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton1.TabIndex = 3;
            this.bunifuImageButton1.TabStop = false;
            this.bunifuImageButton1.Zoom = 15;
            this.bunifuImageButton1.Click += new System.EventHandler(this.bunifuImageButton1_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Black;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 479);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(468, 1);
            this.panel4.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(468, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1, 479);
            this.panel3.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(469, 1);
            this.panel1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(949, 480);
            this.Controls.Add(this.LeftPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.LeftPanel.ResumeLayout(false);
            this.LeftPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private System.Windows.Forms.Panel LeftPanel;
        private Bunifu.Framework.UI.BunifuImageButton bunifuImageButton1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private Bunifu.Framework.UI.BunifuImageButton Min;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtLog;
        private Bunifu.Framework.UI.BunifuiOSSwitch OnServer;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox Port;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.TextBox txtMessage;
    }
}

