using System.Threading.Tasks;
using System.Windows.Documents;
using MaimApp.Interfaces;
using MaimApp.Parser.Models;
using Newtonsoft.Json;

namespace MaimApp.DAL
{
    public class MainParser : IParser
    {
        public async Task<Rootobject> Parse(string url)
        {
            var response = await App.HttpClient.GetAsync(url);

            var resultString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Rootobject>(resultString);
        }
    }
}