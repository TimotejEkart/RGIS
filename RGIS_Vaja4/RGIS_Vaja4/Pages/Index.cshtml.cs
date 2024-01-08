using System.Data.SqlClient;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace RGIS_Vaja4.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly IConfiguration _configuration;
		public List<Potovanje> Potovanja { get; set; }

		public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
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

				string sql = "UPDATE Potovanje SET Rezervirano = 1 WHERE PotovanjeId = @PotovanjeId";
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
				string sql = "SELECT * FROM Potovanje ORDER BY Datum ASC";
				SqlCommand command = new SqlCommand(sql, connection);
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					var allPotovanja = new List<Potovanje>();
					while (reader.Read())
					{
						var datum = reader.GetDateTime(4);
						if (datum >= DateTime.Today)
						{
							allPotovanja.Add(new Potovanje()
							{
								PotovanjeId = reader.GetInt32(0),
								Kraj = reader.GetString(1),
								Cena = reader.GetInt32(2),
								Trajanje = reader.GetInt32(3),
								Datum = datum,
								Opis = reader.IsDBNull(5) ? null : reader.GetString(5),
								Ocene = reader.GetDouble(reader.GetOrdinal("Ocene")),
								Rezervirano = reader.GetBoolean(reader.GetOrdinal("Rezervirano")),
							});
						}
					}

					Potovanja = allPotovanja.ToList();
				}
			}
		}

		public IActionResult OnPostCancelReservation(int potovanjeId)
		{
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

		public IActionResult OnPostConfirmReservation(int tripId)
		{
			string connectionString = _configuration.GetConnectionString("DefaultConnection");
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

				string sql = "INSERT INTO Rezervacija (uporabnikId, potovanjeId) VALUES (1, @PotovanjeId)";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@PotovanjeId", tripId);
					command.ExecuteNonQuery();
				}
			}
			return new JsonResult(new { success = true });
		}
		public IActionResult OnPostReserveHotel(int tripId)
		{
			string connectionString = _configuration.GetConnectionString("DefaultConnection");

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

				string sql = "UPDATE Potovanje SET Rezervirano = 1 WHERE PotovanjeId = @PotovanjeId";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@PotovanjeId", tripId);
					command.ExecuteNonQuery();
				}
			}

			int userId = 1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

				string sql = "INSERT INTO Hoteli (rezervacijaId) VALUES (@RezervacijaId)";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@UporabnikId", userId);
					command.Parameters.AddWithValue("@RezervacijaId", tripId);
					command.ExecuteNonQuery();
				}
			}

			return new JsonResult(new { success = true });
		}

		public IActionResult OnPostReserveTripOnly(int tripId)
		{
			string connectionString = _configuration.GetConnectionString("DefaultConnection");

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				string sql = "UPDATE Potovanje SET Rezervirano = 1 WHERE PotovanjeId = @PotovanjeId";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@PotovanjeId", tripId);
					command.ExecuteNonQuery();
				}
			}
			return new JsonResult(new { success = true });
		}

	}

	public class Hotel
	{
		public int HoteliId { get; set; }
		public string Ime { get; set; }
		public string Kraj { get; set; }
		public int Cena { get; set; }
        public bool VeljavnostHotela()
        {
            return HoteliId > 0;
        }

        public string VrniHotel()
        {
            return $"{Ime} v kraju {Kraj}";
        }

        public string VrniCenoZEnoto()
        {
            return $"Cena: {Cena} na enoto";
        }
    }

	public class Potovanje
	{
		public int PotovanjeId { get; set; }
		public string Kraj { get; set; }
		public int Cena { get; set; }
		public int Trajanje { get; set; }
		public DateTime Datum { get; set; }
		public string Opis { get; set; }
		public double Ocene { get; set; }
		public bool Rezervirano { get; set; }
        public bool VeljavnostPotovanja()
        {
            return !Rezervirano && Datum.Date >= DateTime.Now.Date;
        }

        public string PridobiPodrobneInformacije()
        {
            string statusRezervacije = Rezervirano ? "rezervirano" : "na voljo";
            string datumString = Datum.ToString("dd.MM.yyyy");
            string ocenaString = Ocene > 0 ? Ocene.ToString("N1") : "Brez ocen";

            return $"Potovanje {PotovanjeId}: {Kraj}, Cena: {Cena}, Trajanje: {Trajanje} dni, Datum: {datumString}, Status: {statusRezervacije}, Ocene: {ocenaString}";
        }

        public void DodajOceno(double novaOcena)
        {
            if (novaOcena >= 1.0 && novaOcena <= 5.0)
            {
                Ocene = (Ocene + novaOcena) / 2;
            }
            else
            {
                throw new ArgumentException("Ocena mora biti med 1.0 in 5.0.");
            }
        }
    }
	public class Ponudnik
	{
		public int PonudnikId { get; set; }
		public string Ime { get; set; }
		public string Priimek { get; set; }
		public bool Verodostojnost { get; set; }
		public string Kraj { get; set; }
		public int Starost { get; set; }
		public int LetoRojstva { get; set; }
		public string Naslov { get; set; }

        public bool VerodostojnostPonudnika()
        {
            return Verodostojnost && Starost >= 18;
        }

        public string VrniPolnoImeInKraj()
        {
            return $"{Ime} {Priimek} iz kraja {Kraj}";
        }

        public int IzracunajStarost()
        {
            int trenutnoLeto = DateTime.Now.Year;
            return trenutnoLeto - LetoRojstva;
        }
    }
	public class Odgovori
	{
        public int odgovorId;

		public int potovanjeId { get; set; }
		public string Odgovor1 { get; set; }
		public string Odgovor2 { get; set; }
		public string Odgovor3 { get; set; }
		public string Odgovor4 { get; set; }
		public string Odgovor5 { get; set; }
		public string Odgovor6 { get; set; }
		public string Odgovor7 { get; set; }
		public string Odgovor8 { get; set; }
		public string Odgovor9 { get; set; }
		public string Odgovor10 { get; set; }
		public string Odgovor11 { get; set; }
		public string Odgovor12 { get; set; }
		public string Odgovor13 { get; set; }
		public string Odgovor14 { get; set; }
		public string Odgovor15 { get; set; }

		public bool VeljavnostOdgovorov()
		{
			return odgovorId > 0;
		}

        public int SteviloNepraznihOdgovorov()
        {
            int steviloNepraznih = 0;

            foreach (var property in typeof(Odgovori).GetProperties())
            {
                if (property.PropertyType == typeof(string))
                {
                    var value = (string)property.GetValue(this);
                    if (!string.IsNullOrEmpty(value))
                    {
                        steviloNepraznih++;
                    }
                }
            }

            return steviloNepraznih;
        }

        public bool AliOdgovorObstaja(string iskaniOdgovor)
        {
            foreach (var property in typeof(Odgovori).GetProperties())
            {
                if (property.PropertyType == typeof(string))
                {
                    var value = (string)property.GetValue(this);
                    if (value != null && value.Equals(iskaniOdgovor))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
	public class Administrator
	{
		public int administratorId { get; set; }
		public string Ime { get; set; }
		public string Priimek { get; set; }
		public int SteviloOdstranjenih {  get; set; }
        public bool VeljavnostAdministratorja()
        {
            return administratorId > 0;
        }

        public string VrniAdministratorja()
        {
            return $"{Ime} {Priimek}";
        }

        public void PovecajSteviloOdstranjenih()
        {
            SteviloOdstranjenih++;
        }
    }
	public class Koledar
	{
		public int koledarId { get; set; }
		public DateTime datum { get; set; }
		public DateTime datumKonec { get; set; }

        public bool VeljavnostID()
        {
            return koledarId > 0;
        }

        public bool VeljavnostKoncaDatuma()
        {
            return datumKonec > datum;
        }

        public bool VeljavnostDatuma()
        {
            return datum > DateTime.Now;
        }
    }
	public class SistemPlacilo
	{
		public int sistemPlaciloId { get; set; }
		public double Znesek { get; set; }
		public DateTime DatumPlacila { get; set; }
        public bool VeljavnostPlacila()
        {
            return sistemPlaciloId > 0;
        }

        public bool ZnesekPozitiven()
        {
            return Znesek > 0;
        }

        public bool VeljavnostDatumPlacila()
        {
            return DatumPlacila.Date == DateTime.Now.Date;
        }
    }
	public class Rezervacija
	{
		public int rezervacijaId { get; set; }
		public bool Rezervirano { get; set; }
		public DateTime DatumRezervacije { get; set; }

        public bool VeljavnostRezervacije()
        {
            return rezervacijaId > 0;
        }

        public bool AliJeRezervirano()
        {
            return Rezervirano;
        }

        public bool VeljavnostDatumaRezervacije()
        {
            return DatumRezervacije.Date == DateTime.Now.Date;
        }
    }
	public class Uporabnik
	{
		public int uporabnikId { get; set; }
		public string Ime { get; set; }
		public string Priimek { get; set; }
		public string Kraj { get; set; }
		public int Starost { get; set; }
        public string VrniPolnoIme()
        {
            return $"{Ime} {Priimek}";
        }

        public bool JePolnoleten()
        {
            return Starost >= 18;
        }

        public void PosodobiStarost(int novaStarost)
        {
            Starost = novaStarost;
        }
    }
	public class Evidenca
	{
		public int evidencaId { get; set; }
		public DateTime DatumVnosa { get; set; }
        public string VrniOblikovanDatum()
        {
            return DatumVnosa.ToString("dd.MM.yyyy");
        }

        public void PovecajEvidencaId()
        {
            evidencaId++;
        }

        public int DobiLetoVnosa()
        {
            return DatumVnosa.Year;
        }
    }
}