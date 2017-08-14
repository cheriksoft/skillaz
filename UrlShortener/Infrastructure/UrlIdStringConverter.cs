using System;
using System.Runtime.InteropServices.ComTypes;

namespace UrlShortener.Infrastructure
{
    public class UrlIdStringConverter : IUrlIdStringConverter
    {
        private const string ALPHABET = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public string ConvertFromNumber(long number)
        {
            var result = "";

            while (number >= ALPHABET.Length)
            {
                result = ALPHABET[(int) (number % ALPHABET.Length)] + result;
                number /= ALPHABET.Length;
            }

            return ALPHABET[(int) number] + result;
        }

        public long ConvertFromString(string urlId)
        {
            if (string.IsNullOrWhiteSpace(urlId))
                throw new ArgumentException();

            long result = 0;

            for (var i = 0; i < urlId.Length; i++)
            {
                var index = ALPHABET.IndexOf(urlId[i]);

                if (index < 0) throw new ArgumentException();

                result += ALPHABET.IndexOf(urlId[i]) * LongPower(ALPHABET.Length, urlId.Length - i - 1);
            }

            return result;
        }

        //Чистое возведение в степень, чтобы избежать потенциальных проблем с точностью при кастовании double к long
        private long LongPower(int number, int power)
        {
            var result = 1;

            for (var i = 0; i <= power; i++)
            {
                result *= number;
            }

            return result;
        }
    }
}