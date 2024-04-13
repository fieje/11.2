using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace StudentGrades
{
    public struct Student
    {
        public int ID;
        public string LastName;
        public int Course;
        public string Specialization;
        public double PhysicsGrade;
        public double MathGrade;
        public double AdditionalGrade;
    }

    public class Program
    {
        public static List<Student> ReadStudentsFromFile(string fileName)
        {
            List<Student> students = new List<Student>();

            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] data = line.Split(',');

                        if (data.Length >= 7)
                        {
                            Student student = new Student();
                            student.ID = int.Parse(data[0]);
                            student.LastName = data[1];
                            student.Course = int.Parse(data[2]);
                            student.Specialization = data[3];
                            student.PhysicsGrade = double.Parse(data[4], CultureInfo.InvariantCulture);
                            student.MathGrade = double.Parse(data[5], CultureInfo.InvariantCulture);
                            student.AdditionalGrade = double.Parse(data[6], CultureInfo.InvariantCulture);

                            students.Add(student);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while reading the file: " + ex.Message);
            }

            return students;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter the file path: ");
            string fileName = Console.ReadLine();

            List<Student> students = ReadStudentsFromFile(fileName);

            DisplayStudentsTable(students);

            Console.WriteLine("Enter the output file name: "); 
            string outputFileName = Console.ReadLine();

            WriteStudentsToBinaryFile(students, outputFileName);

            Console.WriteLine("Data has been written to the binary file successfully.");
        }

        static void WriteStudentsToBinaryFile(List<Student> students, string fileName)
        {
            try
            {
                string directory = Path.GetDirectoryName(fileName); 
                string fullPath = Path.Combine(directory, fileName);

                using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(fileStream, students);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while writing to the file: " + ex.Message);
            }
        }

        static void DisplayStudentsTable(List<Student> students)
        {
            Console.WriteLine("{0,-10} {1,-20} {2,-10} {3,-25} {4,-10} {5,-10} {6,-10}",
                              "ID", "Last Name", "Course", "Specialization", "Physics", "Math", "Additional");
            Console.WriteLine(new string('-', 85));

            foreach (var student in students)
            {
                Console.WriteLine("{0,-10} {1,-20} {2,-10} {3,-25} {4,-10} {5,-10} {6,-10}",
                                  student.ID, student.LastName, student.Course, student.Specialization,
                                  student.PhysicsGrade, student.MathGrade, student.AdditionalGrade);
            }
        }
    }
}
