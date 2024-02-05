namespace PoliceDataViewer.Data
{
    public class PoliceDataHttpService : IPoliceDataHttpService
    {
        private readonly IHttpClientFactory _factory;

        public PoliceDataHttpService(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<CrimeRecord>> GetCrimeRecords(string date, double latitude, double longitude)
        {
            var client = _factory.CreateClient();

            client.BaseAddress = new Uri("https://data.police.uk");
            var crimeRecords = await client.GetFromJsonAsync<IEnumerable<CrimeRecord>>($"api/crimes-street/all-crime?lat={latitude}&lng={longitude}&date={date}");

            return crimeRecords ?? new List<CrimeRecord>();
        }
    }
}
