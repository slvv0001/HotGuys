using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotGuys.Models
{
    public class HotChoiceViewModels
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

       
        public string Image { get; set; }



        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Comments> Comments { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}