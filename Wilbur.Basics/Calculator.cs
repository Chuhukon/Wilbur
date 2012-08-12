using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wilbur.Basics
{
    public class Calculator : Wilbur.Basics.ICalculator
    {
        private int _a;
        private int _b;

        public Calculator(int a, int b)
        {
            _a = a;
            _b = b;
        }

        public int Sum()
        {
            return _a + _b;
        }

        public decimal Divide()
        {
            return Convert.ToDecimal(_a) / Convert.ToDecimal(_b);
        }
    }
}
