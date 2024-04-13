using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentGrades;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestReadStudentsFromFile()
        {
            string filePath = @"D:\Visual Studio projects\11.2\11.2\bin\Debug\rt.txt";

            List<Student> students = StudentGrades.Program.ReadStudentsFromFile(filePath);

            Assert.IsNotNull(students, "List of students should not be null.");

            Assert.IsTrue(students.Count > 0, "At least one student should be read from the file.");
             
            foreach (var student in students)
            {
                Assert.IsTrue(student.ID > 0, "Student ID should be greater than 0.");
                Assert.IsFalse(string.IsNullOrEmpty(student.LastName), "Last name should not be empty.");
                Assert.IsTrue(student.Course > 0, "Course should be greater than 0.");
                Assert.IsFalse(string.IsNullOrEmpty(student.Specialization), "Specialization should not be empty.");
                Assert.IsTrue(student.PhysicsGrade >= 0, "Physics grade should be greater than or equal to 0.");
                Assert.IsTrue(student.MathGrade >= 0, "Math grade should be greater than or equal to 0.");
                Assert.IsTrue(student.AdditionalGrade >= 0, "Additional grade should be greater than or equal to 0.");
            }
        }
    }
}
