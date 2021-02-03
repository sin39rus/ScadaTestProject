using CommonSwCandidateTest.Data;
using System;
using System.Collections.Generic;
using System.IO;
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


            //Задание №1
            Console.WriteLine("Медиана свойств Completed: " + CalcMediana(items.Select(t => t.Completed).ToArray()));
            Console.WriteLine();


            //Задание #2
            IEnumerable<BaseItemVertex> trees = CreateGraph(items);

            foreach (var tree in trees)
                DisplayTree(tree);

            Console.ReadLine();
        }

        private static void DisplayTree(BaseItemVertex vertex)
        {
            Console.WriteLine(vertex);
            foreach (var childVertex in vertex.Childs)
                DisplayTree(childVertex);
        }


        /// <summary>
        /// Создаем граф с данными
        /// </summary>
        /// <param name="items">Исходные данные</param>
        private static IEnumerable<BaseItemVertex> CreateGraph(IEnumerable<BaseItem> items)
        {
            List<BaseItemVertex> peaks = new List<BaseItemVertex>(); //Так как вершин может быть несколько, создаем коллекцию вершин

            foreach (BaseItem item in items.Where(t => t.ParentId == null).OrderBy(t => t.Id)) //Записи у которых нет ParentId считаем вершиной нового дерева
            {
                BaseItemVertex treePeak = BaseItemVertex.VertexFromBase(item);
                GetChildVertexs(treePeak, items); //Рекурсивно находим все ветки
                peaks.Add(treePeak);
            }
            return peaks;
        }

        /// <summary>
        /// Получаем из коллекции все ветки
        /// </summary>
        /// <param name="vertex">вершина</param>
        /// <param name="items">Коллекция значений</param>
        private static void GetChildVertexs(BaseItemVertex vertex, IEnumerable<BaseItem> items)
        {
            vertex.Childs = items
                .Where(t => t.ParentId == vertex.Id)
                .Select(t => BaseItemVertex.VertexFromBase(t))
                .OrderBy(t => t.PlannedStart).ThenBy(t => t.PlannedEnd).ThenBy(t => t.Name)
                .ToList();
            if (vertex.Childs.Count > 0)
                foreach (BaseItemVertex leaf in vertex.Childs)
                    GetChildVertexs(leaf, items);
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
    }
}
