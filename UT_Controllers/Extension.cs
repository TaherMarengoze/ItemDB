using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Modeling.ViewModels.Item;

namespace UT_Controllers
{
    public static class Extension
    {
        /// <summary>
        /// Writes a non-generic list objects using the object string method.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="bullet"></param>
        public static void WriteList(this IList list, string bullet = " * ")
        {
            Console.WriteLine("Existing IDs:");
            foreach (var listItem in list)
            {
                Console.WriteLine("{1}{0}", listItem, bullet);
            }
            Console.WriteLine();
        }

        public static void WriteStringList(this IList list, char bullet = '*')
        {
            Console.WriteLine("List: [{0} Item(s)]", list.Count);
            foreach (var entry in list)
            {
                Console.WriteLine(" {1} {0}", entry, bullet);
            }
            Console.WriteLine();
        }

        public static void DrawTable(this IEnumerable<GenericView> list)
        {
            var length = list.Max(s => s.BaseName.Length);

            Console.WriteLine("╔════════╦═{0}═╗", "═".PadRight(length, '═'));
            Console.WriteLine("║ ItemID ║ {0} ║", "Base Name".PadBoth(length));
            Console.WriteLine("╠════════╬═{0}═╣", "═".PadRight(length, '═'));
            foreach (var item in list)
            {
                Console.WriteLine("║ {0} ║ {1} ║",
                    item.ID.PadRight(6),
                    item.BaseName.PadRight(length));
            }
            Console.WriteLine("╚════════╩═{0}═╝", "═".PadRight(length, '═'));
            Console.WriteLine("{0} item(s)", list.Count());
        }


        public static string PadBoth(this string str, int length)
        {
            int spaces = length - str.Length;
            int padLeft = spaces / 2 + str.Length;
            return str.PadLeft(padLeft).PadRight(length);
        }

    }
}
