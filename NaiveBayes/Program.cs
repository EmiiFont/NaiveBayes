﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveBayes
{
    class Program
    {
        static void Main(string[] args)
        {
            var nb = new NaiveBayes();
            nb.Trainer();
            nb.Test();
            Console.ReadLine();
        }
    }
}
