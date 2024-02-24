using Api_AIKO.Interfaces;

namespace Api_AIKO.Controllers
{
    public class StopService
    {
        private readonly IStopRepository _stopRepository;

        public StopService(IStopRepository stopRepository)
        {
            _stopRepository = stopRepository;
        }
    }
}
