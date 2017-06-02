using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace psw
{
    class Program
    {
        static void Main(string[] args)
        {
            string fname = string.Empty;
            do
            {
                Console.Write("Enter File Name  : ");
                fname = Console.ReadLine();
            } while (string.IsNullOrEmpty(fname));
            int menu = 1;

            while (menu != 9)
            {
                menu = Do.ShowMenu('C');
                switch (menu)
                {
                    case 1:
                        Do.ShowAllHosts(fname);
                        break;
                    case 2:
                        Do.SearchHost(fname);
                        break;
                    case 3:
                        Do.InsertHost(fname);
                        break;
                    case 4:
                        Do.DeleteHost(fname);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
