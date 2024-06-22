using System;
using System.ComponentModel.DataAnnotations.Schema;
using Whisper.Entities;

namespace Whisper.Entities
{
    /// <summary>
    /// Veritabanı modellerine karşılık gelen class'ların ( aspnet tabloları hariç) miras alacağı class. Tüm tablolarda ortak olarak kullanacak alanlar
    /// burada tanımlanacak.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityBase<T> : IEntity
    {
        public EntityBase()
        {
            this.CreatedAt = DateTime.Now;
        }
        /// <summary>
        /// Entity identifier
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }

        /// <summary>
        /// Created At
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
