using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EncodingApp.logic
{
    public class GammaEncoder
    {
        private static Dictionary<char, string> gammaMatrix = new Dictionary<char, string>()
        {
            {'а',"000001"},
            {'б',"001001"},
            {'в',"001010"},
            {'г',"001011"},
            {'д',"001100"},
            {'е',"000010"},
            {'ж',"001101"},
            {'з',"001110"},
            {'и',"000011"},
            {'к',"001111"},
            {'л',"010000"},
            {'м',"010001"},
            {'н',"010010"},
            {'о',"000100"},
            {'п',"010011"},
            {'р',"010100"},
            {'с',"010101"},
            {'т',"010110"},
            {'у',"000101"},
            {'ф',"010111"},
            {'х',"011000"},
            {'ц',"011001"},
            {'ч',"011010"},
            {'ш',"011011"},
            {'щ',"011100"},
            {'ы',"011101"},
            {'ь',"011110"},
            {'э',"000110"},
            {'ю',"000111"},
            {'я',"001000"},
            {'#',"000000"}
        };
        private int[] gammaSequence;

        public GammaEncoder(string key)
        {
            DefineGammaSequence(key);
        }
        
        public string Encode(string plainText)
        {
            return EncodeOrDecode(plainText, true);
        }

        public string Decode(string encodedText)
        {
            return EncodeOrDecode(encodedText, false);
        }

        private string EncodeOrDecode(string text, bool encode)
        {
            StringBuilder builder = new StringBuilder();
            int gammaSequenceIndex = 0;
            List<string> encodedSequence = new List<string>();
            foreach (char letter in text)
            {
                if (letter == ' ')
                {
                    builder.Append(" ");
                }
                else
                {
                    string letterBinary = gammaMatrix[letter];
                    string gammaBinary = Convert.ToString(gammaSequence[gammaSequenceIndex], 2)
                        .PadLeft(6, '0');
                    string result;
                    if (encode)
                    {
                        result = Xor(letterBinary, gammaBinary);
                    }
                    else
                    {
                        result = Xor(letterBinary, gammaBinary);
                    }
                    char encodedLetter = gammaMatrix.First(entry => entry.Value == result).Key;
                    if (gammaSequenceIndex == gammaSequence.Length - 1)
                    {
                        gammaSequenceIndex = 0;
                    }
                    else
                    {
                        gammaSequenceIndex++;
                    }

                    builder.Append(encodedLetter);
                }
            }
            
            return builder.ToString();
        }

        private string Xor(string firstOperand, string secondOperand)
        {
            string result = "";
            for (int i = 0; i < firstOperand.Length; i++)
            {
                if (firstOperand[i] != secondOperand[i])
                {
                    result += '1';
                }
                else
                {
                    result += '0';
                }
            }

            return result;
        }
        
        private void DefineGammaSequence(string key)
        {
            gammaSequence = new int[key.Length];
            string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            for (int i = 0; i < key.Length; i++)
            {
                gammaSequence[i] = alphabet.IndexOf(key[i]);
            }
        }
    }
}