using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_task
{
    internal class counterparty // контрагент
    {
        public int counterparty_id;
        public long bin_iin;

        public DateTime dataCreate;
        public DateTime dataUpdate;
        public string authorCreate;
        public string authorUpdate;

        public string Counterparty_id
        {
            set
            {
                int temp;
                bool isNumber = int.TryParse(value, out temp);
                if (!isNumber || temp < 1) Console.WriteLine("Не верный ID");
                else counterparty_id = temp;
            }
        }
        public string Bin_iin
        {
            set
            {
                long temp2;
                bool isNumber = long.TryParse(value, out temp2);
                if (!isNumber) Console.WriteLine("БИН/ИИН не число");
                else if (value.Length < 12) Console.WriteLine("БИН/ИИН короткий"); // БИН и ИИН должны сожержать минимум 12 цифр
                else bin_iin = temp2;
            }
        }
        public bool isDataCreate = false;
        public string DataCreate
        {
            set
            {
                DateTime temp;
                isDataCreate = DateTime.TryParse(value, out temp);
                if (!isDataCreate) Console.WriteLine("не верныфй формат даты");
                else dataCreate = temp;
            }
        }
        public bool isDataUpdate = false;
        public string DataUpdate
        {
            set
            {
                DateTime temp;
                isDataUpdate = DateTime.TryParse(value, out temp);
                if (!isDataUpdate) Console.WriteLine("не верныфй формат даты");
                else if(temp < dataCreate) Console.WriteLine("дата изменения не может быть раньше даты создания");
                else dataUpdate = temp;
            }
        }
        public string AuthorCreate
        {
            set
            {
                int isNotText = (value.Where(x => Char.IsDigit(x)).ToArray()).Length;
                if (value.Length < 1) Console.WriteLine("Фио автора не может быть пустым");
                else if (isNotText > 0) Console.WriteLine("В фио автора не должны входить цифры");
                else authorCreate = value;
            }
        }

        public string AuthorUpdate
        {
            set
            {
                int isNotText = (value.Where(x => Char.IsDigit(x)).ToArray()).Length;
                if (value.Length < 1) Console.WriteLine("Фио редактора не может быть пустым");
                else if (isNotText > 0) Console.WriteLine("В фио редактора не должны входить цифры");
                else authorUpdate = value;
            }
        }

        public bool isValid()
        {
            if (counterparty_id > 0 &&
                bin_iin > 0 &&
                dataCreate < dataUpdate &&
                authorCreate.Length > 0 &&
                authorUpdate.Length > 0)
                return true;
            else return false;

        }


    }

}
