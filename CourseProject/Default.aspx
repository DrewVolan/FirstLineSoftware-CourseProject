<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CourseProject._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-4">
            <h2>Шифр Виженера</h2>
            <p>
                Добро пожаловать на страницу дешифровки шифра Виженера от Волейко Андрея
            </p>
            <p><asp:Button ID="ToEncryptButton" runat="server" Text="Приступим!" CssClass="btn btn-primary" OnClick="ToEncryptButton_Click" /></p>
        </div>
    </div>

</asp:Content>
