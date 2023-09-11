using AutoMapper;
using ProjectBlog.Data.Abstract;
using ProjectBlog.Entities.Concrete;
using ProjectBlog.Entities.Dtos;
using ProjectBlog.Services.Abstract;
using ProjectBlog.Shared.Utilities.Results.Abstract;
using ProjectBlog.Shared.Utilities.Results.ComplexTypes;
using ProjectBlog.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBlog.Services.Concrete
{
    public class ArticleManager : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ArticleManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public async Task<IDataResult<ArticleDto>> Get(int articleId)
        {
            var article=await _unitOfWork.Articles.GetAsync(a=>a.Id == articleId,a=>a.User,a=>a.Category);
            if (article != null)
            {
                return new DataResult<ArticleDto>(ResultStatus.Success, new ArticleDto
                {
                    Article=article,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleDto>(ResultStatus.Error, "Makale bulunamadi", null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAll()
        {
            var articles=await _unitOfWork.Articles.GetAllAsync(null,a=>a.User,a=>a.Category);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                {
                    Articles=articles,
                    ResultStatus=ResultStatus.Success
                });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, "Makaleler bulunamadi", null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDeleted()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(a => !a.IsDeleted, ar => ar.User, ar => ar.Category);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, "Makaleler bulunamadi", null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAndActive()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(a => !a.IsDeleted && a.IsActive, ar => ar.User, ar => ar.Category);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, "Makaleler bulunamadi", null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByCategory(int categoryId)
        {
            var result = await _unitOfWork.Categories.AnyAsync(a => a.Id == categoryId);
            if (result)
            {
                var articles = await _unitOfWork.Articles.GetAllAsync(a => a.CategoryId == categoryId && !a.IsDeleted && a.IsActive, a => a.User, a => a.Category);
                if (articles.Count > -1)
                {
                    return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                    {
                        Articles = articles,
                        ResultStatus = ResultStatus.Success
                    });
                }
                return new DataResult<ArticleListDto>(ResultStatus.Error, "Makaleler bulunamadi", null);
            }
            else
            {
                return new DataResult<ArticleListDto>(ResultStatus.Error, "Boyle bir kategori bulunamadi", null);
            }
           
        }

        public async Task<IResult> Add(ArticleAddDto articleAddDto, string createdByName)
        {
            var article=_mapper.Map<Article>(articleAddDto);
            article.CreatedByName=createdByName;
            article.ModifiedByName = createdByName;
            article.UserId = 1;

            await _unitOfWork.Articles.AddAsync(article).ContinueWith(t=>_unitOfWork.SaveAsync());
            return new Result(ResultStatus.Success, $"{articleAddDto.Title} adli makale basariyla eklenmistir");


        }
        public async Task<IResult> Update(ArticleUpdateDto articleUpdateDto, string modifiedByName)
        {
            var article = _mapper.Map<Article>(articleUpdateDto);
            article.ModifiedByName=modifiedByName;
            await _unitOfWork.Articles.UpdadeAsync(article).ContinueWith(t => _unitOfWork.SaveAsync());
            return new Result(ResultStatus.Success, $"{articleUpdateDto.Title} adli makale basariyla guncellenmistir");

        }
        public async Task<IResult> Delete(int articleId, string modifiedByName)
        {
            var result = await _unitOfWork.Articles.AnyAsync(a => a.Id == articleId);
            if (result)
            {
                var article=await _unitOfWork.Articles.GetAsync(a=>a.Id== articleId);
                article.IsDeleted = true;
                article.ModifiedByName=modifiedByName;
                article.ModifiedDate = DateTime.Now;
                await _unitOfWork.Articles.UpdadeAsync(article).ContinueWith(t => _unitOfWork.SaveAsync());
                return new Result(ResultStatus.Success, $"{article.Title} adli makale basariyla silinmistir");
            }
            return new Result(ResultStatus.Error," Boyle bir makale bulunamamistir.");

        }
        public async Task<IResult> HardDelete(int articleId)
        {
            var result = await _unitOfWork.Articles.AnyAsync(a => a.Id == articleId);
            if (result)
            {
                var article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId);
                await _unitOfWork.Articles.DeleteAsync(article).ContinueWith(t => _unitOfWork.SaveAsync());
                return new Result(ResultStatus.Success, $"{article.Title} adli makale basariyla silinmistir");
            }
            return new Result(ResultStatus.Error, " Boyle bir makale bulunamamistir.");
        }

       
    }
}
