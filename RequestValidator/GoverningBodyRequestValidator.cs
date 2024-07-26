using Api.DTO;
using FluentValidation;
using TrafficDataRequest;

namespace RoadTrafficManagement.RequestValidator
{
    public class GoverningBodyRequestValidator : AbstractValidator<GoverningBodyRequest>
    {
        public GoverningBodyRequestValidator()
        {
            RuleFor(x => x.GoverningCode)
                .NotEmpty().WithMessage("Mã nhân sự là bắt buộc")
                .Length(3, 60).WithMessage("Độ dài ký tự Mã nhân sự phải trong khoảng 3 đến 60.");
        }
    }
}
