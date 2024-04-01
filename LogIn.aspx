<%@ Page Title="LogIn" Language="C#" MasterPageFile="~/App.Master"
    AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="Valvetwebb.LogIn" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
<script type="text/javascript">
    function closeWindow() {
        window.open('', '_parent', '');
        window.close();
    }
</script>
    <a href="javascript:window.open('','_self').close();">close</a>
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
                <table id="TableLogIn" class="custom-table"; style="margin: auto; border: 5px solid darkgray" >
                    <tr></tr>
                    <tr>
                        <td>
                            <h3>Valvinloggning</h3>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label7" Height="4px" Width="3%" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblAnvandarNamn" runat="server" Text="Anändarnamn:" CssClass="rubriktext"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" Height="4px" Width="3%" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtAnvandarNamn" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" Height="4px" Width="3%" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblLosen" runat="server" Text="Huvudlösenord" CssClass="rubriktext"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" Height="4px" Width="3%" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtLosenord" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label8" Height="4px" Width="3%" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="knappOK" runat="server" Text="OK" OnClick="knappOK_Click" CssClass="ButtonClass" />
                            <asp:Label ID="Label3" Width="1%" runat="server"></asp:Label>
                            <%--<asp:Button ID="knappAvbryt" runat="server" Text="Stäng" OnClick="knappAvbryt_Click" CssClass="ButtonClass" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" Height="4px" Width="3%" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
