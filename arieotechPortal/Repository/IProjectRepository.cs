using ArieotechLive.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArieotechLive.Repository
{
    public interface IProjectRepository
    {
        IEnumerable<Project> GetAllProject();
        Project GetProjectByID(int ProjectID);
        void InsertIntoProject( Project projectinsert);
        void UpdateProject(Project projectupdate, int ProjectID);
        Project GetProjectByProjectName(string projectName);
        void DeactivateProject(int ProjectID);
        Employee GetEmployeeIDAgainstProjectID(int ProjectID);
    }
}
