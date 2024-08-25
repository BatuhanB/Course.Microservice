using Course.Shared.Dtos;
using MediatR;

namespace Course.Invoice.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Response<TResponse>>;
