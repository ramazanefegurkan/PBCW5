using Para.Data.Domain;
using Para.Schema.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Para.Data
{
    public interface IReportRepository
    {
        Task<List<CustomerCountByCountry>> GetCustomersCountByCountryReport();
    }
}
