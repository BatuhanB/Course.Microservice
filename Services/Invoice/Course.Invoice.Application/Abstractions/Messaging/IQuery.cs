using Course.Shared.Dtos;
using MediatR;

namespace Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Response<TResponse>>;
