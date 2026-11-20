using System;
using System.Globalization;
using Core.Models;

namespace ConsoleClient
{
    public static class InputHelper
    {
        public static string ReadString(string prompt, bool allowEmpty = false)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = (Console.ReadLine() ?? string.Empty).Trim();

                if (input.Contains('|'))
                {
                    Console.WriteLine("Greška: Tekst ne sme sadržati znak '|'.");
                    continue;
                }

                if (!allowEmpty && string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Greška: Polje ne sme biti prazno.");
                    continue;
                }

                return input;
            }
        }

        public static int ReadInt(string prompt, int? min = null, int? max = null)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = (Console.ReadLine() ?? string.Empty).Trim();

                if (!int.TryParse(input, out int value))
                {
                    Console.WriteLine("Greška: Unesite ispravan ceo broj.");
                    continue;
                }

                if (min.HasValue && value < min.Value)
                {
                    Console.WriteLine($"Greška: Vrednost mora biti najmanje {min.Value}.");
                    continue;
                }

                if (max.HasValue && value > max.Value)
                {
                    Console.WriteLine($"Greška: Vrednost može biti najviše {max.Value}.");
                    continue;
                }

                return value;
            }
        }

        public static double ReadDouble(string prompt, double? min = null, double? max = null)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = (Console.ReadLine() ?? string.Empty).Trim().Replace(',', '.');

                if (!double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
                {
                    Console.WriteLine("Greška: Unesite ispravan broj.");
                    continue;
                }

                if (min.HasValue && value < min.Value)
                {
                    Console.WriteLine($"Greška: Vrednost mora biti najmanje {min.Value}.");
                    continue;
                }

                if (max.HasValue && value > max.Value)
                {
                    Console.WriteLine($"Greška: Vrednost može biti najviše {max.Value}.");
                    continue;
                }

                return value;
            }
        }

        public static DateTime ReadDate(string prompt)
        {
            string[] formats = { "dd.MM.yyyy.", "dd.MM.yyyy" };

            while (true)
            {
                Console.Write(prompt);
                string input = (Console.ReadLine() ?? string.Empty).Trim();

                if (DateTime.TryParseExact(input, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                {
                    return date;
                }

                Console.WriteLine("Greška: Unesite datum u formatu dd.MM.yyyy. (npr. 15.05.2000.).");
            }
        }

        public static StatusPosetioca ReadStatus(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = (Console.ReadLine() ?? string.Empty).Trim();

                if (input == "1") return StatusPosetioca.Redovan;
                if (input == "2") return StatusPosetioca.VIP;

                Console.WriteLine("Greška: Unesite 1 za Redovan ili 2 za VIP.");
            }
        }
    }
}