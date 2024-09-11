using FluentValidation;
using Project.Shared.DTOs.CommentDtos;

namespace Project.UI.Tools.FluentValidation
{
	public class InsertCommentValidator : AbstractValidator<InsertCommentDto>
	{
		public InsertCommentValidator()
		{
			RuleFor(x => x.Comment)
				.NotEmpty().WithMessage("Yorum alanı boş olamaz.")
				.MinimumLength(3).WithMessage("Yorumunuz en az 3 karakter içermelidir.")
				.MaximumLength(180).WithMessage("Yorumunuz en fazla 180 karakter içermelidir.");

			RuleFor(x => x.UserName)
				.NotEmpty().WithMessage("Kullanıcı adı boş olamaz.")
				.MinimumLength(3).WithMessage("Kullanıcı adı en az 3 karakter içermelidir.")
				.MaximumLength(75).WithMessage("Kullanıcı adı en fazla 75 karakter içermelidir.");

			RuleFor(x => x.Email)
				.NotEmpty().WithMessage("Email alanı boş olamaz")
				.MaximumLength(75).WithMessage("Email en fazla 75 karakter olabilir.")
				.EmailAddress().WithMessage("Geçerli bir email adresi girin.");
		}
	}
}
