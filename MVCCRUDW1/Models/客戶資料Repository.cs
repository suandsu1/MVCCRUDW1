using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MVCCRUDW1.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public List<SelectListItem> 客戶分類ItemList()
        {
            List<SelectListItem> 分類ItemList = new List<SelectListItem>();

            分類ItemList.AddRange(new[]{
                new SelectListItem() { Text = "組織一", Value = "組織一", Selected = true},
                new SelectListItem() { Text = "組織二", Value = "組織二", Selected = false},
                new SelectListItem() { Text = "組織三", Value = "組織三", Selected = false}
             });

            return 分類ItemList;

        }

        public 客戶資料 Find(int id)
        {
            return this.All().Where(p => p.Id == id).FirstOrDefault();
        }

        public IQueryable<客戶資料> searchALL(string sortOrder = null, string currentSort = null, string searchString = null, string 客戶分類ItemList = null)
        {
            var 客where = this.All();
            if (!String.IsNullOrEmpty(searchString))
            {
                客where = 客where.Where(s => s.客戶名稱.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(客戶分類ItemList))
            {
                客where = 客where.Where(s => s.客戶分類.Contains(客戶分類ItemList));
            }
            switch (sortOrder)
            {
                case "客戶名稱":
                    if (sortOrder.Equals(currentSort))
                    { 客where = 客where.OrderByDescending(s => s.客戶名稱); }
                    else { 客where = 客where.OrderBy(s => s.客戶名稱); }
                    break;
                case "統一編號":
                    if (sortOrder.Equals(currentSort))
                    { 客where = 客where.OrderByDescending(s => s.統一編號); }
                    else { 客where = 客where.OrderBy(s => s.統一編號); }
                    break;
                case "電話":
                    if (sortOrder.Equals(currentSort))
                    { 客where = 客where.OrderByDescending(s => s.電話); }
                    else { 客where = 客where.OrderBy(s => s.電話); }
                    break;
                case "傳真":
                    if (sortOrder.Equals(currentSort))
                    { 客where = 客where.OrderByDescending(s => s.傳真); }
                    else { 客where = 客where.OrderBy(s => s.傳真); }
                    break;
                case "地址":
                    if (sortOrder.Equals(currentSort))
                    { 客where = 客where.OrderByDescending(s => s.地址); }
                    else { 客where = 客where.OrderBy(s => s.地址); }
                    break;
                case "Email":
                    if (sortOrder.Equals(currentSort))
                    { 客where = 客where.OrderByDescending(s => s.Email); }
                    else { 客where = 客where.OrderBy(s => s.Email); }
                    break;
                case "客戶分類":
                    if (sortOrder.Equals(currentSort))
                    { 客where = 客where.OrderByDescending(s => s.客戶分類); }
                    else { 客where = 客where.OrderBy(s => s.客戶分類); }
                    break;
                default:
                    客where = 客where.OrderBy(s => s.客戶名稱);
                    break;
            }

            return 客where;
        }
        public override IQueryable<客戶資料> All()
        {
            //不顯示已刪除資料
            return base.All().Where(c => c.刪除.Value == false);
        }
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}