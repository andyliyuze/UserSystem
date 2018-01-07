using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSystem.Core.Entity;

namespace UserSystem.Data.Configurations
{
   public class AppClientConfiguration :EntityTypeConfiguration<AppClient>
    {
        public AppClientConfiguration()
        {
            this.HasKey(a => a.ClientId);
            this.Property(a => a.ClientSecret).HasMaxLength(256);
            this.Property(a => a.ClientType).HasMaxLength(20);
            this.Property(a => a.RetrunUrl).HasMaxLength(256);
        }
    }
}
