using System;
using System.Collections.Generic;
using System.Text;

namespace EncodingApp.logic
{
    public class ShuffleEncoder : IEncoder
    {
        private string key;
        private string sortedKey;
        private char[,] encodingMatrix;

        public ShuffleEncoder(string key)
        {
            this.key = key;
        }
        
        public string Encode(string plainText)
        {
            StringBuilder builder = new StringBuilder();
            string keyCopy = key;
            DefineMatrixAndSortKey(plainText);
            foreach (char sortedKeyChar in sortedKey)
            {
                int inKeyIndex = keyCopy.IndexOf(sortedKeyChar);
                keyCopy = DeleteLetter(keyCopy, inKeyIndex);
                builder.Append(GetColumn(inKeyIndex));
                builder.Append(" ");
            }
            return builder.ToString();
        }

        public string Decode(string encodedText)
        {
            string keyCopy = key;
            string[] words = encodedText.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            encodingMatrix = new char[encodingMatrix.GetLength(0), encodingMatrix.GetLength(1)];
            int wordIndex = 0;
            foreach (char letter in sortedKey)
            {
                int originalKeyIndex = keyCopy.IndexOf(letter);
                char[] newKeyCopy = keyCopy.ToCharArray();
                newKeyCopy[originalKeyIndex] = ' ';
                keyCopy = new string(newKeyCopy);
                WriteColumn(words[wordIndex], originalKeyIndex);
                wordIndex++;
            }

            string decodedText = WriteDecodedText();
            return decodedText.Replace('_', ' ');
        }

        private string WriteDecodedText()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < encodingMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < encodingMatrix.GetLength(1); j++)
                {
                    builder.Append(encodingMatrix[i, j]);
                }
            }

            return builder.ToString();
        }
        
        private string DeleteLetter(string originalString, int indexToDelete)
        {
            char[] array = originalString.ToCharArray();
            array[indexToDelete] = ' ';
            return new string(array);
        }
        
        private void WriteColumn(string text, int columnNumber)
        {
            int rowNumber = 0;
            foreach (char letter in text)
            {
                encodingMatrix[rowNumber, columnNumber] = letter;
                rowNumber++;
            }
            
        }
        
        private string GetColumn(int columnNumber)
        {
            StringBuilder builder = new StringBuilder();
            int test = encodingMatrix.GetLength(0);
            for (int i = 0; i < encodingMatrix.GetLength(0); i++)
            {
                builder.Append(encodingMatrix[i, columnNumber]);
            }

            return builder.ToString();
        }
        
        private void DefineMatrixAndSortKey(string plainText)
        {
            string onlyLowerNoSpacesString = plainText.Replace(" ", "_").ToLower();
            int rowsNumber = onlyLowerNoSpacesString.Length / key.Length;
            if (onlyLowerNoSpacesString.Length % key.Length != 0)
            {
                rowsNumber++;
            }

            encodingMatrix = new char[rowsNumber,key.Length];
            
            int charIndex = 0;
            for (int i = 0; i < rowsNumber; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    if (charIndex == onlyLowerNoSpacesString.Length)
                    {
                        encodingMatrix[i, j] = ' ';
                    }
                    else
                    {
                        encodingMatrix[i, j] = onlyLowerNoSpacesString[charIndex];
                        charIndex++;
                    }
                }
            }

            char[] charKey = key.ToCharArray();
            Array.Sort(charKey);
            sortedKey = new string(charKey);
        }
    }
}