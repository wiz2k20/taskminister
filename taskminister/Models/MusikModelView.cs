using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace taskminister.Models
{
    public class MusikModelView
    {
        public int Control { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        
        [DisplayName("Artist")]
        public string Artist { get; set; }

        
        [DisplayName("Url")]
        public string Url { get; set; }

        
        [DisplayName("Cover")]
        public string Cover { get; set; }
    }
}