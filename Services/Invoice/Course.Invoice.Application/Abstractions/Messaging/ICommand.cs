using Course.Shared.Dtos;
using MediatR;

namespace Course.Invoice.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Response<object>>, IBaseCommand;

public interface ICommand<TResponse> : IRequest<Response<TResponse>>, IBaseCommand;

public interface IBaseCommand;
