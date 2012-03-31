using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lombardia.Classes
{
    class Utils
    {
        public static string NumberToWords(int N)
        {
            string ret = "";

            int digit;

            digit = N - ((N / 10) * 10);

            if (N == 0)
                return "զրո";

            switch (digit)
            {
                case 1:
                    ret += "մեկ";
                    break;
                case 2:
                    ret += "երկու";
                    break;
                case 3:
                    ret += "երեք";
                    break;
                case 4:
                    ret += "չորս";
                    break;
                case 5:
                    ret += "հինգ";
                    break;
                case 6:
                    ret += "վեց";
                    break;
                case 7:
                    ret += "յոթ";
                    break;
                case 8:
                    ret += "ութ";
                    break;
                case 9:
                    ret += "ինը";
                    break;
            }

            if (N < 10)
                return ret;

            int digit1 = (N / 10) - ((N / 100) * 10);

            switch (digit1)
            {
                case 1:
                    switch (digit)
                    {
                        case 0:
                            ret = "տաս";
                            break;
                        default:
                            ret = "տասն" + ret;
                            break;
                    }
                    break;
                case 2:
                    ret = "քսան" + ret;
                    break;
                case 3:
                    ret = "երեսուն" + ret;
                    break;
                case 4:
                    ret = "քառասուն" + ret;
                    break;
                case 5:
                    ret = "հիսուն" + ret;
                    break;
                case 6:
                    ret = "վաթսուն" + ret;
                    break;
                case 7:
                    ret = "յոթանասուն" + ret;
                    break;
                case 8:
                    ret = "ութսուն" + ret;
                    break;
                case 9:
                    ret = "իննսուն" + ret;
                    break;
            }

            if (N < 100)
                return ret;

            digit = (N / 100) - ((N / 1000) * 10);

            switch (digit)
            {
                case 1:
                    ret = "հարյուր " + ret;
                    break;
                case 0:
                    break;
                default:
                    ret = NumberToWords(digit) + " հարյուր " + ret;
                    break;
            }

            if (N < 1000)
                return ret;

            digit = (N / 1000) - ((N / 1000000) * 1000);

            if (digit != 0)
            {
                if (digit == 1)
                    ret = "հազար " + ret;
                else
                {
                    ret = NumberToWords(digit) + " հազար " + ret;
                }
            }

            if (N < 1000000)
                return ret;

            digit = (N / 1000000) - ((N / 1000000000) * 1000);

            if (digit != 0)
            {
                ret = NumberToWords(digit) + " միլիոն " + ret;
            }

            if (N < 1000000000)
                return ret;

            if (N == 1000000000)
                return "մեկ միլիարդ";

            return ret;
        }
    }
}
