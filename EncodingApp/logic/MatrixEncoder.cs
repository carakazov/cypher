using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EncodingApp.logic
{
    public class MatrixEncoder
    {
        private int[,] matrix = new int[,]
        {
            {1, 2, 5, 4},
            {3, 13, 9, 7},
            {2, 3, 5, 11},
            {5, 6, -5, 29}
        };
        private int[,] inverseMatrix = new int[,]
        {
            {2096, -115, -1559, 330},
            {-328, 45, 204, -43},
            {3, -3, 43, -16},
            {-293, 10, 234, -41}
        };
        private string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя_ ";
        private char specialChar = '_';
        private int divider = 283;
        private List<int[]> vectors;
        public string Encode(string plainText)
        {
            StringBuilder builder = new StringBuilder();
            string plainTextFull = AddSpecialSymbols(plainText);
            DefineVectorList(DefineVector(plainTextFull));
            foreach (int[] vector in vectors)
            {
                builder.Append(CastVectorToString(MatrixVectorMultiply(vector, matrix)));
            }

            return builder.ToString();
        }

        public string Decode(string encodedText)
        {
            DefineVectorList(VectorFromNumericString(encodedText));
            StringBuilder builder = new StringBuilder();
            foreach (int[] vector in vectors)
            {
                builder.Append(DefineDecodedString(vector));
            }

            string decodedString = builder.ToString();
            return decodedString.Replace("_", "");
        }

        private string DefineDecodedString(int[] vector)
        {
            int[] decodedVector = MatrixVectorMultiply(vector, inverseMatrix);
            for (int i = 0; i < decodedVector.Length; i++)
            {
                decodedVector[i] /= divider;
            }

            return DefineStringFromEncodedVector(decodedVector);
        }
        
        private string DefineStringFromEncodedVector(int[] vector)
        {
            StringBuilder builder = new StringBuilder();
            foreach (int number in vector)
            {
                builder.Append(alphabet[number]);
            }

            return builder.ToString();
        }
        
        private int[] VectorFromNumericString(string numericString)
        {
            string[] words = numericString.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            int[] vector = new int[words.Length];
            for (int i = 0; i < vector.Length; i++)
            {
                vector[i] = Convert.ToInt32(words[i]);
            }

            return vector;
        }
        
        private string CastVectorToString(int[] vector)
        {
            StringBuilder builder = new StringBuilder();
            foreach (int item in vector)
            {
                builder.Append(item).Append(" ");
            }

            return builder.ToString();
        }
        
        private void DefineVectorList(int[] fullVector)
        {
            vectors = new List<int[]>();
            int vectorLength = matrix.GetLength(1);
            int innerVectorIndex = 0;
            int[] innerVector = new int[vectorLength];
            for (int i = 0; i < fullVector.Length; i++)
            {
                innerVector[innerVectorIndex] = fullVector[i];
                if (innerVectorIndex == vectorLength - 1)
                {
                    innerVectorIndex = 0;
                    vectors.Add(innerVector);
                    innerVector = new int[vectorLength];
                }
                else
                {
                    innerVectorIndex++;
                }
            }
        }
        
        private string AddSpecialSymbols(string text)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(text);
            for (int i = 0; i < text.Length % matrix.GetLength(1); i++)
            {
                builder.Append(specialChar);
            }

            return builder.ToString();
        }
        
        private int[] DefineVector(string text)
        {
            int[] vector = new int[text.Length];
            for (int i = 0; i < vector.Length; i++)
            {
                vector[i] = alphabet.IndexOf(text[i]);
            }

            return vector;
        }

        private int[] MatrixVectorMultiply(int[] vector, int[,] matrix)
        {
            int[] result = new int[matrix.GetLength(1)];
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                int sum = 0;
                for (int column = 0; column < matrix.GetLength(0); column++)
                {
                    sum += vector[column] * matrix[row, column];
                }

                result[row] = sum;
            }

            return result;
        } 
    }
}