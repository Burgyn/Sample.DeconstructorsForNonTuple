using System;
using FluentAssertions;
using Xunit;

namespace Sample.DeconstructorsForNonTuple
{
    public class PersonShould
    {
        [Fact]
        public void BeDeconstructedToTwoVariables()
        {
            var person = new Person()
            {
                FirstName = "Janko",
                LastName = "Hraško"
            };

            var (firstName, lastName) = person;

            firstName.Should().Be("Janko");
            lastName.Should().Be("Hraško");
        }

        [Fact]
        public void BeDeconstructedTreeVariables()
        {
            var person = new Person()
            {
                FirstName = "Janko",
                LastName = "Hraško",
                Age = 25
            };

            (string firstName, string lastName, var age) = person;

            firstName.Should().Be("Janko");
            lastName.Should().Be("Hraško");
            age.Should().Be(25);
        }
    }
}