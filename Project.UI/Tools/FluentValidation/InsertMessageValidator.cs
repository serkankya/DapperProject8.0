using FluentValidation;
using Project.Shared.DTOs.MessageDtos;

namespace Project.UI.Tools.FluentValidation
{
	public class InsertMessageValidator : AbstractValidator<InsertMessageDto>
	{
        public InsertMessageValidator()
        {
			RuleFor(x => x.Name)
			.NotEmpty().WithMessage("İsim alanı boş olamaz")
			.MinimumLength(3).WithMessage("İsim en az 3 karakter içermek zorundadır.")
			.MaximumLength(75).WithMessage("İsim en fazla 75 karakter olabilir.");

			RuleFor(x => x.Surname)
				.NotEmpty().WithMessage("Soyad alanı boş olamaz")
				.MinimumLength(3).WithMessage("Soyisim en az 3 karakter içermek zorundadır.")
				.MaximumLength(75).WithMessage("Soyad en fazla 75 karakter olabilir.");

			RuleFor(x => x.Email)
				.NotEmpty().WithMessage("Email alanı boş olamaz")
				.MaximumLength(75).WithMessage("Email en fazla 75 karakter olabilir.")
				.EmailAddress().WithMessage("Geçerli bir email adresi girin.");

			RuleFor(x => x.Subject)
				.MinimumLength(5).WithMessage("Konu en az 5 karakter içermek zorundadır.")
				.MaximumLength(200).WithMessage("Konu en fazla 200 karakter olabilir.")
				.NotEmpty().WithMessage("Konu alanı boş olamaz");

			RuleFor(x => x.Message)
				.MinimumLength(10).WithMessage("Mesaj en az 10 karakter içermek zorundadır.")
				.MaximumLength(350).WithMessage("Mesajınız en fazla 350 karakter olabilir.")
				.NotEmpty().WithMessage("Mesaj alanı boş olamaz");
		}
	}
}
