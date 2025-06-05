using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.CQRS
{
    public interface IQueryHandler<in TQuery, TRespone> : IRequestHandler<TQuery, TRespone>
        where TQuery : IQuery<TRespone>
        where TRespone : notnull
    {
    }
}
