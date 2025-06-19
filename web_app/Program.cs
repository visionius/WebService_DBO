using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

using web_app.web_db_service;

namespace web_app
{
    class Program
    {
        static void Main(string[] args)
        {
            //Here instead of postman we send request to our prepared web service
            Service1 Myapp = new Service1();
            string result = "";
            Console.WriteLine("get_movie_list");
            result = Myapp.get_movie_list();
            Console.WriteLine(result);
            Console.WriteLine("get_serial_list");
            result = Myapp.get_serial_list();
            Console.WriteLine(result);
            Console.WriteLine("get_user_list");
            result= Myapp.get_user_list();
            Console.WriteLine(result);
            Console.WriteLine("get_actors_film_serials");
            result= Myapp.get_actors_film_serials("Rami", "Malek");
            Console.WriteLine(result);
            Console.WriteLine("get_actors_film_serials");
            result= Myapp.get_actors_film_serials("viggo", "mortensen");
            Console.WriteLine(result);
            Console.WriteLine("get_fim_serial_rate");
            result= Myapp.get_fim_serial_rate();
            Console.WriteLine(result);
            Console.WriteLine("get_golden_glob_winners(2016)");
            result= Myapp.get_golden_glob_winners(2016);
            Console.WriteLine(result);
            Console.WriteLine("get_oscars_winner(2018)");
            result= Myapp.get_oscars_winner(2018);
            Console.WriteLine(result);
            Console.WriteLine("get_serial_avg_rate");
            result= Myapp.get_serial_avg_rate();
            Console.WriteLine(result);

        }
    }
}
