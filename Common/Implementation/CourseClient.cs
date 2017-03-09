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
						//call to the course chain first even if it is a flat display as this detects for circular references
						var courseChain = BuildCourseChain(courses);



						if (asFlatList)
						{
							var orderList = BuildCourseOrder(courses);

							retval = orderList?.Count > 0 ? FlatFormat(orderList) : "Something went wrong and the course prerequisite list could not be built";
						}
						else
						{
							retval = courseChain != null ? BreakDownFormat(courseChain) : "Something went wrong and the course chain could not be built";
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

		private string FlatFormat(List<Course> orderedList)
		{
			string retval = null;

			if (orderedList?.Count > 0)
			{
				var builder = new StringBuilder();

				for(var i = 0; i < orderedList.Count; i++)
				{
					var tmpCourse = orderedList[i];

					if(tmpCourse != null)
					{
						builder.Append(tmpCourse.Name);

						if(i < orderedList.Count - 1)
						{
							builder.Append(", ");
						}
					}
				}

				retval = builder.ToString();
			}
			else
			{
				retval = "Ordered list cannot be empty";
			}

			return retval;
		}

		private string BreakDownFormat(Dictionary<string, List<Course>> courseChain)
		{
			string retval = null;

			if(courseChain != null)
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
			else
			{
				retval = "Course chain cannot be empty";
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

		private Dictionary<string, List<Course>> BuildCourseChain(List<Course> courses)
		{
			Dictionary<string, List<Course>> retval = null;

			if (courses != null)
			{
				retval = new Dictionary<string, List<Course>>();

				foreach (var course in courses)
				{
					if (!string.IsNullOrWhiteSpace(course?.Name))
					{
						if (retval.ContainsKey(course.Name))
						{
							throw new ArgumentException("Duplicate courses are not allowed.");
						}

						var chain = new List<Course>();

						BuildCoursePrerequisiteList(course, courses, chain);

						if(chain != null)
						{
							retval.Add(course.Name, chain);
						}
					}
				}
			}

			return retval;
		}

		private List<Course> BuildCourseOrder(List<Course> courses)
		{
			var retval = new List<Course>();

			var noPrerequisite = courses?.Where(x => string.IsNullOrWhiteSpace(x.PrerequisiteName)).ToList();

			if(noPrerequisite?.Count > 0)
			{
				foreach(var course in noPrerequisite)
				{
					if(!string.IsNullOrWhiteSpace(course?.Name))
					{
						var ordered = new List<Course>();

						BuildCourseListWithThisPrerequisite(course, courses, ordered);

						if(ordered != null)
						{
							retval.AddRange(ordered);
						}
					}
				}
			}
			else
			{
				
			}

			return retval;
		}

		private void BuildCourseListWithThisPrerequisite(Course course, List<Course> allCourses, List<Course> toPopulate)
		{
			if(course != null
			   && allCourses != null
			   && toPopulate != null
			   && !string.IsNullOrWhiteSpace(course.Name))
			{
				var duplicate = toPopulate.FirstOrDefault(x => x.Name == course.Name);

				if(duplicate != null)
				{
					throw new ArgumentException("Duplicate entries not allowed.");
				}

				toPopulate.Add(course);
				
				var prerequisiteList = allCourses.Where(x => x.PrerequisiteName == course.Name);

				if(prerequisiteList != null)
				{
					foreach(var prerequisite in prerequisiteList)
					{
						if(prerequisite != null)
						{
							BuildCourseListWithThisPrerequisite(prerequisite, allCourses, toPopulate);
						}
					}
				}
			}
		}

		private void BuildCoursePrerequisiteList(Course course, List<Course> allCourses, List<Course> toPopulate)
		{
			if (course != null
			   && allCourses != null
			   && toPopulate != null
			   && !string.IsNullOrWhiteSpace(course.Name))
			{
				var duplicate = toPopulate.FirstOrDefault(x => x.Name == course.Name);

				if (duplicate != null)
				{
					throw new ArgumentException("Circular reference detected.");
				}

				toPopulate.Add(course);

				if (!string.IsNullOrWhiteSpace(course.PrerequisiteName))
				{
					var prerequisite = allCourses.FirstOrDefault(x => x.Name == course.PrerequisiteName);

					if (prerequisite != null)
					{
						BuildCoursePrerequisiteList(prerequisite, allCourses, toPopulate);
					}
				}
			}
		}
	}
}