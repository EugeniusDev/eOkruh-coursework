using eOkruh.Domain.MilitaryStructures;
using eOkruh.Domain.Personnel;
using eOkruh.Domain.Property;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace eOkruh.Common.DataProcessing
{
    public static class NeoFiller
    {
        public static async Task CreateSampleData()
        {
            using var session = NeoAccessor.driver.AsyncSession();

            List<MilitaryPerson> persons =
            [
                new() { FullName = "Шевченко Іван Олександрович ", Specialities = "Навідник", SpecialProperty1 = "20.10.2023", SpecialProperty2 = "Україна" },
                new() { FullName = "Коваленко Андрій Петрович ", Specialities = "Механік", SpecialProperty1 = "15.11.2023", SpecialProperty2 = "Україна" },
                new() { FullName = "Бондаренко Олексій Михайлович ", Specialities = "Снайпер", SpecialProperty1 = "05.12.2023", SpecialProperty2 = "Україна" },
                new() { FullName = "Соловйов Юрій Васильович ", Specialities = "Кулеметник", SpecialProperty1 = "12.01.2024", SpecialProperty2 = "Польща" },
                new() { FullName = "Литвиненко Микола Сергійович ", Specialities = "Гранатометник", SpecialProperty1 = "20.02.2024", SpecialProperty2 = "Україна" },
                new() { FullName = "Романчук Віталій Іванович ", Specialities = "Піхотинець", SpecialProperty1 = "28.03.2024", SpecialProperty2 = "Словаччина" },
                new() { FullName = "Гаврилюк Сергій Андрійович ", Specialities = "Медик", SpecialProperty1 = "15.04.2024", SpecialProperty2 = "Чехія" },
                new() { FullName = "Тимошенко Олег Миколайович ", Specialities = "Розвідник", SpecialProperty1 = "25.05.2024", SpecialProperty2 = "Молдова" },
                new() { FullName = "Козак Анатолій Іванович ", Specialities = "Механік", SpecialProperty1 = "10.06.2024", SpecialProperty2 = "Румунія" },
                new() { FullName = "Кудряшов Дмитро Павлович ", Specialities = "Кулеметник", SpecialProperty1 = "22.07.2024", SpecialProperty2 = "Латвія" },
                // 10 ordinary
                new() { FullName = "Гончар Вадим Юрійович ", Specialities = "Гранатометник", SpecialProperty1 = "05.08.2024", SpecialProperty2 = "Україна" },
                new() { FullName = "Носов Олександр Віталійович ", Specialities = "Піхотинець", SpecialProperty1 = "18.09.2024", SpecialProperty2 = "Україна" },
                new() { FullName = "Лебедєв Олег Олександрович ", Specialities = "Медик", SpecialProperty1 = "02.10.2024", SpecialProperty2 = "Польща" },
                new() { FullName = "Лозовий Юрій Володимирович ", Specialities = "Розвідник", SpecialProperty1 = "15.11.2024", SpecialProperty2 = "Україна" },
                new() { FullName = "Черниш Андрій Михайлович ", Specialities = "Механік", SpecialProperty1 = "03.12.2024", SpecialProperty2 = "Словаччина" },
                new() { FullName = "Герасимчук Сергій Віталійович ", Specialities = "Кулеметник", SpecialProperty1 = "20.01.2024", SpecialProperty2 = "Чехія" },
                new() { FullName = "Пронін Віталій Андрійович ", Specialities = "Гранатометник", SpecialProperty1 = "05.02.2024", SpecialProperty2 = "Молдова" },
                new() { FullName = "Марченко Дмитро Олексійович ", Specialities = "Піхотинець", SpecialProperty1 = "12.03.2024", SpecialProperty2 = "Румунія" },
                new() { FullName = "Кравченко Олександр Сергійович ", Specialities = "Медик", SpecialProperty1 = "25.04.2024", SpecialProperty2 = "Латвія" },
                new() { FullName = "Климчук Юрій Іванович ", Specialities = "Розвідник", SpecialProperty1 = "5", SpecialProperty2 = "15.10.2023" },
                // 20 ordinary
                new() { FullName = "Іваненко Анатолій Миколайович ", Specialities = "Механік", SpecialProperty1 = "8", SpecialProperty2 = "22.11.2023" },
                new() { FullName = "Тимчук Ігор Васильович ", Specialities = "Кулеметник", SpecialProperty1 = "7", SpecialProperty2 = "10.12.2023" },
                new() { FullName = "Бакум Олексій Віталійович ", Specialities = "Гранатометник", SpecialProperty1 = "9", SpecialProperty2 = "20.01.2024" },
                new() { FullName = "Карпенко Володимир Павлович ", Specialities = "Піхотинець", SpecialProperty1 = "6", SpecialProperty2 = "15.02.2024" },
                new() { FullName = "Гончаренко Сергій Олексійович ", Specialities = "Медик", SpecialProperty1 = "12", SpecialProperty2 = "28.03.2024" },
                new() { FullName = "Шаповалов Олег Андрійович ", Specialities = "Розвідник", SpecialProperty1 = "4", SpecialProperty2 = "10.04.2024" },
                new() { FullName = "Сорокін Віталій Миколайович ", Specialities = "Механік", SpecialProperty1 = "10", SpecialProperty2 = "22.05.2024" },
                new() { FullName = "Власов Дмитро Іванович ", Specialities = "Кулеметник", SpecialProperty1 = "6", SpecialProperty2 = "15.06.2024" },
                new() { FullName = "Павленко Юрій Васильович ", Specialities = "Гранатометник", SpecialProperty1 = "8", SpecialProperty2 = "01.07.2024" },
                new() { FullName = "Коваль Анатолій Олександрович ", Specialities = "Піхотинець", SpecialProperty1 = "7", SpecialProperty2 = "18.08.2024" },
                // 30 ordinary
                new() { FullName = "Ребров Сергій Михайлович ", Specialities = "Медик", SpecialProperty1 = "11", SpecialProperty2 = "10.09.2024" },
                new() { FullName = "Вакуленко Олександр Володимирович ", Specialities = "Розвідник", SpecialProperty1 = "5", SpecialProperty2 = "25.10.2024" },
                new() { FullName = "Корнієнко Іван Іванович ", Specialities = "Механік", SpecialProperty1 = "9", SpecialProperty2 = "15.11.2024" },
                new() { FullName = "Савченко Юрій Віталійович ", Specialities = "Кулеметник", SpecialProperty1 = "7", SpecialProperty2 = "10.12.2024" },
                new() { FullName = "Кравець Андрій Сергійович ", Specialities = "Гранатометник", SpecialProperty1 = "6", SpecialProperty2 = "20.01.2025" },
                new() { FullName = "Євсеєв Олег Павлович ", Specialities = "Піхотинець", SpecialProperty1 = "8", SpecialProperty2 = "05.02.2022" },
                new() { FullName = "Мороз Сергій Олександрович ", Specialities = "Медик", SpecialProperty1 = "10", SpecialProperty2 = "15.03.2022" },
                new() { FullName = "Кононенко Дмитро Михайлович ", Specialities = "Розвідник", SpecialProperty1 = "5", SpecialProperty2 = "10.04.2022" },
                new() { FullName = "Грек Анатолій Володимирович ", Specialities = "Механік", SpecialProperty1 = "7", SpecialProperty2 = "25.05.2022" },
                new() { FullName = "Жук Юрій Іванович ", Specialities = "Кулеметник", SpecialProperty1 = "9", SpecialProperty2 = "18.06.2022" },
                // 40 ordinary
                new() { FullName = "Горбач Володимир Сергійович ", Specialities = "Гранатометник", SpecialProperty1 = "6", SpecialProperty2 = "10.07.2022" },
                new() { FullName = "Гончар Олексій Павлович ", Specialities = "Піхотинець", SpecialProperty1 = "12", SpecialProperty2 = "01.08.2022" },
                new() { FullName = "Дячук Ігор Олександрович ", Specialities = "Медик", SpecialProperty1 = "8", SpecialProperty2 = "15.09.2022" },
                new() { FullName = "Чорний Андрій Віталійович ", Specialities = "Розвідник", SpecialProperty1 = "9", SpecialProperty2 = "05.10.2022" },
                new() { FullName = "Яценко Дмитро Михайлович ", Specialities = "Механік", SpecialProperty1 = "10", SpecialProperty2 = "25.11.2022" },
                new() { FullName = "Дерев'янко Олександр Юрійович", Specialities = "Кулеметник", SpecialProperty1 = "6", SpecialProperty2 = "10.12.2022" },
                new() { FullName = "Кравчук Олексій Володимирович ", Specialities = "Медик", SpecialProperty1 = "7", SpecialProperty2 = "15.03.2021" },
                new() { FullName = "Мельник Сергій Олександрович ", Specialities = "Розвідник", SpecialProperty1 = "8", SpecialProperty2 = "10.04.2022" },
                new() { FullName = "Литвинчук Дмитро Олексійович ", Specialities = "Механік", SpecialProperty1 = "6", SpecialProperty2 = "20.05.2021" },
                new() { FullName = "Шевченко Юрій Сергійович ", Specialities = "Кулеметник", SpecialProperty1 = "11", SpecialProperty2 = "25.06.2021" },
                // 50 ordinary
                new() { FullName = "Сало Анатолій Володимирович ", Specialities = "Гранатометник", SpecialProperty1 = "9", SpecialProperty2 = "10.07.2021" },
                new() { FullName = "Ткач Олександр Віталійович ", Specialities = "Піхотинець", SpecialProperty1 = "7", SpecialProperty2 = "15.08.2021" },
                new() { FullName = "Бондар Дмитро Сергійович ", Specialities = "Медик", SpecialProperty1 = "10", SpecialProperty2 = "20.09.2023" },
                new() { FullName = "Руденко Юрій Володимирович ", Specialities = "Розвідник", SpecialProperty1 = "5", SpecialProperty2 = "10.10.2023" },
                new() { FullName = "Буряк Віталій Олександрович ", Specialities = "Механік", SpecialProperty1 = "8", SpecialProperty2 = "25.11.2023" },
                new() { FullName = "Юрченко Олександр Михайлович ", Specialities = "Кулеметник", SpecialProperty1 = "6", SpecialProperty2 = "20.12.2022" },
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
            // Assigning mostly used ranks to some personnel
            int personsIterator = 0;
            for (; personsIterator <= 18; personsIterator++)
            {
                persons[personsIterator].Rank = RankRepresentations.ordinaryStrings[OrdinaryPersonnel.Recruit];
            }
            for (; personsIterator <= 45; personsIterator++)
            {
                persons[personsIterator].Rank = RankRepresentations.ordinaryStrings[OrdinaryPersonnel.Soldier];
            }
            for (; personsIterator <= 55; personsIterator++)
            {
                persons[personsIterator].Rank = RankRepresentations.ordinaryStrings[OrdinaryPersonnel.SeniorSoldier];
            }

            foreach (var p in persons)
            {
                await NeoSaver.SavePerson(p);
            }

            List<Structure> structures =
            [
                // Branches
                new() { Name = "Відділення 1", SpecialProperty = "Буря" },
                new() { Name = "Відділення 2", SpecialProperty = "Сокіл" },
                new() { Name = "Відділення 3", SpecialProperty = "Хвиля" },
                new() { Name = "Відділення 4", SpecialProperty = "Вітер" },
                new() { Name = "Відділення 5", SpecialProperty = "Грім" },
                new() { Name = "Відділення 6", SpecialProperty = "Легіон" },
                new() { Name = "Відділення 7", SpecialProperty = "Обрій" },
                new() { Name = "Відділення 8", SpecialProperty = "Кобра" },
                new() { Name = "Відділення 9", SpecialProperty = "Щит" },
                new() { Name = "Відділення 10", SpecialProperty = "Титан" },
                new() { Name = "Відділення 11", SpecialProperty = "Арка" },
                new() { Name = "Відділення 12", SpecialProperty = "Грифон" },
                new() { Name = "Відділення 13", SpecialProperty = "Спартан" },
                new() { Name = "Відділення 14", SpecialProperty = "Орел" },
                new() { Name = "Відділення 15", SpecialProperty = "Меридіан" },
                new() { Name = "Відділення 16", SpecialProperty = "Яструб" },
                new() { Name = "Відділення 17", SpecialProperty = "Оберіг" },
                new() { Name = "Відділення 18", SpecialProperty = "Арес" },
                new() { Name = "Відділення 19", SpecialProperty = "Вежа" },
                new() { Name = "Відділення 20", SpecialProperty = "Таран" },
                new() { Name = "Відділення 21", SpecialProperty = "Клин" },
                new() { Name = "Відділення 22", SpecialProperty = "Варта" },
                new() { Name = "Відділення 23", SpecialProperty = "Хортиця" },
                new() { Name = "Відділення 24", SpecialProperty = "Небо" },
                new() { Name = "Відділення 25", SpecialProperty = "Вулкан" },
                new() { Name = "Відділення 26", SpecialProperty = "Схід" },
                new() { Name = "Відділення 27", SpecialProperty = "Фенікс" },
                new() { Name = "Відділення 28", SpecialProperty = "Арей" },
                new() { Name = "Відділення 29", SpecialProperty = "Південь" },
                new() { Name = "Відділення 30", SpecialProperty = "Одіссей" },
                new() { Name = "Відділення 31", SpecialProperty = "Альтаїр" },
                new() { Name = "Відділення 32", SpecialProperty = "Терен" },
                new() { Name = "Відділення 33", SpecialProperty = "Ікар" },
                new() { Name = "Відділення 34", SpecialProperty = "Крук" },
                new() { Name = "Відділення 35", SpecialProperty = "Барс" },
                new() { Name = "Відділення 36", SpecialProperty = "Гепард" },
                new() { Name = "Відділення 37", SpecialProperty = "Блискавка" },
                new() { Name = "Відділення 38", SpecialProperty = "Зубр" },
                new() { Name = "Відділення 39", SpecialProperty = "Сапсан" },
                new() { Name = "Відділення 40", SpecialProperty = "Лавина" },
                new() { Name = "Відділення 41", SpecialProperty = "Кентавр" },
                new() { Name = "Відділення 42", SpecialProperty = "Холод" },
                new() { Name = "Відділення 43", SpecialProperty = "Десна" },
                new() { Name = "Відділення 44", SpecialProperty = "Вихор" },
                new() { Name = "Відділення 45", SpecialProperty = "Бастіон" },
                new() { Name = "Відділення 46", SpecialProperty = "Чиназес" },
                new() { Name = "Відділення 47", SpecialProperty = "Варта" },
                new() { Name = "Відділення 48", SpecialProperty = "Дніпро" },
                new() { Name = "Відділення 49", SpecialProperty = "Караван" },
                new() { Name = "Відділення 50", SpecialProperty = "Горизонт" },
                new() { Name = "Відділення 51", SpecialProperty = "Авангард" },
                new() { Name = "Відділення 52", SpecialProperty = "Прометей" },
                new() { Name = "Відділення 53", SpecialProperty = "Світло" },
                new() { Name = "Відділення 54", SpecialProperty = "Андеграунд" },
                new() { Name = "Відділення 55", SpecialProperty = "Бумеранг" },
                new() { Name = "Відділення 56", SpecialProperty = "Кара" },
                // Platoons
                new() { Name = "Взвод 1", SpecialProperty = "Океан" },
                new() { Name = "Взвод 2", SpecialProperty = "Вулкан" },
                new() { Name = "Взвод 3", SpecialProperty = "Полюс" },
                new() { Name = "Взвод 4", SpecialProperty = "Призма" },
                new() { Name = "Взвод 5", SpecialProperty = "Сова" },
                new() { Name = "Взвод 6", SpecialProperty = "Факел" },
                new() { Name = "Взвод 7", SpecialProperty = "Обрій" },
                new() { Name = "Взвод 8", SpecialProperty = "Айсберг" },
                new() { Name = "Взвод 9", SpecialProperty = "Пегас" },
                new() { Name = "Взвод 10", SpecialProperty = "Дельта" },
                new() { Name = "Взвод 11", SpecialProperty = "Марафон" },
                new() { Name = "Взвод 12", SpecialProperty = "Стріла" },
                new() { Name = "Взвод 13", SpecialProperty = "Лабіринт" },
                new() { Name = "Взвод 14", SpecialProperty = "Меркурій" },
                new() { Name = "Взвод 15", SpecialProperty = "Колос" },
                new() { Name = "Взвод 16", SpecialProperty = "Сільпо" },
                new() { Name = "Взвод 17", SpecialProperty = "Форт" },
                new() { Name = "Взвод 18", SpecialProperty = "Говерла" },
                new() { Name = "Взвод 19", SpecialProperty = "Лють" },
                new() { Name = "Взвод 20", SpecialProperty = "Полярник" },
                new() { Name = "Взвод 21", SpecialProperty = "Галич" },
                new() { Name = "Взвод 22", SpecialProperty = "Борей" },
                new() { Name = "Взвод 23", SpecialProperty = "Стакато" },
                new() { Name = "Взвод 24", SpecialProperty = "Ліга" },
                new() { Name = "Взвод 25", SpecialProperty = "Орбіта" },
                new() { Name = "Взвод 26", SpecialProperty = "Звірі" },
                new() { Name = "Взвод 27", SpecialProperty = "Ягуар" },
                new() { Name = "Взвод 28", SpecialProperty = "Тризуб" },
                // Companies
                new() { Name = "Рота 1", SpecialProperty = "вул. Шевченка, 12, м. Львів" },
                new() { Name = "Рота 2", SpecialProperty = "просп. Миру, 45, м. Київ" },
                new() { Name = "Рота 3", SpecialProperty = "вул. Центральна, 8, м. Харків" },
                new() { Name = "Рота 4", SpecialProperty = "вул. Грушевського, 23, м. Вінниця" },
                new() { Name = "Рота 5", SpecialProperty = "вул. Соборна, 16, м. Одеса" },
                new() { Name = "Рота 6", SpecialProperty = "вул. Лесі Українки, 30, м. Дніпро" },
                new() { Name = "Рота 7", SpecialProperty = "просп. Свободи, 5, м. Тернопіль" },
                new() { Name = "Рота 8", SpecialProperty = "вул. Олександра Боднарчука, 11, м. Запоріжжя" },
                new() { Name = "Рота 9", SpecialProperty = "вул. Підкови, 19, м. Івано-Франківськ" },
                new() { Name = "Рота 10", SpecialProperty = "вул. Спортивна, 3, м. Черкаси" },
                new() { Name = "Рота 11", SpecialProperty = "вул. Січових Стрільців, 14, м. Кременчук" },
                new() { Name = "Рота 12", SpecialProperty = "просп. Незалежності, 27, м. Луцьк" },
                new() { Name = "Рота 13", SpecialProperty = "вул. Головна, 7, м. Херсон" },
                new() { Name = "Рота 14", SpecialProperty = "вул. Івана Франка, 9, м. Полтава" },
                // Bases (index from 98)
                new() { Name = "Військова частина 1", SpecialProperty = "вул. Дениса Прокопенка, 21, м. Маріуполь" },
                new() { Name = "Військова частина 2", SpecialProperty = "вул. Богдана Кротевича, 32, м. Донецьк" },
                new() { Name = "Військова частина 3", SpecialProperty = "вул. Іллі Самойленка, 6, м. Полтава" },
                new() { Name = "Військова частина 4", SpecialProperty = "просп. Андрія Білецького, 5, м. Луганськ" },
                new() { Name = "Військова частина 5", SpecialProperty = "вул. Олександра Альфьорова, 10, м. Мелітополь" },
                new() { Name = "Військова частина 6", SpecialProperty = "вул. Кобзаря, 18, м. Житомир" },
                new() { Name = "Військова частина 7", SpecialProperty = "вул. Зоряна, 2, м. Суми" },
                new() { Name = "Військова частина 8", SpecialProperty = "вул. Пам'ятна, 4, м. Біла Церква" },
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
            // Assigning mostly used types to some of structures
            int structuresIterator = 0;
            for (; structuresIterator <= 55; structuresIterator++)
            {
                structures[structuresIterator].Type = StructureTypeStringPairs.typeStrings[StructureTypes.Branch];
            }
            for (; structuresIterator <= 83; structuresIterator++)
            {
                structures[structuresIterator].Type = StructureTypeStringPairs.typeStrings[StructureTypes.Platoon];
            }
            for (; structuresIterator <= 97; structuresIterator++)
            {
                structures[structuresIterator].Type = StructureTypeStringPairs.typeStrings[StructureTypes.Company];
            }
            for (; structuresIterator <= 105; structuresIterator++)
            {
                structures[structuresIterator].Type = StructureTypeStringPairs.typeStrings[StructureTypes.Base];
            }

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

            List<Weapon> assaultRifles =
            [
                new() { Name = "AR-15", SpecialProperty1 = "5.56 мм", SpecialProperty2 = "600 м" },
                new() { Name = "АК-74", SpecialProperty1 = "5.45 мм", SpecialProperty2 = "500 м" },
                new() { Name = "G36", SpecialProperty1 = "5.56 мм", SpecialProperty2 = "800 м" }
            ];
            assaultRifles.ForEach(w => w.Type = PropertyTypeRepresentations.weaponStrings[WeaponTypes.AssaultRifle]);
            List<Weapon> sniperRifles = 
            [
                new() { Name = "СВД", SpecialProperty1 = "7.62 мм", SpecialProperty2 = "800 м" },
                new() { Name = "Barrett M82", SpecialProperty1 = "12.7 мм", SpecialProperty2 = "1800 м" },
                new() { Name = "Accuracy International AXMC", SpecialProperty1 = "7.62 мм / .338 Lapua Magnum", SpecialProperty2 = "1500 м" }
            ];
            sniperRifles.ForEach(w => w.Type = PropertyTypeRepresentations.weaponStrings[WeaponTypes.SniperRifle]);
            List<Weapon> antitankRocketComplexes = 
            [
                new() { Name = "Javelin", SpecialProperty1 = "1000 мм", SpecialProperty2 = "2.5 км" },
                new() { Name = "Stugna-P", SpecialProperty1 = "800 мм", SpecialProperty2 = "5 км" },
                new() { Name = "Konkurs", SpecialProperty1 = "700 мм", SpecialProperty2 = "4 км" }
            ];
            antitankRocketComplexes.ForEach(w => w.Type = PropertyTypeRepresentations.weaponStrings[WeaponTypes.AntitankRocketComplex]);
            List<Weapon> grenadeLaunchers = 
            [
                new() { Name = "RGW 90", SpecialProperty1 = "90 мм", SpecialProperty2 = "500 м" },
                new() { Name = "RGP-40", SpecialProperty1 = "40 мм", SpecialProperty2 = "400 м" },
                new() { Name = "M203", SpecialProperty1 = "40 мм", SpecialProperty2 = "400 м" }
            ];
            grenadeLaunchers.ForEach(w => w.Type = PropertyTypeRepresentations.weaponStrings[WeaponTypes.GrenadeLauncher]);
            List<Weapon> mortars = 
            [
                new() { Name = "M120 Тюльпан", SpecialProperty1 = "120 мм", SpecialProperty2 = "7 км" },
                new() { Name = "M120 Molot", SpecialProperty1 = "120 мм", SpecialProperty2 = "8 км" },
                new() { Name = "КБА-48М1", SpecialProperty1 = "82 мм", SpecialProperty2 = "4 км" }
            ];
            mortars.ForEach(w => w.Type = PropertyTypeRepresentations.weaponStrings[WeaponTypes.Mortar]);
            List<Weapon> artilleries = 
            [
                new() { Name = "RCH 155", SpecialProperty1 = "155 мм", SpecialProperty2 = "30 км" },
                new() { Name = "M777 Howitzer", SpecialProperty1 = "155 мм", SpecialProperty2 = "24 км" },
                new() { Name = "AS90", SpecialProperty1 = "155 мм", SpecialProperty2 = "24 км" }
            ];
            artilleries.ForEach(w => w.Type = PropertyTypeRepresentations.weaponStrings[WeaponTypes.Artillery]);
            List<Weapon> ballisticMissiles = 
            [
                new() { Name = "ATACMS (MGM-140)", SpecialProperty1 = "300 км", SpecialProperty2 = "500 кг" },
                new() { Name = "Точка У", SpecialProperty1 = "120 км", SpecialProperty2 = "480 кг" }
            ];
            ballisticMissiles.ForEach(w => w.Type = PropertyTypeRepresentations.weaponStrings[WeaponTypes.BallisticMissile]);
            List<Weapon> wingedMissiles = 
            [
                new() { Name = "Нептун (RK-360MC)", SpecialProperty1 = "280 км", SpecialProperty2 = "150 кг" },
                new() { Name = "Tomahawk (BGM-109)", SpecialProperty1 = "1600 км", SpecialProperty2 = "450 кг" }
            ];
            wingedMissiles.ForEach(w => w.Type = PropertyTypeRepresentations.weaponStrings[WeaponTypes.WingedMissile]);
            List<Weapon> reactiveVolleyFireSystems = 
            [
                new() { Name = "HIMARS (M142)", SpecialProperty1 = "227 мм", SpecialProperty2 = "80 км" },
                new() { Name = "BM21 Град", SpecialProperty1 = "122 мм", SpecialProperty2 = "40 км" }
            ];
            reactiveVolleyFireSystems.ForEach(w => w.Type = PropertyTypeRepresentations.weaponStrings[WeaponTypes.ReactiveVolleyFireSystem]);
            List<Weapon> shockUAVs =
            [
                new() { Name = "Rusoriz", SpecialProperty1 = "2.5 кг", SpecialProperty2 = "30 хв" },
                new() { Name = "Rusoriz MEGA", SpecialProperty1 = "8.5 кг", SpecialProperty2 = "20 хв" },
                new() { Name = "Queen of wasps", SpecialProperty1 = "10 кг", SpecialProperty2 = "22 хв" },
                new() { Name = "Rusni peaceda", SpecialProperty1 = "5.2 кг", SpecialProperty2 = "28 хв" }
            ];
            shockUAVs.ForEach(w => w.Type = PropertyTypeRepresentations.weaponStrings[WeaponTypes.ShockUAV]);

            List<Equipment> tanks =
            [
                new() { Name = "Leopard 1A5", SpecialProperty1 = "900 мм", SpecialProperty2 = "105 мм" },
                new() { Name = "Abrams M1A2", SpecialProperty1 = "900 мм", SpecialProperty2 = "120 мм" },
                new() { Name = "T-80UD", SpecialProperty1 = "750 мм", SpecialProperty2 = "125 мм" }
            ];
            tanks.ForEach(e => e.Type = PropertyTypeRepresentations.equipmentStrings[EquipmentTypes.Tank]);
            List<Equipment> armoredTransporters = 
            [
                new() { Name = "БТР-4", SpecialProperty1 = "14 тонн", SpecialProperty2 = "600 км" },
                new() { Name = "БТР-3Е1", SpecialProperty1 = "14 тонн", SpecialProperty2 = "600 км" }
            ];
            armoredTransporters.ForEach(e => e.Type = PropertyTypeRepresentations.equipmentStrings[EquipmentTypes.ArmoredTransporter]);
            List<Equipment> infantryBattleVehicles = 
            [
                new() { Name = "Bradley M2", SpecialProperty1 = "30 тонн", SpecialProperty2 = "25 мм автоматична гармата" },
                new() { Name = "БМП-1", SpecialProperty1 = "14 тонн", SpecialProperty2 = "75 мм автоматична гармата" },
                new() { Name = "БМП-2", SpecialProperty1 = "15 тонн", SpecialProperty2 = "30 мм автоматична гармата" }
            ];
            infantryBattleVehicles.ForEach(e => e.Type = PropertyTypeRepresentations.equipmentStrings[EquipmentTypes.InfantryBattleVehicle]);
            List<Equipment> easyArmoredVehicles = 
            [
                new() { Name = "Козак", SpecialProperty1 = "20 мм", SpecialProperty2 = "120 км/год" },
                new() { Name = "Тритон", SpecialProperty1 = "20 мм", SpecialProperty2 = "100 км/год" },
                new() { Name = "HMMWV (Humvee)", SpecialProperty1 = "20 мм", SpecialProperty2 = "113 км/год" }
            ];
            easyArmoredVehicles.ForEach(e => e.Type = PropertyTypeRepresentations.equipmentStrings[EquipmentTypes.EasyArmoredVehicle]);
            List<Equipment> helicopters = 
            [
                new() { Name = "Мі-8", SpecialProperty1 = "Ракети повітря-земля", SpecialProperty2 = "250 км/год" },
                new() { Name = "Мі-24", SpecialProperty1 = "Ракети повітря-земля, кулемет", SpecialProperty2 = "300 км/год" },
                new() { Name = "AH-64 Apache", SpecialProperty1 = "Ракети повітря-земля, кулемет", SpecialProperty2 = "300 км/год" }
            ];
            helicopters.ForEach(e => e.Type = PropertyTypeRepresentations.equipmentStrings[EquipmentTypes.Helicopter]);
            List<Equipment> fighterJets = 
            [
                new() { Name = "Су-27", SpecialProperty1 = "2 Маха", SpecialProperty2 = "Ракети повітря-повітря, повітря-земля" },
                new() { Name = "МіГ-29", SpecialProperty1 = "2.25 Маха", SpecialProperty2 = "Ракети повітря-повітря, бомби" },
                new() { Name = "F-16 Fighting Falcon", SpecialProperty1 = "2 Маха", SpecialProperty2 = "Ракети повітря-повітря, повітря-земля" }
            ];
            fighterJets.ForEach(e => e.Type = PropertyTypeRepresentations.equipmentStrings[EquipmentTypes.FighterJet]);
            List<Equipment> reconnaissanceUAVs = 
            [
                new() { Name = "PD-2", SpecialProperty1 = "220 км", SpecialProperty2 = "12 годин" },
                new() { Name = "Лелека-100", SpecialProperty1 = "100 км", SpecialProperty2 = "2.5 години" },
                new() { Name = "БпАК-МП-1 (Spectator)", SpecialProperty1 = "100 км", SpecialProperty2 = "3 години" }
            ];
            reconnaissanceUAVs.ForEach(e => e.Type = PropertyTypeRepresentations.equipmentStrings[EquipmentTypes.ReconnaissanceUAV]);
            List<Equipment> groundUVs = 
            [
                new() { Name = "KRAKEN (KRAZ)", SpecialProperty1 = "70 км", SpecialProperty2 = "100 кг" },
                new() { Name = "Grim", SpecialProperty1 = "40 км", SpecialProperty2 = "60 кг" }
            ];
            groundUVs.ForEach(e => e.Type = PropertyTypeRepresentations.equipmentStrings[EquipmentTypes.GroundUV]);
            List<Equipment> antiAircraftMissileComplexes =
            [
                new() { Name = "С300", SpecialProperty1 = "75 км", SpecialProperty2 = "27 км" },
                new() { Name = "IRIS-T SLM", SpecialProperty1 = "40 км", SpecialProperty2 = "20 км" },
                new() { Name = "MIM-104 Patriot", SpecialProperty1 = "70 км", SpecialProperty2 = "24 км" }
            ];
            antiAircraftMissileComplexes.ForEach(e => e.Type = PropertyTypeRepresentations.equipmentStrings[EquipmentTypes.AntiAircraftMissileComplex]);

            List<Weapon> allWeapons = new(assaultRifles);
            allWeapons.AddRange(sniperRifles);
            allWeapons.AddRange(antitankRocketComplexes);
            allWeapons.AddRange(grenadeLaunchers);
            allWeapons.AddRange(mortars);
            allWeapons.AddRange(artilleries);
            allWeapons.AddRange(ballisticMissiles);
            allWeapons.AddRange(wingedMissiles);
            allWeapons.AddRange(reactiveVolleyFireSystems);
            allWeapons.AddRange(shockUAVs);
            foreach (var weapon in allWeapons)
            {
                await NeoSaver.SaveWeapon(weapon);
            }
            List<Equipment> allEquipments = new(tanks);
            allEquipments.AddRange(armoredTransporters);
            allEquipments.AddRange(infantryBattleVehicles);
            allEquipments.AddRange(easyArmoredVehicles);
            allEquipments.AddRange(helicopters);
            allEquipments.AddRange(fighterJets);
            allEquipments.AddRange(reconnaissanceUAVs);
            allEquipments.AddRange(groundUVs);
            allEquipments.AddRange(antiAircraftMissileComplexes);
            foreach (var equipment in allEquipments)
            {
                await NeoSaver.SaveEquipment(equipment);
            }

            // Branches
            for (int i = 0; i < 56; i++)
            {
                await AttachWeaponsToStructure(assaultRifles, structures[i]);
                await AttachWeaponsToStructure(sniperRifles, structures[i]);
                await AttachWeaponsToStructure(antitankRocketComplexes, structures[i]);

                await AttachEquipmentsToStructure(tanks, structures[i]);
                await AttachEquipmentsToStructure(armoredTransporters, structures[i]);
                await AttachEquipmentsToStructure(infantryBattleVehicles, structures[i]);
                await AttachEquipmentsToStructure(easyArmoredVehicles, structures[i]);
            }
            // Platoons
            for (int i = 56; i < 84; i++)
            {
                await AttachWeaponsToStructure(grenadeLaunchers, structures[i]);
                await AttachWeaponsToStructure(mortars, structures[i]);
                await AttachWeaponsToStructure(artilleries, structures[i]);
                await AttachWeaponsToStructure(ballisticMissiles, structures[i]);

                await AttachEquipmentsToStructure(helicopters, structures[i]);
                await AttachEquipmentsToStructure(fighterJets, structures[i]);
                await AttachEquipmentsToStructure(reconnaissanceUAVs, structures[i]);
                await AttachEquipmentsToStructure(groundUVs, structures[i]);
            }
            // Companies
            for (int i = 84; i < 98; i++)
            {
                await AttachWeaponsToStructure(wingedMissiles, structures[i]);
                await AttachWeaponsToStructure(reactiveVolleyFireSystems, structures[i]);
                await AttachWeaponsToStructure(shockUAVs, structures[i]);

                await AttachEquipmentsToStructure(antiAircraftMissileComplexes, structures[i]);
            }
        }

        private static async Task AttachWeaponsToStructure(List<Weapon> weapons, Structure structure)
        {
            var rand = new Random();
            int randomNumber = rand.Next(0, 11);
            foreach (var weapon in weapons)
            {
                if (randomNumber >= 7)
                {
                    await NeoRelationManager.MakeHasProperty(structure, weapon);
                }
            }
        }
        private static async Task AttachEquipmentsToStructure(List<Equipment> equipments, Structure structure)
        {
            var rand = new Random();
            int randomNumber = rand.Next(0, 11);
            foreach (var equipment in equipments)
            {
                if (randomNumber <= 3)
                {
                    await NeoRelationManager.MakeHasProperty(structure, equipment);
                }
            }
        }
    }
}