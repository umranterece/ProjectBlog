using Microsoft.Extensions.DependencyInjection;
using ProjectBlog.Data.Abstract;
using ProjectBlog.Data.Concrete;
using ProjectBlog.Data.Concrete.EntityFramework.Contexts;
using ProjectBlog.Services.Abstract;
using ProjectBlog.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBlog.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices (this IServiceCollection services)
        {
            services.AddDbContext<ProjectBlogContext>();
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IArticleService, ArticleManager>();
            return services;

        }
    }
}
