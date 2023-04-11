namespace testTask.Data.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Region { get; set; } //область
        public string District { get; set; } //район
        public string Locality { get; set; } //населенный пункт
        public string GeoPosition { get; set; }
    }
}
