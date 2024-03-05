using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FilmSessions.Constants
{
    public class FilmSessionMessages
    {
        public const string FilmSessionDateCannotBeforeToday = "Film seans tarihi bugün tarihinden önce olamaz";

        public static string? SaloonIsNotEmpty = "Aynı saatlerde bu salonda başka bir film vardır";
    }
}
