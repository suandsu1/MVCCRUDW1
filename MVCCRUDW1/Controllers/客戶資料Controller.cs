﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCCRUDW1.Models;
using X.PagedList;
namespace MVCCRUDW1.Controllers
{
    [RoutePrefix("客戶資料")]
    public class 客戶資料Controller : BaseController
    {
       
        客戶資料Repository re客;
        View客戶統計表Repository View統計表;
        private int pageSize = 2;
        public 客戶資料Controller()
        {
            re客 = RepositoryHelper.Get客戶資料Repository();
            View統計表 = RepositoryHelper.GetView客戶統計表Repository();
        }

        // GET: 客戶資料
        [Route("{page}")]
        public ActionResult Index(int? page, string sortOrder, string currentSort, string searchString = null, string 客戶分類ItemList = null)
        {
            ViewBag.客戶分類 = re客.客戶分類ItemList();
            ViewBag.客戶分類ItemList = re客.客戶分類ItemList();

            ViewBag.客戶名稱Sort = String.IsNullOrEmpty(currentSort) ? "客戶名稱" : "";
            ViewBag.統一編號Sort = currentSort == "統一編號" ? "" : sortOrder;
            ViewBag.電話Sort = currentSort == "電話" ? "" : sortOrder;
            ViewBag.傳真Sort = currentSort == "傳真" ? "" : sortOrder;
            ViewBag.地址Sort = currentSort == "地址" ? "" : sortOrder;
            ViewBag.EmailSort = currentSort == "Email" ? "" : sortOrder;
            ViewBag.客戶分類Sort = currentSort == "客戶分類" ? "" : sortOrder;

            var 客where = re客.searchALL(sortOrder, currentSort, searchString, 客戶分類ItemList);
            var 資料Export = 客where.Select(c => new { c.客戶分類, c.客戶名稱, c.電話, c.傳真, c.地址 });
            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfProducts = 客where.ToPagedList(pageNumber, pageSize); // will only contain 5 products max because of the pageSize

            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View(onePageOfProducts.ToList());

        }


        //closedXML匯出
        public ActionResult closedXMLDataExport(string sortOrder, string currentSort, string searchString = null, string 客戶分類ItemList = null)
        {
            var 客where = re客.searchALL(sortOrder, currentSort, searchString, 客戶分類ItemList);
            var 資料Export = 客where.Select(c => new { c.客戶分類, c.客戶名稱, c.電話, c.傳真, c.地址 });

            return DataExport(資料Export, "客戶資料");
        }


        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
          
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = re客.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            ViewBag.客戶分類 = re客.客戶分類ItemList();
            return View();
        }

        // POST: 客戶資料/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,刪除,客戶分類")] 客戶資料 客戶資料)
        {
          
            if (ModelState.IsValid)
            {
                客戶資料.刪除 = false;
                re客.Add(客戶資料);
                re客.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.客戶分類 = re客.客戶分類ItemList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = re客.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,刪除,客戶分類")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                客戶資料.刪除 = false;
                re客.UnitOfWork.Context.Entry(客戶資料).State = EntityState.Modified;
                re客.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }


       

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = re客.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        //json:刪除客戶資料
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult JsonDelete(int? id)
        {
            var d = re客.Find(id.Value);
            d.刪除 = true; //代表刪除
            re客.UnitOfWork.Commit();
            return Json(new
            {
                msg = "刪除成功",
            }, JsonRequestBehavior.AllowGet);
        }


        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var 客戶資料 = re客.Find(id);
            客戶資料.刪除 = true; //代表刪除
            re客.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                re客.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
