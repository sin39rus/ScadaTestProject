using CommonSwCandidateTest.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaTestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<BaseItem> items = new ItemRepository().GetAllItems();
            foreach (var plannedStartGroup in items.OrderBy(t => t.PlannedStart).GroupBy(t => t.PlannedStart))
            {
                Console.WriteLine($"Planned Start: {plannedStartGroup.Key.ToShortDateString()}");
                foreach (var plannedEndGroup in plannedStartGroup.OrderBy(t => t.PlannedEnd).GroupBy(t => t.PlannedEnd))
                {
                    Console.WriteLine($"\tPlanned End: {plannedEndGroup.Key.ToShortDateString()}");
                    foreach (var nameGroupe in plannedEndGroup.OrderBy(t => t.Name).GroupBy(t => t.Name))
                    {
                        Console.WriteLine($"\t\tName: {nameGroupe.Key}");
                        foreach (BaseItem item in nameGroupe)
                            Console.WriteLine($"\t\t\tId: {item.Id}, Parent Id: {item.ParentId.ToString()}, Name: {item.Name}, Planned Start: {item.PlannedStart.ToShortDateString()}, Planned End {item.PlannedEnd.ToShortDateString()}");
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
