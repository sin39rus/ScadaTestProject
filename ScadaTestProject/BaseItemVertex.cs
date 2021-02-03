using CommonSwCandidateTest.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaTestProject
{
    class BaseItemVertex : BaseItem
    {
        public BaseItemVertex()
        {
            Childs = new List<BaseItemVertex>();
        }

        public List<BaseItemVertex> Childs { set; get; }

        public static BaseItemVertex VertexFromBase(BaseItem item)
        {
            return new BaseItemVertex
            {
                Id = item.Id,
                ParentId = item.ParentId,
                Name = item.Name,
                PlannedStart = item.PlannedStart,
                PlannedEnd = item.PlannedEnd,
                Completed = item.Completed
            };
        }
        public override string ToString()
        {
            return $"Id = {Id}, ParentId = {ParentId}, Name = {Name}, PlannedStart = {PlannedStart.ToShortDateString()}, PlannedEnd = {PlannedEnd.ToShortDateString()}";
        }
    }
}
