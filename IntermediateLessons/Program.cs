using Dapper;
using IntermediateLessons.Models;
using IntermediateLessons.Data;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// Top level statements go top.
// This could be below the class if intenal class Program {static void Main(string[] args)
// was in page.

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

DataContextDapper dapper = new DataContextDapper(config);

// Constructing SQL query.  Would it be better to use parameterized queries?
//string sql = @"INSERT INTO TutorialAppSchema.Computer (
//    Motherboard,
//    HasWifi,
//    HasLTE,
//    ReleaseDate,
//    Price,
//    VideoCard
//) VALUES ('"+ myComputer.Motherboard
//    + "','" + myComputer.HasWifi
//    + "','" + myComputer.HasLTE
//    + "','" + myComputer.ReleaseDate
//    + "','" + myComputer.Price
//    + "','" + myComputer.VideoCard
//+ "')\n";

//File.WriteAllText("log.txt", "\n" + sql + "\n");

//using StreamWriter openFile = new("log.txt", append: true);

//openFile.WriteLine("\n" + sql + "\n");

//openFile.Close();

string computersPath = "C:\\Users\\antho\\source\\repos\\IntermediateLessons\\IntermediateLessons\\Computers.json";

string computersJson = File.ReadAllText(computersPath);

//Console.WriteLine(computersJson);

// system.Text.Json options ########################################################
JsonSerializerOptions options = new JsonSerializerOptions()
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
};

IEnumerable<Computer>? computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson, options);

IEnumerable<Computer>? computersNewtonSoft = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);

if (computersNewtonSoft != null)
{
    foreach (Computer computer in computersNewtonSoft)
    {
        //Console.WriteLine(computer.Motherboard);
        string sql = @"INSERT INTO TutorialAppSchema.Computer (
            Motherboard,
            HasWifi,
            HasLTE,
            ReleaseDate,
            Price,
            VideoCard
        ) VALUES ('" + EscapeSingleQuote(computer.Motherboard)
            + "','" + computer.HasWifi
            + "','" + computer.HasLTE
            + "','" + computer.ReleaseDate
            + "','" + computer.Price
            + "','" + EscapeSingleQuote(computer.VideoCard)
        + "')\n";

        dapper.ExecuteSql(sql);
    }
}

// Newtonsoft settings ########################################################
JsonSerializerSettings settings = new JsonSerializerSettings()
{
    ContractResolver = new CamelCasePropertyNamesContractResolver()
};

string computersCopyNewtonsoft = JsonConvert.SerializeObject(computersNewtonSoft, settings);
string newtonPath = "C:\\Users\\antho\\source\\repos\\IntermediateLessons\\IntermediateLessons\\computersCopyNewtonsoft.txt";
File.WriteAllText(newtonPath, computersCopyNewtonsoft);

string computersCopySystem = System.Text.Json.JsonSerializer.Serialize(computersSystem, options);
string systemPath = "C:\\Users\\antho\\source\\repos\\IntermediateLessons\\IntermediateLessons\\computersCopySystem.txt";
File.WriteAllText(systemPath, computersCopySystem);

// SQL cannot have single quotes in it.  This method will escape them.
static string EscapeSingleQuote(string input)
{
    string output = input.Replace("'", "''");

    return output;
}