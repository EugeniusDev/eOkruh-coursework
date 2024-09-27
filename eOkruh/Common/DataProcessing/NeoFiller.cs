using eOkruh.Domain.MilitaryStructures;
using eOkruh.Domain.Personnel;

namespace eOkruh.Common.DataProcessing
{
    public static class NeoFiller
    {
        public static async Task CreateSampleData()
        {
            using var session = NeoAccessor.driver.AsyncSession();

            List<MilitaryPerson> persons =
            [
                new() { FullName = "Шевченко Іван Олександрович ", Rank = "Рекрут", Specialities = "Навідник", SpecialProperty1 = "20.10.2023", SpecialProperty2 = "Україна" },
                new() { FullName = "Коваленко Андрій Петрович ", Rank = "Рекрут", Specialities = "Механік", SpecialProperty1 = "15.11.2023", SpecialProperty2 = "Україна" },
                new() { FullName = "Бондаренко Олексій Михайлович ", Rank = "Рекрут", Specialities = "Снайпер", SpecialProperty1 = "05.12.2023", SpecialProperty2 = "Україна" },
                new() { FullName = "Соловйов Юрій Васильович ", Rank = "Рекрут", Specialities = "Кулеметник", SpecialProperty1 = "12.01.2024", SpecialProperty2 = "Польща" },
                new() { FullName = "Литвиненко Микола Сергійович ", Rank = "Рекрут", Specialities = "Гранатометник", SpecialProperty1 = "20.02.2024", SpecialProperty2 = "Україна" },
                new() { FullName = "Романчук Віталій Іванович ", Rank = "Рекрут", Specialities = "Піхотинець", SpecialProperty1 = "28.03.2024", SpecialProperty2 = "Словаччина" },
                new() { FullName = "Гаврилюк Сергій Андрійович ", Rank = "Рекрут", Specialities = "Медик", SpecialProperty1 = "15.04.2024", SpecialProperty2 = "Чехія" },
                new() { FullName = "Тимошенко Олег Миколайович ", Rank = "Рекрут", Specialities = "Розвідник", SpecialProperty1 = "25.05.2024", SpecialProperty2 = "Молдова" },
                new() { FullName = "Козак Анатолій Іванович ", Rank = "Рекрут", Specialities = "Механік", SpecialProperty1 = "10.06.2024", SpecialProperty2 = "Румунія" },
                new() { FullName = "Кудряшов Дмитро Павлович ", Rank = "Рекрут", Specialities = "Кулеметник", SpecialProperty1 = "22.07.2024", SpecialProperty2 = "Латвія" },
                // 10 ordinary
                new() { FullName = "Гончар Вадим Юрійович ", Rank = "Рекрут", Specialities = "Гранатометник", SpecialProperty1 = "05.08.2024", SpecialProperty2 = "Україна" },
                new() { FullName = "Носов Олександр Віталійович ", Rank = "Рекрут", Specialities = "Піхотинець", SpecialProperty1 = "18.09.2024", SpecialProperty2 = "Україна" },
                new() { FullName = "Лебедєв Олег Олександрович ", Rank = "Рекрут", Specialities = "Медик", SpecialProperty1 = "02.10.2024", SpecialProperty2 = "Польща" },
                new() { FullName = "Лозовий Юрій Володимирович ", Rank = "Рекрут", Specialities = "Розвідник", SpecialProperty1 = "15.11.2024", SpecialProperty2 = "Україна" },
                new() { FullName = "Черниш Андрій Михайлович ", Rank = "Рекрут", Specialities = "Механік", SpecialProperty1 = "03.12.2024", SpecialProperty2 = "Словаччина" },
                new() { FullName = "Герасимчук Сергій Віталійович ", Rank = "Рекрут", Specialities = "Кулеметник", SpecialProperty1 = "20.01.2024", SpecialProperty2 = "Чехія" },
                new() { FullName = "Пронін Віталій Андрійович ", Rank = "Рекрут", Specialities = "Гранатометник", SpecialProperty1 = "05.02.2024", SpecialProperty2 = "Молдова" },
                new() { FullName = "Марченко Дмитро Олексійович ", Rank = "Рекрут", Specialities = "Піхотинець", SpecialProperty1 = "12.03.2024", SpecialProperty2 = "Румунія" },
                new() { FullName = "Кравченко Олександр Сергійович ", Rank = "Рекрут", Specialities = "Медик", SpecialProperty1 = "25.04.2024", SpecialProperty2 = "Латвія" },
                new() { FullName = "Климчук Юрій Іванович ", Rank = "Солдат", Specialities = "Розвідник", SpecialProperty1 = "5", SpecialProperty2 = "15.10.2023" },
                // 20 ordinary
                new() { FullName = "Іваненко Анатолій Миколайович ", Rank = "Солдат", Specialities = "Механік", SpecialProperty1 = "8", SpecialProperty2 = "22.11.2023" },
                new() { FullName = "Тимчук Ігор Васильович ", Rank = "Солдат", Specialities = "Кулеметник", SpecialProperty1 = "7", SpecialProperty2 = "10.12.2023" },
                new() { FullName = "Бакум Олексій Віталійович ", Rank = "Солдат", Specialities = "Гранатометник", SpecialProperty1 = "9", SpecialProperty2 = "20.01.2024" },
                new() { FullName = "Карпенко Володимир Павлович ", Rank = "Солдат", Specialities = "Піхотинець", SpecialProperty1 = "6", SpecialProperty2 = "15.02.2024" },
                new() { FullName = "Гончаренко Сергій Олексійович ", Rank = "Солдат", Specialities = "Медик", SpecialProperty1 = "12", SpecialProperty2 = "28.03.2024" },
                new() { FullName = "Шаповалов Олег Андрійович ", Rank = "Солдат", Specialities = "Розвідник", SpecialProperty1 = "4", SpecialProperty2 = "10.04.2024" },
                new() { FullName = "Сорокін Віталій Миколайович ", Rank = "Солдат", Specialities = "Механік", SpecialProperty1 = "10", SpecialProperty2 = "22.05.2024" },
                new() { FullName = "Власов Дмитро Іванович ", Rank = "Солдат", Specialities = "Кулеметник", SpecialProperty1 = "6", SpecialProperty2 = "15.06.2024" },
                new() { FullName = "Павленко Юрій Васильович ", Rank = "Солдат", Specialities = "Гранатометник", SpecialProperty1 = "8", SpecialProperty2 = "01.07.2024" },
                new() { FullName = "Коваль Анатолій Олександрович ", Rank = "Солдат", Specialities = "Піхотинець", SpecialProperty1 = "7", SpecialProperty2 = "18.08.2024" },
                // 30 ordinary
                new() { FullName = "Ребров Сергій Михайлович ", Rank = "Солдат", Specialities = "Медик", SpecialProperty1 = "11", SpecialProperty2 = "10.09.2024" },
                new() { FullName = "Вакуленко Олександр Володимирович ", Rank = "Солдат", Specialities = "Розвідник", SpecialProperty1 = "5", SpecialProperty2 = "25.10.2024" },
                new() { FullName = "Корнієнко Іван Іванович ", Rank = "Солдат", Specialities = "Механік", SpecialProperty1 = "9", SpecialProperty2 = "15.11.2024" },
                new() { FullName = "Савченко Юрій Віталійович ", Rank = "Солдат", Specialities = "Кулеметник", SpecialProperty1 = "7", SpecialProperty2 = "10.12.2024" },
                new() { FullName = "Кравець Андрій Сергійович ", Rank = "Солдат", Specialities = "Гранатометник", SpecialProperty1 = "6", SpecialProperty2 = "20.01.2025" },
                new() { FullName = "Євсеєв Олег Павлович ", Rank = "Солдат", Specialities = "Піхотинець", SpecialProperty1 = "8", SpecialProperty2 = "05.02.2022" },
                new() { FullName = "Мороз Сергій Олександрович ", Rank = "Солдат", Specialities = "Медик", SpecialProperty1 = "10", SpecialProperty2 = "15.03.2022" },
                new() { FullName = "Кононенко Дмитро Михайлович ", Rank = "Солдат", Specialities = "Розвідник", SpecialProperty1 = "5", SpecialProperty2 = "10.04.2022" },
                new() { FullName = "Грек Анатолій Володимирович ", Rank = "Солдат", Specialities = "Механік", SpecialProperty1 = "7", SpecialProperty2 = "25.05.2022" },
                new() { FullName = "Жук Юрій Іванович ", Rank = "Солдат", Specialities = "Кулеметник", SpecialProperty1 = "9", SpecialProperty2 = "18.06.2022" },
                // 40 ordinary
                new() { FullName = "Горбач Володимир Сергійович ", Rank = "Солдат", Specialities = "Гранатометник", SpecialProperty1 = "6", SpecialProperty2 = "10.07.2022" },
                new() { FullName = "Гончар Олексій Павлович ", Rank = "Солдат", Specialities = "Піхотинець", SpecialProperty1 = "12", SpecialProperty2 = "01.08.2022" },
                new() { FullName = "Дячук Ігор Олександрович ", Rank = "Солдат", Specialities = "Медик", SpecialProperty1 = "8", SpecialProperty2 = "15.09.2022" },
                new() { FullName = "Чорний Андрій Віталійович ", Rank = "Солдат", Specialities = "Розвідник", SpecialProperty1 = "9", SpecialProperty2 = "05.10.2022" },
                new() { FullName = "Яценко Дмитро Михайлович ", Rank = "Солдат", Specialities = "Механік", SpecialProperty1 = "10", SpecialProperty2 = "25.11.2022" },
                new() { FullName = "Дерев'янко Олександр Юрійович", Rank = "Солдат", Specialities = "Кулеметник", SpecialProperty1 = "6", SpecialProperty2 = "10.12.2022" },
                new() { FullName = "Кравчук Олексій Володимирович ", Rank = "Старший солдат", Specialities = "Медик", SpecialProperty1 = "7", SpecialProperty2 = "15.03.2021" },
                new() { FullName = "Мельник Сергій Олександрович ", Rank = "Старший солдат", Specialities = "Розвідник", SpecialProperty1 = "8", SpecialProperty2 = "10.04.2022" },
                new() { FullName = "Литвинчук Дмитро Олексійович ", Rank = "Старший солдат", Specialities = "Механік", SpecialProperty1 = "6", SpecialProperty2 = "20.05.2021" },
                new() { FullName = "Шевченко Юрій Сергійович ", Rank = "Старший солдат", Specialities = "Кулеметник", SpecialProperty1 = "11", SpecialProperty2 = "25.06.2021" },
                // 50 ordinary
                new() { FullName = "Сало Анатолій Володимирович ", Rank = "Старший солдат", Specialities = "Гранатометник", SpecialProperty1 = "9", SpecialProperty2 = "10.07.2021" },
                new() { FullName = "Ткач Олександр Віталійович ", Rank = "Старший солдат", Specialities = "Піхотинець", SpecialProperty1 = "7", SpecialProperty2 = "15.08.2021" },
                new() { FullName = "Бондар Дмитро Сергійович ", Rank = "Старший солдат", Specialities = "Медик", SpecialProperty1 = "10", SpecialProperty2 = "20.09.2023" },
                new() { FullName = "Руденко Юрій Володимирович ", Rank = "Старший солдат", Specialities = "Розвідник", SpecialProperty1 = "5", SpecialProperty2 = "10.10.2023" },
                new() { FullName = "Буряк Віталій Олександрович ", Rank = "Старший солдат", Specialities = "Механік", SpecialProperty1 = "8", SpecialProperty2 = "25.11.2023" },
                new() { FullName = "Юрченко Олександр Михайлович ", Rank = "Старший солдат", Specialities = "Кулеметник", SpecialProperty1 = "6", SpecialProperty2 = "20.12.2022" },
                // 56 ordinary


                new() { FullName = "Ткаченко Іван Петрович ", Rank = "Молодший сержант", Specialities = "Навідник", SpecialProperty1 = "5", SpecialProperty2 = "01.01.2022" },
                new() { FullName = "Мельник Петро Іванович ", Rank = "Молодший сержант", Specialities = "Механік-водій", SpecialProperty1 = "10", SpecialProperty2 = "15.06.2023" },
                new() { FullName = "Гринько Олександр Миколайович ", Rank = "Сержант", Specialities = "Командир відділення", SpecialProperty1 = "8", SpecialProperty2 = "12" },
                new() { FullName = "Сидоренко Анатолій Васильович ", Rank = "Сержант", Specialities = "Навідник-артилерист", SpecialProperty1 = "6", SpecialProperty2 = "7" },
                new() { FullName = "Трофименко Віталій Павлович ", Rank = "Старший сержант", Specialities = "Розвідник", SpecialProperty1 = "4", SpecialProperty2 = "3" },
                new() { FullName = "Карпенко Максим Миколайович ", Rank = "Старший сержант", Specialities = "Кулеметник", SpecialProperty1 = "6", SpecialProperty2 = "2" },
                new() { FullName = "Рибак Сергій Олександрович ", Rank = "Головний сержант", Specialities = "Командир взводу", SpecialProperty1 = "7", SpecialProperty2 = "18" },
                new() { FullName = "Шевченко Дмитро Андрійович ", Rank = "Головний сержант", Specialities = "Навідник, механік", SpecialProperty1 = "5", SpecialProperty2 = "15" },
                new() { FullName = "Гордієнко Олексій Петрович ", Rank = "Штаб-сержант", Specialities = "Медик", SpecialProperty1 = "3", SpecialProperty2 = "9" },
                new() { FullName = "Бондар Юрій Васильович ", Rank = "Штаб-сержант", Specialities = "Командир відділення", SpecialProperty1 = "10", SpecialProperty2 = "8" },
                // 10 sergeant
                new() { FullName = "Литвиненко Ігор Іванович ", Rank = "Майстер-сержант", Specialities = "Навідник-артилерист", SpecialProperty1 = "12", SpecialProperty2 = "6" },
                new() { FullName = "Мельничук Андрій Олександрович ", Rank = "Майстер-сержант", Specialities = "Механік-водій", SpecialProperty1 = "15", SpecialProperty2 = "7" },
                new() { FullName = "Черненко Денис Вікторович ", Rank = "Старший майстер-сержант", Specialities = "Кулеметник", SpecialProperty1 = "8", SpecialProperty2 = "5/3" },
                new() { FullName = "Гаврилюк Олександр Васильович ", Rank = "Старший майстер-сержант", Specialities = "Розвідник", SpecialProperty1 = "6", SpecialProperty2 = "4/2" },
                new() { FullName = "Яремчук Павло Миколайович ", Rank = "Головний майстер-сержант", Specialities = "Командир взводу", SpecialProperty1 = "9", SpecialProperty2 = "7" },
                new() { FullName = "Сірик Роман Андрійович ", Rank = "Головний майстер-сержант", Specialities = "Навідник", SpecialProperty1 = "11", SpecialProperty2 = "9" },
                new() { FullName = "Савчук Іван Вікторович ", Rank = "Молодший сержант", Specialities = "Розвідник", SpecialProperty1 = "6", SpecialProperty2 = "10.02.2023" },
                new() { FullName = "Паламарчук Олег Дмитрович ", Rank = "Молодший сержант", Specialities = "Кулеметник", SpecialProperty1 = "8", SpecialProperty2 = "05.03.2024" },
                new() { FullName = "Костенко Володимир Іванович ", Rank = "Сержант", Specialities = "Медик", SpecialProperty1 = "7", SpecialProperty2 = "9" },
                new() { FullName = "Харченко Михайло Павлович ", Rank = "Сержант", Specialities = "Навідник-артилерист", SpecialProperty1 = "5", SpecialProperty2 = "11" },
                // 20 sergeant
                new() { FullName = "Федоренко Богдан Сергійович ", Rank = "Старший сержант", Specialities = "Командир відділення", SpecialProperty1 = "3", SpecialProperty2 = "2" },
                new() { FullName = "Дорошенко Олексій Миколайович ", Rank = "Старший сержант", Specialities = "Механік-водій", SpecialProperty1 = "6", SpecialProperty2 = "1" },
                new() { FullName = "Долгий Тарас Іванович ", Rank = "Головний сержант", Specialities = "Кулеметник", SpecialProperty1 = "4", SpecialProperty2 = "20" },
                new() { FullName = "Зінченко Станіслав Андрійович ", Rank = "Головний сержант", Specialities = "Медик", SpecialProperty1 = "7", SpecialProperty2 = "18" },
                new() { FullName = "Коваль Ігор Васильович ", Rank = "Штаб-сержант", Specialities = "Командир відділення", SpecialProperty1 = "9", SpecialProperty2 = "6" },
                new() { FullName = "Кириленко Володимир Олексійович ", Rank = "Штаб-сержант", Specialities = "Навідник", SpecialProperty1 = "10", SpecialProperty2 = "8" },
                new() { FullName = "Ткачук Євген Олександрович ", Rank = "Майстер-сержант", Specialities = "Кулеметник", SpecialProperty1 = "12", SpecialProperty2 = "9" },
                new() { FullName = "Шевчук Максим Вікторович ", Rank = "Майстер-сержант", Specialities = "Розвідник", SpecialProperty1 = "14", SpecialProperty2 = "8" },
                // 28 sergeant


                new() { FullName = "Петренко Іван Володимирович", Rank = "Молодший лейтенант", Specialities = "Командир взводу", SpecialProperty1 = "2020", SpecialProperty2 = "90" },
                new() { FullName = "Ковальчук Олександр Миколайович", Rank = "Молодший лейтенант", Specialities = "Навідник-артилерист", SpecialProperty1 = "2019", SpecialProperty2 = "85" },
                new() { FullName = "Мельник Андрій Ігорович", Rank = "Молодший лейтенант", Specialities = "Інженер військової техніки", SpecialProperty1 = "2021", SpecialProperty2 = "88" },
                new() { FullName = "Сидоренко Дмитро Валерійович", Rank = "Лейтенант", Specialities = "Командир відділення", SpecialProperty1 = "12", SpecialProperty2 = "25" },
                new() { FullName = "Бойко Олег Вікторович", Rank = "Лейтенант", Specialities = "Інженер-оператор", SpecialProperty1 = "10", SpecialProperty2 = "30" },
                new() { FullName = "Ткаченко Володимир Олексійович", Rank = "Лейтенант", Specialities = "Механік-водій", SpecialProperty1 = "15", SpecialProperty2 = "22" },
                new() { FullName = "Шевченко Максим Михайлович", Rank = "Старший лейтенант", Specialities = "Командир танкової роти", SpecialProperty1 = "3", SpecialProperty2 = "18" },
                new() { FullName = "Григоренко Олексій Сергійович", Rank = "Старший лейтенант", Specialities = "Офіцер розвідки", SpecialProperty1 = "4", SpecialProperty2 = "15" },
                new() { FullName = "Дмитренко Сергій Васильович", Rank = "Старший лейтенант", Specialities = "Інструктор бойової підготовки", SpecialProperty1 = "5", SpecialProperty2 = "20" },
                new() { FullName = "Головко Олег Борисович", Rank = "Капітан", Specialities = "Командир батальйону", SpecialProperty1 = "12", SpecialProperty2 = "8" },
                // 10 officer
                new() { FullName = "Голуб Артем Андрійович", Rank = "Капітан", Specialities = "Прес-офіцер", SpecialProperty1 = "14", SpecialProperty2 = "10" },
                new() { FullName = "Павленко Віктор Андрійович", Rank = "Капітан", Specialities = "Командир танкової бригади", SpecialProperty1 = "14", SpecialProperty2 = "10" },
                new() { FullName = "Кравченко Валентин Володимирович", Rank = "Капітан", Specialities = "Офіцер розвідки", SpecialProperty1 = "15", SpecialProperty2 = "6" },
                new() { FullName = "Зайцев Євген Олександрович", Rank = "Майор", Specialities = "Командир полку", SpecialProperty1 = "50", SpecialProperty2 = "7" },
                new() { FullName = "Романенко Ігор Петрович", Rank = "Майор", Specialities = "Офіцер з бойової підготовки", SpecialProperty1 = "40", SpecialProperty2 = "5" },
                new() { FullName = "Кузьменко Олександр Сергійович", Rank = "Майор", Specialities = "Інструктор військової підготовки", SpecialProperty1 = "35", SpecialProperty2 = "8" },
                new() { FullName = "Гончаренко Микола Олегович", Rank = "Підполковник", Specialities = "Командир оперативної групи", SpecialProperty1 = "12", SpecialProperty2 = "6" },
                new() { FullName = "Кравчук Андрій Іванович", Rank = "Підполковник", Specialities = "Начальник штабу", SpecialProperty1 = "10", SpecialProperty2 = "7" },
                new() { FullName = "Федоренко Олександр Віталійович", Rank = "Підполковник", Specialities = "Керівник оперативного командування", SpecialProperty1 = "15", SpecialProperty2 = "5" },
                new() { FullName = "Морозов Валерій Ігорович", Rank = "Полковник", Specialities = "Начальник військової академії", SpecialProperty1 = "20", SpecialProperty2 = "8" },
                // 20 officer
                new() { FullName = "Олійник Володимир Михайлович", Rank = "Полковник", Specialities = "Офіцер стратегічного командування", SpecialProperty1 = "25", SpecialProperty2 = "9" },
                new() { FullName = "Лисенко Вадим Анатолійович", Rank = "Полковник", Specialities = "Керівник військових операцій", SpecialProperty1 = "22", SpecialProperty2 = "10" },
                new() { FullName = "Корнієнко Віталій Євгенович", Rank = "Бригадний генерал", Specialities = "Стратег військових операцій", SpecialProperty1 = "9", SpecialProperty2 = "15" },
                new() { FullName = "Назаренко Дмитро Володимирович", Rank = "Бригадний генерал", Specialities = "Керівник стратегічного планування", SpecialProperty1 = "8", SpecialProperty2 = "12" },
                new() { FullName = "Жуков Олександр Вікторович", Rank = "Генерал-майор", Specialities = "Офіцер оперативного командування", SpecialProperty1 = "5", SpecialProperty2 = "9" },
                new() { FullName = "Савченко Микола Сергійович", Rank = "Генерал-майор", Specialities = "Керівник оперативного штабу", SpecialProperty1 = "7", SpecialProperty2 = "8" },
                new() { FullName = "Тимошенко Андрій Вікторович", Rank = "Генерал-лейтенант", Specialities = "Керівник міжнародних операцій", SpecialProperty1 = "10", SpecialProperty2 = "7" },
                new() { FullName = "Василенко Олег Петрович", Rank = "Генерал-лейтенант", Specialities = "Стратегічний командир", SpecialProperty1 = "12", SpecialProperty2 = "6" },
                // 28 officer

                new() { FullName = "Сирський Олександр Станіславович", Rank = "Генерал", Specialities = "Гетьман, головнокомандуючий", SpecialProperty1 = "1", SpecialProperty2 = "7" }
                // general
            ];
            foreach (var p in persons)
            {
                await NeoSaver.SavePerson(p);
            }

            List<Structure> structures =
            [
                // Branches
                new() { Name = "Відділення 1", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Буря" },
                new() { Name = "Відділення 2", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Сокіл" },
                new() { Name = "Відділення 3", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Хвиля" },
                new() { Name = "Відділення 4", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Вітер" },
                new() { Name = "Відділення 5", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Грім" },
                new() { Name = "Відділення 6", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Легіон" },
                new() { Name = "Відділення 7", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Обрій" },
                new() { Name = "Відділення 8", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Кобра" },
                new() { Name = "Відділення 9", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Щит" },
                new() { Name = "Відділення 10", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Титан" },
                new() { Name = "Відділення 11", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Арка" },
                new() { Name = "Відділення 12", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Грифон" },
                new() { Name = "Відділення 13", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Спартан" },
                new() { Name = "Відділення 14", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Орел" },
                new() { Name = "Відділення 15", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Меридіан" },
                new() { Name = "Відділення 16", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Яструб" },
                new() { Name = "Відділення 17", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Оберіг" },
                new() { Name = "Відділення 18", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Арес" },
                new() { Name = "Відділення 19", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Вежа" },
                new() { Name = "Відділення 20", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Таран" },
                new() { Name = "Відділення 21", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Клин" },
                new() { Name = "Відділення 22", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Варта" },
                new() { Name = "Відділення 23", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Хортиця" },
                new() { Name = "Відділення 24", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Небо" },
                new() { Name = "Відділення 25", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Вулкан" },
                new() { Name = "Відділення 26", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Схід" },
                new() { Name = "Відділення 27", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Фенікс" },
                new() { Name = "Відділення 28", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Арей" },
                new() { Name = "Відділення 29", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Південь" },
                new() { Name = "Відділення 30", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Одіссей" },
                new() { Name = "Відділення 31", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Альтаїр" },
                new() { Name = "Відділення 32", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Терен" },
                new() { Name = "Відділення 33", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Ікар" },
                new() { Name = "Відділення 34", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Крук" },
                new() { Name = "Відділення 35", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Барс" },
                new() { Name = "Відділення 36", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Гепард" },
                new() { Name = "Відділення 37", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Блискавка" },
                new() { Name = "Відділення 38", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Зубр" },
                new() { Name = "Відділення 39", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Сапсан" },
                new() { Name = "Відділення 40", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Лавина" },
                new() { Name = "Відділення 41", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Кентавр" },
                new() { Name = "Відділення 42", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Холод" },
                new() { Name = "Відділення 43", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Десна" },
                new() { Name = "Відділення 44", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Вихор" },
                new() { Name = "Відділення 45", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Бастіон" },
                new() { Name = "Відділення 46", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Чиназес" },
                new() { Name = "Відділення 47", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Варта" },
                new() { Name = "Відділення 48", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Дніпро" },
                new() { Name = "Відділення 49", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Караван" },
                new() { Name = "Відділення 50", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Горизонт" },
                new() { Name = "Відділення 51", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Авангард" },
                new() { Name = "Відділення 52", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Прометей" },
                new() { Name = "Відділення 53", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Світло" },
                new() { Name = "Відділення 54", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Андеграунд" },
                new() { Name = "Відділення 55", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Бумеранг" },
                new() { Name = "Відділення 56", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch],
                    SpecialProperty = "Кара" },
                // Platoons
                new() { Name = "Взвод 1", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Platoon],
                    SpecialProperty = "Океан" },
                new() { Name = "Взвод 2", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Platoon],
                    SpecialProperty = "Вулкан" },
                new() { Name = "Взвод 3", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Platoon],
                    SpecialProperty = "Полюс" },
                new() { Name = "Взвод 4", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Platoon],
                    SpecialProperty = "Призма" },
                new() { Name = "Взвод 5", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Platoon],
                    SpecialProperty = "Сова" },
                new() { Name = "Взвод 6", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Platoon],
                    SpecialProperty = "Факел" },
                new() { Name = "Взвод 7", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Platoon],
                    SpecialProperty = "Обрій" },
                new() { Name = "Взвод 8", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Platoon],
                    SpecialProperty = "Айсберг" },
                new() { Name = "Взвод 9", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Platoon],
                    SpecialProperty = "Пегас" },
                new() { Name = "Взвод 10", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Platoon],
                    SpecialProperty = "Дельта" },
                new() { Name = "Взвод 11", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Platoon],
                    SpecialProperty = "Марафон" },
                new() { Name = "Взвод 12", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Platoon],
                    SpecialProperty = "Стріла" },
                new() { Name = "Взвод 13", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Platoon],
                    SpecialProperty = "Лабіринт" },
                new() { Name = "Взвод 14", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Platoon],
                    SpecialProperty = "Меркурій" },
                new() { Name = "Взвод 15", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Platoon],
                    SpecialProperty = "Колос" },
                new() { Name = "Взвод 16", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Platoon],
                    SpecialProperty = "Сільпо" },
                new() { Name = "Взвод 17", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Platoon],
                    SpecialProperty = "Форт" },
                new() { Name = "Взвод 18", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Platoon],
                    SpecialProperty = "Говерла" },
                new() { Name = "Взвод 19", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Platoon],
                    SpecialProperty = "Лють" },
                new() { Name = "Взвод 20", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Platoon],
                    SpecialProperty = "Полярник" },
                new() { Name = "Взвод 21", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Platoon],
                    SpecialProperty = "Галич" },
                new() { Name = "Взвод 22", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Platoon],
                    SpecialProperty = "Борей" },
                new() { Name = "Взвод 23", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Platoon],
                    SpecialProperty = "Стакато" },
                new() { Name = "Взвод 24", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Platoon],
                    SpecialProperty = "Ліга" },
                new() { Name = "Взвод 25", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Platoon],
                    SpecialProperty = "Орбіта" },
                new() { Name = "Взвод 26", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Platoon],
                    SpecialProperty = "Звірі" },
                new() { Name = "Взвод 27", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Platoon],
                    SpecialProperty = "Ягуар" },
                new() { Name = "Взвод 28", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Platoon],
                    SpecialProperty = "Тризуб" },
                // Companies
                new() { Name = "Рота 1", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Company],
                    SpecialProperty = "вул. Шевченка, 12, м. Львів" },
                new() { Name = "Рота 2", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Company],
                    SpecialProperty = "просп. Миру, 45, м. Київ" },
                new() { Name = "Рота 3", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Company],
                    SpecialProperty = "вул. Центральна, 8, м. Харків" },
                new() { Name = "Рота 4", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Company],
                    SpecialProperty = "вул. Грушевського, 23, м. Вінниця" },
                new() { Name = "Рота 5", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Company],
                    SpecialProperty = "вул. Соборна, 16, м. Одеса" },
                new() { Name = "Рота 6", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Company],
                    SpecialProperty = "вул. Лесі Українки, 30, м. Дніпро" },
                new() { Name = "Рота 7", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Company],
                    SpecialProperty = "просп. Свободи, 5, м. Тернопіль" },
                new() { Name = "Рота 8", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Company],
                    SpecialProperty = "вул. Олександра Боднарчука, 11, м. Запоріжжя" },
                new() { Name = "Рота 9", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Company],
                    SpecialProperty = "вул. Підкови, 19, м. Івано-Франківськ" },
                new() { Name = "Рота 10", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Company],
                    SpecialProperty = "вул. Спортивна, 3, м. Черкаси" },
                new() { Name = "Рота 11", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Company],
                    SpecialProperty = "вул. Січових Стрільців, 14, м. Кременчук" },
                new() { Name = "Рота 12", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Company],
                    SpecialProperty = "просп. Незалежності, 27, м. Луцьк" },
                new() { Name = "Рота 13", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Company],
                    SpecialProperty = "вул. Головна, 7, м. Херсон" },
                new() { Name = "Рота 14", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Company],
                    SpecialProperty = "вул. Івана Франка, 9, м. Полтава" },
                // Bases (index from 98)
                new() { Name = "Військова частина 1", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Base],
                    SpecialProperty = "вул. Дениса Прокопенка, 21, м. Маріуполь" },
                new() { Name = "Військова частина 2", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Base],
                    SpecialProperty = "вул. Богдана Кротевича, 32, м. Донецьк" },
                new() { Name = "Військова частина 3", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Base],
                    SpecialProperty = "вул. Іллі Самойленка, 6, м. Полтава" },
                new() { Name = "Військова частина 4", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Base],
                    SpecialProperty = "просп. Андрія Білецького, 5, м. Луганськ" },
                new() { Name = "Військова частина 5", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Base],
                    SpecialProperty = "вул. Олександра Альфьорова, 10, м. Мелітополь" },
                new() { Name = "Військова частина 6", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Base],
                    SpecialProperty = "вул. Кобзаря, 18, м. Житомир" },
                new() { Name = "Військова частина 7", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Base],
                    SpecialProperty = "вул. Зоряна, 2, м. Суми" },
                new() { Name = "Військова частина 8", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Base],
                    SpecialProperty = "вул. Пам'ятна, 4, м. Біла Церква" },
                // Divisions
                new() { Name = "Дивізія 1", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Division],
                    SpecialProperty = "ЗСУ" },
                new() { Name = "Дивізія 2", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Division],
                    SpecialProperty = "ГУР МО" },
                // Corpses
                new() { Name = "Корпус 1", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Corps],
                    SpecialProperty = "ЗСУ" },
                new() { Name = "Корпус 2", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Corps],
                    SpecialProperty = "СБУ" },
                // Brigades
                new() { Name = "Бригада 1", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Brigade],
                    SpecialProperty = "НГУ" },
                new() { Name = "Бригада 2", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Brigade],
                    SpecialProperty = "ЗСУ" },

                new() { Name = "Армія 1", Type = StructureTypeStringPairs.typeStrings[StructureTypes.Army],
                    SpecialProperty = Strings.noData },
            ];
            foreach (var s in structures)
            {
                await NeoSaver.SaveStructure(s);
            }

            // COMMANDS relation
            for (int i = 0; i < persons.Count; i++)
            {
                await NeoRelationManager
                    .MakeCommands(persons[i], structures[i]);
            }
            // REGISTERED_IN relation
            for (int i = 0; i < 8; i++)
            {
                await NeoRelationManager
                    .MakeRegisteredIn(persons[i], structures[98]);
            }
            for (int i = 56; i < 60; i++)
            {
                await NeoRelationManager
                    .MakeRegisteredIn(persons[i], structures[98]);
            }
            for (int i = 84; i < 86; i++)
            {
                await NeoRelationManager
                    .MakeRegisteredIn(persons[i], structures[98]);
            }

            for (int i = 8; i < 16; i++)
            {
                await NeoRelationManager
                    .MakeRegisteredIn(persons[i], structures[99]);
            }
            for (int i = 60; i < 64; i++)
            {
                await NeoRelationManager
                    .MakeRegisteredIn(persons[i], structures[99]);
            }
            for (int i = 86; i < 88; i++)
            {
                await NeoRelationManager
                    .MakeRegisteredIn(persons[i], structures[99]);
            }

            for (int i = 16; i < 24; i++)
            {
                await NeoRelationManager
                    .MakeRegisteredIn(persons[i], structures[100]);
            }
            for (int i = 64; i < 68; i++)
            {
                await NeoRelationManager
                    .MakeRegisteredIn(persons[i], structures[100]);
            }
            for (int i = 88; i < 90; i++)
            {
                await NeoRelationManager
                    .MakeRegisteredIn(persons[i], structures[100]);
            }

            for (int i = 24; i < 32; i++)
            {
                await NeoRelationManager
                    .MakeRegisteredIn(persons[i], structures[101]);
            }
            for (int i = 68; i < 72; i++)
            {
                await NeoRelationManager
                    .MakeRegisteredIn(persons[i], structures[101]);
            }
            for (int i = 90; i < 92; i++)
            {
                await NeoRelationManager
                    .MakeRegisteredIn(persons[i], structures[101]);
            }

            for (int i = 32; i < 40; i++)
            {
                await NeoRelationManager
                    .MakeRegisteredIn(persons[i], structures[102]);
            }
            for (int i = 72; i < 76; i++)
            {
                await NeoRelationManager
                    .MakeRegisteredIn(persons[i], structures[102]);
            }
            for (int i = 92; i < 94; i++)
            {
                await NeoRelationManager
                    .MakeRegisteredIn(persons[i], structures[102]);
            }

            for (int i = 40; i < 48; i++)
            {
                await NeoRelationManager
                    .MakeRegisteredIn(persons[i], structures[103]);
            }
            for (int i = 76; i < 80; i++)
            {
                await NeoRelationManager
                    .MakeRegisteredIn(persons[i], structures[103]);
            }
            for (int i = 94; i < 96; i++)
            {
                await NeoRelationManager
                    .MakeRegisteredIn(persons[i], structures[103]);
            }

            for (int i = 48; i < 56; i++)
            {
                await NeoRelationManager
                    .MakeRegisteredIn(persons[i], structures[104]);
            }
            for (int i = 80; i < 84; i++)
            {
                await NeoRelationManager
                    .MakeRegisteredIn(persons[i], structures[104]);
            }
            for (int i = 96; i < 98; i++)
            {
                await NeoRelationManager
                    .MakeRegisteredIn(persons[i], structures[104]);
            }

            for (int i = 98; i < 105; i++)
            {
                await NeoRelationManager
                    .MakeRegisteredIn(persons[i], structures[i]);
            }
            await NeoRelationManager
                    .MakeRegisteredIn(persons[105], structures[104]);

            for (int i = 106, j = 98; i < persons.Count - 1; i++, j++)
            {
                await NeoRelationManager
                    .MakeRegisteredIn(persons[i], structures[j]);
            }
            await NeoRelationManager
                .MakeRegisteredIn(persons[^1], structures[98]);

            // Branches IS_PART_OF platoons
            int divider = 0;
            for (int i = 0; i < 56; i++)
            {
                int parentStrIndex = 56 + (divider / 2);
                divider++;
                await NeoRelationManager
                    .MakeStructureInStructure(structures[i], structures[parentStrIndex]);
            }
            // Platoons IS_PART_OF companies
            divider = 0;
            for (int i = 56; i < 84; i++)
            {
                int parentStrIndex = 84 + (divider / 2);
                divider++;
                await NeoRelationManager
                    .MakeStructureInStructure(structures[i], structures[parentStrIndex]);
            }
            // Companies IS_PART_OF bases
            divider = 0;
            for (int i = 84; i < 98; i++)
            {
                int parentStrIndex = 98 + (divider / 2);
                divider++;
                await NeoRelationManager
                    .MakeStructureInStructure(structures[i], structures[parentStrIndex]);
            }
            // Bases IS_PART_OF divisions, corps, brigades
            for (int i = 98, j = 106; i < 103; i++, j++)
            {
                await NeoRelationManager
                    .MakeStructureInStructure(structures[i], structures[j]);
            }
            for (int i = 103; i < 106; i++)
            {
                await NeoRelationManager
                    .MakeStructureInStructure(structures[i], structures[111]);
            }
            // Divisions, corps, brigades IS_PART_OF army
            for (int i = 106; i < structures.Count - 1; i++)
            {
                await NeoRelationManager
                    .MakeStructureInStructure(structures[i], structures[^1]);
            }
        }
    }
}