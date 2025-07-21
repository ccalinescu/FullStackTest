using FullStackTest.Api.Models;
using FluentValidation.TestHelper;


namespace FullStackTest.Api.Tests.UnitTests
{
    public class CreateTaskRequestValidatorTests
    {
        private readonly CreateTaskRequestValidator _validator = new();

        [Fact]
        public void Should_Have_Error_When_Name_Is_Null_Or_Empty()
        {
            var model = new CreateTaskRequest { Name = null };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);

            model.Name = "";
            result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Exceeds_Max_Length()
        {
            var model = new CreateTaskRequest { Name = new string('a', 11) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Name_Is_Valid()
        {
            var model = new CreateTaskRequest { Name = "ValidName" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }
    }

    public class UpdateTaskRequestValidatorTests
    {
        private readonly UpdateTaskRequestValidator _validator = new();

        [Fact]
        public void Should_Have_Error_When_Id_Is_Less_Than_Or_Equal_To_Zero()
        {
            var model = new UpdateTaskRequest { Id = 0, Name = "Valid" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Id);

            model.Id = -1;
            result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Id_Is_Greater_Than_Zero()
        {
            var model = new UpdateTaskRequest { Id = 1, Name = "Valid" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Null_Or_Empty()
        {
            var model = new UpdateTaskRequest { Id = 1, Name = null };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);

            model.Name = "";
            result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Exceeds_Max_Length()
        {
            var model = new UpdateTaskRequest { Id = 1, Name = new string('a', 11) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Name_Is_Valid()
        {
            var model = new UpdateTaskRequest { Id = 1, Name = "ValidName" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }
    }
}