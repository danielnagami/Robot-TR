using Microsoft.AspNetCore.Mvc;
using RestSharp;
using RobotTR.DataCollector.API.Models;
using RobotTR.DataCollector.API.Utils;
using RobotTR.WebAPI.Core.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotTR.DataCollector.API.Controllers
{
    [Route("api/user")]
    public class UserController : MainController
    {
        private static List<RepositoryContent> _classes = new List<RepositoryContent>();
        public ICodesRepository _repository { get; set; }

        public UserController(ICodesRepository repository)
        {
            _repository = repository;
        }


        [HttpGet("GetUsersRepositories")]
        public IActionResult GetUsersRepositories([FromQuery] string username)
        {
            var repositories = GetRepositories(username);

            return CustomResponse(repositories);
        }

        [HttpGet("GetRepositoryContent")]
        public IActionResult GetRepositoryContent([FromQuery] string repository,
                                                [FromQuery] string username)
        {
            var root = RepositoryContent(username, repository);

            foreach (var item in root)
            {
                GetRecursive(item, username, repository);
            }

            return CustomResponse(_classes);
        }

        private List<string> GetRepositories(string username)
        {
            var result = HTTPRequests.Request<Repository[]>($"users/{username}/repos?sort=updated&per_page=5&page=1", Method.GET);

            var repositories = new List<string>();

            foreach (var item in result)
            {
                repositories.Add(item.name);
            }

            return repositories;
        }

        private void GetRecursive(RepositoryContent path, string username, string repository) 
        {
            if (path.type.Equals("dir"))
            {
                var newContent = RepositoryContent(username, repository, path.path);

                if(newContent != null)
                {
                    foreach (var item in newContent)
                    {
                        GetRecursive(item, username, repository);
                    }
                }
            }
            else if(path.type.Equals("file") && path.name.EndsWith(".cs"))
            {
                var fileContent = RepositoryContentSpecific(username, repository, path.path);

                byte[] data = Convert.FromBase64String(fileContent.content);
                string decodedString = Encoding.UTF8.GetString(data);

                fileContent.content = decodedString;

                _classes.Add(fileContent);
                AddToDatabase(fileContent, repository, username);
            }
        }

        private RepositoryContent[] RepositoryContent(string username, string repository, string path = "")
        {
            string uri = string.IsNullOrEmpty(path) ? $"repos/{username}/{repository}/contents" : $"repos/{username}/{repository}/contents/{path}";
            return HTTPRequests.Request<RepositoryContent[]>(uri, Method.GET);
        }

        private RepositoryContent RepositoryContentSpecific(string username, string repository, string path = "")
        {
            string uri = string.IsNullOrEmpty(path) ? $"repos/{username}/{repository}/contents" : $"repos/{username}/{repository}/contents/{path}";
            return HTTPRequests.Request<RepositoryContent>(uri, Method.GET);
        }

        private void AddToDatabase(RepositoryContent repositoryContent, string projectName, string username)
        {
            _repository.Add(new Codes(Guid.NewGuid(), projectName, repositoryContent.name, repositoryContent.content, username));
        }
    }
}