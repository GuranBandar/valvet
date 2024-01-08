<%@ Page Title="MessageBox" Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true"
    CodeBehind="MessageBox.aspx.cs" Inherits="Valvetwebb.MessageBox" %>

<asp:Content ContentPlaceHolderID="Main" runat="server">
    <div>
        <div class="form">
            <table id="TableNMeddelande" style="margin: auto; border: 5px solid white"
                class="form" runat="server">
                <tr>
                    <td>
                        <div class="table-responsive">
                            <table class="custom-table">
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <table class="custom-table" style="background-color:whitesmoke"">
                            <tr>
                                <td style="height: 5px" align="center"></td>
                                <td>
                                    <div style="margin-left: auto; margin-right: auto; text-align: center;">
                                        <asp:Label ID="Titel" runat="server" Font-Bold="true" Font-Size="12pt"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 5px" align="center"></td>
                            </tr>
                            <tr>
                                <td style="height: 5px" align="center"></td>
                                <td>
                                    <div style="margin-left: auto; margin-right: auto; text-align: center;">
                                        <asp:Label ID="lblMessagetext" runat="server" Width="300px" CssClass="messagetext"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 5px" align="center"></td>
                            </tr>
                            <tr>
                                <td style="height: 5px" align="center"></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <div style="margin-left: auto; margin-right: auto; text-align: center;">
                                        <asp:Button ID="knappOK" Text="OK" OnClick="knappOK_Click" Style="cursor: hand" runat="server" CssClass="ButtonClass"></asp:Button>
                                        <asp:Label ID="Label5" Width="2%" runat="server"></asp:Label>
                                        <asp:Button ID="knappTabort" Text="Nej" OnClick="knappTabort_Click" runat="server" CssClass="ButtonClass"></asp:Button>
                                        <asp:Label ID="Label1" Width="2%" runat="server"></asp:Label>
                                        <asp:Button ID="knappAvbryt" Text="Avbryt" OnClick="knappAvbryt_Click" Style="cursor: hand" runat="server" CssClass="ButtonClass"></asp:Button>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
