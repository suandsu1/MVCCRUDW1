using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCCRUDW1.Models;
using MVCCRUDW1.ViewModel;

namespace MVCCRUDW1.ActionFilters
{
    public class 取的客戶聯絡人清單attribute : ActionFilterAttribute
    {
        客戶聯絡人Repository db聯絡人;
        public 取的客戶聯絡人清單attribute()
        {
            db聯絡人 = RepositoryHelper.Get客戶聯絡人Repository();
        }

        //動作過濾器
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.客戶聯絡人List = (from p in db聯絡人.All()
                                                          select new 客戶聯絡人VM()
                                                          {
                                                              Id = p.Id,
                                                              職稱 = p.職稱,
                                                              手機 = p.手機,
                                                              電話 = p.電話,
                                                              姓名 = p.姓名,
                                                              Email = p.Email
                                                          });
              base.OnActionExecuting(filterContext);
        }

    }
}