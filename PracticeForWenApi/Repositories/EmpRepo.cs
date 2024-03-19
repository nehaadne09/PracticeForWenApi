using Dapper;
using PracticeForWebApi.Context;
using PracticeForWebApi.Models;
using PracticeForWebApi.Repositories.Interfaces;

namespace PracticeForWebApi.Repositories
{
    public class EmpRepo: IEmpRepo
    {
        private readonly DapperContext _context;
        public EmpRepo(DapperContext context) 
        {
            _context = context;
        }

        public async Task<int> AddEmp(EmployeeModel employeeModel)
        {
            int result = 0;
            var query = @"INSERT INTO tblemployee(EmplyeeName, EmpMobile, EmpAddress, salary,image) 
                        VALUES(@EmplyeeName,@EmpMobile,@EmpAddress,@salary, @image);
                        SELECT LAST_INSERT_ID()";

            using (var connection = _context.CreateConnection())
            {
                result = await connection.QuerySingleAsync<int>(query, employeeModel);
                return result;
            }
        }

        public async Task<int> UpdateEmp(EmployeeModel employeeModel)
        {
            int result = 0;
            var query = @"update tblemployee set EmplyeeName=@EmplyeeName,EmpMobile=@EmpMobile,
                        EmpAddress=@EmpAddress,salary=@salary,image=@image";

            using (var connection = _context.CreateConnection())
            {
                result = await connection.ExecuteAsync(query, employeeModel);
                return result;
            }
        }
    }
}
