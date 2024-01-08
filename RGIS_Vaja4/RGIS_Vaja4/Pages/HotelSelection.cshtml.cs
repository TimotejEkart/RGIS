using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace RGIS_Vaja4.Pages
{
    public class HotelSelectionModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public List<Hotel> Hotels { get; set; }
        public int TripId { get; set; }

        public HotelSelectionModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet(int tripId)
        {
            TripId = tripId;

            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM ListHotelov WHERE Kraj = (SELECT Kraj FROM Potovanje WHERE PotovanjeId = @TripId)";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@TripId", tripId);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var allHotels = new List<Hotel>();
                    while (reader.Read())
                    {
                        allHotels.Add(new Hotel()
                        {
                            HoteliId = reader.GetInt32(0),
                            Ime = reader.GetString(1),
                            Kraj = reader.GetString(2),
                            Cena = reader.GetInt32(3)
                        });
                    }

                    Hotels = allHotels.ToList();
                }
            }
        }

		public IActionResult OnPost(int hotelId, int tripId)
		{
			TempData["SelectedHotelId"] = hotelId;
			TempData["SelectedTripId"] = tripId;

			return RedirectToPage("/BankingData", new { tripId = tripId });
		}


	}
}