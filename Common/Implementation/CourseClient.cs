namespace Common.Implementation
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
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
		/// <param name="asFlatList">if set to <c>true</c> [as flat list].</param>
		/// <returns>System.String.</returns>
		public string GetCourseOrder(string[] courseData, bool asFlatList)
		{
			return ProcessCourseData(courseData, asFlatList);
		}

		private string ProcessCourseData(string[] courseData, bool asFlatList)
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

						if(courseChain != null)
						{
							retval = BuildOutput(courseChain, asFlatList);
						}
						else
						{
							retval = "Something went wrong and the course order could not be built";
						}
					}
					catch(ArgumentException ex)
					{
						retval = ex.Message;
					}
				}
				else
				{
					retval = "Failed parsing the courses";
				}
			}
			else
			{
				retval = "No course data provided";
			}

			return retval;
		}

		private string BuildOutput(Dictionary<string, List<Course>> courseChain, bool asFlatList)
		{
			string retval = null;

			if(courseChain != null)
			{
				if(asFlatList)
				{

				}
				else
				{
					var builder = new StringBuilder();
					builder.AppendLine("Course order break down:");

					foreach(var key in courseChain.Keys)
					{
						if(!string.IsNullOrWhiteSpace(key))
						{
							var tmpChain = courseChain[key];

							if(tmpChain != null)
							{
								builder.AppendFormat("{0}: ", key);

								if(tmpChain.Count > 1)
								{
									for(var i = tmpChain.Count - 1; i >= 0; i--)
									{
										var tmpCourse = tmpChain[i];

										if(tmpCourse != null)
										{
											builder.Append(tmpCourse.Name);
										}

										if(i > 0)
										{
											builder.Append(", ");
										}
									}
								}
								else
								{
									builder.Append("No Prerequisite");
								}

								builder.AppendLine();
							}
						}
					}

					retval = builder.ToString();
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