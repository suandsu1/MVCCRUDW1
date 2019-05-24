using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVCCRUDW1.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public 客戶聯絡人 Find(int id)
        {
            return this.All().Where(p => p.Id == id).FirstOrDefault();
        }
       
        public 客戶聯絡人 EmailFind(string Email)
        {
            return this.All().Where(p => p.Email == Email).FirstOrDefault();
        }
        public IQueryable<客戶聯絡人> searchALL(string sortOrder = null, string currentSort = null, string searchString = null)
        {
            var 客where = this.All();
            if (!String.IsNullOrEmpty(searchString))
            {
                客where = 客where.Where(s => s.職稱.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "職稱":
                    if (sortOrder.Equals(currentSort))
                    { 客where = 客where.OrderByDescending(s => s.職稱); }
                    else { 客where = 客where.OrderBy(s => s.職稱); }
                    break;
                case "姓名":
                    if (sortOrder.Equals(currentSort))
                    { 客where = 客where.OrderByDescending(s => s.姓名); }
                    else { 客where = 客where.OrderBy(s => s.姓名); }
                    break;
                case "Email":
                    if (sortOrder.Equals(currentSort))
                    { 客where = 客where.OrderByDescending(s => s.Email); }
                    else { 客where = 客where.OrderBy(s => s.Email); }
                    break;
                case "手機":
                    if (sortOrder.Equals(currentSort))
                    { 客where = 客where.OrderByDescending(s => s.手機); }
                    else { 客where = 客where.OrderBy(s => s.手機); }
                    break;
                case "電話":
                    if (sortOrder.Equals(currentSort))
                    { 客where = 客where.OrderByDescending(s => s.電話); }
                    else { 客where = 客where.OrderBy(s => s.電話); }
                    break;
                case "客戶名稱":
                    if (sortOrder.Equals(currentSort))
                    { 客where = 客where.OrderByDescending(s => s.客戶資料.客戶名稱); }
                    else { 客where = 客where.OrderBy(s => s.客戶資料.客戶名稱); }
                    break;
                default:
                    客where = 客where.OrderBy(s => s.職稱);
                    break;
            }

            return 客where;
        }
        public override IQueryable<客戶聯絡人> All()
        {
            //不顯示已刪除資料
            return base.All().Where(c => c.刪除.Value == false);
        }
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}