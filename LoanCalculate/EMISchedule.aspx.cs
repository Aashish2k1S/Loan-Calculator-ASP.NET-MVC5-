using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;


namespace LoanCalculate
{
    public partial class EMISchedule : System.Web.UI.Page
    {
        private void MsgBox(string message)        
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) PopulatePlanList();
        }

        protected void PopulatePlanList()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_LoanCalculate", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@P_Opmode", 2);
                    connection.Open();

                    DataTable dt = new DataTable();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }

                    ddlPlanName.DataSource = (dt.Rows.Count > 0 ? dt : null);                   
                    ddlPlanName.DataBind();
                    ddlPlanName.Items.Insert(0, new ListItem("--Select Scheme--", ""));
                }
            }
        }

        protected void ddlPlanName_SelectIndexChanged(object sender, EventArgs e)
        {
            if (ddlPlanName.SelectedIndex == 0)
            {
                txtTenure.Text = "";
                txtROI.Text = "";
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_LoanCalculate", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@P_Opmode", 3);
                    command.Parameters.AddWithValue("@PlanID", Convert.ToInt64(ddlPlanName.SelectedValue));
                    connection.Open();

                    DataTable dt = new DataTable();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }

                    txtTenure.Text = Convert.ToString(dt.Rows[0]["Tenure"]);
                    txtROI.Text = Convert.ToString(dt.Rows[0]["ROI"]);
                }
            }
        }

        protected void btnGenerateSchedule_Click(object sender, EventArgs e)
        {
            if (txtEMIAmount.Text == "0" || txtEMIAmount.Text == "")
            {
                MsgBox("Please get the EMI amount first.");
                return;
            }
            else if (txtLoanDate.Text == "")
            {
                MsgBox("Please choose a loan date.");
                return;
            }
            DateTime loanDate = DateTime.ParseExact(txtLoanDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            int tenureMonths = int.Parse(txtTenure.Text);

            List<DateTime> scheduleDates = new List<DateTime>();
            DateTime currentDate = loanDate;

            for (int i = 0; i < tenureMonths; i++)
            {
                scheduleDates.Add(currentDate);
                currentDate = currentDate.AddMonths(1);
                if (currentDate.Day != loanDate.Day)
                {
                    currentDate = new DateTime(currentDate.Year, currentDate.Month, DateTime.DaysInMonth(currentDate.Year, currentDate.Month));
                }
            }
            PopulateGrid(scheduleDates);
        }

        private void PopulateGrid(List<DateTime> scheduleDates)
        {
            if (scheduleDates.Count == 0)
            {
                gvLoanSchedule.DataSource = null;
                gvLoanSchedule.DataBind();
                return;
            }

            gvLoanSchedule.DataSource = null;
            DataTable dt = new DataTable();

            dt.Columns.Add("SerialNumber", typeof(int));
            dt.Columns.Add("LoanDate", typeof(DateTime));
            dt.Columns.Add("LoanAmount", typeof(decimal));

            for (int i = 0; i < scheduleDates.Count; i++)
            {
                int serialNumber = i + 1;
                DateTime date = scheduleDates[i];
                decimal loanAmount = decimal.Parse(txtEMIAmount.Text);

                dt.Rows.Add(serialNumber, date, loanAmount);
            }
            gvLoanSchedule.DataSource = dt;
            gvLoanSchedule.DataBind();
        }

        protected void btnCalculateEMI_Click(object sender, EventArgs e)
        {
            if (txtEnterLoanAmount.Text == "0" || txtEMIAmount.Text == "" || Convert.ToDecimal(txtEnterLoanAmount.Text) <= 0)
            {
                MsgBox("Please select a plan name and enter a valid loan amount greater than 0.");
                return;
            }
            
            decimal 
                LoanAmount = Convert.ToDecimal(txtEnterLoanAmount.Text),
                ROI = Convert.ToDecimal(txtROI.Text);
                
            Int64 Tenure = Convert.ToInt64(txtTenure.Text);
            
            decimal EMIAmount = ( LoanAmount + ( LoanAmount * ( ROI/100 ) ) ) / Tenure;
            txtEMIAmount.Text = EMIAmount.ToString("0.00");
        }


    }
}
