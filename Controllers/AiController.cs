using BetsoCare.Core.DTOS;
using BetsoCare.Core.Entities;
using BetsoCare.Core.Interfaces;
using BetsoCare.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BetsoCare.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IAiService _aiService;

        public AiController(
            ApplicationDbContext context,
            IAiService aiService)
        {
            _context = context;
            _aiService = aiService;
        }

        [HttpPost("chat")]
        public async Task<IActionResult> Chat(AiRequestDto dto)
        {
            try
            {
                // ✅ هات الـ user الحالي
                var userId =
                    User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrWhiteSpace(userId))
                {
                    return Unauthorized();
                }

                // ✅ هات الـ session الحالية
                var session = await _context.ChatSessions
                    .FirstOrDefaultAsync(x => x.UserId == userId);

                // ✅ لو مفيش session اعمل واحدة
                if (session == null)
                {
                    session = new ChatSession
                    {
                        UserId = userId,

                        // ✅ أهم تعديل
                        Animal = "Unknown"
                    };

                    _context.ChatSessions.Add(session);

                    await _context.SaveChangesAsync();
                }

                // ✅ خزّن رسالة المستخدم
                _context.ChatMessages.Add(new ChatMessage
                {
                    ChatSessionId = session.Id,
                    Role = "user",
                    Content = dto.Message
                });

                await _context.SaveChangesAsync();

                // ✅ هات كل الرسائل القديمة
                var history = await _context.ChatMessages
                    .Where(x => x.ChatSessionId == session.Id)
                    .OrderBy(x => x.CreatedAt)
                    .ToListAsync();

                // ✅ حول الرسائل لـ DTO
                var messages = history.Select(x => new ChatMessageDto
                {
                    Role = x.Role,
                    Content = x.Content
                }).ToList();

                // ✅ ابعت للـ AI
                var result = await _aiService.ChatAsync(messages);

                // ✅ خزّن رد الـ AI
                _context.ChatMessages.Add(new ChatMessage
                {
                    ChatSessionId = session.Id,
                    Role = "assistant",
                    Content = result
                });

                await _context.SaveChangesAsync();

                // ✅ رجع الرد
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return StatusCode(500, ex.ToString());
            }
        }
    }
}