using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSecurity.Models
{
    [Table("Comments")]
    public class Comments
    {
        [Key]
        public int CommentId { get; set; }

        [Required(ErrorMessage = "Enter EmailId")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Please enter valid Email Id ")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Enter Comment")]
        //[RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Enter Valid Comment")]
        public string Comment { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;
    }
}
