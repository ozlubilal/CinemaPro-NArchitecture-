using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories;

public class FilmRepository:EfRepositoryBase<Film,Guid,BaseDbContext>,IFilmRepository
{
    public FilmRepository(BaseDbContext context):base(context) { }
    
}
