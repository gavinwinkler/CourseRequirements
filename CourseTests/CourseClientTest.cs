using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseTests
{
	using System.Collections.Generic;
	using Common.Implementation;

	[TestClass]
	public class CourseClientTest
	{
		[TestMethod]
		public void TestCircularData()
		{
			var client = new CourseClient();

			var testData = CircularData();

			Assert.IsNotNull(testData);

			var result = client.ProcessCourseData(testData.ToArray());

			Assert.AreEqual("Circular reference detected.", result);
		}

		[TestMethod]
		public void TestGoodData()
		{
			var client = new CourseClient();

			var testData = GoodData();

			Assert.IsNotNull(testData);

			var result = client.ProcessCourseData(testData.ToArray());

			var x = "";
		}

		private List<string> CircularData()
		{
			return new List<string> {"Intro to Arguing on the Internet: Godwin’s Law"
				,"Understanding Circular Logic: Intro to Arguing on the Internet"
				,"Godwin’s Law: Understanding Circular Logic"};
		}

		private List<string> GoodData()
		{
			return new List<string> { "Introduction to Paper Airplanes: "
				,"Advanced Throwing Techniques: Introduction to Paper Airplanes"
				,"History of Cubicle Siege Engines: Rubber Band Catapults 101"
				,"Advanced Office Warfare: History of Cubicle Siege Engines"
				,"Rubber Band Catapults 101: "
				,"Paper Jet Engines: Introduction to Paper Airplanes"
		};

	}
	}
}
