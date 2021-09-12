using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace GrpcServer
{
    public class CovidStatsService : CovidStats.CovidStatsBase
    {
        private readonly ILogger<CovidStatsService> _logger;
        private readonly CovidStatsDatabaseService _db;

        public CovidStatsService(ILogger<CovidStatsService> logger, CovidStatsDatabaseService db)
        {
            _logger = logger;
            _db = db;
        }

        public override async Task GetAll(EmptyResponse request, IServerStreamWriter<StatEntry> responseStream, ServerCallContext context)
        {
            foreach (var covidStatsEntry in _db.Entries)
            {
                responseStream.WriteAsync(new StatEntry()
                {
                    Cases = covidStatsEntry.cases,
                    Day = covidStatsEntry.day,
                    Deaths = covidStatsEntry.deaths,
                    Month = covidStatsEntry.month,
                    Year = covidStatsEntry.year,
                    DateRep = covidStatsEntry.dateRep,
                    PopData2020 = covidStatsEntry.popData2020,
                    CountriesAndTerritories = covidStatsEntry.countryterritoryCode,
                    ContinentExp = covidStatsEntry.continentExp,
                    CountryterritoryCode = covidStatsEntry.countryterritoryCode,
                    GeoId = covidStatsEntry.geoId
                });
            }
        }

        public override async Task<StatEntryList> GetAllAsList(EmptyResponse request, ServerCallContext context)
        {
            return _db.GrpcEntries;
        }
    }
}