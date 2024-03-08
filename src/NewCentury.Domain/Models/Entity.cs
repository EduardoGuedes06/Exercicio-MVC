using System;

namespace NewCentury.Domain.Models
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
            DataCadastro = DateTime.Now;
        }

        public Guid Id { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}