using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1.Domain;

public class BaseEntity: IBaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedUTC { get; set; }
    public DateTime UpdatedUTC { get; set; }
    public bool IsActive { get; set; } 
}