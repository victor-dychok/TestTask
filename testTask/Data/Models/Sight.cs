using System.Web.Mvc;

namespace testTask.Data.Models
{
    public class Sight
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public virtual Location Location { get; set; }

        /*[AllowHtml]
        public string? Contents { get; set; }
        public byte[]? Img { get; set; }*/
    }
}
