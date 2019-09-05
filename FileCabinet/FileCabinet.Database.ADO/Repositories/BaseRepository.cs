using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinet.Database.ADO.Repositories
{
    public abstract class BaseRepository
    {
        public BaseRepository() {         }

        private string SqlConnect()
        {
            string connection = "Data Source=DESKTOP-NCDA0GK;Initial Catalog=FileCabinet;Integrated Security=True";
            return connection;
        }

        public List<T> GetData<T>(string sqlCommand, Func<System.Data.IDataReader, T> converter, List<SqlParameter> parameters)
        {
            try
            {
                List<T> item = new List<T>();
                using (SqlConnection connection = new SqlConnection(SqlConnect()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlCommand, connection);
                    if (parameters != null)
                        foreach (var i in parameters)
                            command.Parameters.Add(i);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            T repos = converter(reader);
                            item.Add(repos);
                        }
                    return item;
                }
            }
            catch (SqlException sqlException)
            {
                throw;
            }
            catch (Exception ex)
            {
                
                throw;
            }

        }


        public void PostData(string sqlCommand, List<SqlParameter> parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SqlConnect()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlCommand, connection);
                    foreach (var i in parameters)
                    {
                        command.Parameters.Add(i);
                    }
                    int number = command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
