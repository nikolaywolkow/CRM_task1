using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_task
{
    internal class company : counterparty //юридическое лицо
    {
        public string company_name;
        public DateTime company_registration_date;
        public string[] list_ind;

        private List<company> companyList = new List<company>();

        string path = "company.txt";
        public string Company_name
        {
            set
            {
                if (value.Length < 1) Console.WriteLine("Название компании не может быть пустым");
                else company_name = value;
            }
        }
        public bool isCompany_registration_date = false;
        public string Company_registration_date
        {
            set
            {
                DateTime temp;
                isCompany_registration_date = DateTime.TryParse(value, out temp);
                if (!isCompany_registration_date) Console.WriteLine("не верныфй формат даты");
                else company_registration_date = temp;
            }
        }
        public string List_ind
        {
            set
            {
                list_ind = value.Split(',');
            }
            get
            {
                return string.Join(",", list_ind);
            }
        }

        public void read_company()
        {
            bool fileExist = File.Exists(path);
            if (fileExist)
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string line;

                    company comp = new company();
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line == "***")
                        {
                            Console.WriteLine();
                            if (comp.isValid())
                            {
                                companyList.Add(comp);
                                comp = new company();
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

                        if (parameter == "counterparty_id") comp.Counterparty_id = value;
                        if (parameter == "bin_iin") comp.Bin_iin = value;
                        if (parameter == "dataCreate") comp.DataCreate = value;
                        if (parameter == "dataUpdate") comp.DataUpdate = value;
                        if (parameter == "authorCreate") comp.AuthorCreate = value;
                        if (parameter == "authorUpdate") comp.AuthorUpdate = value;

                        if (parameter == "company_name") comp.Company_name = value;
                        if (parameter == "company_registration_date") comp.Company_registration_date = value;
                        if (parameter == "list_ind") comp.List_ind = value;

                    }
                    print_companyl();
                }
            }
            else
            {
                Console.WriteLine("Файл не существует.");
            }
        }
        public void print_companyl()
        {
            // Сортировка по длиннам массивов (кол-во физических лиц)
            var sortedList = from p in companyList
                             orderby p.list_ind.Length descending
                             select p;
             
            foreach (company value in sortedList)
            {
                Console.WriteLine("counterparty_id = \t\t" + value.counterparty_id + "\n" +
                "bin_iin = \t\t\t" + value.bin_iin + "\n" +
                "dataCreate = \t\t\t" + value.dataCreate + "\n" +
                "dataUpdate = \t\t\t" + value.dataUpdate + "\n" +
                "authorCreate = \t\t\t" + value.authorCreate + "\n" +
                "authorUpdate = \t\t\t" + value.authorUpdate + "\n" +
                "company_name = \t\t\t" + value.company_name + "\n" +
                "company_registration_date = \t" + value.company_registration_date + "\n" +
                "list_ind = \t\t\t" +value.List_ind+ "\n");
            }
        }

        public void input_company()
        {
            Console.WriteLine("Введите параметры Юридического лица");
            company comp = new company();


            Console.WriteLine("Введите ID Юридического лица");
            while (comp.counterparty_id == 0)
                comp.Counterparty_id = Console.ReadLine();


            Console.WriteLine("Введите БИН Юридического лица");
            while (comp.bin_iin == 0)
                comp.Bin_iin = Console.ReadLine();

            Console.WriteLine("Введите Дату создания Юридического лица");
            while (!comp.isDataCreate)
                comp.DataCreate = Console.ReadLine();

            Console.WriteLine("Введите дату изменеия Юридического лица");
            while (!comp.isDataUpdate)
                comp.DataUpdate = Console.ReadLine();

            Console.WriteLine("Введите автора Юридического лица");
            while (comp.authorCreate == null)
                comp.AuthorCreate = Console.ReadLine();

            Console.WriteLine("Введите редактора Юридического лица");
            while (comp.authorUpdate == null)
                comp.AuthorUpdate = Console.ReadLine();

            Console.WriteLine("Введите название Юридического лица");
            while (comp.company_name == null)
                comp.Company_name = Console.ReadLine();

            Console.WriteLine("Введите дату регистрации Юридического лица");
            while (!comp.isCompany_registration_date)
                comp.Company_registration_date = Console.ReadLine();

            Console.WriteLine("Введите список физических лиц");
            while (comp.list_ind == null)
                comp.List_ind = Console.ReadLine();

            if (comp.isValid())
            {
                Console.WriteLine("Данные успешно записаны.");
                comp.write_company();
            }
            else Console.WriteLine("Не вырный формат данных.");

        }
        private void write_company()
        {
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLineAsync("counterparty_id=" + this.counterparty_id);
                writer.WriteLineAsync("bin_iin=" + this.bin_iin);
                writer.WriteLineAsync("dataCreate=" + this.dataCreate);
                writer.WriteLineAsync("dataUpdate=" + this.dataUpdate);
                writer.WriteLineAsync("authorCreate=" + this.authorCreate);
                writer.WriteLineAsync("authorUpdate=" + this.authorUpdate);
                writer.WriteLineAsync("company_name=" + this.company_name);
                writer.WriteLineAsync("company_registration_date=" + this.company_registration_date);
                writer.WriteLineAsync("list_ind=" + this.List_ind);
                writer.WriteLineAsync("***");
            }
        }







    }
}