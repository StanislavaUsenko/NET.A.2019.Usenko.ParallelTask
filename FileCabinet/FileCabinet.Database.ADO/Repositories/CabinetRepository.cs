using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileCabinet.Database.Core.Interfaces;
using FileCabinet.Database.Core.Models;

namespace FileCabinet.Database.ADO.Repositories
{
    public class CabinetRepository : BaseRepository, IRepository<Cabinet>
    {
        private const string sqlForUpdateCabinet = "UPDATE Cabinet SET firstName = @firstName, lastName = @lastName, dateBirth = @dateBirth WHERE id = @id";
        private const string sqlForInsertCabinet = "INSERT INTO Cabinet(firstName,lastName,dateBirth) VALUES (@firstName, @lastName, @dateBirth)";
        private const string sqlForDeleteCabinet = "DELETE FROM Cabinet WHERE id = @id";
        private const string sqlForSelectAllCabinets = "SELECT * FROM Cabinet";
        private const string sqlForSelectCabinetById = "Select * From Cabinet Where id = @id";
        private Func<IDataReader, Cabinet> converter = (reader) => new Cabinet((int)reader["id"], reader["firstName"].ToString(), reader["lastName"].ToString(), (DateTime)reader["dateBirth"]);

        public CabinetRepository():base ()
        {        }

        public int Create(Cabinet obj)
        {
            try
            {
                if (obj != null)
                    base.PostData(sqlForInsertCabinet, SQLParameters(obj, false, obj.Id));
                else
                    throw new ArgumentNullException();
                return obj.Id;
            }
            catch(SqlException ex)
            {
                throw;
            }
        }

        public bool Delete(Cabinet obj)
        {
            try
            {
                if (obj != null)
                    base.PostData(sqlForDeleteCabinet, SQLParameters(obj, false, obj.Id));
                else
                    throw new ArgumentNullException();
                return true;
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public List<Cabinet> GetAll()
        {
            try
            {
                return base.GetData<Cabinet>(sqlForSelectAllCabinets, converter, null); ;
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public Cabinet GetById(int id)
        {
            try
            {
                return base.GetData<Cabinet>(sqlForSelectCabinetById, converter, SQLParameters(null, true, id)).FirstOrDefault();
                    
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public Cabinet GetByTag(string tag, string str)
        {
            throw new NotImplementedException();
        }

        public int Update(Cabinet obj)
        {
            try
            {
                if (obj != null)
                    base.PostData(sqlForUpdateCabinet, SQLParameters(obj, false, obj.Id));
                else
                    throw new ArgumentNullException();
                return obj.Id;
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        private List<SqlParameter> SQLParameters(Cabinet item, bool forFinbById, int? id)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            if (!forFinbById)
            {
                sqlParameters.Add(new SqlParameter("@firstName", item.FirstName));
                sqlParameters.Add(new SqlParameter("@lastName", item.LastName));
                sqlParameters.Add(new SqlParameter("@dateBirth", item.DateBirth));
                sqlParameters.Add(new SqlParameter("@id", item.Id));
            }
            else
            {
                if (id != null)
                    sqlParameters.Add(new SqlParameter("@id", id));
                else
                    throw new ArgumentNullException();
            }
            return sqlParameters;
        }
    }
}