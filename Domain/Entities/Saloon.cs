using Core.Persistence.Repositories;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Saloon:Entity<Guid>
{
    public string Name { get; set; }

    public Saloon()
    {
        
    }
    public Saloon(Guid id,string name)
    {
        Id= id;
        Name= name;
    }
}

