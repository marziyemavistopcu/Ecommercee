using Ecommerce.Model;
using FluentValidation;


namespace EcommerceWebsite.Code.Validators
{
    public class UserValidator : AbstractValidator<Users>
    {
        public UserValidator()
        {
            RuleFor(u => u.username).NotEmpty().WithMessage("Kullanıcı adı boş geçilemez.");
            RuleFor(u => u.password).NotEmpty().WithMessage("Parola boş geçilemez.");
            RuleFor(u => u.email).NotEmpty().WithMessage("Email boş geçilemez.");
            RuleFor(u => u.first_name).NotEmpty().WithMessage("İsim boş geçilemez.");
            RuleFor(u => u.username).EmailAddress().WithMessage("Hatalı Email adresi.");
            RuleFor(u => u.password).Length(6, 15).WithMessage("Parola en az 6, en fazla 15 karakterden oluşabilir.");
            RuleFor(u => u.password).Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                                .Matches(@"[!?*.]+").WithMessage("Your password must contain at least one (!? *.).");
            
        }
    }  
}
