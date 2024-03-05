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
            <table id="TableKalender" style="margin: auto; border: 3px solid white"
                class="form" runat="server">
                <tr>
                    <td>
                        <div class="table-responsive">
                            <table>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="lblSearchDate" runat="server"
                                            Text="Bokningsdatum from:" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td>
                                        <asp:Calendar ID="DatePicker" runat="server"
                                            Visible="false"
                                            OnSelectionChanged="DatePicker_selection_changed">
                                            <TitleStyle BackColor="LightGray"
                                                ForeColor="White"></TitleStyle>
                                            <DayStyle BackColor="White"></DayStyle>
                                            <SelectedDayStyle BackColor="LightGray"
                                                Font-Bold="True"></SelectedDayStyle>
                                        </asp:Calendar>
                                        <asp:TextBox ID="txtDatum" runat="server" Enabled="false"></asp:TextBox>
                                        <asp:LinkButton ID="lnkPickDate" runat="server"
                                            OnClick="lnkPickDate_Click" ForeColor="White"
                                            Font-Bold="true">Välj datum</asp:LinkButton>
                                    </td>

                                    <td></td>
                                    <td>
                                        <asp:Button ID="knappSearch" runat="server"
                                            Text="Sök"
                                            OnClick="knappSearch_Click"
                                            CssClass="ButtonClass" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:DataGrid ID="dgList" runat="server" Width="540px" CssClass="normal" BackColor="#F7F7F7" BorderColor="White" BorderStyle="None"
                                        Font-Size="12pt" CellPadding="1"
                                        AutoGenerateColumns="False" AllowSorting="True"
                                        OnItemCommand="dgList_CellContentClick">
                                        <AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
                                        <ItemStyle BackColor="White"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" BackColor="#91BADD"></HeaderStyle>
                                        <Columns>
                                            <asp:BoundColumn Visible="False" DataField="Bokningsnummer"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Datum" HeaderText="Datum"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Bokade tider" HeaderText="Bokade tider"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Bana" HeaderText="Bana"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Dag" HeaderText="Dag"></asp:BoundColumn>
                                            <asp:ButtonColumn
                                                HeaderText="Visa"
                                                ButtonType="PushButton"
                                                Text="Visa"
                                                CommandName="VisaBokning" />
                                        </Columns>
                                    </asp:DataGrid>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="knappAvbryt" runat="server" Text="Tillbaka" OnClick="knappAvbryt_Click" CssClass="ButtonClass" />
                                    <asp:Button ID="knappLogout" runat="server" Text="Logga ut" OnClick="knappLogout_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
