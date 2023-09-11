using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBlog.Shared.Utilities.Results.Abstract
{
    public interface IDataResult<out T>:IResult
    {
        public T Data { get; }
        //new DataResult<Category>(ResultStatus.Succes,category);
        //new DataResult<IList<Category>>(ResultStatus.Success,category);

    }
}
