using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace RGIS_Vaja4.Pages
{
    public class OdstranjevanjePotovanjaModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;
        public List<Potovanje> Potovanja { get; set; }
        [BindProperty]
        public int PotovanjeIdToDelete { get; set; }

        public string Message { get; set; }

        public OdstranjevanjePotovanjaModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public IActionResult OnPostDelete()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "DELETE FROM Potovanje WHERE PotovanjeId = @PotovanjeId";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@PotovanjeId", PotovanjeIdToDelete);
                    command.ExecuteNonQuery();
                }

                Message = "Izlet uspešno odstranjen iz seznama izletov!";
                return RedirectToPage(new { success = true });
            }

            return RedirectToPage();
        }
        public void OnGet(bool success = false)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Potovanje";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Potovanja = new List<Potovanje>();
                    while (reader.Read())
                    {
                        Potovanja.Add(new Potovanje()
                        {
                            PotovanjeId = reader.GetInt32(0),
                            Kraj = reader.GetString(1),
                            Cena = reader.GetInt32(2),
                            Trajanje = reader.GetInt32(3),
                            Datum = reader.GetDateTime(4),
                            Opis = reader.IsDBNull(5) ? null : reader.GetString(5),
                            Ocene = reader.GetDouble(reader.GetOrdinal("Ocene")),
                            Rezervirano = reader.GetBoolean(reader.GetOrdinal("Rezervirano")),
                        });
                    }
                }

                if (success)
                {
                    Message = "Potovanje uspešno odstranjeno iz seznama!";
                }
            }
        }
    }
}
