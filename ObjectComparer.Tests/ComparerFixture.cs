using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectComparer.Helper;
using ObjectComparer.Models;

namespace ObjectComparer.Tests
{
    [TestClass]
    public class ComparerFixture
    {
        [TestMethod]
        public void Null_values_are_similar_test()
        {
            string string1 = null, string2 = null;
            var result = Comparer.AreSimilar(string1, string2);

            Assert.AreEqual(StandardCode.SUCCESS, result);
        }

        [TestMethod]
        public void Student_objects_name_are_not_similar_test()
        {
            var student1 = new Student
            {
                Name = "Shyam",
                Marks = new List<int>
                {
                    10,20,30
                },
                Titles = new List<string>()
                {
                    "ABC", "XYZ"
                },
                Grade = new Dictionary<string, int>()
                {
                    {"A", 80 },
                    {"B", 70 },
                    {"C", 60}
                }

            };

            var student2 = new Student
            {
                Name = "Ram",
                Marks = new List<int>
                {
                    10,20,30
                },
                Titles = new List<string>()
                {
                    "ABC", "XYZ"
                },
                Grade = new Dictionary<string, int>()
                {
                    {"A", 80 },
                    {"B", 70 },
                    {"C", 60}
                }

            };
            var result = Comparer.AreSimilar(student1, student2);

            Assert.AreEqual(StandardCode.FAILED, result);
        }

        [TestMethod]
        public void Student_objects_marks_are_not_similar_test()
        {
            var student1 = new Student
            {
                Name = "Shyam",
                Marks = new List<int>
                {
                    10,20,30
                },
                Titles = new List<string>()
                {
                    "ABC", "XYZ"
                },
                Grade = new Dictionary<string, int>()
                {
                    {"A", 80 },
                    {"B", 70 },
                    {"C", 60}
                }

            };

            var student2 = new Student
            {
                Name = "Ram",
                Marks = new List<int>
                {
                    10,20,40
                },
                Titles = new List<string>()
                {
                    "ABC", "XYZ"
                },
                Grade = new Dictionary<string, int>()
                {
                    {"A", 80 },
                    {"B", 70 },
                    {"C", 60}
                }

            };
            var result = Comparer.AreSimilar(student1, student2);

            Assert.AreEqual(StandardCode.FAILED, result);
        }

        [TestMethod]
        public void Student_objects_are_similar_test()
        {
            var student1 = new Student
            {
                Name = "Shyam",
                Marks = new List<int>
                {
                    10,20,30
                },
                Titles = new List<string>()
                {
                    "ABC", "XYZ"
                },
                Grade = new Dictionary<string, int>()
                {
                    {"A", 80 },
                    {"B", 70 },
                    {"C", 60}
                }

            };

            var student2 = new Student
            {
                Name = "Shyam",
                Marks = new List<int>
                {
                    10,20,30
                },
                Titles = new List<string>()
                {
                    "ABC", "XYZ"
                },
                Grade = new Dictionary<string, int>()
                {
                    {"A", 80 },
                    {"B", 70 },
                    {"C", 60}
                }

            };
             var result = Comparer.AreSimilar(student1, student2);

            Assert.AreEqual(StandardCode.SUCCESS, result);
        }

        [TestMethod]
        public void Nested_object_are_not_similar_test()
        {
            var dept1 = new Department
            {
                Name = "Bio",
                Teachers = new List<Teacher>
                {
                    new Teacher()
                    {
                        Name = "Shaym",
                        Classes = new List<int>()
                        {
                            10,11,12
                        }
                    }
                }

            };

            var dept2 = new Department
            {
                Name = "Chem",
                Teachers = new List<Teacher>
                {
                    new Teacher()
                    {
                        Name = "Ram",
                        Classes = new List<int>()
                        {
                            11,12,10,10
                        }
                    }
                }

            };

            var result = Comparer.AreSimilar(dept1, dept2);
            Assert.AreEqual(StandardCode.FAILED, result);

        }

        [TestMethod]
        public void Nested_object_are_similar_test()
        {
            var dept1 = new Department
            {
                Name = "Bio",
                Teachers = new List<Teacher>
                {
                    new Teacher()
                    {
                        Name = "Shaym",
                        Classes = new List<int>()
                        {
                            10,11,12
                        }
                    }
                }

            };

            var dept2 = new Department
            {
                Name = "Bio",
                Teachers = new List<Teacher>
                {
                    new Teacher()
                    {
                        Name = "Shaym",
                        Classes = new List<int>()
                        {
                            11,12,10
                        }
                    }
                }

            };

            var result =  Comparer.AreSimilar(dept1, dept2);
            Assert.AreEqual(StandardCode.SUCCESS, result);

        }
    }
 }
