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
        };
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