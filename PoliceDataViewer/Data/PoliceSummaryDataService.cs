namespace PoliceDataViewer.Data
{
    using System.Linq;

    public class PoliceSummaryDataService
    {
        private readonly IPoliceDataHttpService _httpService;

        public PoliceSummaryDataService(IPoliceDataHttpService httpService)
        {
            this._httpService = httpService;
        }

        public async Task<IEnumerable<CrimeSummary>> GetCrimeSummaries(SearchModel search)
        {
            var data = await _httpService.GetCrimeRecords(search.Date, search.Latitude, search.Longitude);
            return data
                .GroupBy(d => d.Category)
                .Select(group => new CrimeSummary
                {
                    Category = group.Key,
                    CrimeCount = group.Count()
                });
        }
    }
}
