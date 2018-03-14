﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBManager;

namespace Lab2_2_.Alumno
{
    public partial class TareasAlumno : System.Web.UI.Page
    {
        private DBManager.DBManager dBManager = new DBManager.DBManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority).Contains("localhost"))
            {
                log.Visible = false;
            }
            
            dBManager.Conectar();
            if (!IsPostBack)
            {
                List<String> asignaturas = dBManager.getAsignaturasAlumno(Session["usuario"].ToString());
                DDAsignaturas.DataSource = asignaturas;
                DDAsignaturas.DataBind();
                System.Data.DataTable dt = dBManager.getTareasGenericas(Session["usuario"].ToString()).Tables[0];
                System.Data.DataView dv = new System.Data.DataView(dt);
                dv.RowFilter = "codigoasig ='" + DDAsignaturas.Text.ToString() + "'";
                Session["dataview"] = dv;
                log.Text = dv.RowFilter.ToString();
                GVAsignaturas.DataSource = dv;
                GVAsignaturas.DataBind();
            }

        }

        protected void DDAsignaturas_SelectedIndexChanged(object sender, EventArgs e)
        {
           System.Data.DataView dv = Session["dataview"] as System.Data.DataView;
            dv.RowFilter = "codigoasig ='" + DDAsignaturas.Text.ToString() + "'";
            log.Text += " //// "+ dv.RowFilter.ToString();
            GVAsignaturas.DataSource = dv;
            GVAsignaturas.DataBind();
        }
    }
}