﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.Vehicle
{
    public partial class vehicle_edit : Web.UI.ManagePage
    {
        string defaultpassword = "0|0|0|0"; //默认显示密码
        protected string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        private int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = DTRequest.GetQueryString("action");
            if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.Edit.ToString())
            {
                this.action = DTEnums.ActionEnum.Edit.ToString();//修改类型
                this.id = DTRequest.GetQueryInt("id");
                if (this.id == 0)
                {
                    JscriptMsg("传输参数不正确！", "back", "Error");
                    return;
                }
                if (!new BLL.Vehicle().Exists(this.id))
                {
                    JscriptMsg("信息不存在或已被删除！", "back", "Error");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("vehicle_list", DTEnums.ActionEnum.View.ToString()); //检查权限
                TreeBind(""); //绑定类别
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }
        }

        #region 绑定类别=================================
        private void TreeBind(string strWhere)
        {
            //BLL.Motorcade bll = new BLL.Motorcade();
            //DataTable dt = bll.GetList(0, strWhere, "Id desc").Tables[0];

            //this.ddlMotorcadeName.Items.Clear();
            //this.ddlMotorcadeName.Items.Add(new ListItem("请选择车队...", ""));
            //foreach (DataRow dr in dt.Rows)
            //{
            //    this.ddlMotorcadeName.Items.Add(new ListItem(dr["Name"].ToString(), dr["Name"].ToString()));
            //}

            //BLL.MotorcycleType mtBLL = new BLL.MotorcycleType();
            //DataTable mtdt = mtBLL.GetList(0, strWhere, "Id desc").Tables[0];

            //this.ddlMotorcycleType.Items.Clear();
            //this.ddlMotorcycleType.Items.Add(new ListItem("请选择车型...", ""));
            //foreach (DataRow dr in mtdt.Rows)
            //{
            //    this.ddlMotorcycleType.Items.Add(new ListItem(dr["Name"].ToString(), dr["Name"].ToString()));
            //}
        }
        #endregion

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.Vehicle bll = new BLL.Vehicle();
            Model.Vehicle model = bll.GetModel(_id);
            if (model != null)
            {
                txtCarCode.Text = model.CarCode;

                BLL.Driver driverBLL = new BLL.Driver();
                Model.Driver driver = driverBLL.GetModel(model.CarCode);
                if (driver != null)
                {
                    txtRealName.Text = driver.RealName;
                    txtLinkTel.Text = driver.LinkTel;
                }
            }
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = false;
            Model.Vehicle model = new Model.Vehicle();
            BLL.Vehicle bll = new BLL.Vehicle();

            model.MotorcadeName = "";
            model.CarCode = txtCarCode.Text.Trim();
            model.CarNumber = model.CarCode;
            model.MotorcycleType = "";
            model.EngineType = "";
            model.ChassisNumber = "";
            model.FrameNumber = "";
            model.InsuranceNumber = "";
            model.LoadingCapacity = 0.00M;
            model.Remarks = "";

            Model.Driver driver = new Model.Driver();
            BLL.Driver driverBll = new BLL.Driver();

            driver.CarNumber = model.CarNumber;
            driver.RealName = txtRealName.Text.Trim();
            driver.RealNameCode = "";
            driver.IdCardNumber = "";
            driver.LinkTel = txtLinkTel.Text.Trim();
            driver.LinkAddress = "";
            driver.BeganWorkDate = DateTime.Now;
            driver.IssuedDate = DateTime.Now;
            driver.AnnualDate = DateTime.Now;
            driver.DrivingLicence = "";
            driver.DrivingPermitNumber = "";
            driver.DrivingPermitType = "";
            driver.IsDimission = 0;
            driver.Remarks = "";

            if (bll.Add(model, driver))
            {
                AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加车辆:" + model.CarCode); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = false;
            BLL.Vehicle bll = new BLL.Vehicle();
            Model.Vehicle model = bll.GetModel(_id);
            string carNumber = model.CarNumber;

            model.MotorcadeName = "";
            model.CarCode = txtCarCode.Text.Trim();
            model.CarNumber = model.CarCode;
            model.MotorcycleType = "";
            model.EngineType = "";
            model.ChassisNumber = "";
            model.FrameNumber = "";
            model.InsuranceNumber = "";
            model.LoadingCapacity = 0.00M;
            model.Remarks = "";

            
            BLL.Driver driverBll = new BLL.Driver();
            Model.Driver driver = driverBll.GetModel(carNumber);

            driver.CarNumber = model.CarNumber;
            driver.RealName = txtRealName.Text.Trim();
            driver.RealNameCode = "";
            driver.IdCardNumber = "";
            driver.LinkTel = txtLinkTel.Text.Trim();
            driver.LinkAddress = "";
            driver.BeganWorkDate = DateTime.Now;
            driver.IssuedDate = DateTime.Now;
            driver.AnnualDate = DateTime.Now;
            driver.DrivingLicence = "";
            driver.DrivingPermitNumber = "";
            driver.DrivingPermitType = "";
            driver.IsDimission = 0;
            driver.Remarks = "";

            if (bll.Update(model, driver))
            {
                AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改车辆信息:" + model.CarCode); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("vehicle_list", DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误！", "", "Error");
                    return;
                }
                JscriptMsg("修改车辆成功！", "vehicle_list.aspx", "Success");
            }
            else //添加
            {
                ChkAdminLevel("vehicle_list", DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误！", "", "Error");
                    return;
                }
                JscriptMsg("添加车辆成功！", "vehicle_list.aspx", "Success");
            }
        }
    }
}