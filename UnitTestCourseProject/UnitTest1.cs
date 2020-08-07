using CourseProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
