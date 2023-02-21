using Microsoft.VisualBasic;
using System;
using System.Text;

namespace TheMovies.MVVM.Model
{
    public class Film
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int Duration { get; set; }
        public string Director { get; set; }
        public DateTime PremiereDate { get; set; }



        public Film(string title, string genre, int duration, string director, DateTime premiereData)
        {
            Title = title;
            Genre = genre;
            Duration = duration;
            Director = director;
            PremiereDate = premiereData;
        }

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.Append(Title + ";");
            sb.Append(Genre + ";");
            sb.Append(Duration + ";");
            sb.Append(Director + ";");
            sb.Append(PremiereDate);

            return sb.ToString();
        }
    }
}
