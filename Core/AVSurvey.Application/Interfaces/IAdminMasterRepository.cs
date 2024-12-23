using AVSurvey.Application.Models;
using AVSurvey.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVSurvey.Application.Interfaces
{
    public interface IAdminMasterRepository
    {
        #region Gender
        Task<int> SaveGender(Gender_Request parameters);

        Task<IEnumerable<Gender_Response>> GetGenderList(BaseSearchEntity parameters);

        Task<Gender_Response?> GetGenderById(int Id);
        #endregion

        #region Category
        Task<int> SaveCategory(Category_Request parameters);

        Task<IEnumerable<Category_Response>> GetCategoryList(BaseSearchEntity parameters);

        Task<Category_Response?> GetCategoryById(int Id);
        #endregion

        #region Branch
        Task<int> SaveBranch(Branch_Request parameters);

        Task<IEnumerable<Branch_Response>> GetBranchList(BaseSearchEntity parameters);

        Task<Branch_Response?> GetBranchById(int Id);
        #endregion
    }
}
