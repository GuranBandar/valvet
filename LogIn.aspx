<%@ Page Title="LogIn" Language="C#" MasterPageFile="~/App.Master"
    AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="Valvetwebb.LogIn" %>

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
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtAnvandarNamn" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblLosen" runat="server" Text="Huvudlösenord"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtLosenord" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="knappOK" runat="server" Text="OK" OnClick="knappOK_Click" />
                            <asp:Label ID="Label3" Width="1%" runat="server"></asp:Label>
                            <asp:Button ID="knappAvbryt" runat="server" Text="Avbryt" OnClick="knappAvbryt_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" Width="1%" runat="server"></asp:Label>
                            <asp:Label ID="Label1" Width="1%" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
