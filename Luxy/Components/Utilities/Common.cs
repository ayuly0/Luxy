﻿using System;
using System.Text;

namespace Luxy.Components.Utilities
{
    internal static class Common
    {
        internal static string GenerateRandomString(int length)
        {
            Random random = new Random();
            string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < length; i++)
                result.Append(chars[random.Next(0, chars.Length)]);

            return result.ToString();
        }
    }
}
