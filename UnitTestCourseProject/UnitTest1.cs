using CourseProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestCourseProject
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Проверяет расшифровку текста.
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            Encryptor encryptor = new Encryptor("бщцфаирщри, бл ячъбиуъ щбюэсяёш гфуаа!!!", "скорпион");
            string result = encryptor.EncryptText(false);
            Assert.AreEqual("поздравляю, ты получил исходный текст!!!", result);
        }

        /// <summary>
        /// Проверяет зашифровку текста.
        /// </summary>
        [TestMethod]
        public void TestMethod2()
        {
            Encryptor encryptor = new Encryptor("поздравляю, ты получил исходный текст!!!", "скорпион");
            string result = encryptor.EncryptText(true);
            Assert.AreEqual("бщцфаирщри, бл ячъбиуъ щбюэсяёш гфуаа!!!", result);
        }

        /// <summary>
        /// Проверяет на невозможность использования кодового слова с латинскими буквами.
        /// </summary>
        [TestMethod]
        public void TestMethod3()
        {
            Encryptor encryptor = new Encryptor("поздравляю, ты получил исходный текст!!!", "scorpion");
            string res = null;
            try
            {
                encryptor.EncryptText(true);
            }
            catch (IndexOutOfRangeException ex)
            {
                res = ex.Message;
            }
            Assert.IsNotNull(res);
        }
    }
}
