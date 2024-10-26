using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZamanMakinesi
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> validDates = GetValidDates();

            Console.WriteLine("Cihazın kabul ettiği tarihler:");
            foreach (var date in validDates)
            {
                Console.WriteLine(date);
            }
            Console.WriteLine("Toplam Tarih Sayısı : "+validDates.Count);
            Console.ReadLine();
        }

        static List<string> GetValidDates()
        {
            List<string> validDates = new List<string>();
            DateTime currentDate = DateTime.Now;

            for (int year = 2000; year <= 3000; year++)
            {
                for (int month = 1; month <= 12; month++)
                {
                    for (int day = 1; day <= DateTime.DaysInMonth(year, month); day++)
                    {
                        DateTime date = new DateTime(year, month, day);
                        if (date > currentDate && IsValidDate(day, month, year))
                        {
                            validDates.Add(date.ToString("dd/MM/yyyy"));
                        }
                    }
                }
            }

            return validDates;
        }

        static bool IsValidDate(int day, int month, int year)
        {
            return IsPrime(day) && IsSumOfDigitsEven(month) && IsDigitSumLessThanQuarter(year);
        }

        static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }

        static bool IsSumOfDigitsEven(int number)
        {
            int sum = 0;
            while (number > 0)
            {
                sum += number % 10;
                number /= 10;
            }
            return sum % 2 == 0;
        }

        static bool IsDigitSumLessThanQuarter(int year)
        {
            int digitSum = 0;
            int tempYear = year;

            while (tempYear > 0)
            {
                digitSum += tempYear % 10;
                tempYear /= 10;
            }

            return digitSum < (year / 4);
        }
    }
}
