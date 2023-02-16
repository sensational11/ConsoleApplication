// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Homework
{
    class Program
    {
        HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            Program program = new Program();
            await program.RetrieveTodoList();
        }

        private async Task RetrieveTodoList()
        {
            //Call the API at https://jsonplaceholder.typicode.com/todos using the HttpClient class (can be found in the System.Net.Http NuGet package)
             
            HttpResponseMessage response = await client.GetAsync("https://jsonplaceholder.typicode.com/todos");

            //Verify that the server responds to that call with an HTTP status code of 200/OK (otherwise exit the program and skip the logic in the remaining steps)
            if (response.StatusCode == HttpStatusCode.OK)
            {
                //Read the content of the response into a string
                string jsonData = await response.Content.ReadAsStringAsync();
                
                //Deserialize the response string into an array or list of type ResponseModel (definition below)
                List<ResponseModel>? responseList = JsonConvert.DeserializeObject<List<ResponseModel>>(jsonData);
                //Create a new array or list of type ResponseModel that contains only completed tasks & incompleted tasks
                List<ResponseModel> completedTasks = new List<ResponseModel>();
                List<ResponseModel> incompletedTasks = new List<ResponseModel>();

                //Inserted completed & incompleted tasks into their lists
                foreach (var item in responseList)
                {


                    if (item.Completed)
                    {
                        completedTasks.Add(item);
                    }
                    else
                    {
                        incompletedTasks.Add(item);
                    }

                }
                //Loop through the array or list of completed tasks and for each item write all of the properties of the ResponseModel to the display console (using Console.WriteLine()) on a single line separated by semi-colons in the order of UserId, Id, Title, Completed (e.g.: "UserId;Id;Title;Completed")
                foreach (var item in completedTasks)
                {
                    Console.WriteLine("{0};{1};{2};{3}", item.UserId, item.Id, item.Title, item.Completed);
                }
                Console.WriteLine("");
                foreach (var item in incompletedTasks)
                {
                    Console.WriteLine("{0};{1};{2};{3}", item.UserId, item.Id, item.Title, item.Completed);
                }
                //Output to the display console the total number of completed tasks and incomplete tasks retrieved from the API
                Console.WriteLine("");
                Console.WriteLine("number of completed tasks: {0}", completedTasks.Count());
                Console.WriteLine("number of incompleted tasks: {0}", incompletedTasks.Count());
            }
            else
            {
                
                Environment.Exit(0);
            }
        }
       
    }

    public class ResponseModel
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool Completed { get; set; }
    }

}
