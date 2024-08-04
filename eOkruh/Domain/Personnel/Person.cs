using eOkruh.Common;
using System.Collections.ObjectModel;

namespace eOkruh.Domain.Personnel
{
    class Person
    {
        public string FullName { get; set; } = string.Empty;
        public ObservableCollection<string> Specialities { get; set; } = [];
    }
}
