using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Model
{
    public class Movie
    {
        public string Title { get; set; }
        public string PosterPath { get; set; }
        public string Description { get; set; }

        public List<Screening> Screenings { get; set; }
    }
}

