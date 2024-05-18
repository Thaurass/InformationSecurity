using System.Diagnostics;


namespace EncryptMethodsLogic;

public class EuclideanAlgorithmLogic
{
    public string Encrypt_func_Euclidean_Algorithm_4(string FirstNum, string SecondNum, string Method)
    {
        string Data = "";

        int u1, u2, u3;
        int a = Convert.ToInt32(FirstNum);
        int b = Convert.ToInt32(SecondNum);
        int t = Convert.ToInt32(Method);
        Stopwatch stopwatch = new Stopwatch();

        switch (t)
        {
            case 1: // 
                stopwatch.Start();
                Data = EuclideanAlgorithm(a, b).ToString();
                stopwatch.Stop();


                break;

            case 2: // 
                stopwatch.Start();
                ExtendedEuclideanAlgorithm(a, b, out u1, out u2, out u3);
                stopwatch.Stop();

                Data = ($"a * u1 + b * u2 = НОД(a, b): {a} * {u1} + {b} * {u2} = {u3}");


                break;

            case 3: // 
                stopwatch.Start();

                if (EuclideanAlgorithm(a, b) == 1)
                    Data = ExtendedEuclideanAlgorithm_3(a, b).ToString();

                else
                    Data = "НОД(a,n) (наибольший общий делитель) должен быть равен 1";
                stopwatch.Stop();
                break;


            default:

                break;
        }

        System.Threading.Thread.Sleep(1);
        Data += Environment.NewLine;
        TimeSpan ts = stopwatch.Elapsed;
        string elapsedTime = String.Format("{0:f8}",
        ts.TotalMilliseconds);
        Data += elapsedTime;

        return Data;
    }

    public string Time_Click()
    {
        string Data;

        Stopwatch stopwatch = new Stopwatch();
        int count = 0;
        int a = 0; int b = 0;
        Data = String.Empty;
        while (count != 5)
        {
            Random r = new Random();

            a = r.Next((int)Math.Pow(10, count), (int)Math.Pow(10, count + 1) - 1);
            b = r.Next((int)Math.Pow(10, count), (int)Math.Pow(10, count + 1) - 1);

            if (EuclideanAlgorithm(a, b) == 1)
            {
                count++;
                stopwatch.Start();
                Data += ExtendedEuclideanAlgorithm_3(a, b).ToString() + " otvet";
                stopwatch.Stop();
                Data += Environment.NewLine;
                Data += "a = " + a + "  ---  " + " b = " + b;
                System.Threading.Thread.Sleep(1);
                Data += Environment.NewLine;
                TimeSpan ts = stopwatch.Elapsed;
                string elapsedTime = String.Format("{0:f8}",
                ts.TotalMilliseconds);
                Data += elapsedTime;
                Data += Environment.NewLine;

            }


        }

        return Data;
    }

    static int EuclideanAlgorithm(int m, int n)
    {
        int temp;
        while (n != 0)
        {
            temp = n;
            n = m % n;
            m = temp;
        }
        return m;
    }
    static void ExtendedEuclideanAlgorithm(int a, int b, out int u1, out int u2, out int u3)
    {
        int v1 = 1, v2 = 0, v3 = a;
        u1 = 0; u2 = 1; u3 = b;
        while (v3 != 0)
        {
            int q = u3 / v3;
            int t1 = u1 - v1 * q;
            int t2 = u2 - v2 * q;
            int t3 = u3 - v3 * q;
            u1 = v1; u2 = v2; u3 = v3;
            v1 = t1; v2 = t2; v3 = t3;
        }
    }
    static int ExtendedEuclideanAlgorithm_3(int a, int n)
    {
        int u1 = 0, u2 = 1, u3 = n;
        int v1 = 1, v2 = 0, v3 = a;
        while (u3 != 1)
        {
            int q = u3 / v3;
            int t1 = u1 - v1 * q;
            int t2 = u2 - v2 * q;
            int t3 = u3 - v3 * q;
            u1 = v1; u2 = v2; u3 = v3;
            v1 = t1; v2 = t2; v3 = t3;
        }
        return (u1 % n + n) % n;
    }

}

