using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrtalamaMedyan_
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>();
            int input;

            // Kullanıcıdan pozitif tam sayılar al
            Console.WriteLine("Pozitif tam sayılar girin (Çıkmak için 0 girin):");
            do
            {
                Console.Write("Sayı: ");
                input = int.Parse(Console.ReadLine());

                if (input > 0)
                {
                    numbers.Add(input);
                }

            } while (input != 0);

            // Eğer sayı listesi boşsa işlem yapma
            if (numbers.Count == 0)
            {
                Console.WriteLine("Hiçbir sayı girilmedi.");
            }
            else
            {
                // Ortalama hesapla
                double average = numbers.Average();

                // Medyan hesapla
                numbers.Sort();
                double median;
                int count = numbers.Count;
                if (count % 2 == 0)
                {
                    // Çift sayıda eleman varsa ortadaki iki sayının ortalaması
                    median = (numbers[count / 2 - 1] + numbers[count / 2]) / 2.0;
                }
                else
                {
                    // Tek sayıda eleman varsa ortadaki sayı
                    median = numbers[count / 2];
                }

                // Sonuçları ekrana yazdır
                Console.WriteLine($"Girilen sayıların ortalaması: {average}");
                Console.WriteLine($"Girilen sayıların medyanı: {median}");
                Console.ReadLine();
            }
        }
    }
}
