using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi
{
    public class PIn2
    {
        /// <summary>
        /// Calculate PI
        /// Running time of O(n^2).
        /// </summary>
        public static string Calculate(int digits)
        {
            StringBuilder result = new StringBuilder();
            result.Append("3.");
            DateTime StartTime = DateTime.Now;

            if (digits > 0)
            {

                for (int i = 0; i < digits; i += 9)
                {
                    String ds = CalculatePiDigits(i + 1);
                    int digitCount = Math.Min(digits - i, 9);

                    if (ds.Length < 9)
                        ds = string.Format("{0:D9}", int.Parse(ds));

                    result.Append(ds.Substring(0, digitCount));
                }
            }
            return result.ToString();
        }


        private static int MulMod(int a, int b, int m)
        {
            return (int)(((long)a * (long)b) % m);
        }

        /// <summary>
        /// Return the inverse of x mod y. 
        /// </summary>
        private static int InvMod(int x, int y)
        {
            int q, u, v, a, c, t;
            u = x;
            v = y;
            c = 1;
            a = 0;
            do
            {
                q = v / u;
                t = c;
                c = a - q * c;
                a = t;
                t = u;
                u = v - q * u;
                v = t;
            }
            while (u != 0);

            a = a % y;
            if (a < 0)
            {
                a = y + a;
            }
            return a;
        }

        /// <summary>
        /// Return (a^b) mod m
        /// </summary>
        private static int PowMod(int a, int b, int m)
        {
            int r, aa;
            r = 1;
            aa = a;

            while (true)
            {
                if ((b & 1) != 0)
                {
                    r = MulMod(r, aa, m);
                }
                b = b >> 1;
                if (b == 0)
                {
                    break;
                }
                aa = MulMod(aa, aa, m);
            }
            return r;
        }

        /// <summary>
        /// Return true if n is prime.
        /// </summary>
        private static bool IsPrime(int n)
        {
            if ((n % 2) == 0)
            {
                return false;
            }

            int r = (int)Math.Sqrt(n);

            for (int i = 3; i <= r; i += 2)
            {
                if ((n % i) == 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Return the prime number immediatly after n. 
        /// </summary>
        private static int NextPrime(int n)
        {
            do
            {
                n++;
            }
            while (!IsPrime(n));

            return n;
        }

        private static String CalculatePiDigits(int n)
        {
            int av, vmax, num, den, s, t;
            int N = (int)((n + 20) * Math.Log(10) / Math.Log(2));
            double sum = 0;
            for (int a = 3; a <= (2 * N); a = NextPrime(a))
            {
                vmax = (int)(Math.Log(2 * N) / Math.Log(a));
                av = 1;
                for (int i = 0; i < vmax; i++)
                {
                    av = av * a;
                }
                s = 0;
                num = 1;
                den = 1;
                int v = 0;
                int kq = 1;
                int kq2 = 1;

                for (int k = 1; k <= N; k++)
                {
                    t = k;
                    if (kq >= a)
                    {
                        do
                        {
                            t = t / a;
                            v--;
                        } while ((t % a) == 0);

                        kq = 0;
                    }

                    kq++;
                    num = MulMod(num, t, av);
                    t = 2 * k - 1;

                    if (kq2 >= a)
                    {
                        if (kq2 == a)
                        {
                            do
                            {
                                t = t / a;
                                v++;
                            } while ((t % a) == 0);
                        }
                        kq2 -= a;
                    }

                    den = MulMod(den, t, av);
                    kq2 += 2;

                    if (v > 0)
                    {
                        t = InvMod(den, av);
                        t = MulMod(t, num, av);
                        t = MulMod(t, k, av);

                        for (int i = v; i < vmax; i++)
                        {
                            t = MulMod(t, a, av);
                        }
                        s += t;
                        if (s >= av)
                        {
                            s -= av;
                        }
                    }
                }

                t = PowMod(10, n - 1, av);
                s = MulMod(s, t, av);
                sum = (sum + (double)s / (double)av) % 1.0;
            }

            int Result = (int)(sum * 1e9);
            String StringResult = String.Format("{0:D9}", Result);

            return StringResult;
        }

        /// <summary>
        /// Put a space between every group of 10 digits.
        /// </summary>
        private static String BreakDigitsIntoGroupsOf10(String digits)
        {
            String result = "";
            while (digits.Length > 10)
            {
                result += digits.Substring(0, 10) + " ";
                digits = digits.Substring(10, digits.Length - 10);
            }
            result += digits;
            return result;
        }
    }
}
