﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using DTcms.Model;

namespace DTcms.BLL
{
    public partial class Haulway
    {

        private readonly DTcms.DAL.Haulway dal = new DTcms.DAL.Haulway ();
        public Haulway()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            return dal.Exists(Id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.Haulway model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.Haulway model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {

            return dal.Delete(Id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            return dal.DeleteList(Idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Haulway GetModel(int Id)
        {

            return dal.GetModel(Id);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        //public Model.Haulway GetModelByCache(int Id)
        //{

        //    string CacheKey = "mtms_Model.HaulwayModel-" + Id;
        //    object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
        //    if (objModel == null)
        //    {
        //        try
        //        {
        //            objModel = dal.GetModel(Id);
        //            if (objModel != null)
        //            {
        //                int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
        //                Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
        //            }
        //        }
        //        catch { }
        //    }
        //    return (Model.Haulway)objModel;
        //}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.Haulway> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.Haulway> DataTableToList(DataTable dt)
        {
            List<Model.Haulway> modelList = new List<Model.Haulway>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.Haulway model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.Haulway();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    model.Name = dt.Rows[n]["Name"].ToString();
                    if (dt.Rows[n]["LoadingCapacityRunning"].ToString() != "")
                    {
                        model.LoadingCapacityRunning = decimal.Parse(dt.Rows[n]["LoadingCapacityRunning"].ToString());
                    }
                    if (dt.Rows[n]["NoLoadingCapacityRunning"].ToString() != "")
                    {
                        model.NoLoadingCapacityRunning = decimal.Parse(dt.Rows[n]["NoLoadingCapacityRunning"].ToString());
                    }
                    model.Code = dt.Rows[n]["Code"].ToString();


                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        #endregion

    }
}
