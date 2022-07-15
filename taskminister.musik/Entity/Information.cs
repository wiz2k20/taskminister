using System.ComponentModel;


namespace taskminister.musik.Entity
{
    public class Information
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
