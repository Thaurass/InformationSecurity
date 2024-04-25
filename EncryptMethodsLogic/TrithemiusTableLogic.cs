using FileProcessing;

namespace EncryptMethodsLogic
{
    public class TrithemiusTableLogic
    {

        LoadFile _loadFile = new();
        SaveFile _saveFile = new();
        Dictonaries _dictonaries = new Dictonaries();

        public string[] FileData { get; set; }
        public bool IsSaveSuccessful { get; set; }

        public string Encrypt_func_Trithemius(string Text)
        {
            char[] t = Text.ToCharArray();
            char[] result = new char[t.Length];

            for (int i = 0; i < t.Length; i++)
            {
                if (t[i] == ' ')
                {
                    result[i] = ' ';
                }
                else
                {
                    int letter_num = (Get_Char_Number(t[i], _dictonaries.AllLeters) + i) % (_dictonaries.AllLeters.Length - 1);
                    if (letter_num >= 0)
                    {
                        result[i] = _dictonaries.AllLeters[letter_num];
                    }
                    else
                    {
                        result[i] = _dictonaries.AllLeters[_dictonaries.AllLeters.Length + letter_num - 1];
                    }
                }
                
                
            }

            return new string(result);
        }

        public string Unencrypt_func_Trithemius(string Text)
        {
            char[] t = Text.ToCharArray();
            char[] result = new char[t.Length];


            for (int i = 0; i < t.Length; i++)
            {
                if (t[i] == ' ')
                {
                    result[i] = ' ';
                }
                else
                {
                    int letter_num = (Get_Char_Number(t[i], _dictonaries.AllLeters) - i) % (_dictonaries.AllLeters.Length - 1);
                    if (letter_num >= 0)
                    {
                        result[i] = _dictonaries.AllLeters[letter_num];
                    }
                    else
                    {
                        result[i] = _dictonaries.AllLeters[_dictonaries.AllLeters.Length + letter_num - 1];
                    }
                }
            }

            return new string(result);
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

        public double Message_entropy(string Text)
        {

            Symbol_probabilities(Text);
            double inputEntropy = 0;
            for (int i = 0; i < _dictonaries.AllLeters.Length; i++)
            {
                Char t = _dictonaries.AllLeters[i];
                if (Text.IndexOf(t) != -1)
                    inputEntropy -= (_dictonaries.letterProbability[t] * Math.Log2(_dictonaries.letterProbability[t]));
            }

            return inputEntropy;
        }
        
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


        private int Get_Char_Number(char c, string str)
        {
            return str.IndexOf(c);
        }
    }
}

