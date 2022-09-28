using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace RETAIL_POINT_NET6.Clases
{
    internal class SQLFunctions
    {
        public static string RetailDB
        {
            get
            {
                return @"Data Source=AORUSPRO\SQLEXPRESS;Initial Catalog=RetailDB; Integrated Security=true; Encrypt=False";
            }
        }

        public static string SERVER_RetailDB
        {
            get
            {
                return @"Data Source=SERVER\SQLEXPRESS;Initial Catalog=RetailDB; Integrated Security=true; Encrypt=False";
            }
        }

        public static Tuple<int, DataTable> QueryPasswordByUser(string Users)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = RetailDB;
            connection.Open();
            string _QuerySQL = "Select * from UsersDB where Users = '" + Users + "'";
            SqlCommand command = connection.CreateCommand();
            command.CommandText = _QuerySQL;
            SqlDataAdapter _adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            _adapter.Fill(dt);
            connection.Close();

            return Tuple.Create(0, dt);
        }

        public static Tuple<int, DataTable> QueryUsers()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = RetailDB;
            connection.Open();
            string _QuerySQL = "Select * from UsersDB";
            SqlCommand command = connection.CreateCommand();
            command.CommandText = _QuerySQL;
            SqlDataAdapter _adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            _adapter.Fill(dt);
            connection.Close();

            return Tuple.Create(0, dt);
        }

        public static Tuple<int, DataTable> QueryAllStock()
        {

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = RetailDB;
            connection.Open();
            string _QuerySQL = "Select * from ProductDB";
            SqlCommand command = connection.CreateCommand();
            command.CommandText = _QuerySQL;
            SqlDataAdapter _adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            _adapter.Fill(dt);
            connection.Close();

            return Tuple.Create(0, dt);
        }

        public static Tuple<int, DataTable> Query(string IdProduct)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = RetailDB;
            connection.Open();
            string _QuerySQL = "Select * from ProductDB where Id = '" + IdProduct + "'";
            SqlCommand command = connection.CreateCommand();
            command.CommandText = _QuerySQL;
            SqlDataAdapter _adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            _adapter.Fill(dt);
            connection.Close();

            return Tuple.Create(0, dt);
        }

        public static Tuple<int, DataTable> UpdateStock(string IdProduct, int StockProduct)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = RetailDB;
            connection.Open();
            string _QuerySQL = "Update ProductDB SET Stock= '"+StockProduct+"' where Id = '"+ IdProduct+"'";
            SqlCommand command = connection.CreateCommand();
            command.CommandText = _QuerySQL;
            SqlDataAdapter _adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            _adapter.Fill(dt);
            connection.Close();

            return Tuple.Create(0, dt);
        }

        public static int InsertProduct(string IdProduct, string NameProduct, string DescriptionProduct, string PriceProduct)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = RetailDB;
            connection.Open();
            string _QuerySQL = "Insert into ProductDB ([Id], [Name], [Description], [Price], [Stock])" +  
                               " values ('"+ IdProduct + "', '"+ NameProduct + "', '"+ DescriptionProduct + "', '"+ Convert.ToDecimal(PriceProduct) + "', '"+0+"')";
            SqlCommand command = connection.CreateCommand();
            command.CommandText = _QuerySQL;

            int result = command.ExecuteNonQuery();
   
            connection.Close();

            return result;
        }

        public static int DeleteProduct(string IdProduct)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = RetailDB;
            connection.Open();
            string _QuerySQL = "Delete From ProductDB where Id = '"+IdProduct+"'";
                               
            SqlCommand command = connection.CreateCommand();
            command.CommandText = _QuerySQL;

            int result = command.ExecuteNonQuery();

            connection.Close();

            return result;
        }

    }


}

