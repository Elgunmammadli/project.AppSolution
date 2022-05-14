using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace project.lib
{
    public class Helpers
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="required"></param>
        /// <returns></returns>
        public static string ReadString(string caption, bool required = false)
        {
        l1:
            Console.Write(caption);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            string value = Console.ReadLine();
            Console.ResetColor();
            if (required == true && string.IsNullOrWhiteSpace(value))
            {
                PrintError("Can not be  blank !");
                goto l1;
            }
            return value;
        }

        public static int ReadInt(string caption, int minValue = 0)
        {
        l1:
            Console.Write(caption);
            string value = Console.ReadLine();

            if (!int.TryParse(value, out int number))
            {
                PrintError("You must enter number");
                goto l1;
            }
            else if (minValue > number)
            {
                PrintError($"Minimum of {minValue} can be entered");
                goto l1;
            }
            return number;
        }

        public static double ReadDouble(string caption, double minValue = 0)
        {
        l1:
            Console.Write(caption);
            string value = Console.ReadLine();
            double number;
            if (!double.TryParse(value, out number))
            {
                PrintError("You must enter number");
                goto l1;
            }
            else if (minValue > number)
            {
                PrintError($"Minimum of {minValue} can be entered");
                goto l1;
            }
            return number;
        }

        public static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void Init(string appName)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            Console.Title = appName;

            CultureInfo ci = new CultureInfo("az-Latn-Az");
            ci.NumberFormat.NumberDecimalSeparator = ".";

            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }

        public static MenuStates ReadMenu(string caption)
        {
        l1:
            Console.Write(caption);
            string value = Console.ReadLine();
            if (!Enum.TryParse(value, out MenuStates menu))
            {
                PrintError("Such a menu is not available");
                goto l1;
            }

            bool success = Enum.IsDefined(typeof(MenuStates), menu);
            if (!success)
            {
                PrintError("Such a menu is not available");
                goto l1;
            }
            return menu;
        }
        public static OilType SelectOil(string caption)
        {
        l1:
            Console.Write(caption);
            string value = Console.ReadLine();
            if (!Enum.TryParse(value, out OilType menu))
            {
                PrintError("Such a menu is not available");
                goto l1;
            }

            bool success = Enum.IsDefined(typeof(OilType), menu);
            if (!success)
            {
                PrintError("Such a menu is not available");
                goto l1;
            }
            return menu;
        }

        public static BanTypes SelectBan(string caption)
        {
        l1:
            Console.Write(caption);
            string value = Console.ReadLine();
            if (!Enum.TryParse(value, out BanTypes menu))
            {
                PrintError("Such a menu is not available");
                goto l1;
            }

            bool success = Enum.IsDefined(typeof(BanTypes), menu);
            if (!success)
            {
                PrintError("Such a menu is not available");
                goto l1;
            }
            return menu;
        }

        public static void PrintMenu<T>()
        {
            Helpers.PrintWarning("--------------Menu------------");
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                Helpers.PrintWarning($"{((byte)item).ToString().PadLeft(2)}.{item}");
            }
            Helpers.PrintWarning("------------------------------");
        }

        public static void PrintWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        [Obsolete]
        public static void SaveToFile(string filename, object graphData)
        {
            using (var fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, graphData);
            }
        }

        [Obsolete]
        public static object LoadFromFile(string filename)
        {

            if (!File.Exists(filename))
            {
                return null;
            }

            using (var fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return bf.Deserialize(fs);
            }
        }

        public static void ShowAll<T>(T[] xs)
        {
            Console.WriteLine($"List of {typeof(T).Name}s...");
            foreach (var x in xs)
            {
                Console.WriteLine(x);
            }
        }
    }
}
