using FluentValidation;
using System;

namespace PypTask.Dtos
{
    public class SendFilterDto
    {
     

            public DateTime StartData { get; set; }
            public DateTime EndData { get; set; }
            public string AcceptorEmail { get; set; }
        }

        public class SendFilterDtoValidator : AbstractValidator<SendFilterDto>
        {
            public SendFilterDtoValidator()
            {
                RuleFor(x => x.StartData).NotEmpty().WithMessage("Can not be empty");
                RuleFor(x => x.AcceptorEmail).NotEmpty().WithMessage("Email address is required")
                    .EmailAddress().WithMessage("Valid emails only");
                RuleFor(x => x.EndData).NotEmpty().WithMessage("Can not be empty");
                RuleFor(x => x).Custom((x, context) => {
                    string[] arr = x.AcceptorEmail.Split("@");
                    if (arr[1].Trim().ToLower() != "code.edu.az") context.AddFailure("AcceptorEmail", "only code.edu.az emails");
                });
                RuleFor(x => x).Custom((x, context) =>
                {
                    double time = (x.EndData - x.StartData).TotalMilliseconds;
                    if (time < 0) context.AddFailure("EndData", "Wrong data");
                });
            }
        }
        public enum SendType
        {
            Segment = 1,
            Country,
            Product,
            Discounts
        }

    }

