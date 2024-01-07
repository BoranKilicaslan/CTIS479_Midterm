#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Grade
    {

        public int Id { get; set; }
        [Required]
        [StringLength(11)]
        public string year { get; set; }

        public List<Student> Students { get; set; }
    }
}
