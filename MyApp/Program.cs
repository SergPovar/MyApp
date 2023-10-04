// See https://aka.ms/new-console-template for more information

using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using MyApp;
using MySqlConnector;

internal class Program
{
    public static void Main(string[] args)
    {
        var app = new EmployeeController();
        app.Run();
    }
}