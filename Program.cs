using Npgsql;
using System;
//var cs = "Host=localhost;Username=postgres;password=kangan;Database=testdb";
var cs = "Host=localhost;Username=postgres;Password=kangan;Database=testdb";

using var con = new NpgsqlConnection(cs);
con.Open();

var sql = "SELECT version()";

using var cmd = new NpgsqlCommand(sql, con);

var version = cmd.ExecuteScalar().ToString();
Console.WriteLine($"PostgresSQL ------------version: {version}");

cmd.Connection = con;
cmd.CommandText = "DROP TABLE IF EXISTS cars";
cmd.ExecuteNonQuery();

//--------Create Table--------------
cmd.CommandText = @"CREATE TABLE cars(id SERIAL PRIMARY KEY, name VARCHAR(255), price INT)";
cmd.ExecuteNonQuery();

//---------- Insert cars into table-----------------------
cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Audi',52642)";
cmd.ExecuteNonQuery();

cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Mercedes',57127)";
cmd.ExecuteNonQuery();

cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Skoda',9000)";
cmd.ExecuteNonQuery();

cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Volvo',29000)";
cmd.ExecuteNonQuery();

cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Bentley',350000)";
cmd.ExecuteNonQuery();

cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Citroen',21000)";
cmd.ExecuteNonQuery();

cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Hummer',41400)";
cmd.ExecuteNonQuery();

cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Volkswagen',21600)";
cmd.ExecuteNonQuery();

Console.WriteLine("Table cars created");

//------------Insert Cars with placeholder ------------

var sql2 = "INSERT INTO cars(name, price) VALUES(@name, @price)";
using var cmd2 = new NpgsqlCommand(sql2, con);

cmd2.Parameters.AddWithValue("name", "BMW");
cmd2.Parameters.AddWithValue("price", 36600);
cmd2.Prepare();

cmd.ExecuteNonQuery();

Console.WriteLine("row inserted");

// ------------Get Cars --------------

string sql3 = "SELECT * FROM cars";
using var cmd3 = new NpgsqlCommand(sql3, con);

using NpgsqlDataReader rdr = cmd3.ExecuteReader();

Console.WriteLine($"{rdr.GetName(0),-4} {rdr.GetName(1), -10} {rdr.GetName(2), 10}");

while (rdr.Read())
{
    Console.WriteLine($"{rdr.GetInt32(0), -4} {rdr.GetString(1),-10} {rdr.GetInt32(2), 10}");
}

while (rdr.Read())
{
    Console.WriteLine("{0} {1} {2}", rdr.GetInt32(0), rdr.GetString(1),
            rdr.GetInt32(2));
}


//----- Get Cars With Column Header----------

//var sql4 = "SELECT * FROM cars";

//using var cmd4 = new  NpgsqlCommand(sql3, con);

//using NpgsqlDataReader rdr1 = cmd3.ExecuteReader();
