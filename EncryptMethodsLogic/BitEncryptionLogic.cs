using FileProcessing;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EncryptMethodsLogic;

public class BitEncryptionLogic
{
    LoadFile _loadFile = new();
    SaveFile _saveFile = new();

    public string[] FileData { get; set; }
    public string[] TempData { get; set; }
    public bool IsSaveSuccessful { get; set; }

    public async Task<FileResult> GetCipherData()
    {
        var result = await _loadFile.GetFile();
        if (_loadFile.FirstLine != "first_line")
        {
            FileData = _loadFile.FirstLine.Split('|');
        }
        else
        {
            FileData = new string[] { "first_line" };
        }
        return result;
    }

    public async Task<bool> SaveCipherData(string FileData)
    {
        var result = await _saveFile.Savedata(FileData);
        IsSaveSuccessful = _saveFile.SaveResult;
        return result;
    }

    public string Encrypt_func(string Text)
    {
        TempData = new string[Text.Length + 1];
        string cryptedMessage = string.Empty;

        char[] t = Text.ToCharArray();
        TempData[0] = "Символ\tИсходный код\t Зашифрованный код    Результат";

        for (int i = 0; i < t.Length; i++)
        {
            string byteSymbol = Convert_num_str((int)t[i]).PadLeft(16, '0');

            int leftByte = Convert_Str_num(byteSymbol.Remove(8));
            int rightByte = Convert_Str_num(byteSymbol.Remove(0, 8));

            int newLeftByte = rightByte;
            int newRightByte = leftByte ^ rightByte;

            //зашифрованный символ
            int cryptedChar = (newLeftByte << 8) + newRightByte;

            cryptedMessage += Convert.ToChar(cryptedChar).ToString();

            TempData[i + 1] = t[i] + "\t            " + Convert_num_str((int)t[i]).PadLeft(16, '0') + 
                "\t" + Convert_num_str(cryptedChar).PadLeft(16, '0') + "\t      " + (char)cryptedChar;
        }

        return cryptedMessage;
    }

    public string Unencrypt_func(string Text)
    {
        TempData = new string[Text.Length + 1];
        string decryptedMessage = string.Empty;

        char[] t = Text.ToCharArray();
        TempData[0] = "Символ\tИсходный код\tРасшифрованный код    Результат";

        for (int i = 0; i < t.Length; i++)
        {
            string byteSymbol = Convert_num_str((int)t[i]).PadLeft(16, '0');

            int leftByte = Convert_Str_num(byteSymbol.Remove(8));
            int rightByte = Convert_Str_num(byteSymbol.Remove(0, 8));

            int newLeftByte = leftByte ^ rightByte;
            int newRightByte = leftByte;

            int decryptedChar = (newLeftByte << 8) + newRightByte;

            decryptedMessage += Convert.ToChar(decryptedChar).ToString();

            TempData[i + 1] = t[i] + "\t            " + Convert_num_str((int)t[i]).PadLeft(16, '0') + 
                "\t" + Convert_num_str(decryptedChar).PadLeft(16, '0') + "\t      " + (char)decryptedChar;
        }

        return decryptedMessage;
    }

    private string Convert_num_str(int n)
    {
        if (n == 0) return "0";
        if (n == 1) return "1";
        else return Convert_num_str(n / 2) + (n % 2);
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
}
