namespace PoliceDataViewer.Data
{
    public class SearchModel
    {
        public string Date { get; set; } = string.Empty;

        public double Latitude { get; set; } = 51.319664;

        public double Longitude { get; set; } = -2.208853;

        public bool IsValid
        {
            get
            {
                // we would normally have much better validation here
                return !string.IsNullOrEmpty(Date);
            }
        }
    }
}
