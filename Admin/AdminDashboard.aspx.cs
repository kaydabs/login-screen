﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Admin_AdminDashboard : System.Web.UI.Page
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
            cmd.CommandText = "select top 10 AssignmentNote, AssignmentEnd from Assignment where AssignmentEnd is not null order by AssignmentEnd asc;";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
            AddDepartmentstoSidebar();
        }
    }

    protected void AddDepartmentstoSidebar()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        con.Open();
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select ProjectId, ProjectName from Project";
        cmd.ExecuteNonQuery();
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        Repeater2.DataSource = dt;
        Repeater2.DataBind();
    }
}