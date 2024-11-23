using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public class Helper
    {
        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new char[length];
            using (var crypto = RandomNumberGenerator.Create())
            {
                var data = new byte[length];
                crypto.GetBytes(data);
                for (var i = 0; i < data.Length; i++)
                {
                    random[i] = chars[data[i] % chars.Length];
                }
            }
            return new string(random);
        }

        public static float CalculateAverageFloat(List<float> num)
        {
            int count = num.Count();
            float sum = 0;
            foreach (var item in num)
            {
                sum += item;
            }
            return (float)Math.Round((double)sum/count, 1);
        }

        public static int CalculateAverageInt(List<int> num)
        {
            int count = num.Count();
            float sum = 0;
            foreach (var item in num)
            {
                sum += item;
            }
            return (int)sum / count;
        }

        public static List<T> GetRandomObjects<T>(List<T> list, int count)
        {
            if (list == null || count <= 0) return new List<T>();

            var random = new Random();
            var randomList = new List<T>();

            count = Math.Min(count, list.Count);

            var tempList = new List<T>(list); 

            for (int i = 0; i < count; i++)
            {
                var randomIndex = random.Next(0, tempList.Count);
                randomList.Add(tempList[randomIndex]);
                tempList.RemoveAt(randomIndex); 
            }

            return randomList;
        }
    } 
}
