using ProjectBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBlog.Entities.Dtos
{
    public class ArticleAddDto
    {
        [DisplayName("Başlık")]
        [Required(ErrorMessage ="{0} alani bos gecilmemelidir.")]
        [MaxLength(100,ErrorMessage ="{0} alani {1} karakterden buyuk olmamalidir.")]
        [MinLength(5,ErrorMessage ="{0} alani {1} karakterden buyuk olmamalidir.")]
        public string Title { get; set; }
        [DisplayName("İçerik")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir.")]
        [MinLength(20, ErrorMessage = "{0} alani {1} karakterden buyuk olmamalidir.")]
        public string Content { get; set; }
        [DisplayName("Thumnail")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir.")]
        [MaxLength(250, ErrorMessage = "{0} alani {1} karakterden buyuk olmamalidir.")]
        [MinLength(5, ErrorMessage = "{0} alani {1} karakterden buyuk olmamalidir.")]
        public string Thumbnail { get; set; }
        [DisplayName("Tarih")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir.")]
        [DisplayFormat(ApplyFormatInEditMode =true,DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
        [DisplayName("Seo Yazar")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir.")]
        [MaxLength(50, ErrorMessage = "{0} alani {1} karakterden buyuk olmamalidir.")]
        [MinLength(0, ErrorMessage = "{0} alani {1} karakterden buyuk olmamalidir.")]
        public string SeoAuthor { get; set; }
        [DisplayName("Seo Açıklama")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir.")]
        [MaxLength(150, ErrorMessage = "{0} alani {1} karakterden buyuk olmamalidir.")]
        [MinLength(0, ErrorMessage = "{0} alani {1} karakterden buyuk olmamalidir.")]
        public string SeoDescription { get; set;}
        [DisplayName("Seo Etiketler")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir.")]
        [MaxLength(70, ErrorMessage = "{0} alani {1} karakterden buyuk olmamalidir.")]
        [MinLength(0, ErrorMessage = "{0} alani {1} karakterden buyuk olmamalidir.")]
        public string SeoTags { get; set;}
        [DisplayName("Kategori")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir.")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [DisplayName("Aktif Mi?")]
        [Required(ErrorMessage = "{0} alani bos gecilmemelidir.")]
        public bool IsActive { get; set; }




    }
}
