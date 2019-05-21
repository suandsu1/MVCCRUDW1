using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVCCRUDW1.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public 客戶銀行資訊 Find(int id)
        {
            return this.All().Where(p => p.Id == id).FirstOrDefault();
        }

        public IQueryable<客戶銀行資訊> searchALL(string sortOrder, string currentSort, string searchString = null)
        {
            var 客where = this.All();
            if (!String.IsNullOrEmpty(searchString))
            {
                客where = 客where.Where(s => s.帳戶名稱.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "客戶Id":
                    if (sortOrder.Equals(currentSort))
                    { 客where = 客where.OrderByDescending(s => s.客戶Id); }
                    else { 客where = 客where.OrderBy(s => s.客戶Id); }
                    break;
                case "帳戶名稱":
                    if (sortOrder.Equals(currentSort))
                    { 客where = 客where.OrderByDescending(s => s.帳戶名稱); }
                    else { 客where = 客where.OrderBy(s => s.帳戶名稱); }
                    break;
                case "帳戶號碼":
                    if (sortOrder.Equals(currentSort))
                    { 客where = 客where.OrderByDescending(s => s.帳戶號碼); }
                    else { 客where = 客where.OrderBy(s => s.帳戶號碼); }
                    break;
                case "銀行代碼":
                    if (sortOrder.Equals(currentSort))
                    { 客where = 客where.OrderByDescending(s => s.銀行代碼); }
                    else { 客where = 客where.OrderBy(s => s.銀行代碼); }
                    break;
                case "銀行名稱":
                    if (sortOrder.Equals(currentSort))
                    { 客where = 客where.OrderByDescending(s => s.銀行名稱); }
                    else { 客where = 客where.OrderBy(s => s.銀行名稱); }
                    break;
                default:
                    客where = 客where.OrderBy(s => s.客戶Id);
                    break;
            }

            return 客where;
        }
        public override IQueryable<客戶銀行資訊> All()
        {
            //不顯示已刪除資料
            //db.客戶銀行資訊.Include(客 => 客.客戶資料);
            return base.All().Where(c => c.刪除.Value == false);
        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}