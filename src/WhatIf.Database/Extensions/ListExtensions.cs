﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WhatIf.Database.Extensions
{
    public static class ListExtensions
    {
        private static readonly Random Rng = new Random(DateTime.Now.Millisecond);

        public static void Shuffle<T>(this IList<T> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = Rng.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
