using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dapper;
using OurFirstApi.Models;

namespace OurFirstApi.Controllers
{
    //api/employees
    public class EmployeesController : ApiController
    {
        //api/employees
        public HttpResponseMessage Get()
        {
            using (var connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString))
            {
                try
                {
                    connection.Open();

                    var result = connection.Query<EmployeeListResult>("select * " +
                                                                      "from Employee");


                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                catch (Exception ex)
                {
                   
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Query blew up");
                }
            }
        }

        //api/employees/3000
        public HttpResponseMessage Get(int id)
        {
            using (var connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString))
            {
                try
                {
                    connection.Open();

                    var result =
                        connection.Query<EmployeeListResult>("Select * From Employee where EmployeeId = @id",
                            new { id = id }).FirstOrDefault();

                    if (result == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Employee with the Id {id} was not found");
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                }
            }
        }

        //PUT api/employees/5
        public HttpResponseMessage Put(int id, EmployeeListResult employee)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString))
            {
                try
                {
                    var rowsAffected = connection.Execute($@"Update Employee
                                                                set FirstName = @firstName,
                                                                    LastName = @lastName
                                                             Where EmployeeId = @employeeId",

                        new { firstName = employee.FirstName, lastName = employee.LastName, employeeId = id });
                                       
                }
                catch (Exception ex)
                {
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                }
            }

            return Request.CreateResponse(HttpStatusCode.Accepted);
        }

        // POST api/values
        public HttpResponseMessage Post(EmployeeListResult employee)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString))
            {
                try
                {
                    connection.Open();

                    var rowsAffected = connection.Execute("Insert into Employee(FirstName, LastName) " +
                                                          "Values(@FirstName, @LastName)",
                        new { FirstName = employee.FirstName, LastName = employee.LastName });

                }

                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);

                }


                return Request.CreateResponse(HttpStatusCode.Created);
            }
        }

        // DELETE api/values/5
        public HttpResponseMessage Delete(int id)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString))
            {
                //connection.Open();
                var sqlDelete = $@"Delete From Employee where EmployeeId = @EmployeeId";

                try
                {
                    var affectedRows = connection.Execute(sqlDelete, new { EmployeeId = id });
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);

                }
            }
        }
    }
}
