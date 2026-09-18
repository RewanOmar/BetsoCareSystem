using BetsoCare.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class VaccineReminderJob
{
    private readonly ApplicationDbContext _context;
    private readonly AppNotificationService _notificationService;

    public VaccineReminderJob(ApplicationDbContext context, AppNotificationService notificationService)
    {
        _context = context;
        _notificationService = notificationService;
    }

    public async Task Execute()
    {
        var now = DateTime.UtcNow;

        // ================= REMINDER BEFORE 1 DAY =================

        var tomorrow = now.Date.AddDays(1);

        var tomorrowVaccines = await _context.Vaccines
            .Where(v =>
                v.Date.HasValue &&
                v.Date.Value.Date == tomorrow)
            .ToListAsync();

        foreach (var v in tomorrowVaccines)
        {
            if (string.IsNullOrEmpty(v.UserId))
                continue;

            int userId = int.Parse(v.UserId);

            await _notificationService.Create(
                userId,
                "💉 Vaccine Reminder",
                "Reminder: You have a vaccination appointment tomorrow."
            );
        }

        // ================= REMINDER BEFORE FEW HOURS =================

        var after3Hours = now.AddHours(3);

        var upcomingVaccines = await _context.Vaccines
            .Where(v =>
                v.Date.HasValue &&

                // نفس اليوم
                v.Date.Value.Date == now.Date &&

                // بين دلوقتي وبعد 3 ساعات
                v.Date.Value >= now &&
                v.Date.Value <= after3Hours)
            .ToListAsync();

        foreach (var v in upcomingVaccines)
        {
            if (string.IsNullOrEmpty(v.UserId))
                continue;

            int userId = int.Parse(v.UserId);

            await _notificationService.Create(
                userId,
                "⏰ Upcoming Vaccine",
                "Your vaccination appointment is scheduled within the next 3 hours."
            );
        }
    }
}