using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesDB.Business.Entities;

namespace MoviesDB.Data.Mapping
{
    public class MovieActorMappingConfiguration : EntityTypeConfiguration<MovieActorMapping>
    {
        public MovieActorMappingConfiguration()
        {
            HasKey(t => t.Id);
            Ignore(t => t.EntityId);
        }
    }
}
