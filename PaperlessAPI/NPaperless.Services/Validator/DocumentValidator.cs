using FluentValidation;
using NPaperless.Services.Models;
using System;

namespace NPaperless.Services.Validator
{
    public class DocumentTitleValidator : AbstractValidator<Document>
    {
        public DocumentTitleValidator()
        {
            RuleFor(x => x.title).NotEmpty();
            RuleFor(x => x.title).MaximumLength(20);
            RuleFor(x => x.title).Matches("[A-Za-z]");
        }
    }

    public class DocumentCreatedDateValidator : AbstractValidator<Document>
    {
        public DocumentCreatedDateValidator()
        {
            RuleFor(x => x.created_date).NotEmpty();
            RuleFor(x => x.created_date).LessThan(DateTime.Now);
        }
    }

    public class DocumentValidator : AbstractValidator<Document>
    {
        public DocumentValidator()
        {
            Include(new DocumentTitleValidator());
            Include(new DocumentCreatedDateValidator());
        }
    }
}
