﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Carubbi.Utils.Persistence;
using Itanio.Leads.Domain;
using Itanio.Leads.Domain.Entidades;

namespace Itanio.Leads.DataAccess
{
    public class Contexto : DbContext, IContexto
    {
        private readonly Dictionary<Type, object> _dicionarioSerializadores = new Dictionary<Type, object>();

        public Contexto()
            : base("name=StringConexao")
        {
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

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        private Serializer<TEntidade> ObterSerializador<TEntidade>()
        {
            var tipoEntidade = typeof(TEntidade);
            if (!_dicionarioSerializadores.ContainsKey(tipoEntidade))
                _dicionarioSerializadores.Add(tipoEntidade, new Serializer<TEntidade>());

            return (Serializer<TEntidade>) _dicionarioSerializadores[tipoEntidade];
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Parametro>().Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Usuario>().Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Projeto>().Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Arquivo>().Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Acesso>().Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Visitante>().Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<IdentificadorVisitante>().Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Log>();
            modelBuilder.Entity<Projeto>().HasMany(p => p.Arquivos).WithRequired(a => a.Projeto);
            modelBuilder.Entity<Arquivo>().HasMany(a => a.Acessos).WithOptional(a => a.Arquivo);
            modelBuilder.Entity<Acesso>().HasOptional(a => a.Visitante).WithMany(a => a.Acessos);
            modelBuilder.Entity<Acesso>().HasRequired(a => a.Projeto).WithMany(a => a.Acessos);
            modelBuilder.Entity<Visitante>().HasMany(a => a.Identificadores);
        }

        public void Salvar(Usuario usuarioLogado)
        {
            var agora = DateTime.Now;
            if (usuarioLogado != null)
                foreach (var entry in ChangeTracker?.Entries()?.Where(x => x.Entity?.GetType() != typeof(Log)))
                    GerarLog(entry, usuarioLogado);
            SaveChanges();
        }

        private void GerarLog(DbEntityEntry entry, Usuario usuarioLogado)
        {
            var serializador = GetType()
                .GetMethod("ObterSerializador")
                .MakeGenericMethod(entry.Entity.GetType())
                .Invoke(this, null);

            var tipoSerializador = serializador.GetType();

            var metodoSerializar = tipoSerializador
                .GetMethod("XmlSerialize");


            var xmlAntigo = string.Empty;
            var xmlNovo = string.Empty;

            if (entry.State == (EntityState.Deleted | EntityState.Modified))
                xmlAntigo = metodoSerializar
                    .Invoke(serializador, new object[] {entry.OriginalValues}).ToString();

            if (entry.State == (EntityState.Added | EntityState.Modified))
                xmlNovo = metodoSerializar
                    .Invoke(serializador, new object[] {entry.CurrentValues}).ToString();

            if (!string.IsNullOrWhiteSpace(xmlAntigo) || !string.IsNullOrWhiteSpace(xmlNovo))
                Entry(new Log
                {
                    EstadoAntigo = xmlAntigo,
                    EstadoNovo = xmlNovo,
                    DataHora = DateTime.Now,
                    Usuario = usuarioLogado,
                    IdEntitdade = ((Entidade) entry.Entity).Id,
                    Tipo = entry.Entity.GetType().ToString()
                }).State = EntityState.Added;
        }
    }
}