using UserManagement.Application.Common.Security;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace UserManagement.Application.Users.UpdateUser;

/// <summary>
/// Handler for processing UpdateUserCommand requests
/// </summary>
/// /// <param name="_repository">The user repository</param>
/// <param name="_mapper">The AutoMapper instance</param>
/// <param name="_passwordHasher">The validator for CreateUserCommand</param>
public class UpdateUserHandler(
    IUserRepository _repository,
    IMapper _mapper,
    IPasswordHasher _passwordHasher) : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
{
    /// <summary>
    /// Handles the UpdateUserCommand request
    /// </summary>
    /// <param name="command">The UpdateUser command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated user details</returns>
    public async Task<UpdateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"User with ID {request.Id} not found");

        var user = _mapper.Map<User>(request);
        user.Password = _passwordHasher.HashPassword(request.Password);
        existingUser.Update(user);

        var updatedUser = await _repository.UpdateAsync(existingUser, cancellationToken);
        return _mapper.Map<UpdateUserResponse>(updatedUser);
    }
}