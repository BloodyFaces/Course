using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseMVVM.Model
{
    public class DbContex
    {
        private static UsersContex _contex;
        private static List<Account> _accounts;
        private static List<Categories> _categories;
        private static List<CatQuestions> _catqt;
        private static List<CatAnswers> _catAns;
        private static List<Student> _students;

        public static UsersContex GetDbContex()
        {
            if (_contex == null)
                _contex = new UsersContex();
            return _contex;
        }

        public static List<Student> GetDbStudentList()
        {
            if(_students == null)
                _students = GetDbContex().Student.ToList();
            return _students;
        }

        public static List<CatAnswers> GetDbCatAnswers(int ansID)
        {
            _catAns = GetDbContex().CatAnswers.Where(s => s.ACat_id == ansID).ToList();
            return _catAns;
        }

        public static List<CatQuestions> GetDbListQuestions(int cat_id)
        {
            _catqt = GetDbContex().CatQuestions.Where(s => s.QCat_id.Value == cat_id).ToList();
            return _catqt;
        }

        public static void Refresh()
        {
            _contex.Dispose();
            _contex = new UsersContex();
            _accounts = DbContex.GetDbContex().Account.ToList() ;
            _categories = DbContex.GetDbContex().Categories.ToList();
            _students = DbContex.GetDbContex().Student.ToList();

        }

        public static List<Account> GetDbListAccount()
        {
            if (_accounts == null)
                _accounts = GetDbContex().Account.ToList();
            return _accounts;
        }

        public static List<Categories> GetDbListCategories()
        {
            if (_categories == null)
                _categories = GetDbContex().Categories.ToList();
            return _categories;
        }

        public static List<Results> FindResults(string login, string password)
        {
            List<Results> results = new List<Results>();
            foreach(var s in _accounts)
            {
                if(s.Login == login && s.Password == password)
                {
                    results = s.Results.ToList();
                }
            }
            return results;
        }

        public static List<Results> FindResults(string login)
        {
            List<Results> results = new List<Results>();
            foreach (var s in GetDbListAccount())
            {
                if (s.Login == login)
                {
                    results = s.Results.ToList();
                }
            }
            return results;
        }
    }
}
