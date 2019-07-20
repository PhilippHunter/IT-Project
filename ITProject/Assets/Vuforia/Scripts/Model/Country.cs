using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Model
{
    public class Country
    {
        public Country(int id, string name)
        {
            ID = id;
            Name = name;
            Questions = new List<Question>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}
