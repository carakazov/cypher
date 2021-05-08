using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EncodingApp.logic
{
    public class GammaEncoder : IEncoder
    {
        private static Dictionary<char, string> gammaMatrix = Utils.GammaMatrix;
        private int[] gammaSequence = new[] {2, 3, 10, 4, 1, 5, 6, 7, 8, 11, 15, 14, 12, 13, 9, 0};
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
                        result = Convert.ToString(Convert.ToInt32(letterBinary, 2)
                                                  + Convert.ToInt32(gammaBinary, 2), 2)
                            .PadLeft(6, '0');
                    }
                    else
                    {
                        result = Convert.ToString(Convert.ToInt32(letterBinary, 2)
                                                  - Convert.ToInt32(gammaBinary, 2), 2)
                            .PadLeft(6, '0');
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
    }
}