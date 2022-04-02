using Dtos.WorkDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
   public  class WorkCreateDtoValidator:AbstractValidator<WorkCreateDto>
    {
        public WorkCreateDtoValidator()
        {
            //RuleFor(x => x.Definition).NotEmpty().WithMessage("Definition gerekli(FluentValidationdan geliyor.)").Must(NotBeBerkay).WithMessage("Berkay ismi babaya ait kullanma!");
            RuleFor(x => x.Definition).NotEmpty();
        }


        //
        //private bool NotBeBerkay(string arg)
        //{
        //    return arg != "Berkay" & arg != "berkay";
        //}
    }
}
