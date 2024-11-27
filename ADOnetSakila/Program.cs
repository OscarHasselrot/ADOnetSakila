using Microsoft.Data.SqlClient;
namespace ADOnetSakila
{
    public class Program
    {
        static void Main()
        {
            string? firstName;
            string? lastName;

            var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Sakila;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

            Console.WriteLine("Skriv in vilken skådespelare du vill kolla upp.");
            Console.Write("Förnamn: ");
            firstName = Console.ReadLine();
            Console.Write("Efternamn: ");
            lastName = Console.ReadLine();


            var findActorFilms = new SqlCommand($"SELECT title\r\n\t\t\r\nFROM film\r\ninner join film_actor on film.film_id = film_actor.film_id\r\ninner join actor on film_actor.actor_id = actor.actor_id\r\n\r\nwhere actor.first_name = '{firstName}'\r\n\tand actor.last_name = '{lastName}';\r\n", connection);

            connection.Open();
            var rec = findActorFilms.ExecuteReader();
            Console.Clear();
            if (rec.HasRows)
            {
                Console.WriteLine($"{firstName} {lastName} har varit med i filmerna:");
                while (rec.Read())
                {
                    Console.WriteLine(rec[0]);
                }
            }
            else
            {
                Console.WriteLine($"{firstName} {lastName} hittades inte.");
            }
            connection.Close();
        }
    }
}