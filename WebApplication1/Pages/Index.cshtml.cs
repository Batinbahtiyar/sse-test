using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using sse_library;
using WebApplication1.Models;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly SseServiceClient _sseClient;
        private readonly ILogger<IndexModel> _logger;
        public IList<SseDataInfo> SseInfos { get; set; }

        public IndexModel(SseServiceClient sseClient, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _sseClient = sseClient;
        }

        public IActionResult OnGet()
        {
            SseInfos = _sseClient.GetSseData();

            return Page();
        }
    }
}
