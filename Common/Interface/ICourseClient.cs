namespace Common.Interface
{
	/// <summary>
	/// Interface ICourseClient
	/// </summary>
	public interface ICourseClient
	{
		/// <summary>
		/// Gets the course order.
		/// </summary>
		/// <param name="courseData">The course data.</param>
		/// <returns>System.String.</returns>
		string GetCourseOrder(string[] courseData);
	}
}