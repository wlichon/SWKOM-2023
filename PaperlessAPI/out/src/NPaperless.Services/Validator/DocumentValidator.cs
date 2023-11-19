using FluentValidation;
using NPaperless.Services.Models;
using System;

namespace NPaperless.Services.Validator
{
    public class DocumentValidator : AbstractValidator<Document>
    {
        public DocumentValidator()
        {
            RuleFor(x => x.Added).NotEqual(DateTime.MinValue);
        }
    }
}
