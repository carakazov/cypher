using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncodingApp.logic
{
    class VigenereEncoder 
    {

        private string key;
        private string alphabet;

        private List<string> encodingAlphabets = new List<string>();

        public VigenereEncoder(string key, string alphabet)
        {
            this.key = key;
            this.alphabet = alphabet;
            DefineEncodingAlphabets();
        }

        public string Encode(string plainText)
        {
            StringBuilder builder = new StringBuilder();
            string onlyLower = plainText.ToLower();
            int index = 0;
            while (index < onlyLower.Length)
            {
                if (onlyLower[index] == ' ')
                {
                    builder.Append(onlyLower[index]);
                    onlyLower = onlyLower.Remove(index, 1);
                }
                else
                {
                    int inAlphabetIndex = alphabet.IndexOf(onlyLower[index]);
                    int encodingAlphabetIndex = index % encodingAlphabets.Count;
                    char encodedChar = encodingAlphabets[encodingAlphabetIndex][inAlphabetIndex];
                    builder.Append(encodedChar);
                    index++;
                }
            }
            
            return builder.ToString();
        }

        public string Decode(string encodedText)
        {
            StringBuilder builder = new StringBuilder();
            int index = 0;
            while (index < encodedText.Length)
            {
                if (encodedText[index] == ' ')
                {
                    builder.Append(encodedText[index]);
                    encodedText = encodedText.Remove(index, 1);
                }
                else
                {
                    int encodingAlphabetIndex = index % encodingAlphabets.Count;
                    int encodedCharIndex = encodingAlphabets[encodingAlphabetIndex].IndexOf(encodedText[index]);
                    char decodedChar = alphabet[encodedCharIndex];
                    builder.Append(decodedChar);
                    index++;
                }
            }

            return builder.ToString();
        }

        private void DefineEncodingAlphabets()
        {
            StringBuilder builder = new StringBuilder();
            foreach(char ch in key)
            {
                if(ch == alphabet[0])
                {
                    encodingAlphabets.Add(alphabet);   
                }
                else
                {
                    int firstIndex = alphabet.IndexOf(ch);
                    builder.Append(alphabet.Substring(firstIndex))
                        .Append(alphabet.Substring(0, firstIndex));
                    encodingAlphabets.Add(builder.ToString());
                    builder.Clear();
                }
            }
        }
    }
}
