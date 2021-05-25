using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EncodingApp.logic
{
    public class GammaEncoder
    {
        private static Dictionary<char, string> gammaMatrix = Utils.GammaMatrix;
        private int[] gammaSequence;

        public GammaEncoder(string key, bool russian)
        {
            DefineGammaSequence(key, russian);
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
            string alphabet = 
            for (int i = 0; i < key.Length; i++)
            {
                gammaSequence[i] = alphabet.IndexOf(key[i]);
            }
        }
    }
}