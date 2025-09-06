using UserManagement.Domain.Enums;
using UserManagement.Domain.Specifications;
using UserManagement.Unit.Domain.Specifications.TestData;
using FluentAssertions;
using Xunit;

namespace UserManagement.Unit.Domain.Specifications
{
    public class ActiveUserSpecificationTests
    {
        [Theory]
        [InlineData(UserStatus.Active, true)]
        [InlineData(UserStatus.Inactive, false)]
        [InlineData(UserStatus.Suspended, false)]
        public void Predicate_ShouldValidateUserStatus(UserStatus status, bool expectedResult)
        {
            // Arrange
            var user = ActiveUserSpecificationTestData.GenerateUser(status);
            var specification = new ActiveUserSpecification(user.Email);

            // Act
            var predicate = specification.Predicate.Compile();
            var result = predicate.Invoke(user);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
