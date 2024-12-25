using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Celerate.Services.DataService
{
    public class DataService
    {
        private readonly string connectionString = "Data Source=192.168.1.5;Initial Catalog=sefim;User ID=sa;Password=123456a.A;Trust Server Certificate=True";

        public async Task<List<ChartData>> GetChartDataAsync()
        {
            var chartData = new List<ChartData>();

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                    SELECT ProductName, SUM(Price * Quantity) AS TotalSales
                    FROM Bill
                    GROUP BY ProductName
                    ORDER BY TotalSales DESC";

                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        chartData.Add(new ChartData
                        {
                            Text = reader["ProductName"].ToString(),
                            Value = (decimal)reader["TotalSales"]
                        });
                    }
                }
            }

            return chartData;
        }

        public async Task<decimal> GetTotalSalesAsync()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT SUM(Price * Quantity) FROM Bill";

                using (var command = new SqlCommand(query, connection))
                {
                    var result = await command.ExecuteScalarAsync();
                    return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                }
            }
        }
    }

    public class ChartData
    {
        public string Text { get; set; }
        public decimal Value { get; set; }
    }
}
