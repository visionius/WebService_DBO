using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebService1
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service1 : System.Web.Services.WebService
    {
        [WebMethod]
        public string get_movie_list()
        {
            var dbMovie = new DataClasses1DataContext();
            var query = dbMovie.GetMovies();
            string result = "";
            foreach (var c in query)
            {   
                result += c.film_title + " " +  c.director_name + " " +  c.director_family_name + " " +  c.genre_name + " " +  c.imdb + " " + c.story_brief +  " "+ 
                c.box_office + " " + c.film_duration + "\n";
            }
            if (result == "")
            {
                result = "[-] No movie title found\n";
            }
            return result;
        }
        [WebMethod]
        public string get_serial_list()
        {
            var dbMovie = new DataClasses1DataContext();
            var query = dbMovie.GetSerials();
            string result = "";
            foreach (var c in query)
            {
                result += c.serial_title + " " + c.director_name + " " + c.director_family_name + " " + c.year + " " + c.genre_name + " " + c.show_channel+ " " +
                c.create_channel+ "\n";
            }
            if (result == "")
            {
                result = "[-] No serial title found\n";
            }
            return result;
        }
        [WebMethod]
        public string get_user_list()
        {
            var dbMovie = new DataClasses1DataContext();
            var query = dbMovie.GetUsers();
            string result = "";
            foreach (var c in query)
            {
                result += c.name + " " + c.family_name+ " " + c.sex + " " + c.age+ " " + c.genre_name+ " " + c.nationality+ " " + c.state+ " " +
                c.phone_number + "\n";
            }
            if (result == "")
            {
                result = "[-] No user data found\n";
            }
            return result;
        }
        [WebMethod]
        public string get_golden_glob_winners(int year)
        {
            var dbMovie = new DataClasses1DataContext();
            var query = dbMovie.golden_glob_winners(year);
            string result = "";
            foreach (var c in query)
            {
                result += c.serial_title + " " + c.winnerof + " " + c.winner_name + " " + c.director_name + " " + c.director_family_name + "\n";
            }
            if (result == "")
            {
                result = "[-] No golden glob winner found\n";
            }
            return result;

        }
        [WebMethod]
        public string get_oscars_winner(int year)
        {
            var dbMovie = new DataClasses1DataContext();
            var query = dbMovie.oscars_winners(year);
            string result = "";
            foreach (var c in query)
            {
                result += c.film_title + " " + c.winnerof + " " + c.winner_name + " " + c.director_name + " " + c.director_family_name + "\n";
            }
            if (result == "")
            {
                result = "[-] No oscare winner found\n";
            }
            return result;

        }
        [WebMethod]
        public string get_serial_avg_rate()
        {
            var dbMovie = new DataClasses1DataContext();
            var query = from c in dbMovie.serial_avgRates select c; 
            string result = "";
            foreach (var c in query)
            {
                result += c.serial_name + " " + c.Averate_rate + "\n";
            }
            if (result == "")
            {
                result = "[-] No serial average rate data found\n";
            }
            return result;
        }
        [WebMethod]
        public string get_fim_serial_rate()
        {
            var dbMovie = new DataClasses1DataContext();
            var query = from c in dbMovie.film_serial_ratings select c;
            string result = "";
            foreach (var c in query)
            {
                result += c.film_title + " " + c.serial_title+ " " + c.name  + " " + c.family_name + " " + c.rate + "\n";
            }
            if (result == "")
            {
                result = "[-] No film serial rate data found\n";
            }
            return result;
        }
        [WebMethod]
        public string get_actors_film_serials(string name, string familyName)
        {
            var dbMovie = new DataClasses1DataContext();
            var query = dbMovie.actor_films_serials(name, familyName);
            string result = "";
            foreach (var c in query)
            {
                result += c.film_title + " " + c.film_genre + " " + c.Film_year + " " + c.serial_title+ " " + c.serial_genre +
                    c.Serial_year + "\n";
            }
            if (result == "")
            {
                result = "[-] No actor(s) film serial data found\n";
            }
            return result;
        }


    }
}