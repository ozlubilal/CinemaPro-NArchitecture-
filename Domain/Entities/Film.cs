using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Film:Entity<Guid>
{
    public string  Name { get; set; }
    public string ImageUrl { get; set; }
    public Film()
    {
        
    }
    public Film(Guid id,string name)
    {
        Id = id;
        Name = name;
    }
}
