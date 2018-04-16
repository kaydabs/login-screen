using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Dashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect("Login.aspx");
        if (!IsPostBack)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top 10 ProjectName, Deadline from Projects, Works_On where Projects.ProjectID = Works_On.ProjectID AND  Works_On.EmployeeID =" + Session["emp"] + " AND Deadline is not null order by Deadline asc;";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
            AddDepartmentstoSidebar();
            AddPrivateBoards();
        }
    }
    protected void logoutbtn_Click(object sender, EventArgs e)
    {
        Session.Remove("user");
        Response.Redirect("~/Login.aspx");
    }

    protected void AddPrivateBoards()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        con.Open();
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select ProjectID, ProjectName from Projects where ManagerID=" + Session["emp"] + " and isPublic=0 ";
        cmd.ExecuteNonQuery();
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        Repeater3.DataSource = dt;
        Repeater3.DataBind();
    }
    protected void AddDepartmentstoSidebar()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        con.Open();
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select distinct Department.DepartmentID, Department.DepartmentName from Department, Projects, Works_On where Department.DepartmentID = Projects.DepartmentID and Projects.ProjectID = Works_On.ProjectID and Works_On.EmployeeID = " + Session["emp"];

        cmd.ExecuteNonQuery();
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        Repeater2.DataSource = dt;
        Repeater2.DataBind();
    }
}