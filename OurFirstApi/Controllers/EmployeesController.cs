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
using OurFirstApi.DataAccess;

namespace OurFirstApi.Controllers
{
    //api/employees
    public class EmployeesController : ApiController
    {
        //api/employees
        public HttpResponseMessage Get()
        {
            try
            {
                var employeeData = new EmployeeDataAccess();
                var employees = employeeData.GetAll();

                return Request.CreateResponse(HttpStatusCode.OK, employees);
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Query blew up");
            }
        }



        //api/employees/id
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var employeeData = new EmployeeDataAccess();
                var employee = employeeData.Get(id);

                if (employee == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Employee with the Id {id} was not found");
                }

                return Request.CreateResponse(HttpStatusCode.OK, employee);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        //PUT api/employees/5
        public HttpResponseMessage Put(int id, EmployeeListResult employee)
        {
            try
            {
                var employeeData = new EmployeeDataAccess();
                var rowsAffected = employeeData.Put(id, employee);

                if (rowsAffected == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Employee with the Id {id} was not found");
                }

                return Request.CreateResponse(HttpStatusCode.OK, $"Rows updated {rowsAffected}");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

        }

        // POST api/values
        public HttpResponseMessage Post(EmployeeListResult employee)
        {
            
            try
            {
                var employeeAdd = new EmployeeDataAccess();
                var rowsAdd = employeeAdd.Post(employee);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);

            }


             return Request.CreateResponse(HttpStatusCode.Created);
        }

        // DELETE api/values/5
        public HttpResponseMessage Delete(int id)
        {
           
            try
            {
                var employee = new EmployeeDataAccess();
                var rowDelete = employee.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK,$"Row deleted {rowDelete}");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);

            }
            
        }
    }
}
