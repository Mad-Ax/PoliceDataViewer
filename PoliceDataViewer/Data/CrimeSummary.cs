namespace PoliceDataViewer.Data
{
    public record class CrimeSummary
    {
        public string Category { get; set; } = string.Empty;

        public int CrimeCount { get; set; }
    }
}
