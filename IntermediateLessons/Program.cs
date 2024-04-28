using Dapper;
using IntermediateLessons.Models;
using IntermediateLessons.Data;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using Microsoft.Extensions.Options;

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

string computersPath = "C:\\Users\\antho\\source\\repos\\IntermediateLessons\\IntermediateLessons\\ComputersSnake.json";

string computersJson = File.ReadAllText(computersPath);

// Using computer ID to identify source/destination.  
// Using anonymous function calls.  Mapping computer_id to ComputerId.
// Snake case mapped to camel case.  There is a simpler solution.
// Mapping can be used for other forms of conversion like math.
//Mapper mapper = new Mapper(new MapperConfiguration((cfg) =>
//{
//    cfg.CreateMap<ComputerSnake, Computer>()
//        .ForMember(destination => destination.ComputerId, options =>
//            options.MapFrom(source => source.computer_id));
//    cfg.CreateMap<ComputerSnake, Computer>()
//        .ForMember(destination => destination.CPUCores, options =>
//            options.MapFrom(source => source.cpu_cores));
//    cfg.CreateMap<ComputerSnake, Computer>()
//        .ForMember(destination => destination.HasLTE, options =>
//            options.MapFrom(source => source.has_lte));
//    cfg.CreateMap<ComputerSnake, Computer>()
//        .ForMember(destination => destination.HasWifi, options =>
//            options.MapFrom(source => source.has_wifi));
//    cfg.CreateMap<ComputerSnake, Computer>()
//        .ForMember(destination => destination.Motherboard, options =>
//            options.MapFrom(source => source.motherboard));
//    cfg.CreateMap<ComputerSnake, Computer>()
//        .ForMember(destination => destination.VideoCard, options =>
//            options.MapFrom(source => source.video_card));
//    cfg.CreateMap<ComputerSnake, Computer>()
//        .ForMember(destination => destination.ReleaseDate, options =>
//            options.MapFrom(source => source.release_date));
//    cfg.CreateMap<ComputerSnake, Computer>()
//        .ForMember(destination => destination.Price, options =>
//            options.MapFrom(source => source.price));
//}));

//IEnumerable<ComputerSnake>? computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<ComputerSnake>>(computersJson);

//if (computersSystem != null)
//{
//    IEnumerable<Computer> computerResult = mapper.Map<IEnumerable<Computer>>(computersSystem);

//    foreach(Computer computer in computerResult)
//    {
//        Console.WriteLine(computer.Motherboard);
//    }   
//}

// No Automapper necessary.
IEnumerable<Computer>? computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson);

if (computersSystem != null)
{
    foreach (Computer computer in computersSystem)
    {
        Console.WriteLine(computer.Motherboard);
    }
}

////Console.WriteLine(computersJson);

//// system.Text.Json options ########################################################
//JsonSerializerOptions options = new JsonSerializerOptions()
//{
//    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
//};

//IEnumerable<Computer>? computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson, options);

//IEnumerable<Computer>? computersNewtonSoft = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);

//if (computersNewtonSoft != null)
//{
//    foreach (Computer computer in computersNewtonSoft)
//    {
//        //Console.WriteLine(computer.Motherboard);
//        string sql = @"INSERT INTO TutorialAppSchema.Computer (
//            Motherboard,
//            HasWifi,
//            HasLTE,
//            ReleaseDate,
//            Price,
//            VideoCard
//        ) VALUES ('" + EscapeSingleQuote(computer.Motherboard)
//            + "','" + computer.HasWifi
//            + "','" + computer.HasLTE
//            + "','" + computer.ReleaseDate
//            + "','" + computer.Price
//            + "','" + EscapeSingleQuote(computer.VideoCard)
//        + "')\n";

//        dapper.ExecuteSql(sql);
//    }
//}

//// Newtonsoft settings ########################################################
//JsonSerializerSettings settings = new JsonSerializerSettings()
//{
//    ContractResolver = new CamelCasePropertyNamesContractResolver()
//};

//string computersCopyNewtonsoft = JsonConvert.SerializeObject(computersNewtonSoft, settings);
//string newtonPath = "C:\\Users\\antho\\source\\repos\\IntermediateLessons\\IntermediateLessons\\computersCopyNewtonsoft.txt";
//File.WriteAllText(newtonPath, computersCopyNewtonsoft);

//string computersCopySystem = System.Text.Json.JsonSerializer.Serialize(computersSystem, options);
//string systemPath = "C:\\Users\\antho\\source\\repos\\IntermediateLessons\\IntermediateLessons\\computersCopySystem.txt";
//File.WriteAllText(systemPath, computersCopySystem);

// SQL cannot have single quotes in it.  This method will escape them.
static string EscapeSingleQuote(string input)
{
    string output = input.Replace("'", "''");

    return output;
}