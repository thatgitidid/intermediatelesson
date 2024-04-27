using Dapper;
using IntermediateLessons.Models;
using IntermediateLessons.Data;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

// Top level statements go top.
// This could be below the class if intenal class Program {static void Main(string[] args)
// was in page.


//////////Moved to data
// String contains info for creating connection.
//string connectionString =
//    "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true;";

//// IDbConnection object is used to connect, uses info from the connectString to create the SqlConnection.
//// Now dbConnection has access to query the database.
//IDbConnection dbConnection = new SqlConnection(connectionString);

//// Testing sql connection with this query.
//string sqlCommand = "SELECT GETDATE()";
/////////Moved to data

// Running the query with Dapper.


//Requires the ConnectionStrings key in the Json file.
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

DataContextDapper dapper = new DataContextDapper(config);
DataContextEF entityFramework = new DataContextEF(config);

DateTime rightNow = dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");

// Writing the result to console to confirm.  Query does not auto pop it out even if not set
// to a variable.
//Console.WriteLine(rightNow);


Computer myComputer = new Computer()
{
    Motherboard = "Z690",
    HasWifi = true,
    HasLTE = false,
    ReleaseDate = DateTime.Now,
    Price = 943.87m,
    VideoCard = "RTX 2060"
};

// Entity framework version of adding the myComputer to table.
entityFramework.Add(myComputer);
entityFramework.SaveChanges();

// Constructing SQL query.  Would it be better to use parameterized queries?
string sql = @"INSERT INTO TutorialAppSchema.Computer (
    Motherboard,
    HasWifi,
    HasLTE,
    ReleaseDate,
    Price,
    VideoCard
) VALUES ('"+ myComputer.Motherboard
    + "','" + myComputer.HasWifi
    + "','" + myComputer.HasLTE
    + "','" + myComputer.ReleaseDate
    + "','" + myComputer.Price
    + "','" + myComputer.VideoCard
+ "')";

//Console.WriteLine(sql);

//int result = dapper.ExecuteSqlWithRowCount(sql);
bool result = dapper.ExecuteSql(sql);

//Console.WriteLine(result);

string sqlSelect = @"
SELECT 
    Computer.Motherboard,
    Computer.HasWifi,
    Computer.HasLTE,
    Computer.ReleaseDate,
    Computer.Price,
    Computer.VideoCard 
FROM TutorialAppSchema.computer";

// Create IEnumerable of computers.  When sqlSelect is called, it will return
// an IEnumerable.  Would need to do List and ToList() if wanted a list.
IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);

// Something is wrong and this loops the 0 id entry.
Console.WriteLine("'ComputerId','Motherboard','HasWifi','HasLTE','ReleaseDate','Price','VideoCard'");
// Does not have to show this view, just copy pasted to validate working in console.
foreach (Computer singleComputer in computers)
{
    Console.WriteLine("'" + singleComputer.ComputerId
    + "','" + singleComputer.Motherboard
    + "','" + singleComputer.HasWifi
    + "','" + singleComputer.HasLTE
    + "','" + singleComputer.ReleaseDate
    + "','" + singleComputer.Price
    + "','" + singleComputer.VideoCard
+ "'");
}


// Entity Framework To List Call
// Does same as the dapper.
IEnumerable<Computer>? computersEF = entityFramework.Computer?.ToList<Computer>();

if (computersEF != null)
{
    Console.WriteLine("'ComputerId','Motherboard','HasWifi','HasLTE','ReleaseDate','Price','VideoCard'");
    // Does not have to show this view, just copy pasted to validate working in console.
    foreach (Computer singleComputer in computersEF)
    {
        Console.WriteLine("'" + singleComputer.ComputerId
        + "','" + singleComputer.Motherboard
        + "','" + singleComputer.HasWifi
        + "','" + singleComputer.HasLTE
        + "','" + singleComputer.ReleaseDate
        + "','" + singleComputer.Price
        + "','" + singleComputer.VideoCard
    + "'");
    }
}

//myComputer.HasWifi = false;
//Console.WriteLine(myComputer.Motherboard);
//Console.WriteLine(myComputer.HasWifi);
//Console.WriteLine(myComputer.ReleaseDate);
//Console.WriteLine(myComputer.VideoCard);



