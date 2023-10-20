using System.Data.SqlClient;
string conectionString = "Server=DESKTOP-GUVMUS8\\SQLEXPRESS;Database=MinionsDB; Integrated Security = true; ";
SqlConnection dbCon = new SqlConnection(conectionString);
dbCon.Open();
using (dbCon)
{
    SqlCommand command = new SqlCommand("SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount  FROM Villains AS v JOIN MinionsVillains AS mv ON v.Id = mv.VillainId GROUP BY v.Id, v.Name HAVING COUNT(mv.VillainId) > 3 ORDER BY COUNT(mv.VillainId)", dbCon);
    SqlDataReader reader = command.ExecuteReader();
    using (reader)
    {
        while (reader.Read())
        {
            Console.WriteLine($"{reader[0]} - {reader[1]}");
        }
    }
}
