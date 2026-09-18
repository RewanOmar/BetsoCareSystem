//using BetsoCare.Core.Interfaces;
//using Microsoft.AspNetCore.Mvc;
//using System.Security.Claims;
//using Microsoft.AspNetCore.Authorization;

//namespace BetsoCare.APIS.Controllers
//{
//    [ApiController]
//    [Route("api/openrouter")]
//    [Authorize] // 🔥 مهم علشان نجيب userId
//    public class OpenRouterController : ControllerBase
//    {
//        private readonly IAiService _ai;
//        private readonly IDiagnosisService _diagnosis;

//        public OpenRouterController(IAiService ai, IDiagnosisService diagnosis)
//        {
//            _ai = ai;
//            _diagnosis = diagnosis;
//        }

//        // 🔹 Test بسيط
//        [HttpGet("test")]
//        public async Task<IActionResult> Test()
//        {
//            try
//            {
//                var systemPrompt = @"
//You are a veterinary assistant chatbot.
//Respond in Arabic and be helpful.
//";

//                var userPrompt = "Say hello";

//                var res = await _ai.AskAsync(systemPrompt, userPrompt);

//                return Ok(new
//                {
//                    success = true,
//                    data = res
//                });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new
//                {
//                    success = false,
//                    error = ex.Message
//                });
//            }
//        }

//        // 🔥 Chat الحقيقي
//        [HttpPost("chat")]
//        public async Task<IActionResult> Chat([FromBody] ChatRequest request)
//        {
//            try
//            {
//                if (request == null || string.IsNullOrEmpty(request.Message))
//                {
//                    return BadRequest(new
//                    {
//                        success = false,
//                        message = "Message is required"
//                    });
//                }

//                // 🔥 أهم سطر (userId من التوكن)
//                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

//                if (string.IsNullOrEmpty(userId))
//                {
//                    return Unauthorized(new
//                    {
//                        success = false,
//                        message = "User not authenticated"
//                    });
//                }

//                var result = await _diagnosis.HandleChat(request.Message, _ai, userId);

//                return Ok(new
//                {
//                    success = true,
//                    data = result
//                });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new
//                {
//                    success = false,
//                    error = ex.Message
//                });
//            }
//        }
//    }

//    // 🔹 DTO
//    public class ChatRequest
//    {
//        public string Message { get; set; }
//    }
//}