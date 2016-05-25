using Carubbi.Utils.Persistence;
using Itanio.Leads.Domain;
using Itanio.Leads.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace Itanio.Leads.DataAccess
{
    public class Contexto : DbContext, IContexto
    {

        public Contexto()
              : base("name=StringConexao")
        {
        }
        private Dictionary<Type, object> _dicionarioSerializadores = new Dictionary<Type, object>();

        private Serializer<TEntidade> ObterSerializador<TEntidade>()
        {
            Type tipoEntidade = typeof(TEntidade);
            if (!_dicionarioSerializadores.ContainsKey(tipoEntidade))
            {
                _dicionarioSerializadores.Add(tipoEntidade, new Serializer<TEntidade>());
            }

            return (Serializer<TEntidade>)_dicionarioSerializadores[tipoEntidade];
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Parametro>();
            modelBuilder.Entity<Log>();
            modelBuilder.Entity<Usuario>();
            modelBuilder.Entity<Projeto>().HasMany(p => p.Arquivos).WithRequired(a => a.Projeto);
            modelBuilder.Entity<Arquivo>().HasMany(a => a.Acessos).WithOptional(a => a.Arquivo);
            modelBuilder.Entity<Acesso>().HasRequired(a => a.Visitante).WithMany(a => a.Acessos);
            modelBuilder.Entity<Acesso>().HasRequired(a => a.Projeto).WithMany(a => a.Acessos);
        }

        public void Atualizar<TEntidade>(TEntidade objetoAntigo, TEntidade objetoNovo) where TEntidade : class
        {
            Entry(objetoAntigo).CurrentValues.SetValues(objetoNovo);
            Entry(objetoAntigo).State = EntityState.Modified;
        }

        public void Salvar()
        {
            Salvar(null);
        }

        public void Salvar(Usuario usuarioLogado)
        {
            DateTime agora = DateTime.Now;
            if (usuarioLogado != null)
            {
                foreach (var entry in ChangeTracker?.Entries()?.Where(x => x.Entity?.GetType() != typeof(Log)))
                {
                    GerarLog(entry, usuarioLogado);
                }
            }
            SaveChanges();
        }

        private void GerarLog(DbEntityEntry entry, Usuario usuarioLogado)
        {
            var serializador = this
                          .GetType()
                          .GetMethod("ObterSerializador")
                          .MakeGenericMethod(entry.Entity.GetType())
                          .Invoke(this, null);

            var tipoSerializador = serializador.GetType();

            var metodoSerializar = tipoSerializador
              .GetMethod("XmlSerialize");


            var xmlAntigo = string.Empty;
            var xmlNovo = string.Empty;

            if (entry.State == (EntityState.Deleted | EntityState.Modified))
            {
                xmlAntigo = metodoSerializar
                  .Invoke(serializador, new object[] { entry.OriginalValues }).ToString();
            }

            if (entry.State == (EntityState.Added | EntityState.Modified))
            {
                xmlNovo = metodoSerializar
                  .Invoke(serializador, new object[] { entry.CurrentValues }).ToString();
            }

            if (!string.IsNullOrWhiteSpace(xmlAntigo) || !string.IsNullOrWhiteSpace(xmlNovo))
            {
                Entry(new Log
                {
                    EstadoAntigo = xmlAntigo.ToString(),
                    EstadoNovo = xmlNovo.ToString(),
                    DataHora = DateTime.Now,
                    Usuario = usuarioLogado,
                    IdEntitdade = ((Entidade)entry.Entity).Id,
                    Tipo = entry.Entity.GetType().ToString()
                }).State = EntityState.Added;
            }
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}
