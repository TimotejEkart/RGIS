using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using RGIS_Vaja4;
using System.Data.SqlClient;

namespace RGIS_Vaja4.Pages
{
    public class PregledVprasalnikModel : PageModel
    {
        public List<Odgovori> OdgovoriList { get; set; }
        private readonly IConfiguration _configuration;

        public PregledVprasalnikModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Odgovori";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    OdgovoriList = new List<Odgovori>();
                    while (reader.Read())
                    {
                        OdgovoriList.Add(new Odgovori()
                        {
                            odgovorId = reader.GetInt32(reader.GetOrdinal("odgovorId")),
                            potovanjeId = reader.GetInt32(reader.GetOrdinal("potovanjeId")),
                            Odgovor1 = reader.GetString(reader.GetOrdinal("Odgovor1")),
                            Odgovor2 = reader.GetString(reader.GetOrdinal("Odgovor2")),
                            Odgovor3 = reader.GetString(reader.GetOrdinal("Odgovor3")),
                            Odgovor4 = reader.GetString(reader.GetOrdinal("Odgovor4")),
                            Odgovor5 = reader.GetString(reader.GetOrdinal("Odgovor5")),
                            Odgovor6 = reader.GetString(reader.GetOrdinal("Odgovor6")),
                            Odgovor7 = reader.GetString(reader.GetOrdinal("Odgovor7")),
                            Odgovor8 = reader.GetString(reader.GetOrdinal("Odgovor8")),
                            Odgovor9 = reader.GetString(reader.GetOrdinal("Odgovor9")),
                            Odgovor10 = reader.GetString(reader.GetOrdinal("Odgovor10")),
                            Odgovor11 = reader.GetString(reader.GetOrdinal("Odgovor11")),
                            Odgovor12 = reader.GetString(reader.GetOrdinal("Odgovor12")),
                            Odgovor13 = reader.GetString(reader.GetOrdinal("Odgovor13")),
                            Odgovor14 = reader.GetString(reader.GetOrdinal("Odgovor14")),
                            Odgovor15 = reader.GetString(reader.GetOrdinal("Odgovor15"))
                        });
                    }
                }
            }
        }
    }
}
