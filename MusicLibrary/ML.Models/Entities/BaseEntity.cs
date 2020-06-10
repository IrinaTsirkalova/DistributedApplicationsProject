using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ML.Models.Entities
{
    public class BaseEntity
    {
        [Required]
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }// ? Въпросителният знак позволява стойността да е null 
    }
}
