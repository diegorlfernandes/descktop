
using System;
using System.Data;
using Microsoft.Data.Sqlite;

namespace FormWithButton
{
    public class db
    {
        private static SqliteConnection connection;
        public db(string dbname)
        {
            connection = new SqliteConnection("" + new SqliteConnectionStringBuilder { DataSource = dbname });
            connection.Open();
        }

        public bool inserir(string message)
        {

            SqliteTransaction transaction = connection.BeginTransaction();
            SqliteCommand command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = "insert into message ( text ) values ( $text )";
            command.Parameters.AddWithValue("$id", 1);
            command.Parameters.AddWithValue("$text", message);
            int ret = command.ExecuteNonQuery();
            transaction.Commit();


            if (ret == 0)
                return true;
            else
                return false;

        }


        public DataTable SelecionarTodos()
        {

            DataTable Collection = new DataTable();
            Collection.Columns.Add("ID", typeof(int));
            Collection.Columns[0].ColumnName = "ID";
            Collection.Columns.Add("Message", typeof(string));
            Collection.Columns["Message"].ColumnName = "Message";


            SqliteTransaction transaction = connection.BeginTransaction();
            SqliteCommand command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = "SELECT id, text FROM message";
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Collection.Rows.Add(reader.GetInt16(0), reader.GetString(1));
            }

            transaction.Commit();

            return Collection;

        }




    }
}
