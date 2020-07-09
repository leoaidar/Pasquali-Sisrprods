using System;
using System.Linq.Expressions;
using Pasquali.Sisprods.Domain.Entities;

namespace Pasquali.Sisprods.Domain.Queries
{
    public static class ProductQueries
    {
        public static Expression<Func<Product, bool>> GetById(int id)
        {
            return x => x.ProductId == id;
        }

    }
}