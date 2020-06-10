using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ML.Business.DTOs
{
    public class BaseDto
    {
        [Required] 
        public int Id { get; set; }
    }
}
