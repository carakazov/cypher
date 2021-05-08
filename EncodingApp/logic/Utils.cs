using System.Collections.Generic;
using System.Security.Cryptography;

namespace EncodingApp.logic
{
    public class Utils
    {
        public static string CyrillicAlphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        public static string LatinAlphabet = "abcdefghijklmnokqrstuvwxyz";
        public static string CyrillicAlphabetWithSpecialChar = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя_ ";

        public static int[,] Matrix = new int[,]
        {
            {1, 2, 5, 4},
            {3, 13, 9, 7},
            {2, 3, 5, 11},
            {5, 6, -5, 29}
        };

        public static int[,] InverseMatrix = new int[,]
        {
            {2096, -115, -1559, 330},
            {-328, 45, 204, -43},
            {3, -3, 43, -16},
            {-293, 10, 234, -41}
        };

        public static int Divider = 283;
        
        public static Dictionary<char, string> GammaMatrix = new Dictionary<char, string>()
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
    }
}