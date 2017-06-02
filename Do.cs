using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace psw
{
    public static class Do
    {


        static int tableWidth = 73;
        public static int ShowMenu(char clr = 'D')
        {
            int result;
            if (clr == 'C')
            {
                Console.Clear();
            }
            Console.WriteLine(new string('-', tableWidth + 4));
            int center = (tableWidth - 2) / 2;
            Console.WriteLine(new string('-', center) + " MENU " + new string('-', center + 1));
            Console.WriteLine(new string('-', tableWidth + 4));
            Console.WriteLine("1. List Of The All Hosts");
            Console.WriteLine("2. Search Hosts");
            Console.WriteLine("3. Insert Host");
            Console.WriteLine("4. Delete Host");
            Console.WriteLine("9. Close Program");
            Console.WriteLine(new string('-', tableWidth + 4));
            Console.WriteLine("  Enter Selection : ");
            Console.WriteLine(new string('-', tableWidth + 4));


            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            y = Console.CursorTop - 2;
            x = Console.CursorLeft + 20;

            Console.SetCursorPosition(x, y);


            int.TryParse(Console.ReadLine().ToString(), out result);
            Console.WriteLine();
            Console.Write(" ");
            Console.WriteLine('>');
            return result;
        }

        public static void InsertHost(string fname)
        {
            Console.Write("   Enter Name Of The Host : ");
            string hostName = Console.ReadLine();
            Console.Write("   Enter User Name : ");
            string username = Console.ReadLine();
            Console.Write("   Enter Password Of The Host : ");
            string pass = string.Empty;
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if ((int)key.KeyChar >= 32 && (int)key.KeyChar <= 126)
                {
                    pass += key.KeyChar;
                    //Console.Write("*");
                    Console.Write(key.KeyChar.ToString());
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Substring(0, (pass.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            }
            while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();

            EnDy.WriteFile(hostName, username, pass, fname);
        }

        internal static void ShowAllHosts(string fname)
        {
            string[] lines = { };
            if (System.IO.File.Exists(fname))
            {
                lines = System.IO.File.ReadAllLines(fname);
            }
            string row = string.Empty,
                host = string.Empty,
                username = string.Empty,
                pass = string.Empty,
                number = string.Empty;

            int i = 0;

            Do.PrintLine();
            Do.PrintRow("    ", "Host Name", "User Name", "Password");
            Do.PrintLine();

            foreach (var line in lines)
            {
                row = EnDy.Decrypt(line, false, "012345678901234567890123").ToString();

                host = row.Split(new string[] { "||" }, StringSplitOptions.None)[0];
                username = row.Split(new string[] { "||" }, StringSplitOptions.None)[1];
                pass = row.Split(new string[] { "||" }, StringSplitOptions.None)[2];

                host = EnDy.Decrypt(host, false, "012345678901234567890123").ToString();
                username = EnDy.Decrypt(username, false, "012345678901234567890123").ToString();
                pass = EnDy.Decrypt(pass, false, "012345678901234567890123").ToString();


                ++i;

                if (i < 10)
                {
                    number = " " + i;
                }
                else if (i >= 10 && i < 100)
                {
                    number = "" + i;
                }
                else
                {
                    number = i.ToString();
                }

                Do.PrintRow("  " + number, host, username, pass);

            }


            Do.PrintLine();

            Console.WriteLine();
            Console.ReadKey();
        }

        internal static void SearchHost(string fname)
        {

            Console.Write("   Enter Name Of The Host : ");
            string hostName = Console.ReadLine();
            Console.WriteLine();

            string[] lines = { };
            if (System.IO.File.Exists(fname))
            {
                lines = System.IO.File.ReadAllLines(fname);
            }
            string row = string.Empty,
                host = string.Empty,
                username = string.Empty,
                pass = string.Empty,
                number = string.Empty;
            int i = 1;
            Do.PrintLine();
            Do.PrintRow("    ", "Host Name", "User Name", "Password");
            Do.PrintLine();

            foreach (var line in lines)
            {
                row = EnDy.Decrypt(line, false, "012345678901234567890123").ToString();

                host = row.Split(new string[] { "||" }, StringSplitOptions.None)[0];
                username = row.Split(new string[] { "||" }, StringSplitOptions.None)[1];
                pass = row.Split(new string[] { "||" }, StringSplitOptions.None)[2];

                host = EnDy.Decrypt(host, false, "012345678901234567890123").ToString();
                username = EnDy.Decrypt(username, false, "012345678901234567890123").ToString();
                pass = EnDy.Decrypt(pass, false, "012345678901234567890123").ToString();

                if (host.Contains(hostName))
                {
                    if (i < 10)
                    {
                        number = " " + i;
                    }
                    else if (i >= 10 && i < 100)
                    {
                        number = "" + i;
                    }
                    else
                    {
                        number = i.ToString();
                    }

                    Do.PrintRow("  " + number, host, username, pass);
                }

                i++;
            }
            Do.PrintLine();
            Console.WriteLine();
            Console.ReadKey();
        }

        internal static void DeleteHost(string fname)
        {

            string[] lines = { };
            if (System.IO.File.Exists(fname))
            {
                lines = System.IO.File.ReadAllLines(fname);
            }
            string row = string.Empty,
                host = string.Empty,
                username = string.Empty,
                pass = string.Empty,
                number = string.Empty;
            int i = 0;
            Do.PrintLine();
            Do.PrintRow("    ", "Host Name", "User Name", "Password");
            Do.PrintLine();


            foreach (var line in lines)
            {
                row = EnDy.Decrypt(line, false, "012345678901234567890123").ToString();

                host = row.Split(new string[] { "||" }, StringSplitOptions.None)[0];
                username = row.Split(new string[] { "||" }, StringSplitOptions.None)[1];
                pass = row.Split(new string[] { "||" }, StringSplitOptions.None)[2];

                host = EnDy.Decrypt(host, false, "012345678901234567890123").ToString();
                username = EnDy.Decrypt(username, false, "012345678901234567890123").ToString();
                pass = EnDy.Decrypt(pass, false, "012345678901234567890123").ToString();


                ++i;

                if (i < 10)
                {
                    number = " " + i;
                }
                else if (i >= 10 && i < 100)
                {
                    number = "" + i;
                }
                else
                {
                    number = i.ToString();
                }

                Do.PrintRow("  " + number, host, username, pass);

            }
            ++i;

            if (i < 10)
            {
                number = " " + i;
            }
            else if (i >= 10 && i < 100)
            {
                number = "" + i;
            }
            else
            {
                number = i.ToString();
            }

            Do.PrintLine();
            Do.PrintRow("  " + number, "Cancel", "");
            Do.PrintLine();

            Console.WriteLine();

            Console.Write(" ");
            Console.WriteLine('>');

            int hostNumber;
            do
            {
                Console.Write("   Enter Number Of The Host To be Deleted : ");
                int.TryParse(Console.ReadLine().ToString(), out hostNumber);
            } while (hostNumber > i || hostNumber == 0);
            Console.WriteLine();
            if (hostNumber == i)
            {
                return;
            }
            else
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname, false))
                {
                    file.Flush();
                }


                for (int j = 1; j <= lines.Length; j++)
                {
                    if (j == hostNumber)
                    {
                        continue;
                    }
                    else
                    {
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname, true))
                        {
                            file.Write(string.Format("{0}{1}", lines[j - 1], "\n"));
                        }
                    }

                }

            }
        }


        public static void PrintLine()
        {
            string temp = string.Empty;

            temp = new string('-', tableWidth);

            temp = "    " + temp;
            Console.WriteLine(temp);
        }

        public static void PrintRow(string space, params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = space + "|";
            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        public static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }


    }
}
