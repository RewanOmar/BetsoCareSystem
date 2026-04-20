using BetsoCare.Core.Entities;
using BetsoCare.Infrastructure.Data;
using static System.Net.Mime.MediaTypeNames;

public static class ArticleSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (context.Articles.Any())
            return;

        var articles = new List<Article>
        {
           new Article
{
    Title = "السعار: ماذا يعني وكيف ينتقل؟",
    TitleEn = "Rabies: What does it mean and how is it transmitted?",

    Summary = "(Rabies)السعار :\r\n\r\nينتقل الفيروس عبر لعاب الحيوان المصاب إلى الجرح المفتوح عند الإنسان، وذلك غالبًا من خلال عضة أو خدش من حيوان مصاب مثل الكلاب أو القطط.\r\n\r\nيؤثر الفيروس على الدماغ والحبل الشوكي، ويُعد مرضًا قاتلًا بمجرد ظهور الأعراض، لذلك فإن الوقاية والتدخل المبكر بعد التعرض مهمان جدًا.",
    SummaryEn = "Rabies is a viral disease that affects the nervous system of both animals and humans and is usually transmitted through bites or scratches from infected animals.",

    Content = @"طرق الانتقال:

العض
الخدوش
ملامسة الأغشية المخاطية

يبدأ انتقال المرض عند انتقال لعاب الحيوان المصاب إلى شخص أو حيوان سليم.

في حالات نادرة قد ينتقل الفيروس عبر:

زراعة الأعضاء أو القرنية (من شخص إلى شخص)
الرذاذ في كهوف الخفافيش أو في المختبرات

تنبيه:
لا تلمس الحيوانات المصابة، وراقب الحيوانات، واغسل أي عضة أو خدش فورًا.",

    ContentEn = @"Rabies is a viral disease that affects the nervous system of both animals and humans. It is usually transmitted to humans through bites or scratches from infected animals such as dogs and cats. The virus spreads through the saliva of the infected animal into an open wound and affects the brain and spinal cord. Therefore, early prevention and quick action after exposure are very important.

Modes of transmission:
The disease begins when saliva from an infected animal enters a healthy person or animal through:
- Bites
- Scratches
- Contact with mucous membranes

In rare cases, the virus may spread in laboratories or bat caves through aerosols, or through corneal or organ transplants.

Warning: Do not touch infected animals and monitor any bite or scratch immediately.",

    ImageUrl = "/Images/Articles/rabies1.jpeg",
    
    Source = "World Health Organization (2024). Rabies.",
    Category = "Rabies",
    PublishDate = DateTime.Parse("2025-01-01"),
    CreatedAt = DateTime.UtcNow
},

new Article
{
    Title = "أهم أعراض السعار عند الحيوانات: كيف تميز الإصابة؟",
    TitleEn = "Main symptoms of rabies in animals: how to recognize infection?",

    Summary = "تغيرات سلوكية وعصبية تظهر على الحيوان المصاب بالسعار.",
    SummaryEn = "Behavioral and neurological changes appear in infected animals.",

    Content = @"قد لا تظهر علامات واضحة للسعار في مراحله المبكرة، لكن مع تطور الإصابة تتغير سلوك الحيوان وتظهر عليه أعراض مميزة مثل:

- عدوان غير مبرر أو هياج شديد
- سيولة لعاب غزيرة
- صعوبة في البلع
- تغير في الصوت مثل نباح غير طبيعي
- جري عشوائي واضطراب عصبي سلوكي واضح

تنبيه هام: إذا لاحظت هذه الأعراض في حيوان قريب منك، تجنب التعامل المباشر واطلب مساعدة بيطرية فورية.",

    ContentEn = @"In early stages, rabies symptoms may not be obvious, but as the disease progresses, the animal’s behavior changes and noticeable symptoms appear such as:

- Unprovoked aggression or extreme agitation
- Excessive salivation
- Difficulty swallowing
- Abnormal sounds or barking
- Random movement and clear neurological disturbance

Important warning: If you notice these symptoms in an animal near you, avoid direct contact and seek immediate veterinary help.",

    ImageUrl = "/Images/Articles/rabies2.jpeg",
    Source = "World Organisation for Animal Health (2025). Rabies.",
    Category = "Symptoms",
    PublishDate = DateTime.Parse("2025-01-02"),
    CreatedAt = DateTime.UtcNow
},
new Article
{
    Title = "هل يمكن الشفاء من السعار بعد ظهور الأعراض؟",
    TitleEn = "Can rabies be cured after symptoms appear?",

    Summary = "السعار مرض قاتل تقريباً بعد ظهور الأعراض.",
    SummaryEn = "Rabies is almost always fatal once symptoms appear.",

    Content = @"بعد التعرض لعضة أو خدش من حيوان مشتبه بإصابته بالسعار، قد تظهر الأعراض في الإنسان خلال أيام حتى أشهر، وتشمل:

المرحلة المبكرة (Prodromal Phase):
في الأيام الأولى بعد التعرض للفيروس، قد تظهر أعراض تشبه الإنفلونزا مثل:
حمى
صداع
آلام الحلق
سعال
التهاب المعدة والأمعاء
تعب عام

مرحلة الإثارة (Excitation Phase):
مع تقدم المرض يظهر:
ألم أو تنميل حول العضة
قلق
حساسية للصوت والضوء والمؤثرات اللمسية
هياج
ألم مستمر
تشنجات
هذيان وسلوك غير طبيعي

تبدو صرخات الخوف والألم كأنها نباح كلب، وذلك بسبب شلل جزئي في عضلات الأحبال الصوتية.
صعوبة شديدة في البلع (Hydrophobia: رهاب الماء).

الشكل الشللي (Paralytic Rabies):
في بعض الحالات يظهر:
هدوء غير معتاد
شلل يبدأ من مكان العضة
قد يُشخّص خطأً

المرحلة المتقدمة:
عند ظهور الأعراض المتقدمة، يكون المرض في مرحلة خطيرة للغاية، ولا يوجد علاج شافٍ بعد بدء الأعراض.

لذلك يُعد التصرف السريع والحصول على الرعاية الطبية وتلقي التطعيم فور التعرض هو العامل الحاسم للوقاية من تطور المرض.",

    ContentEn = @"Rabies is one of the most dangerous viral diseases. Once symptoms appear, it is almost always fatal. Therefore, prevention and early treatment after exposure are crucial.

Once symptoms such as fever, convulsions, and fear of water appear, survival becomes extremely unlikely.

This is why immediate medical attention after any suspicious animal bite is essential.",

    ImageUrl = "/Images/Articles/rabies3.jpeg",
    Source = "WHO",
    Category = "Awareness",
    PublishDate = DateTime.Parse("2025-01-03"),
    CreatedAt = DateTime.UtcNow
},
new Article
{
    Title = "الإسعافات الأولية بعد التعرض لعضة حيوان",
    TitleEn = "First aid after an animal bite",

    Summary = "خطوات فورية تقلل خطر الإصابة بالسعار.",
    SummaryEn = "Immediate steps to reduce rabies risk.",

    Content = @"التصرف السريع بعد التعرض لعضة أو خدش من حيوان يمكن أن يحد من خطر الإصابة بالسعار.

الإجراءات الصحيحة:

غسل الجرح بالماء الجاري والصابون لمدة 15 دقيقة على الأقل
تطهير الجرح بمطهر طبي مناسب
لا تغلق الجرح برباط ضيق أو محكم
توجّه فورًا لأقرب مستشفى لتلقي لقاح ما بعد التعرض (PEP)

تنبيه هام:
تُعتبر الإصابة حالة طوارئ",

    ContentEn = @"If bitten by an animal:
- Wash the wound with soap and water for 15 minutes
- Disinfect the wound
- Seek medical care immediately

These steps greatly reduce infection risk.",

    ImageUrl = "/Images/Articles/rabies1.jpeg",
    Source = "WHO",
    Category = "Prevention",
    PublishDate = DateTime.Parse("2025-01-04"),
    CreatedAt = DateTime.UtcNow
},
new Article
{
    Title = "كيف تحمي نفسك من السعار؟",
    TitleEn = "How to protect yourself from rabies?",

    Summary = "طرق الوقاية من السعار.",
    SummaryEn = "Ways to prevent rabies.",

    Content = @"التطعيم ضد السعار فعال بنسبة عالية جدًا في الوقاية من المرض.

أنواع اللقاحات:

لقاح التحصين للحيوانات: يُعطى للكلاب والقطط والحيوانات الأليفة بانتظام لمنع انتشار المرض
لقاح ما بعد التعرض للبشر (PEP): يُعطى بعد عضة أو خدش من حيوان مشتبه في إصابته بالسعار
لقاح ما قبل التعرض للبشر (PrEP): يُعطى للفئات الأكثر عرضة مثل الأطباء البيطريين والعاملين مع الحيوانات أو في المختبرات كوقاية قبل أي تعرض محتمل

التطعيم يقلل بشكل كبير من خطر انتقال المرض ويحافظ على صحة الحيوان والبشر معًا",

    ContentEn = @"Prevention includes:
- Vaccinating pets
- Avoiding stray animals
- Community awareness

Prevention is always the best solution.",

    ImageUrl = "/Images/Articles/rabies2.jpeg",
    Source = "WHO",
    Category = "Prevention",
    PublishDate = DateTime.Parse("2025-01-05"),
    CreatedAt = DateTime.UtcNow
},
new Article
{
    Title = "هل القطط تنقل السعار؟",
    TitleEn = "Can cats transmit rabies?",

    Summary = "نعم، القطط قد تنقل المرض.",
    SummaryEn = "Yes, cats can transmit rabies.",

    Content = @"الوقاية مسؤولية مشتركة بين أفراد المجتمع للحفاظ على الصحة العامة.

تجنب التعامل مع الحيوانات الضالة أو المشتبه بها
علّم أطفالك عدم الاقتراب من الحيوانات غير المألوفة
غسل اليدين جيدًا بعد التعامل مع الحيوانات
إبلاغ الجهات المختصة عند رؤية حيوان عدواني
تأكد من تحديث تطعيم حيوانك الأليف بانتظام",

    ContentEn = @"Cats can carry rabies if exposed to infected animals. Vaccination and monitoring are essential.",

    ImageUrl = "/Images/Articles/rabies3.jpeg",
    Source = "WHO",
    Category = "Awareness",
    PublishDate = DateTime.Parse("2025-01-06"),
    CreatedAt = DateTime.UtcNow
},
new Article
{
    Title = "هل جميع عضات الحيوانات خطيرة؟",
    TitleEn = "Are all animal bites dangerous?",

    Summary = "ليست كل العضات تنقل السعار.",
    SummaryEn = "Not all bites transmit rabies.",

    Content = @"إذا صادفت حيوانًا يظهر عليه سلوك غير طبيعي أو أعراض مشتبه بها (عدوانية، سيلان لعاب شديد، صعوبة في الحركة)، اتبع الخطوات:

خطوات آمنة:

ابقَ بعيدًا ولا تلمس الحيوان
حدّد موقعك وبلّغ الجهات البيطرية المختصة
اشرح الأعراض والسلوكيات التي لاحظتها
حاول إبعاد الآخرين عن الحيوان حتى تصل المساعدة

تحذير:
لا تتعامل مع الحيوان بنفسك",

    ContentEn = @"Not all animal bites transmit rabies, but all should be taken seriously.",

    ImageUrl = "/Images/Articles/rabies1.jpeg",
    Source = "WHO",
    Category = "Awareness",
    PublishDate = DateTime.Parse("2025-01-07"),
    CreatedAt = DateTime.UtcNow
},
new Article
{
    Title = "لماذا يعتبر السعار مرضاً مميتاً؟",
    TitleEn = "Why is rabies considered deadly?",

    Summary = "لأنه يؤثر على الجهاز العصبي.",
    SummaryEn = "Because it affects the nervous system.",

    Content = @"السعار يؤثر مباشرة على الدماغ، مما يؤدي إلى الوفاة في أغلب الحالات.",

    ContentEn = @"Rabies directly affects the brain, leading to death in most cases.",

    ImageUrl = "/Images/Articles/rabies2.jpeg",
    Source = "WHO",
    Category = "Awareness",
    PublishDate = DateTime.Parse("2025-01-08"),
    CreatedAt = DateTime.UtcNow
},
new Article
{
    Title = "هل يمكن الوقاية من السعار بالتطعيم؟",
    TitleEn = "Can rabies be prevented by vaccination?",

    Summary = "نعم التطعيم فعال جداً.",
    SummaryEn = "Yes, vaccination is very effective.",

    Content = @"التطعيم قبل أو بعد التعرض يقلل بشكل كبير من خطر الإصابة.",

    ContentEn = @"Vaccination before or after exposure significantly reduces risk.",

    ImageUrl = "/Images/Articles/rabies3.jpeg",
    Source = "WHO",
    Category = "Vaccination",
    PublishDate = DateTime.Parse("2025-01-09"),
    CreatedAt = DateTime.UtcNow
},
new Article
{
    Title = "دور التوعية في مكافحة السعار",
    TitleEn = "Role of awareness in combating rabies",

    Summary = "التوعية تقلل الإصابات.",
    SummaryEn = "Awareness reduces infections.",

    Content = @"التوعية المجتمعية تساعد في تقليل انتشار المرض بشكل كبير.",

    ContentEn = @"Community awareness helps significantly reduce disease spread.",

    ImageUrl = "/Images/Articles/rabies1.jpeg",
    Source = "WHO",
    Category = "Awareness",
    PublishDate = DateTime.Parse("2025-01-10"),
    CreatedAt = DateTime.UtcNow
},
new Article
{
    Title = "اليوم العالمي للسعار",
    TitleEn = "World Rabies Day",

    Summary = "حملة عالمية للتوعية.",
    SummaryEn = "A global awareness campaign.",

    Content = @"يتم الاحتفال باليوم العالمي للسعار لزيادة الوعي حول المرض وطرق الوقاية منه.",

    ContentEn = @"World Rabies Day is celebrated to raise awareness about the disease and prevention methods.",

    ImageUrl = "/Images/Articles/rabies2.jpeg",
    Source = "WHO",
    Category = "Awareness",
    PublishDate = DateTime.Parse("2025-01-11"),
    CreatedAt = DateTime.UtcNow
}
        };

        await context.Articles.AddRangeAsync(articles);
        await context.SaveChangesAsync();
    }
}