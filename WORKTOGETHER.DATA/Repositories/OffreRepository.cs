using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WORKTOGETHER.DATA.Entities;

namespace WORKTOGETHER.DATA.Repositories
{
    public class OffreRepository : Repository<Offre>
    {
        // Offres triées par prix
        public List<Offre> FindAllOrderByPrix()
        {
            return table
                .OrderBy(o => o.PrixMensuel)
                .ToList();
        }
    }
}
