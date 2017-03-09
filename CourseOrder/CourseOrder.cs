using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseOrder
{
	using System.IO;
	using Common.Implementation;

	/// <summary>
	/// Class CourseOrder.
	/// </summary>
	/// <seealso cref="System.Windows.Forms.Form" />
	public partial class CourseOrder : Form
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CourseOrder"/> class.
		/// </summary>
		public CourseOrder()
		{
			InitializeComponent();
		}

		private void btnOrder_Click(object sender, EventArgs e)
		{
			if(!string.IsNullOrWhiteSpace(txtCourseData.Text))
			{
				string result = null;

				var logic = new CourseClient();

				try
				{
					var courseData = GetCourseData();

					if(logic != null
					   && courseData != null)
					{
						if(chkPretty != null
						   && chkPretty.Checked)
						{
							result = logic.GetCourseOrder(courseData, false);
						}
						else
						{
							result = logic.GetCourseOrder(courseData, true);
						}
					}
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message);
				}

				txtResults.Text = result;
			}
			else
			{
				MessageBox.Show("You must provide course data");
			}

			
		}

		private string[] GetCourseData()
		{
			string[] retval = null;

			txtResults.Text = string.Empty;

			if(!string.IsNullOrWhiteSpace(txtCourseData?.Text))
			{
				var lines = new List<string>();

				using(var reader = new StringReader(txtCourseData.Text))
				{
					while(reader.Peek() != -1)
					{
						var line = reader.ReadLine();

						if(!string.IsNullOrWhiteSpace(line))
						{
							var tmpSplit = line.Split(':');

							if(tmpSplit?.Length != 2)
							{
								throw new ArgumentException("Invalid course entry");
							}

							lines.Add(line);
						}
					}
				}

				retval = lines.ToArray();
			}

			return retval;
		}
	}
}
