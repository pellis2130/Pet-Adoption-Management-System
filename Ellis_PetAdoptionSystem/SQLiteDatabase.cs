/*******************************************************************
* Name: Princess Ellis
* Date: June 7, 2026
* Assignment: SDC320 Week 4 Course Project - Database Implementation
*
* Class that creates or connects to the SQLite database.
*/
using System;
using System.Data.SQLite;

public class SQLiteDatabase
{
    public static SQLiteConnection Connect(string database)
    {
        string cs = @"Data Source=" + database;
        SQLiteConnection conn = new SQLiteConnection(cs);

        try
        {
            conn.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Database connection error: " + ex.Message);
        }

        return conn;
    }
}