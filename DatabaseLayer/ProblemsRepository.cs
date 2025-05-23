using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer
{
    public class ProblemsRepository : IRepository<Problem>
    {
        public void Add(Problem entity)
        {
            using (var db = new HostelDBContext())
            {
                db.Problems.Add(entity);
            }
        }

        public void Delete(Problem entity)
        {
            using (var db = new HostelDBContext())
            {
                db.Problems.Remove(entity);
            }
        }

        public IEnumerable<Problem> GetAll()
        {
            using (var db = new HostelDBContext())
            {
                return db.Problems;
            }
        }

        public Problem GetById(int id)
        {
            using (var db = new HostelDBContext())
            {
                return db.Problems.Find(id);
            }
        }

        public void Update(Problem entity)
        {
            using (var db = new HostelDBContext())
            {
                var order = db.Orders.Find(entity.Id);
                db.Entry(order).CurrentValues.SetValues(entity);
                db.SaveChanges();
            }
        }
    }
}
