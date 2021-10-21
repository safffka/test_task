using System;
using System.Data;
using System.IO;
using System.Xml;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;



public class Db
{
	SqlConnection connection;
	public Db()
	{
		StreamReader sr = new StreamReader("bd params.txt");
		string server= sr.ReadLine();
		string database= sr.ReadLine();
		string user= sr.ReadLine();
		string password= sr.ReadLine();

		string connectionString = server + database+ user+ password;
		connection = new SqlConnection(connectionString);
		connection.Open();
		sr.Close();


	}
    ~Db() 
	{
		connection.Close();
	}
	public void create_db()
	{
		SqlCommand command = new SqlCommand();
		command.CommandText = @"CREATE TABLE [changings] (
                    [assembly_num] varchar(100) NOT NULL,
					[change_num] int NOT NULL,
                    [author] varchar(100) NOT NULL,
                    [reason] varchar(1000) NOT NULL,
                    [change_date] datetime NOT NULL
                    );"; 
		command.Connection = connection;
		command.ExecuteNonQuery();

	}
	public void insert_db(string text_sql)
    {
		SqlCommand command = new SqlCommand();
		command.CommandText = "INSERT INTO changings (assembly_num,change_num,author,reason,change_date) VALUES " + text_sql;
		command.Connection = connection;
		command.ExecuteNonQuery();
	}
	 public DataTable select_db (string param1,string param2)
	{
		
		SqlCommand command = new SqlCommand();
		command.Connection = connection;
		SqlDataAdapter adapter = new SqlDataAdapter();
		adapter.SelectCommand = command;

		DataTable table = new DataTable();
		if (param1 is null )
		{
			command.CommandText = "SELECT * FROM changings WHERE assembly_num= " + param2;



		}
		else
		{
			command.CommandText = "SELECT * FROM changings WHERE change_date= " + param1;
			

		}
		adapter.Fill(table);
		return table;
	}
	
}
