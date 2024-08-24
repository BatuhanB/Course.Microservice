using Course.Shared.Dtos;
using MediatR;

namespace Application.Abstractions.Messaging;

public interface ICommandHandler<in TCommand>
    : IRequestHandler<TCommand, Response<object>>
    where TCommand : ICommand;

public interface ICommandHandler<in TCommand, TResponse>
    : IRequestHandler<TCommand, Response<TResponse>>
    where TCommand : ICommand<TResponse>;
