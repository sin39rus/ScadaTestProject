using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaTestProject
{
    public class ItemId
    {
        public ItemId(int id)
        {
            Id = id;
            Parents = new List<Parent>();
        }

        public int Id { private set; get; }
        public IEnumerable<Parent> Parents { set; get; }
    }
}
