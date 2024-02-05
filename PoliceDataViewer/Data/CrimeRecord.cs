namespace PoliceDataViewer.Data
{
    public record class CrimeRecord
    {
        public string Category { get; set; } = string.Empty;

        public int Id { get; set; }
    }
}
