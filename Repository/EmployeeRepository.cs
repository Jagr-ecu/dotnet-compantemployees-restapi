using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        //----Cambiar cuando haya millones de registros ya que obtiene la cuenta de registros desde la bd----
        //----mejorando los tiempos de busqueda----
        //public async Task<PagedList<Employee>> GetEmployeesAsync(Guid companyId,
        //    EmployeeParameters employeeParameters, bool trackChanges) 
        //{ 
        //    var employees = await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
        //        .OrderBy(e => e.Name)
        //        .Skip((employeeParameters.PageNumber - 1) * employeeParameters.PageSize)
        //        .Take(employeeParameters.PageSize).ToListAsync(); 

        //    var count = await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
        //        .CountAsync(); 
            
        //    return new PagedList<Employee>(employees, count, employeeParameters.PageNumber, 
        //        employeeParameters.PageSize); 
        //}

        public async Task<PagedList<Employee>> GetEmployeesAsync(Guid companyId, 
            EmployeeParameters employeeParemeters, bool trackChanges)
        {
            var employees = await FindByCondition(e => e.CompanyId.Equals(companyId) 
            && (e.Age >= employeeParemeters.MinAge && e.Age <= employeeParemeters.MaxAge), trackChanges)
                .FilterEmployees(employeeParemeters.MinAge, employeeParemeters.MaxAge)
                .Search(employeeParemeters.SearchTerm)
                .Sort(employeeParemeters.OrderBy)
                .OrderBy(e => e.Name)
                .ToListAsync();

            return PagedList<Employee>
                .ToPagedList(employees, employeeParemeters.PageNumber, employeeParemeters.PageSize);
        }

        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges) =>
            await FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(id), trackChanges)
                .SingleOrDefaultAsync();

        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee) => Delete(employee);
    }
}
