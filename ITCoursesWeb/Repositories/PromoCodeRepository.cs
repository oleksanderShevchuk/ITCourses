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
            if (string.IsNullOrWhiteSpace(courseId))
            {
                throw new ArgumentException("CourseId cannot be null or empty.", nameof(courseId));
            }

            if (!Guid.TryParse(courseId, out var courseGuid))
            {
                throw new ArgumentException("Invalid CourseId format. It must be a valid GUID.", nameof(courseId));
            }
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("GeneratePromoCodes", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CountPromoCodes", countPromoCodes);
                    command.Parameters.Add("@CourseId", SqlDbType.UniqueIdentifier).Value = Guid.Parse(courseId);
                    command.Parameters.AddWithValue("@DateTo", dateTo);
                    command.Parameters.AddWithValue("@Discount", discount);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
