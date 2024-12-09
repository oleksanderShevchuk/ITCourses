using Dapper;
using ITCoursesWeb.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ITCoursesWeb.DataAccess
{
    public class SqPromoCode
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public SqPromoCode(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new SqlConnection(_configuration.GetConnectionString("AppDbContextConnection"));
        }

        public IEnumerable<PromoCode> GetPromoCodesByCourseId(string courseId)
        {
            string query = "SELECT * FROM PromoCodes WHERE CourseId = @CourseId";
            return _dbConnection.Query<PromoCode>(query, new { CourseId = courseId });
        }

        public PromoCode GetPromoCodeById(string id)
        {
            string query = "SELECT * FROM PromoCodes WHERE Id = @Id";
            return _dbConnection.QueryFirstOrDefault<PromoCode>(query, new { Id = id });
        }

        public void AddPromoCode(PromoCode promoCode)
        {
            string query = "INSERT INTO PromoCodes (Id, Code, CourseId, DateTo, IsUsed, Percent) VALUES (@Id, @Code, @CourseId, @DateTo, @IsUsed, @Percent)";
            _dbConnection.Execute(query, promoCode);
        }

        public void UpdatePromoCode(PromoCode promoCode)
        {
            string query = "UPDATE PromoCodes SET IsUsed = @IsUsed, Percent = @Percent, DateTo = @DateTo, PersonId = @PersonId WHERE Id = @Id";
            _dbConnection.Execute(query, promoCode);
        }

        public void DeletePromoCode(string id)
        {
            string query = "DELETE FROM PromoCodes WHERE Id = @Id";
            _dbConnection.Execute(query, new { Id = id });
        }

        public bool PromoCodeExists(string code)
        {
            string query = "SELECT COUNT(1) FROM PromoCodes WHERE Code = @Code";
            return _dbConnection.ExecuteScalar<bool>(query, new { Code = code });
        }
    }
}
