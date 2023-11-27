using FluentValidation;
using NPaperless.Services.Models;
using System;

namespace NPaperless.Services.Validator
{
    public class DocumentTitleValidator : AbstractValidator<Document>
    {
        public DocumentTitleValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Title).MaximumLength(20);
            RuleFor(x => x.Title).Matches("[A-Za-z]");
        }
    }

    public class DocumentValidator : AbstractValidator<Document>
    {
        public DocumentValidator()
        {
            Include(new DocumentTitleValidator());
        }
    }
}
