using Microsoft.AspNetCore.Mvc;
using OpenAI.API.Completions;
using SampleWebApi.Models;
using System.Linq.Dynamic.Core;

namespace SampleWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QueryController: ControllerBase
    {
        private readonly IQueryPrompt _queryPrompt;

        public QueryController(IQueryPrompt queryPrompt )
        {
            _queryPrompt = queryPrompt;
        }

        [HttpGet(Name = "GetQuery/{prompt}")]
        public async Task<IQueryable<Person>> Get(string prompt)
        {
            IQueryable<Person> PersonCollection = new List<Person>
            {
                new Person()
                {
                    Id = 1,
                    Name = "ChatGpt",
                    Age = 1,
                    Birthday =  DateTime.Parse("1996-10-17"),
                },
                new Person()
                {
                    Id = 2,
                    Name = "Alfredo",
                    Age = 26,
                    Birthday = DateTime.Parse("1996-10-17"),
                },
                new Person()
                {
                    Id = 3,
                    Name = "Alfredo",
                    Age = 27,
                    Birthday = DateTime.Parse("1996-10-17"),
                },
                new Person()
                {
                    Id = 4,
                    Name = "Jose",
                    Age = 26,
                    Birthday = DateTime.Parse("2000-10-17"),
                },
                new Person()
                {
                    Id = 5,
                    Name = "Newton",
                    Age = 16,
                    Birthday = DateTime.Parse("2023-10-17"),
                },
            }.AsQueryable();

            var querysimple = PersonCollection.Where(p => p.Name != "Alfredo" && p.Birthday >= new DateTime(1999, 1, 1) && p.Birthday <= new DateTime(2020, 12, 31)).Distinct();


            //var querysimple = PersonCollection.Where(x => x.Name != "Alfredo").Select(x => x.Name).Distinct(); 

            // funciona
            //var queryfromai = await _queryPrompt.RunQuery(PersonCollection, "Obtener todos los objetos Person en los que la edad sea mayor que 25 y su nombnre inicie con la letra A  utilizando LINQ.");

            // Funciona
            //var queryfromai = await _queryPrompt.RunQuery(PersonCollection, "Obtener todos primeros 5 elementos utilizando LINQ.");

            //funciona
            //var queryfromai = await _queryPrompt.RunQuery(PersonCollection, "Obtener la lista inversa usando LINQ.");

            // fUNCIONA RARO
            //var queryfromai = await _queryPrompt.RunQuery(PersonCollection, "Obtener todos los objetos Person en los que la edad sea mayor que 25 y su nombre sea Alfredo y ordenarlos de manera ascendente por sus fechas de cumpleaños utilizando LINQ.");

            //Funciona
            //var queryfromai = await _queryPrompt.RunQuery(PersonCollection, "Obtener todos los objetos Person con nombre Alfredo");

            // Funciona
            //var queryfromai = await _queryPrompt.RunQuery(PersonCollection, "Obtener todos los objetos Person con nombre Alfredo con identificador igual a 2");


            // Falla
            //var queryfromai = await _queryPrompt.RunQuery(PersonCollection, "Obtener todos los objetos Person con nombre Alfredo con identificador igual a 2 y su edad mostrarla multiplicada por 2 ");

            // Falla
            //var queryfromai = await _queryPrompt.RunQuery(PersonCollection, "Obtener todos los nombres de los objetos Person utilizando LINQ.");

            // Funciona
            //var queryfromai = await _queryPrompt.RunQuery(PersonCollection, "Obtener todos los objetos Person distintos a Alfredo LINQ.");

            //Funciona
            //var queryfromai = await _queryPrompt.RunQuery(PersonCollection, "Obtener todos los objetos Person distintos a Alfredo y cuyos cumpleaños sucedan en octubre LINQ.");

            // Problemas con los dateTime
            //var queryfromai = await _queryPrompt.RunQuery(PersonCollection, "Obtener todos los objetos Person distintos a Alfredo y cuyos cumpleaños esten entre los años de 1999 y el 2020  LINQ.");

            var queryfromai = await _queryPrompt.RunQuery(PersonCollection, prompt);

            return queryfromai;
        }


    
    }
}
