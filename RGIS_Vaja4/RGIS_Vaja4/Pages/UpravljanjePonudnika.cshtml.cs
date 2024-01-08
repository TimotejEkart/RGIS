using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace RGIS_Vaja4.Pages
{
    public class UpravljanjePonudnikaModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;
        public Administrator NovAdministrator { get; set; }
        public List<Ponudnik> Ponudniki { get; set; }
        public UpravljanjePonudnikaModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public IActionResult OnPostDelete(int PonudnikIdToDelete)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "DELETE FROM Ponudnik WHERE PonudnikId = @PonudnikId";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@PonudnikId", PonudnikIdToDelete);


                sql = "UPDATE Administrator SET SteviloOdstranjenih = SteviloOdstranjenih + 1 WHERE administratorId = 1";
                SqlCommand command1 = new SqlCommand(sql, connection);

                connection.Open();
                command.ExecuteNonQuery();
                command1.ExecuteNonQuery();
            }

            return RedirectToPage();
        }
        public void OnGet()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Ponudnik";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Ponudniki = new List<Ponudnik>();
                    while (reader.Read())
                    {
                        Ponudniki.Add(new Ponudnik()
                        {
                            PonudnikId = reader.GetInt32(0),
                            Ime = reader.GetString(3),
                            Priimek = reader.GetString(4),
                            Starost = reader.GetInt32(5),
                            LetoRojstva = reader.GetInt32(6),
                            Naslov = reader.GetString(7),
                            Kraj = reader.GetString(8),
                            Verodostojnost = reader.GetBoolean(reader.GetOrdinal("Verodostojnost")),
                        });
                    }
                }
            }
        }
    }
}
