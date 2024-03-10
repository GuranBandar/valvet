<%@ Page Title="Meny" Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true"
    CodeBehind="Meny.aspx.cs" Inherits="Valvetwebb.Meny" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
    <div>
        <table id="TableHeader" class="form" runat="server">
            <tr>
                <td></td>
                <td>
                    <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="TableRubrik" style="margin: auto; border: 3px solid white">
            <tr>
                <td>
                    <asp:Label ID="Label9" Width="1%" runat="server"></asp:Label>
                    <h3>Välj sida</h3>
                    <asp:Label ID="Label10" Width="1%" runat="server"></asp:Label>
                </td>
            </tr>
            <tr></tr>
            <tr>
                <td>
                    <asp:Label ID="Label8" Width="1%" runat="server"></asp:Label>
                    <asp:Label ID="lblValvlista" runat="server" CssClass="rubriktext" Text="Lista:"></asp:Label>
                    <asp:Label ID="Label5" Width="1%" runat="server"></asp:Label>
                </td>
                <td></td>
                <td></td>
                <td>
                    <asp:Button ID="knappValvlista" runat="server" Text="Lista"
                        OnClick="KnappValvlista_Click" />
                    <asp:Label ID="Label11" Width="1%" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height: 5px" align="center"></td>
            </tr>
            <tr></tr>
            <tr>
                <td>
                    <asp:Label ID="Label7" Width="1%" runat="server"></asp:Label>
                    <asp:Label ID="lblValvpost" runat="server" CssClass="rubriktext" Text="Ny valvpost:"></asp:Label>
                    <asp:Label ID="Label1" Width="1%" runat="server"></asp:Label>
                </td>
                <td></td>
                <td></td>
                <td>
                    <asp:Button ID="knappValvpost" runat="server" Text="Valvpost"
                        OnClick="knappValvpost_Click" />
                    <asp:Label ID="Label12" Width="1%" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height: 5px" align="center"></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" Width="1%" runat="server"></asp:Label>
                    <asp:Label ID="lblNyttLöseord" runat="server" CssClass="rubriktext" Text="Nytt lösenord:"></asp:Label>
                    <asp:Label ID="Label3" Width="1%" runat="server"></asp:Label>
                </td>
                <td></td>
                <td></td>
                <td>
                    <asp:Button ID="knappNyLösen" runat="server" Text="Nytt lösenord"
                        OnClick="knappNyLösen_Click" />
                    <asp:Label ID="Label14" Width="1%" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height: 5px" align="center"></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td>
                    <asp:Button ID="knappAvbryt" runat="server" Text="Tillbaka" OnClick="knappAvbryt_Click" />
                </td>
            </tr>
            <tr>
                <td style="height: 5px" align="center"></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td>
                    <asp:Button ID="knappLogout" runat="server" Text="Logga ut" OnClick="knappLogout_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label15" Width="1%" runat="server"></asp:Label>
                    <asp:Label ID="Label16" Width="1%" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="TableUser" class="form" runat="server">
            <tr>
                <td></td>
                <td>
                    <asp:Label ID="lblUserName" runat="server"></asp:Label>
                </td>
                <td></td>
                <td>
                    <asp:Label ID="lblDatabase" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
