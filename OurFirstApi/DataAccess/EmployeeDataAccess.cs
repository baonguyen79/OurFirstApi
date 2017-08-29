using OurFirstApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;

namespace OurFirstApi.DataAccess
{
    public class EmployeeDataAccess : IRepository<EmployeeListResult>
    {

        // Get all Employees
        public List<EmployeeListResult> GetAll()
        {
            using (var connection =
               new SqlConnection(ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString))
            {
                {
                    connection.Open();

                    var result = connection.Query<EmployeeListResult>("select * " +
                                                                       "from Employee");
                    return result.ToList();

                }
            }
                                    
        }


        //Get Employee by Id
        public EmployeeListResult Get(int id)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString))
            {
                connection.Open();
                var result = 
                    connection.QueryFirstOrDefault<EmployeeListResult>("Select * From Employee where EmployeeId = @id",
                    new { id = id });

                return result;
            }
        }

        public int Put(int id, EmployeeListResult employee)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString))
            {
                var rowsAffected = connection.Execute($@"Update Employee
                                                                set FirstName = @firstName,
                                                                    LastName = @lastName
                                                             Where EmployeeId = @employeeId",

                        new { firstName = employee.FirstName, lastName = employee.LastName, employeeId = id });

                return rowsAffected;

               
            }
        }

        public int Post(EmployeeListResult employee)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString))
            {
                connection.Open();

                var rowsAffected = connection.Execute("Insert into Employee(FirstName, LastName) " +
                                                        "Values(@FirstName, @LastName)",
                    new { FirstName = employee.FirstName, LastName = employee.LastName });

                return rowsAffected;

            }
        }

        public int Delete(int id)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString))
            {
                //connection.Open();
                var sqlDelete = $@"Delete From Employee where EmployeeId = @EmployeeId";


                var affectedRows = connection.Execute(sqlDelete, new { EmployeeId = id });

                return affectedRows;
            }
        }
    }

    public interface IRepository<T>
    {
        List<T> GetAll();

        T Get(int id);

        int Put(int id, T employee);

        int Post(T employee);

        int Delete(int id);
    }
}