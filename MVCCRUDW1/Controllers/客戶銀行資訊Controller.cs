using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCCRUDW1.Models;

namespace MVCCRUDW1.Controllers
{
    public class 客戶銀行資訊Controller : BaseController
    {
        客戶銀行資訊Repository db客戶銀行;
        客戶資料Repository re客;
        public 客戶銀行資訊Controller()
        {
            db客戶銀行 = RepositoryHelper.Get客戶銀行資訊Repository();
            re客 = RepositoryHelper.Get客戶資料Repository();
        }

        // GET: 客戶銀行資訊
        public ActionResult Index(string sortOrder, string currentSort, string searchString = null)
        {
            ViewBag.客戶IdSort = String.IsNullOrEmpty(currentSort) ? "客戶Id" : "";
            ViewBag.帳戶名稱Sort = currentSort == "帳戶名稱" ? "" : sortOrder;
            ViewBag.帳戶號碼Sort = currentSort == "帳戶號碼" ? "" : sortOrder;
            ViewBag.銀行代碼Sort = currentSort == "銀行代碼" ? "" : sortOrder;
            ViewBag.銀行名稱Sort = currentSort == "銀行名稱" ? "" : sortOrder;
            var 客戶銀行資訊 = db客戶銀行.searchALL(sortOrder, currentSort, searchString);
            return View(客戶銀行資訊.ToList());
        }
        //closedXML匯出
        public ActionResult closedXMLDataExport(string sortOrder, string currentSort, string searchString = null)
        {
            var 客where = db客戶銀行.searchALL(sortOrder, currentSort, searchString);
            var 資料Export = 客where.Select(c => new { c.客戶資料.客戶名稱, c.帳戶名稱, c.帳戶號碼, c.銀行代碼, c.銀行名稱 });

            return DataExport(資料Export, "客戶銀行資訊");
        }
        // GET: 客戶銀行資訊/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = db客戶銀行.Find(id.Value);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(re客.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶銀行資訊/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼,刪除")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                客戶銀行資訊.刪除 = false;
                db客戶銀行.Add(客戶銀行資訊);
                db客戶銀行.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(re客.All(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = db客戶銀行.Find(id.Value);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(re客.All(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // POST: 客戶銀行資訊/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼,刪除")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                客戶銀行資訊.刪除 = false;
                db客戶銀行.UnitOfWork.Context.Entry(客戶銀行資訊).State = EntityState.Modified;
                db客戶銀行.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(re客.All(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = db客戶銀行.Find(id.Value);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // POST: 客戶銀行資訊/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var 客戶銀行資訊 = db客戶銀行.Find(id);
            客戶銀行資訊.刪除 = true; //代表刪除
            db客戶銀行.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db客戶銀行.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
