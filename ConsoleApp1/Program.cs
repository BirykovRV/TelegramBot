using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person1 = new Person("Roman", "Birukov", 26);
            Person person2 = new Person("Irina", "Romashina", 29);
            List<Person> peoples = new List<Person>() { person1, person2 };

            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(List< Person >));

            using (FileStream fs = new FileStream("people.json", FileMode.OpenOrCreate))
            {
                jsonSerializer.WriteObject(fs, peoples);
            }

            using (FileStream fs = new FileStream("people.json", FileMode.OpenOrCreate))
            {
                List< Person > newPeoples = (List< Person >) jsonSerializer.ReadObject(fs);

                foreach (var p in newPeoples)
                {
                    Console.WriteLine($"Имя: {p.FirstName}\nФамилия: {p.LastName}\nВозраст: {p.Age}");
                }                
            }
            Console.ReadKey();
        }
    }
}
