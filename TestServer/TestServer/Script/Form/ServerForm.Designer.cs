
namespace TestServer
{
    partial class ServerForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.TextBoxID = new System.Windows.Forms.TextBox();
            this.TextBoxPW = new System.Windows.Forms.TextBox();
            this.Connect = new System.Windows.Forms.Button();
            this.LogOut = new System.Windows.Forms.Button();
            this.labelID = new System.Windows.Forms.Label();
            this.labelPW = new System.Windows.Forms.Label();
            this.TextConsole = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // TextBoxID
            // 
            this.TextBoxID.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.TextBoxID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxID.Font = new System.Drawing.Font("Monaco", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxID.ForeColor = System.Drawing.SystemColors.Window;
            this.TextBoxID.Location = new System.Drawing.Point(82, 6);
            this.TextBoxID.Name = "TextBoxID";
            this.TextBoxID.Size = new System.Drawing.Size(100, 24);
            this.TextBoxID.TabIndex = 0;
            // 
            // TextBoxPW
            // 
            this.TextBoxPW.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.TextBoxPW.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxPW.Font = new System.Drawing.Font("Monaco", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxPW.ForeColor = System.Drawing.SystemColors.Window;
            this.TextBoxPW.Location = new System.Drawing.Point(82, 33);
            this.TextBoxPW.Name = "TextBoxPW";
            this.TextBoxPW.Size = new System.Drawing.Size(100, 24);
            this.TextBoxPW.TabIndex = 1;
            // 
            // Connect
            // 
            this.Connect.Font = new System.Drawing.Font("Monaco", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Connect.Location = new System.Drawing.Point(10, 64);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(172, 27);
            this.Connect.TabIndex = 2;
            this.Connect.Text = "Connect";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // LogOut
            // 
            this.LogOut.Font = new System.Drawing.Font("Monaco", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LogOut.Location = new System.Drawing.Point(10, 97);
            this.LogOut.Name = "LogOut";
            this.LogOut.Size = new System.Drawing.Size(172, 27);
            this.LogOut.TabIndex = 6;
            this.LogOut.TabStop = false;
            this.LogOut.Text = "LogOut";
            this.LogOut.UseVisualStyleBackColor = true;
            this.LogOut.Visible = false;
            this.LogOut.Click += new System.EventHandler(this.LogOut_Click);
            // 
            // labelID
            // 
            this.labelID.AutoSize = true;
            this.labelID.Font = new System.Drawing.Font("Monaco", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelID.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelID.Location = new System.Drawing.Point(8, 9);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(51, 19);
            this.labelID.TabIndex = 3;
            this.labelID.Text = "UserID";
            // 
            // labelPW
            // 
            this.labelPW.AutoSize = true;
            this.labelPW.Font = new System.Drawing.Font("Monaco", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPW.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelPW.Location = new System.Drawing.Point(8, 36);
            this.labelPW.Name = "labelPW";
            this.labelPW.Size = new System.Drawing.Size(65, 19);
            this.labelPW.TabIndex = 4;
            this.labelPW.Text = "Password";
            // 
            // TextConsole
            // 
            this.TextConsole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.TextConsole.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextConsole.Dock = System.Windows.Forms.DockStyle.Right;
            this.TextConsole.Font = new System.Drawing.Font("Monaco", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextConsole.Location = new System.Drawing.Point(188, 0);
            this.TextConsole.Name = "TextConsole";
            this.TextConsole.ReadOnly = true;
            this.TextConsole.Size = new System.Drawing.Size(957, 337);
            this.TextConsole.TabIndex = 7;
            this.TextConsole.TabStop = false;
            this.TextConsole.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1145, 337);
            this.Controls.Add(this.TextConsole);
            this.Controls.Add(this.LogOut);
            this.Controls.Add(this.labelPW);
            this.Controls.Add(this.labelID);
            this.Controls.Add(this.TextBoxPW);
            this.Controls.Add(this.TextBoxID);
            this.Controls.Add(this.Connect);
            this.MaximumSize = new System.Drawing.Size(1161, 9999);
            this.MinimumSize = new System.Drawing.Size(1161, 200);
            this.Name = "ServerForm";
            this.Text = "TestServer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextBoxID;
        private System.Windows.Forms.TextBox TextBoxPW;
        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.Button LogOut;
        private System.Windows.Forms.Label labelID;
        private System.Windows.Forms.Label labelPW;
        private System.Windows.Forms.RichTextBox TextConsole;
    }
}

