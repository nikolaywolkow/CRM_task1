using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string key = "";
            while (key != "0")
            {
                individual ind = new individual();
                company comp = new company();

                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1 - Считать физических лиц из файла.");
                Console.WriteLine("2 - Записать физических лиц в файл");
                Console.WriteLine("3 - Считать юридических лиц из файла.");
                Console.WriteLine("4 - Записать юридических лиц в файл");

                Console.WriteLine("0 - Выход");


                key = Console.ReadLine();
                if (key == "1") ind.read_individual();
                if (key == "2") ind.input_individual();
                if (key == "3") comp.read_company();
                if (key == "4") comp.input_company();

            }

        }
    }
}
