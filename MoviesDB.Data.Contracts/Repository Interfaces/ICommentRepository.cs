﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using MoviesDB.Business.Entities;

namespace MoviesDB.Data.Contracts
{
    public interface ICommentRepository : IDataRepository<Comment>
    {
        IEnumerable<Comment> GetByPage(int page, int pageSize);
    }
}