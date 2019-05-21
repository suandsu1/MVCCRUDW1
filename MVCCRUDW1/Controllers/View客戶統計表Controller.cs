using MVCCRUDW1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCCRUDW1.Controllers
{
    public class View客戶統計表Controller : Controller
    {
        View客戶統計表Repository View統計表;
        public View客戶統計表Controller()
        {
            View統計表 = RepositoryHelper.GetView客戶統計表Repository();
        }
        // GET: View客戶統計表
        public ActionResult Index()
        {
            return View(View統計表.All());
        }
    }
}