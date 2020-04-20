namespace TravelClient
{
	partial class LogInWindow
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(109, 195);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(87, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Username:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(109, 288);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(82, 20);
			this.label2.TabIndex = 1;
			this.label2.Text = "Password:";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(202, 192);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(156, 26);
			this.textBox1.TabIndex = 2;
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(202, 285);
			this.textBox2.Name = "textBox2";
			this.textBox2.PasswordChar = '*';
			this.textBox2.Size = new System.Drawing.Size(156, 26);
			this.textBox2.TabIndex = 3;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(154, 381);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(204, 85);
			this.button1.TabIndex = 4;
			this.button1.Text = "Log in";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// LogInWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(520, 558);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "LogInWindow";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.LogInWindow_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Button button1;
	}
}

