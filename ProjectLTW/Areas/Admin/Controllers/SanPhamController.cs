using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectLTW.Data;
using ProjectLTW.Models;

namespace ProjectLTW.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SanPhamController : Controller
	{
		private readonly ApplicationDbContext _db;
		public SanPhamController(ApplicationDbContext db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			IEnumerable<SanPham> sanpham = _db.SanPham.Include("TheLoai").ToList();
			return View(sanpham);
		}
		//get
		[HttpGet]
		public IActionResult Upsert(int id)
		{
			IEnumerable<SelectListItem> dstheloai = _db.TheLoai.Select(
				item => new SelectListItem
				{
					Value = item.Id.ToString(),
					Text = item.Name
				}
			);
			ViewBag.DSTheLoai = dstheloai;

			SanPham sanpham;

			if (id == 0)
			{
				sanpham = new SanPham();
			}
			else
			{
				sanpham = _db.SanPham.Include("TheLoai").FirstOrDefault(sp => sp.Id == id);
				if (sanpham == null)
				{
					return NotFound(); // Xử lý nếu sản phẩm không tồn tại
				}
			}

			return View(sanpham);
		}
		[HttpPost]
		public IActionResult Upsert(SanPham sanpham)
		{
			if (ModelState.IsValid)
			{
				if (sanpham.Id == 0)
				{
					_db.SanPham.Add(sanpham);
				}
				else
				{
					_db.SanPham.Update(sanpham);
				}
				_db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(sanpham);
		}
		[HttpPost]
		public IActionResult Delete(int id)
		{
			var sanpham = _db.SanPham.FirstOrDefault(sp => sp.Id == id);
			if (sanpham == null)
			{ return NotFound(); }
			_db.SanPham.Remove(sanpham);
			_db.SaveChanges();
			return Json(new { success = true });
		}
	}
}
