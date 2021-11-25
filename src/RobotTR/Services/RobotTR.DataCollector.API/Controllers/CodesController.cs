using Microsoft.AspNetCore.Mvc;
using RobotTR.DataCollector.API.Models;
using RobotTR.WebAPI.Core.Controllers;

namespace RobotTR.DataCollector.API.Controllers
{
    [Route("api/codes")]
    public class CodesController : MainController
    {
        public ICodesRepository _repository { get; set; }

        public CodesController(ICodesRepository repository)
        {
            _repository = repository;
        }

        [HttpDelete("delete-project")]
        public IActionResult DeleteProject(string name)
        {
            _repository.DropProject(name);
            return CustomResponse();
        }
    }
}
