<%@ Page Title="LogIn" Language="C#" MasterPageFile="~/App.Master"
    AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="Valvet.LogIn" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
    <script type="text/javascript">
        function CloseWindow() {
            window.close();
        }
    </script>
    <div>
        <div>
            <div class="table-responsive">
                <table id="TableHeader" class="form" runat="server">
                    <tr>
                        <td></td>
                        <td>
                            <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table id="TableLogIn" style="margin: auto; border: 5px solid white">
                    <tr></tr>
                    <tr>
                        <td>
                            <h3>Logga in till Valvet</h3>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblAnvandarNamn" runat="server" Text="Anändarnamn:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAnvandarNamn" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblLosen" runat="server" Text="Lösenord:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLosenord" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="knappOK" runat="server" Text="OK" OnClick="knappOK_Click" />
                            <asp:Button ID="knappAvbryt" runat="server" Text="Avbryt" OnClick="knappAvbryt_Click" />
                        </td>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
