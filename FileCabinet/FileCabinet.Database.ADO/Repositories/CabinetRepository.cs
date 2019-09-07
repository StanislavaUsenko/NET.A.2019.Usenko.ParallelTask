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
        /// <summary>
        /// sql For Update Cabinet
        /// </summary>
        private const string sqlForUpdateCabinet = "UPDATE Cabinet SET firstName = @firstName, lastName = @lastName, dateBirth = @dateBirth WHERE id = @id";
        /// <summary>
        /// sql For Insert Cabinet
        /// </summary>
        private const string sqlForInsertCabinet = "INSERT INTO Cabinet(firstName,lastName,dateBirth) VALUES (@firstName, @lastName, @dateBirth)";
        /// <summary>
        /// sql For Delete Cabinet
        /// </summary>
        private const string sqlForDeleteCabinet = "DELETE FROM Cabinet WHERE id = @id";
        /// <summary>
        /// sql For Select All Cabinets
        /// </summary>
        private const string sqlForSelectAllCabinets = "SELECT * FROM Cabinet";
        /// <summary>
        /// sql For Select Cabinet By Id
        /// </summary>
        private const string sqlForSelectCabinetById = "Select * From Cabinet Where id = @id";
        /// <summary>
        /// convert data from DataReader to user class Cabinet
        /// </summary>
        private Func<IDataReader, Cabinet> converter = (reader) => new Cabinet((int)reader["id"], reader["firstName"].ToString(), reader["lastName"].ToString(), (DateTime)reader["dateBirth"]);

        /// <summary>
        /// ctor
        /// </summary>
        public CabinetRepository():base ()
        {        }
        
        /// <summary>
        /// write new object in database
        /// </summary>
        /// <param name="obj">object that creats in database</param>
        /// <returns>id of object</returns>
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

        /// <summary>
        /// delete object in database
        /// </summary>
        /// <param name="obj">object that deletes in database</param>
        /// <returns>return flag of deleting</returns>
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

        /// <summary>
        /// read all Cabinets from database
        /// </summary>
        /// <returns>list of data</returns>
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

        /// <summary>
        /// read one record of Cabinet in database
        /// </summary>
        /// <param name="id">what id are you looking for</param>
        /// <returns>return one record from database</returns>
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

        /// <summary>
        /// update record
        /// </summary>
        /// <param name="obj">object what you update</param>
        /// <returns>return id of record</returns>
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

        /// <summary>
        /// find some string in all fields of records
        /// </summary>
        /// <param name="findString">string that should be inside the record</param>
        /// <returns>all records where is present string</returns>
        public List<Cabinet> Find(string findString)
        {
            List<Cabinet> cabinets = base.GetData<Cabinet>(sqlForSelectAllCabinets, converter, null);

            return cabinets
                    .Where(x => x.FirstName.Contains(findString) || 
                            x.DateBirth.ToString().Contains(findString) ||
                            x.LastName.Contains(findString))
                    .ToList();
        }

        /// <summary>
        /// method for creating sql parameters
        /// </summary>
        /// <param name="item">object for which you create sql parameters</param>
        /// <param name="forFinbById">flag true if sql commands such us update, delete, select by id</param>
        /// <param name="id"> write id of object if flag is true</param>
        /// <returns>returs list of sql parameters</returns>
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