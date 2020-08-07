using System;
using System.Linq;

namespace CourseProject
{
    /// <summary>
    /// Шифровщик.
    /// </summary>
    public class Encryptor
    {
        /// <summary>
        /// Исходный текст.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Ключевое слово.
        /// </summary>
        public string KeyWord { get; private set; }

        /// <summary>
        /// Результат шифровки/дешифровки.
        /// </summary>
        public string Result { get; private set; }

        /// <summary>
        /// Создаёт экземпляр класса с исходным текстом и кодовым словом.
        /// </summary>
        /// <param name="text">Исходный текст.</param>
        /// <param name="keyWord">Ключевое слово.</param>
        public Encryptor(string text, string keyWord)
        {
            Text = text;
            KeyWord = keyWord;
        }

        /// <summary>
        /// Шифрует/дешифрует исходный текст по кодовому слову.
        /// </summary>
        /// <param name="mode">true - если необходимо зашифровать, а false - расшифровать.</param>
        /// <returns>Результат работы шифратора.</returns>
        public string EncryptText(bool mode)
        {
            char[] charsUpper = new char[] { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я' };
            char[] charsLower = new char[] { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };//, '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            const byte N = 33;
            KeyWord = KeyWord.ToUpper();
            Result = "";
            int keywordIndex = 0;
            if (mode)
            {
                foreach (char symbol in Text)
                {
                    if (Char.IsUpper(symbol) && charsUpper.Contains(symbol))
                    {
                            byte c = (byte)((Array.IndexOf(charsUpper, symbol) + Array.IndexOf(charsUpper, KeyWord[keywordIndex])) % N);
                            Result += charsUpper[c];
                            if ((keywordIndex + 1) != KeyWord.Length)
                            {
                                keywordIndex++;
                            }
                            else
                            {
                                keywordIndex = 0;
                            }
                    }
                    else if (Char.IsLower(symbol) && charsLower.Contains(symbol))
                    {
                            byte c = (byte)((Array.IndexOf(charsLower, symbol) + Array.IndexOf(charsUpper, KeyWord[keywordIndex])) % N);
                            Result += charsLower[c];
                            if ((keywordIndex + 1) != KeyWord.Length)
                            {
                                keywordIndex++;
                            }
                            else
                            {
                                keywordIndex = 0;
                            }
                    }
                    else
                    {
                        Result += symbol;
                    }
                }
            }
            else
            {
                foreach (char symbol in Text)
                {
                    if (Char.IsUpper(symbol) && charsUpper.Contains(symbol))
                    {
                        byte p = (byte)((Array.IndexOf(charsUpper, symbol) + N - Array.IndexOf(charsUpper, KeyWord[keywordIndex])) % N);
                        Result += charsUpper[p];
                        if ((keywordIndex + 1) != KeyWord.Length)
                        {
                            keywordIndex++;
                        }
                        else
                        {
                            keywordIndex = 0;
                        }
                    }
                    else if (Char.IsLower(symbol) && charsLower.Contains(symbol))
                    {
                        byte p = (byte)((Array.IndexOf(charsLower, symbol) + N - Array.IndexOf(charsUpper, KeyWord[keywordIndex])) % N);
                        Result += charsLower[p];
                        if ((keywordIndex + 1) != KeyWord.Length)
                        {
                            keywordIndex++;
                        }
                        else
                        {
                            keywordIndex = 0;
                        }
                    }
                    else
                    {
                        Result += symbol;
                    }
                }
            }
            return Result;
        }
    }
}