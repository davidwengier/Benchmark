using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace BenchmarkFull
{
    [MemoryDiagnoser]
    public class IEnumerableVsList
    {
        [Benchmark]
        public IEnumerable<string> IEnumerable()
        {
            IEnumerable<string> list = new List<string>();
            for (int i = 0; i < 500; i++)
            {
                list = AddItem(list, "boo");
            }

            return list.ToArray();

            IEnumerable<string> AddItem(IEnumerable<string> items, string itemToAdd)
            {
                return items.Concat(new[] { itemToAdd });
            }
        }

        [Benchmark]
        public IEnumerable<string> ListWithoutReturning()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < 500; i++)
            {
                AddItem(list, "boo");
            }

            return list;

            void AddItem(List<string> items, string itemToAdd)
            {
                items.Add(itemToAdd);
            }
        }

        [Benchmark]
        public IEnumerable<string> ReturningAList()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < 500; i++)
            {
                list = AddItem(list, "boo");
            }

            return list;

            List<string> AddItem(List<string> items, string itemToAdd)
            {
                items.Add(itemToAdd);
                return items;
            }
        }
    }
}
