using API.Dtos;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace API.Helpers
{
 public class ApiUrlResolver : IMemberValueResolver<object, object, string, string>
{
    private readonly string _apiUrl;

    public ApiUrlResolver(IConfiguration config)
    {
        _apiUrl = config["ApiUrl"];
    }

    public string Resolve(object source, object destination, string sourceMember, string destMember, ResolutionContext context)
    {
        string url = sourceMember as string;
        if (url == null)
            return null;

        return _apiUrl + url;
    }
}

}