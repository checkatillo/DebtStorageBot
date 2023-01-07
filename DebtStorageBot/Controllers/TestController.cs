using Microsoft.AspNetCore.Mvc;

namespace DebtStorageBot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly DebtStorageSettings _settings;

        public TestController(ILogger<TestController> logger, DebtStorageSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        public string Get()
        {
            return _settings.BotAPIKey;
        }
    }
}