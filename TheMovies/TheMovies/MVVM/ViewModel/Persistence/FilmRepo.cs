using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TheMovies.MVVM.Model;
using System.Configuration;
using System.Runtime.ConstrainedExecution;

namespace TheMovies.MVVM.ViewModel.Persistence
{
    public class FilmRepo
    {
        #region Singleton Pattern
        // Implementation of Singleton pattern, to ensure there only exists one single instance of this class!
        private static FilmRepo _instance;
        public static FilmRepo Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FilmRepo();
                    return _instance;
                }

                return _instance;
            }
        }

        private FilmRepo()
        {

        }
        #endregion

        private string filePath = Path.GetFullPath("../../../Files/RegisteredFilms.csv");

        List<Film> films = new();

        string ConnectionString = ConfigurationManager.ConnectionStrings["DatabaseServerInstance"].ConnectionString;

        #region CRUD
        //public Film CreateFilm(string title, string genre, int duration, string director, DateTime premiereDate)
        //{
        //    Film newFilm = new Film(title, genre, duration, director, premiereDate);

        //    films.Add(newFilm);

        //    return newFilm;
        //}

        public List<Film> RetrieveAll()
        {
            return films;
        }

        public void UpdateFilm()
        {

        }

        public void DeleteFilm()
        {

        }
        #endregion

        public Film Create(Film film)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO FILM (Genre, Duration, FilmInstructor, PremiereDate, Title)" +
                                                 "VALUES(@Genre,@Duration,@FilmInstructor,@PremiereDate, @Title)" +
                                                 "SELECT @@IDENTITY", con);
                cmd.Parameters.Add("@Genre", SqlDbType.NVarChar).Value = film.Genre;
                cmd.Parameters.Add("@Duration", SqlDbType.Int).Value = film.Duration;
                cmd.Parameters.Add("@FilmInstructor", SqlDbType.NVarChar).Value = film.Director;
                cmd.Parameters.Add("@PremiereDate", SqlDbType.SmallDateTime).Value = film.PremiereDate;
                cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = film.Title;
                film.Id = Convert.ToInt32(cmd.ExecuteScalar());
                films.Add(film);
                return film;
            }

        }

        public void Load()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM FILM", con);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string Title = dr["Title"].ToString();
                        string Genre = dr["Genre"].ToString();
                        int Duration = int.Parse(dr["Duration"].ToString());
                        string Director = dr["FilmInstructor"].ToString();
                        DateTime PremiereDate = DateTime.Parse(dr["PremiereDate"].ToString());
                        int FilmID = int.Parse(dr["FilmID"].ToString());

                        Film film = new Film(Title, Genre, Duration, Director, PremiereDate);
                        film.Id = FilmID;
                        
                        films.Add(film);
                    }
                }
            }
        }

        public void Update(Film film)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE FILM SET Title = @Title, Genre = @Genre, Duration = @Duration, " +
                    "FilmInstructor = @Director, PremiereDate = @PremiereDate WHERE FilmID = @Id", con);
                cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = film.Title;
                cmd.Parameters.Add("@Genre", SqlDbType.NVarChar).Value = film.Genre;
                cmd.Parameters.Add("@Duration", SqlDbType.Int).Value = film.Duration;
                cmd.Parameters.Add("@Director", SqlDbType.NVarChar).Value = film.Director;
                cmd.Parameters.Add("@PremiereDate", SqlDbType.SmallDateTime).Value = film.PremiereDate;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = film.Id;
                cmd.ExecuteNonQuery();
            }

        }

        public void Delete(Film film)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM FILM WHERE FilmID = @Id", con);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = film.Id;
                cmd.ExecuteNonQuery();
            }
            films.Remove(film);
        }


        //public void Load()
        //{
        //    if (!File.Exists(filePath))
        //        File.Create(filePath).Close();

        //    try
        //    {
        //        // Create an instance of StreamReader to read from a file.
        //        // The using statement also closes the StreamReader.
        //        using (StreamReader sr = new StreamReader(filePath))
        //        {
        //            string line;
        //            while ((line = sr.ReadLine()) != null)
        //            {
        //                string[] film = line.Split(';');

        //                string title = film[0];
        //                string genre = film[1];
        //                int duration = int.Parse(film[2]);
        //                string director = film[3];
        //                DateTime premiereDate = DateTime.Parse(film[4]);

        //                Film newFilm = new Film(title, genre, duration, director, premiereDate);

        //                films.Add(newFilm);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        // Let the user know what went wrong.
        //        Console.WriteLine("The file could not be read:");
        //        Console.WriteLine(e.Message);
        //    }
        //}

        public void Save()
        {
            if (!File.Exists(filePath))
                File.Create(filePath).Close();

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach (Film film in films)
                {
                    sw.WriteLine(film);
                }


            }
        }
    }
}
