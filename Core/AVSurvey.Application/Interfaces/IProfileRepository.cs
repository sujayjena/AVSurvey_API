using AVSurvey.Application.Models;
using AVSurvey.Persistence.Repositories;

namespace AVSurvey.Application.Interfaces
{
    public interface IProfileRepository
    {
        #region Role 

        Task<int> SaveRole(Role_Request parameters);

        Task<IEnumerable<Role_Response>> GetRoleList(BaseSearchEntity parameters);

        Task<Role_Response?> GetRoleById(long Id);

        #endregion

        #region RoleHierarchy 

        Task<int> SaveRoleHierarchy(RoleHierarchy_Request parameters);

        Task<IEnumerable<RoleHierarchy_Response>> GetRoleHierarchyList(BaseSearchEntity parameters);

        Task<RoleHierarchy_Response?> GetRoleHierarchyById(long Id);

        #endregion
    }
}
