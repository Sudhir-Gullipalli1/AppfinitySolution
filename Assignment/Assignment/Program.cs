using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class Program
    {
        static Dictionary<int, Item> dictItems = new Dictionary<int, Item>();
        static Dictionary<int, Item> outputDictSubstituteItems = new Dictionary<int, Item>();
        static Dictionary<int, Item> outputDictRelatedItems = new Dictionary<int, Item>();
        const int LIMIT = 50;

        static void Main(string[] args)
        {
            // Create items
            dictItems.Add(1, new Item() { Id = 1, Name = "Tyre A", Price = 103.45, Substitute = new List<int> { 2, 3 }, Related = new List<int> { 4, 5 } });
            dictItems.Add(2, new Item() { Id = 2, Name = "Tyre B", Price = 140.00, Substitute = new List<int> { 1, 3 }, Related = new List<int> { 6 } });
            dictItems.Add(3, new Item() { Id = 3, Name = "Tyre C", Price = 101.75, Substitute = new List<int> { 1, 2 }, Related = new List<int> { 1 } });
            dictItems.Add(4, new Item() { Id = 4, Name = "Nut A", Price = 1.45, Substitute = new List<int> { }, Related = new List<int> { 6 } });
            dictItems.Add(5, new Item() { Id = 5, Name = "Disc Break A", Price = 203.45, Substitute = new List<int> { }, Related = new List<int> { 1, 2, 3 } });
            dictItems.Add(6, new Item() { Id = 6, Name = "Tyre D", Price = 103.45, Substitute = new List<int> { 1 }, Related = new List<int> { 1, 2, 3, 4 } });

            string searchText = null;
            Console.Write("Enter Search Item Name: ");
            searchText = Console.ReadLine();

            int itemId = 0;
            if (!string.IsNullOrEmpty(searchText))
            {
                foreach (var item in dictItems)
                {
                    if (item.Value.Name.ToLower().Contains(searchText.ToLower()))
                    {
                        itemId = item.Value.Id;
                        break;
                    }
                }
            }
            else
                return;

            if (itemId == 0) return;


            IterateRelatedItems(dictItems[itemId]);
            IterateSubstituteItems(dictItems[itemId]);
            // Print Related Items
            Console.WriteLine("------------- Related Items Start -------------");
            foreach (var r in outputDictRelatedItems)
                Console.WriteLine(r.Key + " : " + r.Value.Name);
            Console.WriteLine("------------- Related Items End -------------");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            // Print Substitute Items
            Console.WriteLine("------------- Substitute Items Start -------------");
            foreach (var s in outputDictSubstituteItems)
                Console.WriteLine(s.Key + " : " + s.Value.Name);
            Console.WriteLine("------------- Substitute Items End -------------");

            Console.ReadKey();

        }

        /// 
        /// Iterates through a list of related items. 
        /// 
        private static void IterateRelatedItems(Item parent)
        {

            if (outputDictRelatedItems.Keys.Count == LIMIT || outputDictRelatedItems.ContainsKey(parent.Id) || parent.Related == null || parent.Related.Count == 0) return;
            outputDictRelatedItems.Add(parent.Id, parent);

            foreach (var g in parent.Related)
            {
                IterateRelatedItems(dictItems[g]);
            }
        }


        /// 
        /// Iterates through a list of substitute items. 
        /// 
        private static void IterateSubstituteItems(Item parent)
        {
            if (outputDictSubstituteItems.Keys.Count == LIMIT || outputDictSubstituteItems.ContainsKey(parent.Id) || parent.Substitute == null || parent.Substitute.Count == 0) return;
            outputDictSubstituteItems.Add(parent.Id, parent);

            foreach (var g in parent.Substitute)
            {
                IterateSubstituteItems(dictItems[g]);
            }
        }

        public class Item
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Price { get; set; }
            public List<int> Substitute { get; set; }
            public List<int> Related { get; set; }

            public Item()
            {
                this.Substitute = new List<int>();
                this.Related = new List<int>();
            }
        }
    }
}
