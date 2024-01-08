using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace RGIS_Vaja4.Pages
{
	public class PregledPotovanjModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly IConfiguration _configuration;
		public List<Potovanje> Potovanja { get; set; }
		[BindProperty]
		public Odgovori NoviOdgovori { get; set; }
		public PregledPotovanjModel(ILogger<IndexModel> logger, IConfiguration configuration)
		{
			_logger = logger;
			_configuration = configuration;
		}

		public IActionResult OnPost()
		{
			int potovanjeId = int.Parse(Request.Form["PotovanjeId"]);

			string connectionString = _configuration.GetConnectionString("DefaultConnection");
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

				string sql = "UPDATE Potovanje SET Rezervirano = 0 WHERE PotovanjeId = @PotovanjeId";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@PotovanjeId", potovanjeId);
					command.ExecuteNonQuery();
				}
			}

			return RedirectToPage();
		}

		public void OnGet()
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
			}
		}

        public IActionResult OnPostSubmitOdgovori()
        {
            int potovanjeId = int.Parse(Request.Form["PotovanjeId"]);

            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = @"
            INSERT INTO Odgovori 
            (potovanjeId, Odgovor1, Odgovor2, Odgovor3, Odgovor4, Odgovor5, Odgovor6, Odgovor7, Odgovor8, Odgovor9, Odgovor10, Odgovor11, Odgovor12, Odgovor13, Odgovor14, Odgovor15)
            VALUES 
            (@PotovanjeId, @Odgovor1, @Odgovor2, @Odgovor3, @Odgovor4, @Odgovor5, @Odgovor6, @Odgovor7, @Odgovor8, @Odgovor9, @Odgovor10, @Odgovor11, @Odgovor12, @Odgovor13, @Odgovor14, @Odgovor15)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@PotovanjeId", potovanjeId);
                    command.Parameters.AddWithValue("@Odgovor1", Request.Form["Odgovor1"].ToString());
                    command.Parameters.AddWithValue("@Odgovor2", Request.Form["Odgovor2"].ToString());
                    command.Parameters.AddWithValue("@Odgovor3", Request.Form["Odgovor3"].ToString());
                    command.Parameters.AddWithValue("@Odgovor4", Request.Form["Odgovor4"].ToString());
                    command.Parameters.AddWithValue("@Odgovor5", Request.Form["Odgovor5"].ToString());
                    command.Parameters.AddWithValue("@Odgovor6", Request.Form["Odgovor6"].ToString());
                    command.Parameters.AddWithValue("@Odgovor7", Request.Form["Odgovor7"].ToString());
                    command.Parameters.AddWithValue("@Odgovor8", Request.Form["Odgovor8"].ToString());
                    command.Parameters.AddWithValue("@Odgovor9", Request.Form["Odgovor9"].ToString());
                    command.Parameters.AddWithValue("@Odgovor10", Request.Form["Odgovor10"].ToString());
                    command.Parameters.AddWithValue("@Odgovor11", Request.Form["Odgovor11"].ToString());
                    command.Parameters.AddWithValue("@Odgovor12", Request.Form["Odgovor12"].ToString());
                    command.Parameters.AddWithValue("@Odgovor13", Request.Form["Odgovor13"].ToString());
                    command.Parameters.AddWithValue("@Odgovor14", Request.Form["Odgovor14"].ToString());
                    command.Parameters.AddWithValue("@Odgovor15", Request.Form["Odgovor15"].ToString());
                    command.ExecuteNonQuery();
                }

            }

            return RedirectToPage();
        }


    }
}