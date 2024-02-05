namespace PoliceDataViewer.Data
{
    public interface IPoliceDataHttpService
    {
        Task<IEnumerable<CrimeRecord>> GetCrimeRecords(string date, double latitude, double longitude);
    }
}
