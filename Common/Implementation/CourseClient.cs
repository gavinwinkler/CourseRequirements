namespace Common.Implementation
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Interface;
	using Model;

	/// <summary>
	/// Class CourseClient.
	/// </summary>
	/// <seealso cref="Common.Interface.ICourseClient" />
	public class CourseClient : ICourseClient
	{
		/// <summary>
		/// Gets the course order.
		/// </summary>
		/// <param name="courseData">The course data.</param>
		/// <returns>System.String.</returns>
		public string GetCourseOrder(string[] courseData)
		{
			throw new System.NotImplementedException();
		}

		public string ProcessCourseData(string[] courseData)
		{
			string retval = null;

			if(courseData != null)
			{
				var courses = ParseCourseData(courseData);

				if(courses != null)
				{
					try
					{
						var courseChain = BuildCourseChain(courses);

						var stop = "";
					}
					catch(ArgumentException ex)
					{
						retval = ex.Message;
					}

					
				}
			}

			return retval;
		}




		private Dictionary<string, List<Course>> BuildCourseChain(List<Course> courses)
		{
			Dictionary<string, List<Course>> retval = null;

			if(courses != null)
			{
				retval = new Dictionary<string, List<Course>>();

				foreach(var course in courses)
				{
					if(course != null
					   && !string.IsNullOrWhiteSpace(course.Name))
					{
						if(retval.ContainsKey(course.Name))
						{
							throw new ArgumentException("Duplicate courses are not allowed.");
						}

						var toPopulate = new List<Course>();

						retval.Add(course.Name, BuildPrerequisiteChain(course, courses, toPopulate));
					}
				}
			}

			return retval;
		}

		private List<Course> ParseCourseData(string[] courseData)
		{
			var retval = new List<Course>();

			if(courseData != null)
			{
				retval.AddRange(courseData.Select(ParseCourse).Where(course => course != null));
			}

			return retval;
		}

		private Course ParseCourse(string courseData)
		{
			Course retval = null;

			if(!string.IsNullOrWhiteSpace(courseData))
			{
				//assumes the delimiter is :
				var split = courseData.Split(':');

				//assumes there is only 1 prerequisite allowed
				if(split.Length == 2)
				{
					if(!string.IsNullOrWhiteSpace(split[0]))
					{
						retval = new Course
						         {
							         Name = split[0].Trim()
						         };

						if(!string.IsNullOrWhiteSpace(split[1]))
						{
							retval.PrerequisiteName = split[1].Trim();
						}
					}
				}
			}

			return retval;
		}

		private List<Course> BuildPrerequisiteChain(Course course, List<Course> allCourses, List<Course> toPopulate  )
		{
			if(course != null
			   && allCourses != null
			   && toPopulate != null
			   && !string.IsNullOrWhiteSpace(course.Name))
			{
				var duplicate = toPopulate.FirstOrDefault(x => x.Name == course.Name);

				if(duplicate != null)
				{
					throw new ArgumentException("Circular reference detected.");
				}

				toPopulate.Add(course);

				if(!string.IsNullOrWhiteSpace(course.PrerequisiteName))
				{
					if(course.Name == course.PrerequisiteName)
					{
						throw new ArgumentException("A course cannot be its own prerequisite.");
					}

					var prerequisite = allCourses.FirstOrDefault(x => x.Name == course.PrerequisiteName);

					if(prerequisite != null)
					{
						var tmpChain = BuildPrerequisiteChain(prerequisite, allCourses, toPopulate);

						//if(tmpChain != null)
						//{
						//	toPopulate.AddRange(tmpChain);
						//}
					}
				}
			}

			return toPopulate;
		}
	}
}