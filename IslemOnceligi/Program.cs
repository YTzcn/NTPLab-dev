using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IslemOnceligi
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Matematiksel ifadeyi girin (örneğin: 3 + 4 * 2 / (1 - 5) ^ 2 ^ 3):");
            string expression = Console.ReadLine();

            try
            {
                // İfade çözümle ve sonucu bul
                string stepByStepSolution = EvaluateExpression(expression, out double result);

                // Çözüm sürecini ve sonucu ekrana yazdır
                Console.WriteLine("\nİfadenin çözüm süreci:");
                Console.WriteLine(stepByStepSolution);
                Console.WriteLine($"\nSonuç: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
            }
            Console.ReadLine();
        }

        static string EvaluateExpression(string expression, out double finalResult)
        {
            // Bu metod işlem önceliğine göre adım adım ifadeyi çözer ve süreci döndürür.
            var dataTable = new DataTable();
            string steps = "";
            string currentExpression = expression;

            // İşlem öncelik sırası: Parantezler, üs alma (^), çarpma/bölme (*,/), toplama/çıkarma (+,-)
            while (true)
            {
                string simplifiedExpression = SimplifyExpression(currentExpression);
                if (simplifiedExpression == currentExpression)
                {
                    // Eğer daha fazla sadeleştirme yoksa, son sonucu belirle
                    finalResult = Convert.ToDouble(dataTable.Compute(currentExpression, null));
                    steps += $"{currentExpression} = {finalResult}\n";
                    break;
                }

                // Adım adım sonucu ekle
                double intermediateResult = Convert.ToDouble(dataTable.Compute(simplifiedExpression, null));
                steps += $"{simplifiedExpression} = {intermediateResult}\n";
                currentExpression = simplifiedExpression;
            }

            return steps;
        }

        static string SimplifyExpression(string expression)
        {
            // Öncelik sırasına göre işlemleri basitleştir
            // Üs alma çözümü (sağdan sola doğru işlem yapılır)
            var powerMatch = Regex.Match(expression, @"(\d+\.?\d*)\s*\^\s*(\d+\.?\d*)");
            if (powerMatch.Success)
            {
                double baseNum = double.Parse(powerMatch.Groups[1].Value);
                double exponent = double.Parse(powerMatch.Groups[2].Value);
                double result = Math.Pow(baseNum, exponent);
                expression = expression.Replace(powerMatch.Value, result.ToString());
                return expression;
            }

            // Çarpma ve bölme çözümü
            var mulDivMatch = Regex.Match(expression, @"(\d+\.?\d*)\s*([\*/])\s*(\d+\.?\d*)");
            if (mulDivMatch.Success)
            {
                double left = double.Parse(mulDivMatch.Groups[1].Value);
                double right = double.Parse(mulDivMatch.Groups[3].Value);
                double result = mulDivMatch.Groups[2].Value == "*" ? left * right : left / right;
                expression = expression.Replace(mulDivMatch.Value, result.ToString());
                return expression;
            }

            // Toplama ve çıkarma çözümü
            var addSubMatch = Regex.Match(expression, @"(\d+\.?\d*)\s*([\+-])\s*(\d+\.?\d*)");
            if (addSubMatch.Success)
            {
                double left = double.Parse(addSubMatch.Groups[1].Value);
                double right = double.Parse(addSubMatch.Groups[3].Value);
                double result = addSubMatch.Groups[2].Value == "+" ? left + right : left - right;
                expression = expression.Replace(addSubMatch.Value, result.ToString());
                return expression;
            }

            // İşlem yapılmadıysa aynı ifadeyi döndür
            return expression;
        }
    }

}

