using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Pomidoros.Model;

namespace Pomidoros.Controller
{
    public class PersonController
    {
        public PersonController()
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://138.201.153.220/api/swagger/?format=openapi");
                //HTTP GET
                var responseTask = client.GetAsync("person");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<Person[]>();
                    readTask.Wait();

                    var students = readTask.Result;

                    foreach (var student in students)
                    {
                        Console.WriteLine(student.Name);
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
