using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PracticeRound1.Models
{
    public class CoursePatchVM
    {
        [Required]
        public int CourseId { get; set; }

        [Required]
        [MaxLength(5)]
        public string Title { get; set; }
    }
}
