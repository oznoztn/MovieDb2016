using System;
using System.ComponentModel.Composition;
using Core.Common.Proxy;
using MoviesDB.Client.Contracts;
using MoviesDB.Client.Entities;

namespace MoviesDB.Client.Proxies
{
    [Export(typeof(INewsService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class NewsProxy : ProxyBase<INewsService>, INewsService
    {
        public News Get(int id)
        {
            return Channel.Get(id);
        }

        public News[] GetAll()
        {
            return Channel.GetAll();
        }

        public News Update(News entity)
        {
            return Channel.Update(entity);
        }

        public void Delete(int id)
        {
            Channel.Delete(id);
        }

        public News[] GetByPage(int page, int pageSize)
        {
            return Channel.GetByPage(page, pageSize);
        }

        public int TotalCount()
        {
            return Channel.TotalCount();
        }
    }
}
