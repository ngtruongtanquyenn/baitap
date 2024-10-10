using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectLTW.Models
{
	public class SanPham
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public double price { get; set; }
		public string? Description { get; set; }
		public string? ImageUrl { get; set; }
		[Required]
		public int TheLoaiId { get; set; }
		[ForeignKey("TheLoaiId")]
		[ValidateNever]
		public TheLoai TheLoai { get; set; }
	}
}
