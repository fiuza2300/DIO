using curso.api.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace curso.api.Infraestruture.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("TB_USUARIO");
            builder.HasKey(u => u.Codigo);
            builder.Property(u => u.Codigo).ValueGeneratedOnAdd();
            builder.Property(u => u.Login);
            builder.Property(u => u.Senha);
            builder.Property(u => u.Email);
        }
    }
}
