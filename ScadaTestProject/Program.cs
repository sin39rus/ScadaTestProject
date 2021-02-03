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

            Console.WriteLine("Медиана свойств Completed: " + CalcMediana(items.Select(t => t.Completed).ToArray()));

            IEnumerable<ItemId> itemsTree = CreateItemsTree(items);

            Console.ReadLine();
        }



        /// <summary>
        /// Расчет медианы методом предварительной сортировки массива
        /// </summary>
        /// <param name="calculationData">Массив чисел для расчета</param>
        /// <returns>Медиана массива данных</returns>
        private static double CalcMediana(byte[] dataForCalculation)
        {
            var sortedData = dataForCalculation.ToList();
            sortedData.Sort();

            if (sortedData.Count % 2 == 1)      // Если количество значений не четное, берем значение среднего элемента
                return sortedData[sortedData.Count / 2];
            else                                // Иначе находим среднее между центральными эелементами массива
                return (sortedData[sortedData.Count / 2] + sortedData[sortedData.Count / 2 - 1]) / 2d;
        }

        /// <summary>
        /// Создание дерева данных
        /// </summary>
        /// <param name="items">Базовые элементы</param>
        /// <returns>Коллекцию объектов ItemId</returns>
        private static IEnumerable<ItemId> CreateItemsTree(IEnumerable<BaseItem> items)
        {
            return items.GroupBy(t => t.Id)
                .Select(t => new ItemId(t.Key)
                {
                    Parents = t.GroupBy(z => z.ParentId)
                    .Select(z => new Parent(z.Key)
                    {
                        Bases = z.OrderBy(x => new { x.PlannedStart, x.PlannedEnd, x.Name }) // Сортируем по ТЗ "должны быть отсортированы по item.PlannedStart, item.PlannedEnd, item.Name свойствам."
                         .Select(x => new Base { Name = x.Name, Completed = x.Completed, PlannedEnd = x.PlannedEnd, PlannedStart = x.PlannedStart })
                    })
                });
        }
    }
}
