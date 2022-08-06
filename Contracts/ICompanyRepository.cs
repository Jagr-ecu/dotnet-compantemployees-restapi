﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges);
        Task<Company> GetCompanyAsync(Guid companyId, bool trackChanges);
        void CreateCompany(Company company);
        Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> companies, bool trackChanges);
        void DeleteCompany(Company company);
    }
}

//los metodos Create y Delete se dejan sincrónicas.
//porque en estos metodos, no estamos haciendo ningún cambio en la base de datos.
