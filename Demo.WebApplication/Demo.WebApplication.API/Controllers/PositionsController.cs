using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Dapper;
using Demo.WebApplication.Common;
using Demo.WebApplication.Service;

namespace Demo.WebApplication.API.Controllers
{
    [Route("api/v1/positions")]
    [ApiController]
    public class PositionsController : BasesController<Position>
    {
        #region Field

        private readonly IPositionService _positionService;

        #endregion

        #region Constructor

        public PositionsController(IPositionService positionService) : base(positionService) 
        {
            _positionService = positionService;
        }

        #endregion

    }
}
