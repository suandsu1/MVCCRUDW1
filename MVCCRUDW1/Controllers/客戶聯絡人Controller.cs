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
    public class 客戶聯絡人Controller : ClosedXMLController
    {
        客戶聯絡人Repository db聯絡人;
        客戶資料Repository re客;
        public 客戶聯絡人Controller()
        {
            db聯絡人 = RepositoryHelper.Get客戶聯絡人Repository();
            re客 = RepositoryHelper.Get客戶資料Repository();
        }

        // GET: 客戶聯絡人
        public ActionResult Index(string sortOrder, string currentSort, string searchString = null)
        {
            ViewBag.職稱Sort = String.IsNullOrEmpty(currentSort) ? "職稱" : sortOrder;
            ViewBag.姓名Sort = currentSort == "姓名" ? "" : sortOrder;
            ViewBag.EmailSort = currentSort == "Email" ? "" : sortOrder;
            ViewBag.電話Sort = currentSort == "電話" ? "" : sortOrder;
            ViewBag.手機Sort = currentSort == "手機" ? "" : sortOrder;
            ViewBag.客戶名稱Sort = currentSort == "客戶名稱" ? "" : sortOrder;
            var 客where = db聯絡人.searchALL(sortOrder, currentSort, searchString);
            return View(客where.ToList());
        }
        //closedXML匯出
        public ActionResult closedXMLDataExport(string sortOrder, string currentSort, string searchString = null)
        {
            var 客where = db聯絡人.searchALL(sortOrder, currentSort, searchString);
            var 資料Export = 客where.Select(c => new { c.客戶資料.客戶名稱, c.職稱, c.姓名, c.Email, c.電話, c.手機 });

            return DataExport(資料Export, "客戶聯絡人");
        }
        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db聯絡人.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(re客.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話,刪除")] 客戶聯絡人 客戶聯絡人)
        {
            var s = db聯絡人.EmailFind(客戶聯絡人.Email);
            if (ModelState.IsValid && (s == null))
            {
                客戶聯絡人.刪除 = false;
                db聯絡人.Add(客戶聯絡人);
                db聯絡人.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            if (s != null)
            {
                ViewBag.ErrorMessage = "你輸入的Email已經使用過";
            }
            ViewBag.客戶Id = new SelectList(re客.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db聯絡人.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(re客.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話,刪除")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                客戶聯絡人.刪除 = false;
                db聯絡人.UnitOfWork.Context.Entry(客戶聯絡人).State = EntityState.Modified;
                db聯絡人.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(re客.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db聯絡人.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var 客戶聯絡人 = db聯絡人.Find(id);
            客戶聯絡人.刪除 = true; //代表刪除
            db聯絡人.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db聯絡人.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
