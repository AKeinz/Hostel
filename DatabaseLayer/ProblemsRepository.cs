using Microsoft.EntityFrameworkCore;
using Model;

namespace DatabaseLayer
{
    public class ProblemsRepository : IRepository<Problem>
    {
        public void Add(Problem entity)
        {
            using (var db = new HostelDBContext())
            {
                db.Problems.Add(entity);
                db.SaveChanges();
                db.Entry(entity).State = EntityState.Modified;
            }
        }

        public void Delete(Problem entity)
        {
            using (var db = new HostelDBContext())
            {
                db.Problems.Remove(entity);
                db.SaveChanges();
            }
        }

        public IEnumerable<Problem> GetAll()
        {
            List<Problem> result = new List<Problem>();
            using (var db = new HostelDBContext())
            {
                result = db.Problems.OrderByDescending((p) => p.Id).ToList();
            }
            return result;
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
                var order = db.Problems.Find(entity.Id);
                db.Entry(order).CurrentValues.SetValues(entity);
                db.SaveChanges();
            }
        }
    }
}
