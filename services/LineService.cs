using Api_AIKO.Interfaces;

namespace Api_AIKO.Services
{
    public class LineService
    {
        private readonly ILineRepository _lineRepository;

        public LineService(ILineRepository lineRepository)
        {
            _lineRepository = lineRepository;
        }
    }
}
