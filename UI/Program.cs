using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    class Program
    {
        enum MyEnum { FirstThing, SecondThing }
        enum Countries { USA, Denmark, England, Canada, Australia, Etc }

        static void Main(string[] args)
        {
            int getInt = UIGeneric.UI<Countries>("Hello", typeof(Countries), ConsoleColor.White, ConsoleColor.Blue, ConsoleColor.Cyan);
            Console.Clear();
            Console.WriteLine(getInt);
            Console.WriteLine(Enum.GetName(typeof(Countries), getInt));
            
            Console.ReadLine();
        }
    }

    class UIGeneric
    {
        public static int UI<T>(string text, Type enumerator, ConsoleColor textColor,ConsoleColor cursorColor, ConsoleColor choicesColor) where T : struct, IConvertible, IComparable, IFormattable
        {
            if (!typeof(T).IsEnum) throw new ArgumentException("Not an enum");
            ConsoleKey keyPressed;
            int cursor = 0;
            Type t = enumerator.GetType();
            string[] arr = Enum.GetNames(enumerator);
            do
            {
                Console.Clear();
                Console.ForegroundColor = textColor;
                Console.WriteLine(text + "\n");
                int num = 0;
                foreach(string name in arr)
                {
                    if (cursor == num++) Console.ForegroundColor = cursorColor;
                    else Console.ForegroundColor = choicesColor;
                    Console.WriteLine(name);
                }
                keyPressed = Console.ReadKey(true).Key;
                if (keyPressed == ConsoleKey.UpArrow && cursor > 0) cursor--;
                else if (keyPressed == ConsoleKey.DownArrow && cursor < arr.Length - 1) cursor++;
            } while (keyPressed != ConsoleKey.Enter);

            return cursor;
        }
    }
}
