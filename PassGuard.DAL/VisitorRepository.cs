using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PassGuard.Models;

namespace PassGuard.DAL
{
    public class VisitorRepository
    {
        private readonly PassGuardContext _context;

        public VisitorRepository(PassGuardContext context)
        {
            _context = context;
        }

        public Visitor? GetById(int id)
        {
            return _context.Visitors
                .Include(v => v.VisitPasses)
                .FirstOrDefault(v => v.VisitorId == id);
        }

        public Visitor? GetByFullNameAndPhone(string fullName, string phone)
        {
            return _context.Visitors.FirstOrDefault(v => v.FullName == fullName && v.Phone == phone);
        }

        public List<Visitor> GetAll()
        {
            return _context.Visitors
                .Include(v => v.VisitPasses)
                .OrderBy(v => v.FullName)
                .ToList();
        }

        public void Add(Visitor visitor)
        {
            _context.Visitors.Add(visitor);
            _context.SaveChanges();
        }

        public void Update(Visitor visitor)
        {
            _context.Visitors.Update(visitor);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Visitor? visitor = _context.Visitors.FirstOrDefault(v => v.VisitorId == id);

            if (visitor != null)
            {
                _context.Visitors.Remove(visitor);
                _context.SaveChanges();
            }
        }
    }
}
