<%@ Page Title="Plan Master" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PlanMaster.aspx.cs" Inherits="LoanCalculate.PlanMaster" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
     <div class="container" style="width:50%; box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);">
        <h3>Creata Scheme</h3>
         <div class="form-group row">
         <label for="Label1" class="col-sm-10 col-form-label"></label>
             </div>
          <div class="form-group row">
            <label for="lblPlanName" class="col-sm-4 col-form-label">Plan Name:</label>
            <div class="col-sm-6">
                <asp:TextBox ID="txtPlanName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group row">
            <label for="lblTenure" class="col-sm-4 col-form-label">Tenure (Months):</label>
            <div class="col-sm-6">
                <asp:TextBox ID="txtTenure" runat="server" CssClass="form-control" TextMode="Number" Text="0" 
                    onFocus="if (this.value == '0') this.value = '';" 
                    onBlur="if (this.value == '') this.value = '0';"
                ></asp:TextBox>
            </div>
        </div>
        <div class="form-group row">
            <label for="lblROI" class="col-sm-4 col-form-label">ROI (%):</label>
            <div class="col-sm-6">
                <asp:TextBox ID="txtROI" runat="server" CssClass="form-control" TextMode="Number" Text="0"
                    onFocus="if (this.value == '0') this.value = '';" 
                    onBlur="if (this.value == '') this.value = '0';"
                ></asp:TextBox>
            </div>
        </div>
        <div class="form-group row">
            <div class="col-sm-10 offset-sm-2">
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClientClick="return validateForm();" OnClick="btnSave_Click" Class="btn btn-success float-right" BackColor ="#2C632C"  />
            </div>
        </div>
    </div>
    <script>
        function validateForm() {
            var planName = document.getElementById('<%= txtPlanName.ClientID %>').value.trim();
             var tenure = document.getElementById('<%= txtTenure.ClientID %>').value.trim();
             var roi = document.getElementById('<%= txtROI.ClientID %>').value.trim();

             if (planName === '') {
                 alert(`Oops!! PlanName can't be empty.`);
                 return;
             }

             if (isNaN(tenure) || tenure === '' || tenure < 0 || tenure === 0) {
                 alert(`Oops!! Tenure(month) is invalid.`);
                 return; 
             }

             if (isNaN(roi) || roi === '' || roi < 0) {
                 alert(`Oops!! ROI(%) is invalid.`);
                 return; 
             }
         }
    </script>
</asp:Content>
