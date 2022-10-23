using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CRM_task
{
    internal class individual : counterparty // Физическое лицо
    {
        string surname;
        string name;
        string patronymic;
        DateTime date_of_Birth;

        string path = "individual.txt";
        private void write_individual()
        {
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLineAsync("counterparty_id=" + this.counterparty_id);
                writer.WriteLineAsync("bin_iin=" + this.bin_iin);
                writer.WriteLineAsync("dataCreate=" + this.dataCreate);
                writer.WriteLineAsync("dataUpdate=" + this.dataUpdate);
                writer.WriteLineAsync("authorCreate=" + this.authorCreate);
                writer.WriteLineAsync("authorUpdate=" + this.authorUpdate);
                writer.WriteLineAsync("surname=" + this.surname);
                writer.WriteLineAsync("name=" + this.name);
                writer.WriteLineAsync("patronymic=" + this.patronymic);
                writer.WriteLineAsync("date_of_Birth=" + this.date_of_Birth);
                writer.WriteLineAsync("***");
            }
        }
        private List<individual> indList = new List<individual>();
        public void read_individual()
        {
            bool fileExist = File.Exists(path);
            if (fileExist)
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string line;

                    individual ind = new individual();
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line == "***")
                        {
                            Console.WriteLine();
                            if (ind.isValid())
                            {
                                indList.Add(ind);
                                ind = new individual();
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("Не верный формат входного файла!!!");
                                break;
                            }
                        }
                        string parameter = line.Split('=').First();
                        string value = line.Split('=').Last();

                        if (parameter == "counterparty_id") ind.Counterparty_id = value;
                        if (parameter == "bin_iin") ind.Bin_iin = value;
                        if (parameter == "dataCreate") ind.DataCreate = value;
                        if (parameter == "dataUpdate") ind.DataUpdate = value;
                        if (parameter == "authorCreate") ind.AuthorCreate = value;
                        if (parameter == "authorUpdate") ind.AuthorUpdate = value;

                        if (parameter == "surname") ind.Surname = value;
                        if (parameter == "name") ind.Name = value;
                        if (parameter == "patronymic") ind.Patronymic = value;
                        if (parameter == "date_of_Birth") ind.Date_of_Birth = value;

                    }
                    print_individual();
                }
            }
            else
            {
                Console.WriteLine("Файл не существует.");
            }
        }
        public void print_individual()
        {
            var sortedList = from p in indList
                             orderby p.surname, p.name, p.patronymic
                             select p;

            foreach (individual value in sortedList)
            {
                Console.WriteLine("counterparty_id = \t" + value.counterparty_id + "\n" +
                "bin_iin = \t\t" + value.bin_iin + "\n" +
                "dataCreate = \t\t" + value.dataCreate + "\n" +
                "dataUpdate = \t\t" + value.dataUpdate + "\n" +
                "authorCreate = \t\t" + value.authorCreate + "\n" +
                "authorUpdate = \t\t" + value.authorUpdate + "\n" +
                "surname = \t\t" + value.surname + "\n" +
                "name = \t\t\t" + value.name + "\n" +
                "patronymic = \t\t" + value.patronymic + "\n" +
                "date_of_Birth = \t" + value.date_of_Birth + "\n\n");
            }
        }

        public void input_individual()
        {
            Console.WriteLine("Введите параметры физического лица");
            individual ind = new individual();


            Console.WriteLine("Введите ID физического лица");
            while (ind.counterparty_id == 0)
                ind.Counterparty_id = Console.ReadLine();


            Console.WriteLine("Введите ИИН физического лица");
            while (ind.bin_iin == 0)
                ind.Bin_iin = Console.ReadLine();

            Console.WriteLine("Введите Дату создания физического лица");
            while (!ind.isDataCreate)
                ind.DataCreate = Console.ReadLine();

            Console.WriteLine("Введите дату изменеия физического лица");
            while (!ind.isDataUpdate)
                ind.DataUpdate = Console.ReadLine();

            Console.WriteLine("Введите автора физического лица");
            while (ind.authorCreate == null)
                ind.AuthorCreate = Console.ReadLine();

            Console.WriteLine("Введите редактора физического лица");
            while (ind.authorUpdate == null)
                ind.AuthorUpdate = Console.ReadLine();

            Console.WriteLine("Введите фамилию физического лица");
            while (ind.surname == null)
                ind.Surname = Console.ReadLine();

            Console.WriteLine("Введите имя физического лица");
            while (ind.name == null)
                ind.Name = Console.ReadLine();

            Console.WriteLine("Введите отчество физического лица");
            while (ind.patronymic == null)
                ind.Patronymic = Console.ReadLine();

            Console.WriteLine("Введите дату рождения физического лица");
            while (!ind.isDate_of_Birth)
                ind.Date_of_Birth = Console.ReadLine();

            if (ind.isValid())
            {
                Console.WriteLine("Данные успешно записаны.");
                ind.write_individual();
            }
            else Console.WriteLine("Не вырный формат данных.");

        }

        public string Surname
        {
            set
            {
                int isNotText = (value.Where(x => Char.IsDigit(x)).ToArray()).Length;
                if (value.Length < 1) Console.WriteLine("Фамилия не может быть пустой");
                else if (isNotText > 0) Console.WriteLine("В Фамилию не должны входить цифры");
                else surname = value;
            }
        }
        public string Name
        {
            set
            {
                int isNotText = (value.Where(x => Char.IsDigit(x)).ToArray()).Length;
                if (value.Length < 1) Console.WriteLine("Имя не может быть пустым");
                else if (isNotText > 0) Console.WriteLine("В Имя не должны входить цифры");
                else name = value;
            }
        }
        public string Patronymic
        {
            set
            {
                int isNotText = (value.Where(x => Char.IsDigit(x)).ToArray()).Length;
                if (value.Length < 1) Console.WriteLine("Отчество не может быть пустым");
                else if (isNotText > 0) Console.WriteLine("В Отчество не должны входить цифры");
                else patronymic = value;
            }
        }
        public bool isDate_of_Birth = false;
        public string Date_of_Birth
        {
            set
            {
                DateTime temp;
                isDate_of_Birth = DateTime.TryParse(value, out temp);
                if (!isDate_of_Birth) Console.WriteLine("не верныфй формат даты");
                else date_of_Birth = temp;
            }
        }
    }
}