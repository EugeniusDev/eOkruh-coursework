using eOkruh.Common;

namespace eOkruh.Domain.Personnel
{
    class Person
    {
        public string FullName { get; set; } = string.Empty;
        public MilitarySpeciality Speciality { get; set; }
    }
}
