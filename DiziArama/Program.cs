using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiziArama
{
    class Program
    {
        static void Main(string[] args)
        {  // Kullanıcıdan dizi boyutunu ve elemanlarını al
            Console.Write("Kaç adet sayı gireceksiniz? ");
            int n = int.Parse(Console.ReadLine());
            int[] numbers = new int[n];

            Console.WriteLine("Lütfen sayıları girin:");
            for (int i = 0; i < n; i++)
            {
                Console.Write($"Sayı {i + 1}: ");
                numbers[i] = int.Parse(Console.ReadLine());
            }

            // Diziyi sırala
            Array.Sort(numbers);
            Console.WriteLine("Sıralanmış dizi: " + string.Join(", ", numbers));

            // Kullanıcıdan aranacak sayıyı al
            Console.Write("Aramak istediğiniz sayıyı girin: ");
            int target = int.Parse(Console.ReadLine());

            // İkili arama algoritması ile kontrol et
            bool found = BinarySearch(numbers, target);

            // Sonucu ekrana yazdır
            if (found)
            {
                Console.WriteLine($"{target} sayısı dizide bulunuyor.");
            }
            else
            {
                Console.WriteLine($"{target} sayısı dizide bulunmuyor.");
            }
            Console.ReadLine();
        }

        // İkili arama fonksiyonu
        static bool BinarySearch(int[] array, int target)
        {
            int left = 0;
            int right = array.Length - 1;

            while (left <= right)
            {
                int mid = (left + right) / 2;

                if (array[mid] == target)
                {
                    return true; // Sayı bulundu
                }
                else if (array[mid] < target)
                {
                    left = mid + 1; // Sağ tarafa devam et
                }
                else
                {
                    right = mid - 1; // Sol tarafa devam et
                }
            }

            return false; // Sayı bulunamadı
        }
    }
}
