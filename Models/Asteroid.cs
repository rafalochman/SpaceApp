namespace NASAapp.Models
{
    public class Asteroid
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal EstimatedDiameterMin { get; set; }
        public decimal EstimatedDiameterMax { get; set; }
        public string Hazardous { get; set; }
        public string Time { get; set; }
        public decimal RelativeVelocity { get; set; }
        public int MissDistance { get; set; }
        public string Date { get; set; }
    }
}
