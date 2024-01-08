using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace BankingData
{
    public class BankingDataModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public int TripId { get; set; }

        private readonly IConfiguration _configuration;

        public BankingDataModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
        }

		public IActionResult OnPostSubmitBankingDetails(int tripId)
		{
			string connectionString = _configuration.GetConnectionString("DefaultConnection");
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

				int selectedHotelId = Convert.ToInt32(TempData["SelectedHotelId"] ?? "0");

				string sql = "UPDATE Potovanje SET Rezervirano = 1 WHERE PotovanjeId = @PotovanjeId";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@PotovanjeId", tripId);
					command.ExecuteNonQuery();
				}
			}

			TempData["ReservationMessage"] = "Izlet uspešno rezerviran!";
			return RedirectToPage("Index");
		}



	}
}
