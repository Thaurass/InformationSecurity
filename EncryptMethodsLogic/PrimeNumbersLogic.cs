using System.Diagnostics;
using System.Numerics;

namespace EncryptMethodsLogic;

public class PrimeNumbersLogic
{
    public bool RabinSimplicityTest(BigInteger n, int k)
    {
        // если n == 2 или n == 3 - эти числа простые, возвращаем true
        if (n == 2 || n == 3)
            return true;

        // если n < 2 или n четное - возвращаем false
        if (n < 2 || n % 2 == 0)
            return false;

        // представим n − 1 в виде (2^s)·t, где t нечётно, это можно сделать последовательным делением n - 1 на 2
        BigInteger t = n - 1;

        int s = 0;

        while (t % 2 == 0)
        {
            t /= 2;
            s += 1;
        }

        // повторить k раз
        for (int i = 0; i < k; i++)
        {
            // выберем случайное целое число a в отрезке [2, n − 2]
            Random rng = new();

            byte[] _a = new byte[n.ToByteArray().LongLength];

            BigInteger a;

            do
            {
                rng.NextBytes(_a);
                a = new BigInteger(_a);
            }
            while (a < 2 || a >= n - 2);

            // x ← a^t mod n, вычислим с помощью возведения в степень по модулю
            BigInteger x = BigInteger.ModPow(a, t, n);

            // если x == 1 или x == n − 1, то перейти на следующую итерацию цикла
            if (x == 1 || x == n - 1)
                continue;

            // повторить s − 1 раз
            for (int r = 1; r < s; r++)
            {
                // x ← x^2 mod n
                x = BigInteger.ModPow(x, 2, n);

                // если x == 1, то вернуть "составное"
                if (x == 1)
                    return false;

                // если x == n − 1, то перейти на следующую итерацию внешнего цикла
                if (x == n - 1)
                    break;
            }

            if (x != n - 1)
                return false;
        }

        // вернуть "вероятно простое"
        return true;
    }

    public string FunctionAutomatisation(string Step)
    {
        string Data = String.Empty;
        Random rng = new();
        Stopwatch stopwatch = new();

        ulong firstNum = Convert.ToUInt32(rng.Next(3, 10));
        Data += firstNum + "   ";
        stopwatch.Start();
        Data += RabinSimplicityTest(firstNum, Convert.ToInt32(Step)).ToString();
        stopwatch.Stop();
        
        System.Threading.Thread.Sleep(1);
        Data += Environment.NewLine;
        TimeSpan ts = stopwatch.Elapsed;
        string elapsedTime = String.Format("{0:f8}", ts.TotalMilliseconds);
        Data += elapsedTime;

        Data += Environment.NewLine;
        ulong secondNum = Convert.ToUInt32(rng.Next(10, 100));
        Data += secondNum + "   ";
        stopwatch.Restart();
        Data += RabinSimplicityTest(secondNum, Convert.ToInt32(Step)).ToString();
        stopwatch.Stop();

        System.Threading.Thread.Sleep(1);
        Data += Environment.NewLine;
        ts = stopwatch.Elapsed;
        elapsedTime = String.Format("{0:f8}", ts.TotalMilliseconds);
        Data += elapsedTime;

        Data += Environment.NewLine;
        ulong thirdNum = Convert.ToUInt32(rng.Next(100, 1000));
        Data += thirdNum + "   ";
        stopwatch.Restart();
        Data += RabinSimplicityTest(thirdNum, Convert.ToInt32(Step)).ToString();
        stopwatch.Stop();

        System.Threading.Thread.Sleep(1);
        Data += Environment.NewLine;
        ts = stopwatch.Elapsed;
        elapsedTime = String.Format("{0:f8}", ts.TotalMilliseconds);
        Data += elapsedTime;

        Data += Environment.NewLine;
        ulong fourthNum = Convert.ToUInt64(rng.Next(1000, 10000));
        Data += fourthNum + "   ";
        stopwatch.Restart();
        Data += RabinSimplicityTest(fourthNum, Convert.ToInt32(Step)).ToString();
        stopwatch.Stop();

        System.Threading.Thread.Sleep(1);
        Data += Environment.NewLine;
        ts = stopwatch.Elapsed;
        elapsedTime = String.Format("{0:f8}", ts.TotalMilliseconds);
        Data += elapsedTime;

        Data += Environment.NewLine;
        ulong fifthNum = Convert.ToUInt64(rng.Next(10000, 100000));
        Data += fifthNum + "   ";
        stopwatch.Restart();
        Data += RabinSimplicityTest(fifthNum, Convert.ToInt32(Step)).ToString();
        stopwatch.Stop();

        System.Threading.Thread.Sleep(1);
        Data += Environment.NewLine;
        ts = stopwatch.Elapsed;
        elapsedTime = String.Format("{0:f8}", ts.TotalMilliseconds);
        Data += elapsedTime;

        return Data;
    }
}
