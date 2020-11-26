using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    [Table("EvernoteUsers")]
    public class EvernoteUser:MyEntityBase
    {
        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(20)]
        public string SurName { get; set; }
        [Required, StringLength(20)]
        public string UserName { get; set; }
        [Required, StringLength(70)]
        public string Email { get; set; }
        [Required, StringLength(20)]
        public string Password { get; set; }
        [StringLength(30)] //user_10.jpeg mesala yani
        public string ProfileImageFilename { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        [Required]
        public Guid ActivateGuid { get; set; }
        public virtual List<Note> Notes { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Liked> Likes { get; set; }

    }
}
