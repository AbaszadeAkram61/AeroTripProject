using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.Dtos.Error
{
    public class ValidationErrorDto
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
