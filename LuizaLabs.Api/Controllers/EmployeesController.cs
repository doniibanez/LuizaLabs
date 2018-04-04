using LuizaLabs.Api.Helpers;
using LuizaLabs.Data.DataContexts;
using LuizaLabs.Domain;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LuizaLabs.Api.Controllers
{
    [RoutePrefix("api/v1/public")]
    public class EmployeesController : ApiController
    {    
        private LuizaLabsDataContext db = new LuizaLabsDataContext();

        [Route("employees")]
        public HttpResponseMessage GetEmployees()
        {
            var result = db.Employees.ToList();

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("employees/{pageSize}/{page}")]
        public HttpResponseMessage GetEmployees(int pageSize, int page)
        {
            PagedList<Employee> pagedList = PagedList<Employee>.Create(db.Employees.ToList(),page,pageSize);

            var result = pagedList;

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("employees/{employeeId}")]
        public HttpResponseMessage GetEmployees(int employeeId)
        {
            var result = db.Employees.Where(x=>x.Id == employeeId);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("employees")]
        public HttpResponseMessage PostEmployee(Employee employee)
        {
            if (employee == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                db.Employees.Add(employee);
                db.SaveChanges();

                var result = employee;
                return Request.CreateResponse(HttpStatusCode.Created, result);
            }
            catch(Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao incluir empregado!");
            }                      
        }

        [Route("employees/{employeeId}")]
        public HttpResponseMessage DeleteEmployee(int employeeId)
        {
            Employee employee = db.Employees.Find(employeeId);

            if (employee == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                db.Employees.Remove(employee);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao excluir empregado!");
            }
        }

        //// GET: api/Employees
        //public IQueryable<Employee> GetEmployees()
        //{
        //    return db.Employees;
        //}

        //// GET: api/Employees/5
        //[ResponseType(typeof(Employee))]
        //public IHttpActionResult GetEmployee(int id)
        //{
        //    Employee employee = db.Employees.Find(id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(employee);
        //}

        //// PUT: api/Employees/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutEmployee(int id, Employee employee)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != employee.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(employee).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EmployeeExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/Employees
        //[ResponseType(typeof(Employee))]
        //public IHttpActionResult PostEmployee(Employee employee)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Employees.Add(employee);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = employee.Id }, employee);
        //}

        //// DELETE: api/Employees/5
        //[ResponseType(typeof(Employee))]
        //public IHttpActionResult DeleteEmployee(int id) 
        //{
        //    Employee employee = db.Employees.Find(id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Employees.Remove(employee);
        //    db.SaveChanges();

        //    return Ok(employee);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //private bool EmployeeExists(int id)
        //{
        //    return db.Employees.Count(e => e.Id == id) > 0;
        //}
    }
}