using MaimApp.Parser.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace MaimApp.Interfaces
{
    public interface IParser
    {
         Task<Rootobject> Parse(string url);
    }
}