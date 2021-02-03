using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaTestProject
{
    public class Parent
    {
        public Parent(int? id)
        {
            Id = id;
            Bases = new List<Base>();
        }

        public int? Id { private set; get; }
        public IEnumerable<Base> Bases { set; get; }
    }
}
