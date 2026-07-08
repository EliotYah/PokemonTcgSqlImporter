using System.Text.Json;
using Microsoft.Data.SqlClient;

string connectionString = @"Server=.\SQLEXPRESS;Database=PokemonTCG;Trusted_Connection=True;TrustServerCertificate=True;";

string folderPath = @"C:\Users\Eliot\Desktop\pokemon-tcg-data-master\pokemon-tcg-data-master\cards\en";
string[] jsonFiles = Directory.GetFiles(folderPath, "*.json");
List<PokemonCardData> allCardList = new List<PokemonCardData>();
foreach (string jsonFile in jsonFiles)
{
    string fileName = Path.GetFileName(jsonFile);

    Console.WriteLine($"Found JSON file: {fileName}");
    Console.WriteLine("Attempting to read the JSON file...");
    using (StreamReader reader = new StreamReader(jsonFile))
    {
        string readFile = reader.ReadToEnd();
        Console.WriteLine("Characters: " + readFile.Length);

        List<PokemonCardData> cards = JsonSerializer.Deserialize<List<PokemonCardData>>(readFile);
        allCardList.AddRange(cards);
        Console.WriteLine("Total cards in file: " + cards.Count);
    }
}
Console.WriteLine("Total card count: " + allCardList.Count);

for (int i = 0; i < 5; i++)
{     
    PokemonCardData card = allCardList[i];
    Console.WriteLine(card.name);
}
//string getfirstitem(List<string> list, string fallbacktext)
//{
//    if (list != null && list.Count > 0)
//    {
//        return list[0];

//    }
//    else
//    {
//        return fallbacktext;
//    }
//}

object nullChecker(string value)
{
    if (string.IsNullOrEmpty(value) == true)
    {
        return DBNull.Value;
    }
    else
    {
        return value;
    }
}




using (SqlConnection connection = new SqlConnection(connectionString))
{
    connection.Open();
    Console.WriteLine("Connected to database.");

    string insertQuery = "INSERT INTO dbo.Cards (Id, Name, Hp, Number, Artist, Supertype, Rarity, Level, EvolvesFrom) VALUES (@Id, @Name, @Hp, @Number, @Artist, @Supertype, @Rarity, @Level, @EvolvesFrom)";

    for (int i = 0; i < allCardList.Count; i++)
    {
        SqlCommand command = new SqlCommand(insertQuery, connection);
        PokemonCardData card = allCardList[i];
        command.Parameters.AddWithValue("@Id", card.id);
        command.Parameters.AddWithValue("@Name", card.name);
        command.Parameters.AddWithValue("@Hp", nullChecker(card.hp));
        command.Parameters.AddWithValue("@Number", nullChecker(card.number));
        command.Parameters.AddWithValue("@Artist", nullChecker(card.artist));
        command.Parameters.AddWithValue("@Supertype", nullChecker(card.supertype));
        command.Parameters.AddWithValue("@Rarity", nullChecker(card.rarity));
        command.Parameters.AddWithValue("@Level", nullChecker(card.level));
        command.Parameters.AddWithValue("@EvolvesFrom", nullChecker(card.evolvesFrom));

        command.ExecuteNonQuery();
    }

    Console.WriteLine("Inserted All cards into the database. ");
}

