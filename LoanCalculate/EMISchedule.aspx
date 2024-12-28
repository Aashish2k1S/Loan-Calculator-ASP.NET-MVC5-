<%@ Page Title="EMI Schedule" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EMISchedule.aspx.cs" Inherits="LoanCalculate.EMISchedule" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.5.0/css/bootstrap-datepicker.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.js">
    </script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.5.0/js/bootstrap-datepicker.js">
    </script>
    <div class="container">
        <div class="row">
            <div class="col-sm-6" style="box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);">
                <h3>EMI Schedule</h3>
                <label for="Label1" class="col-sm-10 col-form-label"></label>
                <label for="Label1" class="col-sm-10 col-form-label"></label>
                <div class="form-group row">
                    <label for="lblPlanName" class="col-sm-4 col-form-label">Select Plan Name:</label>
                    <div class="col-sm-6">
                        <asp:DropDownList ID="ddlPlanName" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPlanName_SelectIndexChanged"></asp:DropDownList>
                    </div>
                    <label for="Label1" class="col-sm-10 col-form-label"></label>
                    <label for="lblTenure" class="col-sm-4 col-form-label">Tenure (Months):</label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtTenure" runat="server" CssClass="form-control" TextMode="Number" Enabled="false" Text=""></asp:TextBox>
                    </div>
                    <label for="Label1" class="col-sm-10 col-form-label"></label>
                    <label for="lblROI" class="col-sm-4 col-form-label">ROI (%):</label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtROI" runat="server" CssClass="form-control" TextMode="Number" Enabled="false" Text=""></asp:TextBox>
                    </div>
                    <label for="Label1" class="col-sm-10 col-form-label"></label>
                    <label for="lblEnterLoanAmount" class="col-sm-4 col-form-label">Enter Loan Amount:</label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtEnterLoanAmount" runat="server" CssClass="form-control" TextMode="Number" Text="0"
                            onFocus="if (this.value == '0') this.value = '';" 
                            onBlur="if (this.value == '') this.value = '0';"
                        ></asp:TextBox>
                    </div>
                    <label for="Label1" class="col-sm-10 col-form-label"></label>
                    <label for="lblLoanDate" class="col-sm-4 col-form-label">Loan Date:</label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtLoanDate" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <label for="Label1" class="col-sm-10 col-form-label"></label>
                    <div class="col-sm-10">
                        <asp:Button ID="btnCalculateEMI" runat="server" Text="Calculate" OnClick="btnCalculateEMI_Click" Class="btn btn-primary float-right" BackColor="#196076" />
                    </div>
                    <label for="Label1" class="col-sm-10 col-form-label"></label>
                    <label for="lblEMIAmount" class="col-sm-4 col-form-label">EMI Amount:</label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtEMIAmount" runat="server" CssClass="form-control" TextMode="Number" Enabled="false" Text="0"></asp:TextBox>
                    </div>
                    <label for="Label1" class="col-sm-10 col-form-label"></label>
                    <div class="col-sm-10">
                        <asp:Button ID="btnGenerateSchedule" runat="server" Text="Generate Schedule" OnClick="btnGenerateSchedule_Click" Class="btn btn-success float-right" BackColor="#2C632C" />
                    </div>
                </div>
            </div>
            <div class="col-sm-6" style="box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);">
                <div class="col-sm-12">
                    <asp:GridView ID="gvLoanSchedule" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
                        <HeaderStyle BackColor="#A9A9A9" Font-Bold="True" />
                        <Columns>
                            <asp:BoundField DataField="SerialNumber" HeaderText="EMI No" />
                            <asp:BoundField DataField="LoanDate" HeaderText="Due Date" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="LoanAmount" HeaderText="EMI Amount" DataFormatString="₹{0:N2}" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%= txtLoanDate.ClientID %>').datepicker({
            language: "tr",
            autoclose: true,
            format: 'dd/mm/yyyy'
        });
    });
    </script>








</asp:Content>
