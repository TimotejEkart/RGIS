using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace RGIS_Vaja4.Pages
{
    public class DodajanjePotovanjaModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public Potovanje NovoPotovanje { get; set; }
        public Koledar NovKoledar { get; set; }
		public string Message { get; set; }


		public DodajanjePotovanjaModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet(bool success = false)
        {
            if (success)
            {
                Message = "Izlet uspešno dodan na seznam izletov!";
            }
        }


        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = "INSERT INTO Potovanje (Kraj, Cena, Trajanje, Datum, Opis, Ocene) VALUES (@Kraj, @Cena, @Trajanje, @Datum, @Opis, @Ocene)";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@Kraj", NovoPotovanje.Kraj);
                    command.Parameters.AddWithValue("@Cena", NovoPotovanje.Cena);
                    command.Parameters.AddWithValue("@Trajanje", NovoPotovanje.Trajanje);
                    command.Parameters.AddWithValue("@Datum", NovoPotovanje.Datum);
                    command.Parameters.AddWithValue("@Opis", NovoPotovanje.Opis ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Ocene", NovoPotovanje.Ocene);
                    command.Parameters.AddWithValue("@Rezervirano", NovoPotovanje.Rezervirano);

                    DateTime datumKonec = NovoPotovanje.Datum.AddDays(NovoPotovanje.Trajanje);

                    sql = "INSERT INTO Koledar(Datum, DatumKonec) VALUES (@Datum, @DatumKonec)";
                    SqlCommand command1 = new SqlCommand(sql, connection);
                    command1.Parameters.AddWithValue("@Datum", NovoPotovanje.Datum);
                    command1.Parameters.AddWithValue("@DatumKonec", datumKonec);


                    connection.Open();
                    command.ExecuteNonQuery();
                    command1.ExecuteNonQuery();

					Message = "Izlet uspešno dodan na seznam izletov!";
					return RedirectToPage(new { success = true });
				}

            }

            return Page();
        }
    }
}

