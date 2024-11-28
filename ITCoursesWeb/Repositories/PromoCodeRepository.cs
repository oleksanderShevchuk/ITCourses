using Microsoft.Data.SqlClient;
using System.Data;

namespace ITCoursesWeb.Repositories
{
    public class PromoCodeRepository
    {
        private readonly string _connectionString;

        public PromoCodeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AppDbContextConnection");
        }

        public void GeneratePromoCodes(int countPromoCodes, string courseId, DateTime dateTo, int discount)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("GeneratePromoCodes", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CountPromoCodes", countPromoCodes);
                    command.Parameters.AddWithValue("@CourseId", courseId);
                    command.Parameters.AddWithValue("@DateTo", dateTo);
                    command.Parameters.AddWithValue("@Discount", discount);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
