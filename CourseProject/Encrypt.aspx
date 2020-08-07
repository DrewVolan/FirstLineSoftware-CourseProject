<%@ Page Language="C#" Title="Зашифровка" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Encrypt.aspx.cs" Inherits="CourseProject.Encrypt" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .marginer {
            margin-bottom: 16px;
            margin-top: 16px;
            margin-right: 16px;
            resize: none;
            max-width: 1600px;
        }
    </style>

    <div class="row">
        <div class="marginer">
            <asp:RadioButtonList ID="VigenereModeRadioButtonList" runat="server">
                <asp:ListItem Selected="True" Value="TrueVigenere">Зашифровать</asp:ListItem>
                <asp:ListItem Value="FalseVigenere">Расшифровать</asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div class="marginer">
            <asp:FileUpload CssClass="custom-file-input" ID="FileUploader" runat="server" />
        </div>
        <div class="marginer">
            <label>Введите ключевое слово...</label>
            <asp:TextBox ID="KeyWordTextBox" runat="server"></asp:TextBox>
            <asp:Button ID="UploadButton" runat="server" OnClick="UploadButton_Click" Text="Загрузить файл и выполнить шифрование" CssClass="btn btn-primary btn-sm" />
        </div>
        <div>
            <h2 class="Display-4">Исходный текст</h2>
            <asp:TextBox ID="BeforeTextBox" runat="server" TextMode="MultiLine" CssClass="marginer" Height="160px" Width="800px"></asp:TextBox>
        </div>
        <div class="marginer">
            <asp:Button ID="ManualEncryptButton" runat="server" Text="Выполнить шифрование без загрузки файла" CssClass="btn btn-primary btn-sm btn-block" OnClick="ManualEncryptButton_Click" />
        </div>
        <div class="marginer">
            <h2 class="Display-4">Результат</h2>
            <asp:TextBox ID="AfterTextBox" runat="server" TextMode="MultiLine" CssClass="marginer" ReadOnly="true" Height="160px" Width="800px"></asp:TextBox>
        </div>
        <div class="marginer">
            <label>Введите здесь название файла для скачивания...</label>
            <p>
                <asp:TextBox ID="FileNameTextBox" runat="server" Height="40px" Width="320px" TextMode="MultiLine" CssClass="marginer"></asp:TextBox>
            </p>
            <div class="btn-group btn-group-lg" role="group" aria-label="Basic example">
                <asp:Button ID="DownloadTxtButton" runat="server" OnClick="DownloadTxtButton_Click" Text="Скачать .Txt файл" CssClass="btn btn-primary" />
                <asp:Button ID="DownloadDocxButton" runat="server" OnClick="DownloadDocxButton_Click" Text="Скачать .Docx файл" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>
</asp:Content>
