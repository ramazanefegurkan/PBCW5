using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Para.Data.Domain;
using Para.Schema.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Data
{
    public class ReportRepository : IReportRepository
    {
        private readonly IConfiguration configuration;
        public ReportRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<List<CustomerCountByCountry>> GetCustomersCountByCountryReport()
        {
            var sql = @"SELECT Count(*) AS TotalCount, COALESCE(Country,'Bilinmiyor') AS Country FROM Customer LEFT JOIN CustomerAddress on Customer.Id = CustomerAddress.CustomerId GROUP BY Country";
            await using var connection = new SqlConnection(configuration.GetConnectionString("MsSqlConnection"));

            connection.Open();
            var result = await connection.QueryAsync<CustomerCountByCountry>(sql);
            return result.ToList();
        }
    }
}
