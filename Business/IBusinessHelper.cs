using Objects.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IBusinessHelper
    {
        Task<SessionDto> GetSession(SessionRequestDto request);

    }
}
