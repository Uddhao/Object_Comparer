using System.Collections.Generic;

namespace ObjectComparer.Models
{
    public class Department
    {
        public string Name { get; set; }

        public List<Teacher> Teachers { get; set; }
    }
}
