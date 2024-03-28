<%@ Page Title="Nytt Lösenord" Language="C#" MasterPageFile="~/App.Master"
    AutoEventWireup="true" CodeBehind="NyttLösenord.aspx.cs" Inherits="Valvetwebb.NyttLösenord" %>

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
        <div class="form">
            <table id="TableNyttLösenord" style="margin: auto; border: 5px solid white"
                class="custom-table" runat="server">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label6" Width="1%" runat="server"></asp:Label>
                                    <h3>Nytt lösenord</h3>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label7" Width="1%" runat="server"></asp:Label>
                        <asp:Label ID="lblLoginName" runat="server" Text="Loginnamn: "></asp:Label>
                        <asp:Label ID="Label1" Width="2%" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLoginName" runat="server" CssClass="brodtext" Height="21" Width="137" Enabled="false"></asp:TextBox>
                        <asp:Label ID="Label8" Width="1%" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td style="height: 5px" align="center"></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label9" Width="1%" runat="server"></asp:Label>
                        <asp:Label ID="lblOldPassword" runat="server" Text="Gammalt lösenord: "></asp:Label>
                        <asp:Label ID="Label2" Width="2%" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOldPassword" runat="server" CssClass="brodtext" Height="21" Width="137" Enabled="false"></asp:TextBox>
                        <asp:Label ID="Label15" Width="1%" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td style="height: 5px" align="center"></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label11" Width="1%" runat="server"></asp:Label>
                        <asp:Label ID="lblNewPassword" runat="server" Text="Nytt lösenord: "></asp:Label>
                        <asp:Label ID="Label3" Width="2%" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNewPassword" runat="server" CssClass="brodtext" Height="21px" Width="137px" OnTextChanged="txtNewPasswordChanged"
                            TextMode="Password"></asp:TextBox>
                        <asp:Label ID="Label10" Width="1%" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td style="height: 5px" align="center"></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label13" Width="1%" runat="server"></asp:Label>
                        <asp:Label ID="lblNewPassword2" runat="server" Text="Bekräfta nytt lösenord: "></asp:Label>
                        <asp:Label ID="Label4" Width="2%" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNewPasswordKonfirmera" runat="server" CssClass="brodtext" Height="21px" Width="137px"
                            TextMode="Password">></asp:TextBox>
                        <asp:Label ID="Label14" Width="1%" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height: 5px" align="center"></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="knappOK" Text="OK" OnClick="knappOK_Click" Style="cursor: hand" runat="server" CssClass="ButtonClass"></asp:Button>
                        <asp:Label ID="Label5" Width="2%" runat="server"></asp:Label>
                        <asp:Button ID="knappAvbryt" Text="Tillbaka" OnClick="knappAvbryt_Click" Style="cursor: hand" runat="server" CssClass="ButtonClass"></asp:Button>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

