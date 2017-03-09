namespace CourseOrder
{
	partial class CourseOrder
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
			this.txtCourseData = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtResults = new System.Windows.Forms.TextBox();
			this.chkPretty = new System.Windows.Forms.CheckBox();
			this.btnOrder = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(184, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(585, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Each line contains name of a course followed by a colon and space then the name o" +
    "f the prerequisite course if there is one";
			// 
			// txtCourseData
			// 
			this.txtCourseData.Location = new System.Drawing.Point(105, 45);
			this.txtCourseData.Multiline = true;
			this.txtCourseData.Name = "txtCourseData";
			this.txtCourseData.Size = new System.Drawing.Size(751, 94);
			this.txtCourseData.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(30, 87);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(69, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Course Data:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(27, 205);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Course Order:";
			// 
			// txtResults
			// 
			this.txtResults.Location = new System.Drawing.Point(105, 161);
			this.txtResults.Multiline = true;
			this.txtResults.Name = "txtResults";
			this.txtResults.Size = new System.Drawing.Size(751, 94);
			this.txtResults.TabIndex = 4;
			// 
			// chkPretty
			// 
			this.chkPretty.AutoSize = true;
			this.chkPretty.Location = new System.Drawing.Point(283, 329);
			this.chkPretty.Name = "chkPretty";
			this.chkPretty.Size = new System.Drawing.Size(88, 17);
			this.chkPretty.TabIndex = 5;
			this.chkPretty.Text = "Pretty Format";
			this.chkPretty.UseVisualStyleBackColor = true;
			// 
			// btnOrder
			// 
			this.btnOrder.Location = new System.Drawing.Point(403, 325);
			this.btnOrder.Name = "btnOrder";
			this.btnOrder.Size = new System.Drawing.Size(133, 23);
			this.btnOrder.TabIndex = 6;
			this.btnOrder.Text = "Get Course Order";
			this.btnOrder.UseVisualStyleBackColor = true;
			this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
			// 
			// CourseOrder
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(865, 397);
			this.Controls.Add(this.btnOrder);
			this.Controls.Add(this.chkPretty);
			this.Controls.Add(this.txtResults);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtCourseData);
			this.Controls.Add(this.label1);
			this.Name = "CourseOrder";
			this.Text = "Course Order";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtCourseData;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtResults;
		private System.Windows.Forms.CheckBox chkPretty;
		private System.Windows.Forms.Button btnOrder;
	}
}

