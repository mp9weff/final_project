using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace final_project
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Phone> phones = new List<Phone>();

            string[] files = new string[] { "phones1.txt", "phones2.txt" };
            foreach (string file in files)
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length != 4)
                            continue;

                        if (file == "phones1.txt")
                        {
                            MobilePhone phone = new MobilePhone();
                            phone.Name = parts[0];
                            phone.Brand = parts[1];
                            if (decimal.TryParse(parts[2], out decimal price))
                                phone.Price = price;
                            phone.Color = parts[3];
                            if (int.TryParse(parts[4], out int memory))
                                phone.Memory = memory;
                            phones.Add(phone);
                        }
                        else if (file == "phones2.txt")
                        {
                            RadioPhone phone = new RadioPhone();
                            phone.Name = parts[0];
                            phone.Brand = parts[1];
                            if (decimal.TryParse(parts[2], out decimal price))
                                phone.Price = price;
                            if (decimal.TryParse(parts[3], out decimal range))
                                phone.Range = range;
                            if (bool.TryParse(parts[4], out bool hasAnsweringMachine))
                                phone.HasAnsweringMachine = hasAnsweringMachine;
                            phones.Add(phone);
                        }
                    }
                }
            }

            // 1. Вивести всі телефони, посортовані за ціною із зазначенням загальної суми.
            decimal totalPrice = phones.Sum(p => p.Price);
            phones = phones.OrderBy(p => p.Price).ToList();
            using (StreamWriter writer = new StreamWriter("all_phones_sorted_by_price.txt"))
            {
                writer.WriteLine($"Total price: {totalPrice:C}");
                foreach (Phone phone in phones)
                {
                    writer.WriteLine($"{phone.Name}, {phone.Brand}, {phone.Price:C}");
                }
            }

            // 2. Вивести радіотелефони з автовідповідачем.
            List<RadioPhone> radioPhonesWithAnsweringMachine = phones.OfType<RadioPhone>().Where(p => p.HasAnsweringMachine).ToList();
            using (StreamWriter writer = new StreamWriter("radio_phones_with_answering_machine.txt"))
            {
                foreach (RadioPhone phone in radioPhonesWithAnsweringMachine)
                {
                    writer.WriteLine($"{phone.Name}, {phone.Brand}, {phone.Price:C}, {phone.Range}, {phone.HasAnsweringMachine}");
                }
            }
        }
    }
}