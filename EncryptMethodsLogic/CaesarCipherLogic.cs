using FileProcessing;

namespace EncryptMethodsLogic
{
    public class CaesarCipherLogic
    {
        LoadFile _loadFile = new();
        SaveFile _saveFile = new();
        Dictonaries _dictonaries = new();

        public string[] FileData { get; set; }
        public bool IsSaveSuccessful { get; set; }

        private void Symbol_probabilities(string inputText)
        {
            string s = inputText;
            for (int i = 0; i < s.Length; i++)
                if (_dictonaries.AllLeters.IndexOf(s[i]) == -1)
                {
                    s = s.Remove(i, 1);
                    i--;
                }
            _dictonaries.letterProbability = new();
            int totalCharacters = s.Length;

            foreach (char character in s)
            {
                if (_dictonaries.letterProbability.ContainsKey(character)) 
                { 
                    _dictonaries.letterProbability[character] += 1.0 / totalCharacters; 
                }
                else { _dictonaries.letterProbability[character] = 1.0 / totalCharacters; }
            }

            _dictonaries.letterProbability = 
                _dictonaries.letterProbability.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        public string DecryptMessage(string unencryptText)
        {
            string result = string.Empty;
            for (int i = 0; i <= 32; i++)
            {
                result += Unencrypt_func(unencryptText, i);
                result += Environment.NewLine;
            }
            return result;
        }

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

        public string Encrypt_func(string Text, string Step)
        {
            char[] c = Text.ToCharArray();
            int s = Convert.ToInt32(Step);

            c = shift(c, s);

            return new string(c);
        }

        public string Unencrypt_func(string Text, int s)
        {
            char[] c = Text.ToCharArray();
            c = shift(c, -1 * s);
            string str = new string(c);
            Symbol_probabilities(str);
            double d = Math.Round(xi_sqeare(str), 2);

            if (str.Length > 10)
                str = str.Substring(0, 10);


            return str + "\t" + s + "\t" + d;
        }

        private double xi_sqeare(string str)
        {
            double x = 0;
            for (int i = 0; i < _dictonaries.AllLeters.Length; i++)
            {
                Char t = _dictonaries.AllLeters[i];
                if (str.IndexOf(t) != -1)
                    x += (_dictonaries.letterProbability[t] - _dictonaries.Probability[t]) * 
                        (_dictonaries.letterProbability[t] - _dictonaries.Probability[t]) / _dictonaries.Probability[t];
            }

            return x;
        }

        private char[] shift(char[] c, int s)
        {
            for (int i = 0; i < c.Length; i++)
                if (!Check_Char(c[i], _dictonaries.AllLeters))
                    c[i] = Convert.ToChar(_dictonaries.AllLeters[0] + normalize_step(s, c[i]));

            return c;
        }

        private int normalize_step(int s, char c)
        {
            s = ((s + _dictonaries.AllLeters.Length * 100) % _dictonaries.AllLeters.Length + 
                c - _dictonaries.AllLeters[0]) % _dictonaries.AllLeters.Length;
            return s;
        }

        private bool Check_Char(char c, string Leters)
        {
            bool err = true;
            string s = Leters;

            if (s.IndexOf(c) != -1) { err = false; }

            return err;

        }
    }
}
