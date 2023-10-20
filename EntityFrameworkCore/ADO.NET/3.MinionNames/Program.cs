using System.Data.SqlClient;


string connectionString = "Server=DESKTOP-GUVMUS8\\SQLEXPRESS;Database=MinionsDB; Integrated Security = true; ";
SqlConnection sqlConnection = new SqlConnection(connectionString);
sqlConnection.Open();
using (sqlConnection)
{
    int idNum = int.Parse(Console.ReadLine());
    SqlCommand command = new SqlCommand("SELECT ROW_NUMBER() OVER (ORDER BY m.Name) AS RowNum, m.Name, m.Age FROM MinionsVillains AS mv JOIN Minions As m ON mv.MinionId = m.Id WHERE mv.VillainId = @Id ORDER BY m.Name",sqlConnection);
    command.Parameters.AddWithValue("@Id", idNum);

    SqlDataReader reader = command.ExecuteReader();
    using (reader)
    {
        while (reader.Read())
        {
            int Idvillian = (int)(reader["m.Name"]);

            if (true)
            {

            }
        }
    }

}






//SqlConnection dbCon = new SqlConnection(conectionString);
//dbCon.Open();
//using (dbCon)
//{
//    SqlCommand command = new SqlCommand("SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount  FROM Villains AS v JOIN MinionsVillains AS mv ON v.Id = mv.VillainId GROUP BY v.Id, v.Name HAVING COUNT(mv.VillainId) > 3 ORDER BY COUNT(mv.VillainId)", dbCon);
//    SqlDataReader reader = command.ExecuteReader();
//    using (reader)
//    {
//        while (reader.Read())
//        {
//            Console.WriteLine($"{reader[0]} - {reader[1]}");
//        }
//    }
//}
