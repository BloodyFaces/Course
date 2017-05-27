using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseMVVM.Model
{
    public class AccountData
    {
        public string Login { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Group { get; set; }
    }

    public class ResultsData
    {
        public string Number { get; set; }
        public string Category { get; set; }
        public string TotalPoints { get; set; }
        public string Points { get; set; }
        public string Mark { get; set; }
    }
}
