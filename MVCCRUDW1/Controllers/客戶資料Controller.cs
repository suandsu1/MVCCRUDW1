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
    public class 客戶資料Controller : Controller
    {
        客戶資料Repository re客;
        public 客戶資料Controller()
        {
            re客 = RepositoryHelper.Get客戶資料Repository();
        }

        // GET: 客戶資料
        public ActionResult Index()
        {
            ViewBag.客戶分類 = re客.客戶分類ItemList();
            return View(re客.All());
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
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

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
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
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,刪除")] 客戶資料 客戶資料)
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
