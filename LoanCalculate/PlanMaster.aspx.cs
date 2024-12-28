using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services.Description;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;


namespace LoanCalculate
{
    public partial class PlanMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string PlanName = txtPlanName.Text.ToString().Trim();
            Int64 Tenure = Convert.ToInt64(txtTenure.Text);
            decimal ROI = Convert.ToDecimal(txtROI.Text);

            if (PlanName =="" || Tenure <= 0 || ROI <= 0) return;

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();

                    SqlCommand command = new SqlCommand("SP_LoanCalculate", connection, transaction);
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the SqlCommand
                    command.Parameters.AddWithValue("@P_Opmode", 1);
                    command.Parameters.AddWithValue("@PlaneName", PlanName);
                    command.Parameters.AddWithValue("@Tenure", Tenure);
                    command.Parameters.AddWithValue("@ROI", ROI);

                    SqlParameter messageParameter = new SqlParameter("@P_Return", SqlDbType.NVarChar, 100);
                    messageParameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(messageParameter);

                    // Execute the stored procedure
                    int rowsAffected = command.ExecuteNonQuery();

                    

                    if (rowsAffected > 0)
                    {
                        transaction.Commit();
                        string message = messageParameter.Value.ToString();
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
                        
                    }
                    else
                    {
                        transaction.Rollback();
                        string message = messageParameter.Value.ToString();
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
                    }
                    txtROI.Text = "0";
                    txtTenure.Text = "0";
                    txtPlanName.Text = "";
                    connection.Close();
                }
                catch (Exception ex)
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert({ex.Message});", true);
                    }
                }
            }
        }
    }
}