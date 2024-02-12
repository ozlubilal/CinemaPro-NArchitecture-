using Core.Persistence.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Repositories;

public interface IFilmRepository:IAsyncRepository<Film,Guid>,IRepository<Film,Guid>
{
}
