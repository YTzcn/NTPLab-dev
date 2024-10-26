using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArdışılSayı
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
                // Sayıları sıralama
                numbers.Sort();

                // Ardışık grupları tespit et
                List<string> ranges = new List<string>();
                int start = numbers[0];
                int end = numbers[0];

                for (int i = 1; i < numbers.Count; i++)
                {
                    if (numbers[i] == end + 1)
                    {
                        // Ardışık sayı ise aralığın sonunu güncelle
                        end = numbers[i];
                    }
                    else
                    {
                        // Ardışık değilse mevcut aralığı kaydet
                        if (start == end)
                        {
                            ranges.Add(start.ToString());
                        }
                        else
                        {
                            ranges.Add($"{start}-{end}");
                        }
                        // Yeni aralığa başla
                        start = numbers[i];
                        end = numbers[i];
                    }
                }

                // Son aralığı ekle
                if (start == end)
                {
                    ranges.Add(start.ToString());
                }
                else
                {
                    ranges.Add($"{start}-{end}");
                }

                // Sonuçları ekrana yazdır
                Console.WriteLine("Ardışık sayı grupları:");
                Console.WriteLine(string.Join(", ", ranges));
                Console.ReadLine();
            }
        }
    }
}
