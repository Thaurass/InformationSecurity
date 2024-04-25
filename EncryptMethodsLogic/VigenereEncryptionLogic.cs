using FileProcessing;

namespace EncryptMethodsLogic
{
    public class VigenereEncryptionLogic
    {
        LoadFile _loadFile = new();
        SaveFile _saveFile = new();
        Dictonaries _dictonaries = new Dictonaries();

        public string[] FileData { get; set; }
        public bool IsSaveSuccessful { get; set; }

        public string Encrypt_func_Vigener(string Text, string Key)
        {
            char[] t = Text.ToCharArray();
            char[] k = normalize_key(Text, Key).ToCharArray();
            char[] result = new char[t.Length];


            for (int i = 0; i < t.Length; i++)
            {
                int first = Get_Char_Number(t[i], _dictonaries.AllLeters);
                int second = Get_Char_Number(k[i], _dictonaries.AllLeters);
                result[i] = _dictonaries.AllLeters[(first + second) % _dictonaries.AllLeters.Length];
            }

            return new string(result);
        }

        public string Unencrypt_func_Vigener(string Text, string Key)
        {
            char[] t = Text.ToCharArray();
            char[] k = normalize_key(Text, Key).ToCharArray();
            char[] result = new char[t.Length];


            for (int i = 0; i < t.Length; i++)
            {
                int first = Get_Char_Number(t[i], _dictonaries.AllLeters);
                int second = Get_Char_Number(k[i], _dictonaries.AllLeters);
                result[i] = _dictonaries.AllLeters[(Math.Abs(first - second)) % _dictonaries.AllLeters.Length];
            }

            return new string(result);
        }

        public async Task<FileResult> GetCipherData()
        {
            var result = await _loadFile.GetFile();
            FileData = _loadFile.FirstLine.Split('|');
            return result;
        }

        public async Task<bool> SaveCipherData(string FileData)
        {
            var result = await _saveFile.Savedata(FileData);
            IsSaveSuccessful = _saveFile.SaveResult;
            return result;
        }

        private string normalize_key(string Text, string Key)
        {
            string tmp = string.Empty;
            while (tmp.Length < Text.Length)
            {
                tmp += Key;
            }
            return new string(tmp);
        }

        private int Get_Char_Number(char c, string str)
        {
            return str.IndexOf(c);
        }
    }
}
