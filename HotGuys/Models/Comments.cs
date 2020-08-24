using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotGuys.Models
{
    public class Comments
    {
        public int Id { get; set; }

        public float Rating { get; set; }

        public string Comment { get; set; }
        [ForeignKey("HotChoice")]
        public int HotChoiceId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
        public HotChoiceViewModels HotChoice { get; set; }


    }
}