using Application.Features.Films.Commands.Update;
using Application.Features.Films.Queries.GetById;

namespace WebApp.ViewModels;

public class UpdateFilmViewModel
{
    public GetByIdFilmResponse? GetByIdFilmResponse { get; set; }
    public UpdateFilmCommand? UpdateFilmCommand { get; set; }
}
