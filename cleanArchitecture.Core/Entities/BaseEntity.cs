using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace cleanArchitecture.Core.Entities
{
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public BaseEntity()
        {
        }
    }
}
