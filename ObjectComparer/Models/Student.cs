using System.Collections.Generic;

namespace ObjectComparer.Models
{
    public class Student
    {
        public string Name { get; set; }

        public List<string> Titles { get; set; }
        public IList<int> Marks { get; set; } 

        public IDictionary<string, int> Grade { get; set; }
    }
}
