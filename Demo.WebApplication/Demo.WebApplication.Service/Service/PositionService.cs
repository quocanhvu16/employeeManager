using Demo.WebApplication.Common;
using Demo.WebApplication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.Service
{
    public class PositionService : BaseService<Position>, IPositionService
    {
        #region Field
        private readonly IPositionRepository _positionRepository;
        #endregion

        #region Constructor
        public PositionService(IPositionRepository positionRepository) : base(positionRepository)
        {
            _positionRepository = positionRepository;
        } 
        #endregion

    }
}
