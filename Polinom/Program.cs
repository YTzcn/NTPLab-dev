using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Polinom
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Polinom işlemi (exit yazarak çıkabilirsiniz).");

            while (true)
            {
                // Kullanıcıdan polinomları al
                Console.WriteLine("\nBirinci polinomu girin (örneğin: 2x^2 + 3x - 5):");
                string poly1Input = Console.ReadLine();
                if (poly1Input.ToLower() == "exit") break;

                Console.WriteLine("İkinci polinomu girin (örneğin: x^2 - 4):");
                string poly2Input = Console.ReadLine();
                if (poly2Input.ToLower() == "exit") break;

                try
                {
                    // Polinomları ayrıştır ve işlem yap
                    var poly1 = ParsePolynomial(poly1Input);
                    var poly2 = ParsePolynomial(poly2Input);

                    // Toplama ve çıkarma işlemleri
                    var sum = AddPolynomials(poly1, poly2);
                    var difference = SubtractPolynomials(poly1, poly2);

                    // Sonuçları göster
                    Console.WriteLine("\nPolinomların Toplamı:");
                    PrintPolynomial(sum);

                    Console.WriteLine("\nPolinomların Farkı:");
                    PrintPolynomial(difference);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata: {ex.Message}");
                }
            }

            Console.WriteLine("Programdan çıkılıyor...");
        }

        // Polinomu ayrıştırma metodu
        static Dictionary<int, double> ParsePolynomial(string polynomial)
        {
            var terms = new Dictionary<int, double>();
            var termMatches = Regex.Matches(polynomial, @"([+-]?\s*\d*\.?\d*)\s*\*?\s*x\^?(\d*)");

            foreach (Match match in termMatches)
            {
                string coefficientStr = match.Groups[1].Value.Replace(" ", "");
                string exponentStr = match.Groups[2].Value;

                double coefficient = string.IsNullOrEmpty(coefficientStr) || coefficientStr == "+" ? 1 :
                                     coefficientStr == "-" ? -1 : double.Parse(coefficientStr);

                int exponent = string.IsNullOrEmpty(exponentStr) ? 1 : int.Parse(exponentStr);

                if (terms.ContainsKey(exponent))
                    terms[exponent] += coefficient;
                else
                    terms[exponent] = coefficient;
            }

            // Sabit terim (x olmadan)
            var constantMatch = Regex.Match(polynomial, @"([+-]?\s*\d+\.?\d*)\b(?!x)");
            if (constantMatch.Success)
            {
                double constant = double.Parse(constantMatch.Groups[1].Value.Replace(" ", ""));
                if (terms.ContainsKey(0))
                    terms[0] += constant;
                else
                    terms[0] = constant;
            }

            return terms;
        }

        // İki polinomu toplama
        static Dictionary<int, double> AddPolynomials(Dictionary<int, double> poly1, Dictionary<int, double> poly2)
        {
            var result = new Dictionary<int, double>(poly1);

            foreach (var term in poly2)
            {
                if (result.ContainsKey(term.Key))
                    result[term.Key] += term.Value;
                else
                    result[term.Key] = term.Value;
            }

            return result;
        }

        // İki polinomu çıkarma
        static Dictionary<int, double> SubtractPolynomials(Dictionary<int, double> poly1, Dictionary<int, double> poly2)
        {
            var result = new Dictionary<int, double>(poly1);

            foreach (var term in poly2)
            {
                if (result.ContainsKey(term.Key))
                    result[term.Key] -= term.Value;
                else
                    result[term.Key] = -term.Value;
            }

            return result;
        }

        // Polinomu ekrana yazdırma
        static void PrintPolynomial(Dictionary<int, double> polynomial)
        {
            var terms = new List<string>();

            foreach (var term in polynomial)
            {
                if (term.Value == 0) continue;

                string termStr = term.Value.ToString("0.##");
                if (term.Key > 0)
                {
                    termStr += "x";
                    if (term.Key > 1)
                        termStr += "^" + term.Key;
                }

                terms.Add((term.Value > 0 ? "+" : "") + termStr);
            }

            string result = string.Join(" ", terms).Trim();
            if (result.StartsWith("+")) result = result.Substring(1).Trim();
            Console.WriteLine(result);
        }
    }
}
