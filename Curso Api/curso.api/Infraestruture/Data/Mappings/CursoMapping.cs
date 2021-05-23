using curso.api.Business.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace curso.api.Infraestruture.Data.Mappings
{
    public class CursoMapping : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("TB_CURSO");
            builder.HasKey(c => c.Codigo);
            builder.Property(c => c.Codigo).ValueGeneratedOnAdd();            
            builder.Property(c => c.Nome);
            builder.Property(c => c.Descricao);
            builder.HasOne(c => c.Usuario).WithMany().HasForeignKey(fk => fk.CodigoUsuario);
        }
    }
}
