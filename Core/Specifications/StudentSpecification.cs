using Core.Entities;

namespace Core.Specifications
{
    public class StudentSpecification : BaseSpecification<Student>
    {
        public StudentSpecification(StudentSpecParams studentParams)
            : base(x =>
            (string.IsNullOrEmpty(studentParams.Search) || x.FullName.ToLower().Contains(studentParams.Search)) &&
            (!studentParams.Stage.HasValue || x.Stage == studentParams.Stage) &&
            (!studentParams.Department.HasValue || x.Department == studentParams.Department)
            )
        {
            AddInclude(x => x.Documents);
            AddInclude(x => x.Activities);
            AddInclude(x => x.Notes);
            AddOrderBy(x => x.FullName);
            ApplyPaging(studentParams.PageSize * (studentParams.PageIndex - 1),
                studentParams.PageSize);

            if (!string.IsNullOrEmpty(studentParams.Sort))
            {
                switch (studentParams.Sort)
                {
                    // case "stageeAsc":
                    //     AddOrderBy(p => p.Stage);
                    //     break;
                    // case "stageDesc":
                    //     AddOrderByDescending(p => p.Stage);
                    //     break;
                    default:
                        AddOrderBy(n => n.FullName);
                        break;
                }
            }
        }

        public StudentSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Documents);
            AddInclude(x => x.Activities);
            AddInclude(x => x.Notes);
        }
    }
}