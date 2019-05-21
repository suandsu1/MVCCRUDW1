using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVCCRUDW1.Models
{   
	public  class View客戶統計表Repository : EFRepository<View客戶統計表>, IView客戶統計表Repository
	{
        public View客戶統計表 Find(int id)
        {
            return this.All().Where(p => p.Id == id).FirstOrDefault();
        }
        public override IQueryable<View客戶統計表> All()
        {
            //不顯示已刪除資料
            return base.All().Where(c => false == c.刪除.Value);
        }
    }

	public  interface IView客戶統計表Repository : IRepository<View客戶統計表>
	{

	}
}