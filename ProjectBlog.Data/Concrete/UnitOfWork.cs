using ProjectBlog.Data.Abstract;
using ProjectBlog.Data.Concrete.EntityFramework.Contexts;
using ProjectBlog.Data.Concrete.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBlog.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjectBlogContext _projectBlogContext;
        private readonly EfArticleRepository _articleRepository;
        private readonly EfCategoryRepository _categoryRepository;
        private readonly EfCommentRepository _commentRepository;
        private readonly EfRoleRepository _roleRepository;
        private readonly EfUserRepository _userRepository;


        public UnitOfWork(ProjectBlogContext projectBlogContext)
        {
            _projectBlogContext = projectBlogContext;
           
        }

        public IArticleRepository Articles => _articleRepository ?? new EfArticleRepository(_projectBlogContext);

        public ICategoryRepository Categories => _categoryRepository ?? new EfCategoryRepository(_projectBlogContext);

        public ICommentRepository Comments => _commentRepository ?? new EfCommentRepository(_projectBlogContext);

        public IRoleRepository Roles => _roleRepository ?? new EfRoleRepository(_projectBlogContext);

        public IUserRepository Users => _userRepository ?? new EfUserRepository(_projectBlogContext);

      

        public async ValueTask DisposeAsync()
        {
            await _projectBlogContext.DisposeAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _projectBlogContext.SaveChangesAsync();
        }
    }
}
