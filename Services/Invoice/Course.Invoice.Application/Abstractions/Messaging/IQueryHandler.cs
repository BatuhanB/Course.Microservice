using Course.Shared.Dtos;
using MediatR;

namespace Course.Invoice.Application.Abstractions.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Response<TResponse>>
    where TQuery : IQuery<TResponse>;
