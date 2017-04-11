using ArcPrime.WPF.Model;
using System.Net;

namespace ArcPrime.WPF.Communication
{
    public interface IExperimentClient
    {
        Result Describe(string login, string token);
        HttpStatusCode Execute(string login, string token, string command, string value);
    }
}