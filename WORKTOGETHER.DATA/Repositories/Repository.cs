using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WORKTOGETHER.DATA.Entities;

namespace WORKTOGETHER.DATA.Repositories
{
    // class générique pour les opérations CRUD de base
    public class Repository<T>  where T : class
    {
        // contexte de base de données et DbSet pour l'entité T
        protected WorktogetherContext context;
        protected DbSet<T> table;

        // constructeur qui initialise le contexte et le DbSet
        public Repository()
        {
            context = new WorktogetherContext();
            table = context.Set<T>();
        }

        // méthode pour créer une nouvelle entité
        public void Create(T entity)
        {
            table.Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            table.Update(entity);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = table.Find(id);

            if (entity != null)
            {
                table.Remove(entity);
                context.SaveChanges();
            }
        }

        // méthode pour trouver une entité par son identifiant
        public T FindById(int id)
        {
            return table.Find(id);
        }

        // 
        public List<T> FindAll() 
        {
            return table.ToList(); 
        }
    }
}