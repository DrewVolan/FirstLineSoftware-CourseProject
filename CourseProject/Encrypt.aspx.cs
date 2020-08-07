using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace CourseProject
{
    public partial class Encrypt : Page
    {

        private string EncryptText
        {
            get
            {
                return (string)Session["EncryptText"];
            }
            set
            {
                Session["EncryptText"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (FileUploader.HasFile && !string.IsNullOrWhiteSpace(KeyWordTextBox.Text))
            {
                try
                {
                    if (FileUploader.FileName.Split('.')[FileUploader.FileName.Split('.').Length - 1] == "txt")
                    {
                        FileUploader.PostedFile.SaveAs(Server.MapPath("~/Data/") + FileUploader.FileName);
                        string s = File.ReadAllText(Server.MapPath("~/Data/") + FileUploader.FileName);
                        if (s.Contains((char)65533))
                        {
                            s = ChangeEncoding();
                        }
                        BeforeTextBox.Text = s;
                        Encryptor encryptor = new Encryptor(s, KeyWordTextBox.Text);
                        if (VigenereModeRadioButtonList.SelectedValue == "TrueVigenere")
                        {
                            EncryptText = encryptor.EncryptText(true);
                        }
                        else
                        {
                            EncryptText = encryptor.EncryptText(false);
                        }
                        AfterTextBox.Text = EncryptText;
                    }
                    else if (FileUploader.FileName.Split('.')[FileUploader.FileName.Split('.').Length - 1] == "docx")
                    {
                        FileUploader.PostedFile.SaveAs(Server.MapPath("~/Data/") + FileUploader.FileName);
                        var s = "";
                        using (var wordDocument = WordprocessingDocument.Open(Server.MapPath("~/Data/") + FileUploader.FileName as string, false))
                        {
                            s = wordDocument.MainDocumentPart.Document.Body.InnerText;
                        }
                        BeforeTextBox.Text = s;
                        Encryptor encryptor = new Encryptor(s, KeyWordTextBox.Text);
                        if (VigenereModeRadioButtonList.SelectedValue == "TrueVigenere")
                        {
                            EncryptText = encryptor.EncryptText(true);
                        }
                        else
                        {
                            EncryptText = encryptor.EncryptText(false);
                        }
                        AfterTextBox.Text = EncryptText;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    Response.Write("Кодовое слово должно быть введено на кириллице");
                }
                finally
                {
                    File.Delete(Server.MapPath("~/Data/") + FileUploader.FileName);
                }
            }
            else
            {
                Response.Write("Файл не выбран, либо не введено кодовое слово");
            }
        }

        private string ChangeEncoding()
        {
            Encoding encoding = Encoding.Default;
            string s = File.ReadAllText(Server.MapPath("~/Data/") + FileUploader.FileName, encoding);
            byte[] beforeBytes = encoding.GetBytes(s);
            byte[] afterBytes = Encoding.Convert(encoding, Encoding.UTF8, beforeBytes);
            return Encoding.UTF8.GetString(afterBytes);
        }

        protected void DownloadDocxButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(FileNameTextBox.Text) && !FileNameTextBox.Text.Contains('/') &&
                !FileNameTextBox.Text.Contains(@"\") && !FileNameTextBox.Text.Contains('?') && !FileNameTextBox.Text.Contains('|')
                && !FileNameTextBox.Text.Contains('*') && !FileNameTextBox.Text.Contains(':') && !FileNameTextBox.Text.Contains('"')
                && !FileNameTextBox.Text.Contains('<') && !FileNameTextBox.Text.Contains('>'))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Create(memoryStream, WordprocessingDocumentType.Document, true))
                    {
                        MainDocumentPart mainDocumentPart = wordprocessingDocument.AddMainDocumentPart();
                        mainDocumentPart.Document = new Document();
                        Body body = mainDocumentPart.Document.AppendChild(new Body());
                        Paragraph paragraph = body.AppendChild(new Paragraph());
                        Run run = paragraph.AppendChild(new Run());
                        run.AppendChild(new Text(AfterTextBox.Text));
                    }
                    Context.Response.AppendHeader("Content-Disposition", "attachment;filename=" + FileNameTextBox.Text + ".docx");
                    memoryStream.Position = 0;
                    memoryStream.CopyTo(Context.Response.OutputStream);
                    Context.Response.Flush();
                    Context.Response.End();
                    File.Delete(Server.MapPath("~/Data/") + FileNameTextBox.Text + ".docx");
                }
            }
            else
            {
                Response.Write("Введите название файла!");
            }
        }

        protected void DownloadTxtButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(FileNameTextBox.Text) && !FileNameTextBox.Text.Contains('/') &&
                !FileNameTextBox.Text.Contains(@"\") && !FileNameTextBox.Text.Contains('?') && !FileNameTextBox.Text.Contains('|')
                && !FileNameTextBox.Text.Contains('*') && !FileNameTextBox.Text.Contains(':') && !FileNameTextBox.Text.Contains('"')
                && !FileNameTextBox.Text.Contains('<') && !FileNameTextBox.Text.Contains('>'))
            {
                StreamWriter streamWriter;
                streamWriter = File.CreateText(Server.MapPath("~/Data/") + FileNameTextBox.Text + ".txt");
                streamWriter.Write(AfterTextBox.Text);
                streamWriter.Flush();
                streamWriter.Close();
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "filename=" + FileNameTextBox.Text + ".txt");
                Response.TransmitFile(Server.MapPath("~/Data/") + FileNameTextBox.Text + ".txt");
                Response.End();
                File.Delete(Server.MapPath("~/Data/") + FileNameTextBox.Text + ".txt");
            }
            else
            {
                Response.Write("Название файла не введено или введено с недопустимыми символами!");
            }
        }

        protected void ManualEncryptButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(KeyWordTextBox.Text))
            {
                try
                {
                    string s = BeforeTextBox.Text;
                    Encryptor encryptor = new Encryptor(s, KeyWordTextBox.Text);
                    if (VigenereModeRadioButtonList.SelectedValue == "TrueVigenere")
                    {
                        EncryptText = encryptor.EncryptText(true);
                    }
                    else
                    {
                        EncryptText = encryptor.EncryptText(false);
                    }
                    AfterTextBox.Text = EncryptText;
                }
                catch (IndexOutOfRangeException)
                {
                    Response.Write("Кодовое слово должно быть введено на кириллице");
                }
            }
            else
            {
                Response.Write("Не введено кодовое слово");
            }
        }
    }
}