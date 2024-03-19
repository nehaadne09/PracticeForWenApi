using PracticeForWebApi.Models;

namespace PracticeForWebApi.Repositories.Interfaces
{
    public interface IEmpRepo
    {
        public Task<int> AddEmp(EmployeeModel employeeModel);
        public Task<int> UpdateEmp(EmployeeModel employeeModel);
    }
}
