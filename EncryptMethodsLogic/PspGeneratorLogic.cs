namespace EncryptMethodsLogic;

public class PspGeneratorLogic
{
    public string Encrypt_func_PSP(string Input, string Step)
    {

        string Data = String.Empty;

        string s = String.Empty;
        string[] arr = { "31", "19", "2", "1", "0" };
        string bits = Input;

        if (Input == "0")
        {
            bits = Generate_zero_one(32);
        }

        Data += bits + " Start BITS";

        Data += Environment.NewLine;


        for (int j = 0; j < Convert.ToInt32(Step); j++)
        {
            s = xor_bit(bits[Convert.ToInt32(arr[0])].ToString(), bits[Convert.ToInt32(arr[1])].ToString());
            Data += "b" + Convert.ToInt32(arr[0]) + " = " + bits[Convert.ToInt32(arr[0])].ToString();
            Data += Environment.NewLine;
            Data += "b" + Convert.ToInt32(arr[1]) + " = " + bits[Convert.ToInt32(arr[1])].ToString();
            Data += Environment.NewLine;

            for (int i = 2; i < arr.Length; i++)
            {
                s = xor_bit(s, bits[Convert.ToInt32(arr[i])].ToString());
                Data += "b" + Convert.ToInt32(arr[i]) + " = " + bits[Convert.ToInt32(arr[i])].ToString();
                Data += Environment.NewLine;
            }

            Data += s + " THIS SUM BITS";
            Data += Environment.NewLine;

            bits = cycle_shift_l(bits, 1);
            bits = bits.Remove(bits.Length - 1);
            bits += s;
            Data += bits + " THIS NEW BITS" + " ITERATION " + j;
            Data += Environment.NewLine;

        }

        return Data;
    }

    private string Generate_zero_one(int count)
    {
        Random r = new Random();
        string s = String.Empty;
        int k = 0;
        for (int i = 0; i < count; i++)
        {
            k = r.Next(1, 10000);
            if (k > 5000)
                s += "1";
            else
                s += "0";
        }

        return s;
    }

    private string xor_bit(string s1, string s2)
    {
        char[] r = s1.ToCharArray();
        for (int i = 0; i < s1.Length; i++)
        {
            if (s1[i] == s2[i])
                r[i] = '0';
            else
                r[i] = '1';

        }
        return new string(r);
    }

    private string cycle_shift_l(string s, int c)
    {
        int number = Convert_Str_num(s);
        int numBits = s.Length;

        number &= (1 << numBits) - 1;
        number = (number << c) | (number >> (numBits - c));

        // Преобразование результата обратно в двоичную строку
        return (Convert_num_str(number & ((1 << numBits) - 1))).PadLeft(numBits, '0');
    }

    private int Convert_Str_num(string s)
    {
        double num = 0;
        int t;
        for (int i = s.Length - 1; i >= 0; i--)
        {
            if (s[i] == '0')
                t = 0;
            else
                t = 1;
            num += Math.Pow(2, s.Length - 1 - i) * t;
        }
        return (int)num;
    }

    private string Convert_num_str(int n)
    {
        if (n == 0) return "0";
        if (n == 1) return "1";
        else return Convert_num_str(n / 2) + (n % 2);
    }
}

