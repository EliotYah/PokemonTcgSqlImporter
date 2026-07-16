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
object TextToDbValue(string value)
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

object NumberToDbValue(string value)
{
    if (string.IsNullOrEmpty(value) == true)
    {
        return DBNull.Value;
    }
    else
    {
        if (int.TryParse(value, out int result))
        {
            return result;
        }
        else
        {
            return DBNull.Value;
        }
    }

}

using (SqlConnection connection = new SqlConnection(connectionString))
{
    connection.Open();
    Console.WriteLine("Connected to database.");

    string insertQuery = "INSERT INTO dbo.Cards (CardId, Name, Hp, Number, Artist, Supertype, Rarity, Level, EvolvesFrom) VALUES (@CardId, @Name, @Hp, @Number, @Artist, @Supertype, @Rarity, @Level, @EvolvesFrom)";

    for (int i = 0; i < allCardList.Count; i++)
    {
        SqlCommand command = new SqlCommand(insertQuery, connection);
        PokemonCardData card = allCardList[i];
        command.Parameters.AddWithValue("@CardId", card.id);
        command.Parameters.AddWithValue("@Name", card.name);
        command.Parameters.AddWithValue("@Hp", NumberToDbValue(card.hp));
        command.Parameters.AddWithValue("@Number", TextToDbValue(card.number));
        command.Parameters.AddWithValue("@Artist", TextToDbValue(card.artist));
        command.Parameters.AddWithValue("@Supertype", TextToDbValue(card.supertype));
        command.Parameters.AddWithValue("@Rarity", TextToDbValue(card.rarity));
        command.Parameters.AddWithValue("@Level", NumberToDbValue(card.level));
        command.Parameters.AddWithValue("@EvolvesFrom", TextToDbValue(card.evolvesFrom));

        command.ExecuteNonQuery();
    }

    Console.WriteLine("Inserted All cards into the database. ");
}

