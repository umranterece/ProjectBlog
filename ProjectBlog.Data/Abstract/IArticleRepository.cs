using ProjectBlog.Entities.Concrete;
using ProjectBlog.Shared.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBlog.Data.Abstract
{
    public interface IArticleRepository:IEntityRepository<Article>
    {
    }
}
