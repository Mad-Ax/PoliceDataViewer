using PoliceDataViewer.Data;

namespace PoliceDataViewer.Tests.PoliceSummaryDataServiceTests
{
    public class GetCrimeSummariesTests
    {
        [Fact]
        public async Task Uses_HttpService()
        {
            // Arrange
            var httpService = A.Fake<IPoliceDataHttpService>();
            var summaryService = new PoliceSummaryDataService(httpService);

            var model = new SearchModel
            {
                Date = "2021-01",
                Latitude = 51.5,
                Longitude = -2.1
            };

            // Act
            await summaryService.GetCrimeSummaries(model);

            // Assert
            A.CallTo(() => httpService.GetCrimeRecords(A<string>._, A<double>._, A<double>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => httpService.GetCrimeRecords(model.Date, model.Latitude, model.Longitude)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Returns_Summarised_Data()
        {
            // Arrange
            var httpService = A.Fake<IPoliceDataHttpService>();
            var summaryService = new PoliceSummaryDataService(httpService);

            var model = new SearchModel();

            var crimeRecords = new List<CrimeRecord>();
            crimeRecords.Add(new CrimeRecord { Category = "ASBO", Id = 101 });
            crimeRecords.Add(new CrimeRecord { Category = "Burglary", Id = 102 });
            crimeRecords.Add(new CrimeRecord { Category = "Violent crime", Id = 103 });
            crimeRecords.Add(new CrimeRecord { Category = "Loitering with intent", Id = 104 });
            crimeRecords.Add(new CrimeRecord { Category = "Burglary", Id = 105 });
            crimeRecords.Add(new CrimeRecord { Category = "ASBO", Id = 106 });
            crimeRecords.Add(new CrimeRecord { Category = "ASBO", Id = 107 });
            crimeRecords.Add(new CrimeRecord { Category = "Violent crime", Id = 108 });
            crimeRecords.Add(new CrimeRecord { Category = "Loitering with intent", Id = 109 });
            crimeRecords.Add(new CrimeRecord { Category = "Cracking an egg at the sharp end", Id = 110 });

            A.CallTo(() => httpService.GetCrimeRecords(A<string>._, A<double>._, A<double>._))
                .Returns(crimeRecords);

            // Act
            var results = await summaryService.GetCrimeSummaries(model);

            // Assert
            Assert.NotNull(results);
            Assert.Equal(5, results.Count());

            Assert.Contains(results, r => r.Category == "ASBO" && r.CrimeCount == 3);
            Assert.Contains(results, r => r.Category == "Burglary" && r.CrimeCount == 2);
            Assert.Contains(results, r => r.Category == "Violent crime" && r.CrimeCount == 2);
            Assert.Contains(results, r => r.Category == "Loitering with intent" && r.CrimeCount == 2);
            Assert.Contains(results, r => r.Category == "Cracking an egg at the sharp end" && r.CrimeCount == 1);
        }
    }
}
