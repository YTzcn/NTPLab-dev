using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizemliAda
{
    class Program
    {
        static void Main()
        {
            // Örnek sayı dizisi
            int[] numbers = { 3, 5, 2, 8 };
            List<string> results = new List<string>();
            FindCombinations(numbers, results);

            Console.WriteLine("Geçerli dizilimler:");
            foreach (var result in results)
            {
                Console.WriteLine(result);
            }

            if (results.Count == 0)
            {
                Console.WriteLine("Geçerli bir dizilim bulunamadı.");
            }
            Console.ReadLine();
        }

        static void FindCombinations(int[] numbers, List<string> results)
        {
            string[] operators = { "+", "-", "*", "/" };
            int n = numbers.Length;

            // Tüm kombinasyonları bul
            GenerateCombinations(numbers, operators, 0, "", results);
        }

        static void GenerateCombinations(int[] numbers, string[] operators, int index, string current, List<string> results)
        {
            // Eğer tüm sayılar kullanıldıysa
            if (index == numbers.Length)
            {
                // Hesapla ve kontrol et
                if (Evaluate(current) > 0)
                {
                    results.Add(current);
                }
                return;
            }

            // Sayıyı ekle
            string number = numbers[index].ToString();

            // İlk sayıyı direkt ekle
            if (current == "")
            {
                GenerateCombinations(numbers, operators, index + 1, number, results);
            }
            else
            {
                // Her operatör için döngü
                foreach (var op in operators)
                {
                    string newExpression = current + op + number;

                    // Geçerli dizilimi oluştur
                    GenerateCombinations(numbers, operators, index + 1, newExpression, results);
                }
            }
        }

        static double Evaluate(string expression)
        {
            // Basit bir matematiksel ifade değerlendirme
            try
            {
                DataTable table = new DataTable();
                return Convert.ToDouble(table.Compute(expression, ""));
            }
            catch
            {
                return double.MinValue; // Hatalı bir durum için
            }
        }
    }
}
