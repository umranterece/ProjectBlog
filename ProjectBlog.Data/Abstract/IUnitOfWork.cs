﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBlog.Data.Abstract
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        IArticleRepository Articles { get; }
        ICategoryRepository Categories { get; }
        ICommentRepository Comments { get; }
        IRoleRepository Roles { get; }
        IUserRepository Users { get; }
        //_unitOfWork.Categories.AddAsync(category);
        //_unitOfWork.Users.AddAsync(user);
        //_unitOfWork.SaveAsync();
        Task<int> SaveAsync();
    }
}
