<%@ Page Title="Valvlista" Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true"
    CodeBehind="Valvlista.aspx.cs" Inherits="Valvetwebb.Valvlista" %>

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
            <table id="TableValvlista" style="margin: auto; border: 3px solid white"
                class="custom-table" runat="server">
                <tr></tr>
                <tr>
                    <td>
                        <asp:Label ID="RubrikValvLista" runat="server" Text="Mitt valv" CssClass="h3"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="table-responsive">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSearchPost" runat="server" Text="Sök i Mitt valv" Font-Bold="true" CssClass="rubriktext"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtSearchPost" runat="server"></asp:TextBox>
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="knappSearch" runat="server" Text="Sök"
                                            OnClick="knappSearch_Click" CssClass="ButtonClass" />
                                    </td>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label3" Height="4px" Width="3%" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="knappAvbryt" runat="server" Text="Tillbaka" OnClick="knappAvbryt_Click" CssClass="ButtonClass" />
                                            <asp:Button ID="knappNy" runat="server" Text="Ny post" OnClick="knappNy_Click" CssClass="ButtonClass" />
                                            <asp:Button ID="knappLogout" runat="server" Text="Logga ut" OnClick="knappLogout_Click" />
                                            <asp:Button ID="knappSkapaPdf" runat="server" Text="Skapa Pdf" OnClick="knappSkapaPdf_Click" />
                                        </td>
                                    </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" Height="4px" Width="3%" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:DataGrid ID="dgList" runat="server" Width="300px" CssClass="normal" BackColor="#F7F7F7" BorderColor="White" BorderStyle="None"
                                        Font-Size="12pt" CellPadding="1"
                                        AutoGenerateColumns="False" AllowSorting="True"
                                        OnItemCommand="dgList_CellContentClick">
                                        <AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" BackColor="#91BADD"></HeaderStyle>
                                        <Columns>
                                            <asp:BoundColumn Visible="false" DataField="PostID"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Postnummer"></asp:BoundColumn>
                                            <asp:TemplateColumn>
                                                <ItemTemplate>
                                                    <asp:Image ID="Postlogga" Height="10" Width="10" runat="server" DataImageUrlField="http://acer.se/icon" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="Postnamn" HeaderText="Namn"></asp:BoundColumn>
                                            <asp:ButtonColumn HeaderText="Visa" ButtonType="PushButton" Text="Visa" CommandName="VisaPost" />
                                        </Columns>
                                    </asp:DataGrid>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" Height="4px" Width="3%" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
