namespace Common.Model
{
	/// <summary>
	/// Class Course.
	/// </summary>
	public class Course
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Course"/> class.
		/// </summary>
		public Course()
		{
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the prerequisite.
		/// </summary>
		/// <value>The prerequisite.</value>
		public Course Prerequisite { get; set; }
	}
}
