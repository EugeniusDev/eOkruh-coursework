using eOkruh.Common;
using eOkruh.Common.DataProcessing;
using eOkruh.Domain.Personnel;

namespace eOkruh.Presentation.Pages.Tabs;

public partial class PersonnelTab : ContentPage
{
    private static readonly Dictionary<string, (string, string)> specialPropertyInfoTuples = new()
    {
            { RankRepresentations.ordinaryStrings[OrdinaryPersonnel.Recruit],
                ("Дата початку рекрутингу", "Місце рекрутингу (країна)") },
            { RankRepresentations.ordinaryStrings[OrdinaryPersonnel.Soldier],
                ("Кількість пройдених навчальних курсів", "Дата присвоєння звання") },
            { RankRepresentations.ordinaryStrings[OrdinaryPersonnel.SeniorSoldier],
                ("Досвід участі у бойових діях (у місяцях)", "Дата підвищення в званні") },

            { RankRepresentations.sergeantStrings[SergeantPersonnel.JuniorSergeant],
                ("Кількість підлеглих", "Дата призначення на керівну посаду") },
            { RankRepresentations.sergeantStrings[SergeantPersonnel.Sergeant],
                ("Рівень бойової підготовки (за шкалою від 1 до 10)", "Кількість виконаних наказів") },
            { RankRepresentations.sergeantStrings[SergeantPersonnel.SeniorSergeant],
                ("Стаж у керівних посадах (у роках)", "Кількість нагород за службу") },
            { RankRepresentations.sergeantStrings[SergeantPersonnel.ChiefSergeant],
                ("Рівень військової кваліфікації (за шкалою від 1 до 10)", "Кількість виконаних бойових завдань") },
            { RankRepresentations.sergeantStrings[SergeantPersonnel.StaffSergeant],
                ("Кількість проведених операцій", "Рівень авторитету серед підлеглих (за шкалою від 1 до 10)") },
            { RankRepresentations.sergeantStrings[SergeantPersonnel.MasterSergeant],
                ("Тривалість служби в армії (у роках)", "Кількість проведених тренувань для підлеглих") },
            { RankRepresentations.sergeantStrings[SergeantPersonnel.SeniorMasterSergeant],
                ("Кількість проведених стратегічних операцій", "Співвідношення успішних тактичних операцій до провальних") },
            { RankRepresentations.sergeantStrings[SergeantPersonnel.ChiefMasterSergeant],
                ("Кількість успішних операцій під керівництвом", "Рівень впливу на оперативне планування") },

            { RankRepresentations.officerStrings[OfficerPersonnel.JuniorLieutenant],
                ("Рік випуску з військової академії", "Середній бал під час навчання") },
            { RankRepresentations.officerStrings[OfficerPersonnel.Lieutenant],
                ("Кількість підлеглих під час першої служби", "Кількість виконаних бойових завдань") },
            { RankRepresentations.officerStrings[OfficerPersonnel.SeniorLieutenant],
                ("Стаж у лейтенантському званні (у роках)", "Кількість командних рішень під час операцій") },
            { RankRepresentations.officerStrings[OfficerPersonnel.Captain],
                ("Кількість успішних операцій під командуванням", "Кількість нагород за військову службу") },
            { RankRepresentations.officerStrings[OfficerPersonnel.Major],
                ("Кількість підлеглих під час командування", "Тривалість служби у званні майора") },
            { RankRepresentations.officerStrings[OfficerPersonnel.LieutenantColonel],
                ("Кількість успішних стратегічних операцій", "Кількість років на керівних посадах") },
            { RankRepresentations.officerStrings[OfficerPersonnel.Colonel],
                ("Кількість проведених військових навчань", "Кількість нагород за досягнення в командуванні") },
            { RankRepresentations.officerStrings[OfficerPersonnel.BrigadeGeneral],
                ("Рівень стратегічного мислення (за шкалою від 1 до 10)", "Кількість реалізованих стратегічних планів") },
            { RankRepresentations.officerStrings[OfficerPersonnel.GeneralMajor],
                ("Кількість проведених операцій на високому рівні", "Рівень впливу на прийняття стратегічних рішень") },
            { RankRepresentations.officerStrings[OfficerPersonnel.GeneralLieutenant],
                ("Кількість років на генералітеті", "Рівень участі у міжнародних операціях (за шкалою від 1 до 10)") },
            { RankRepresentations.officerStrings[OfficerPersonnel.General],
                ("Кількість років служби у званні генерала", "Загальне ставлення особового складу до методів оборони генерала") }
    };

    public PersonnelTab()
	{
		InitializeComponent();
        SearchTypePicker.SelectedIndex = 0;
        RankPicker.ItemsSource = PersonnelManager.allRanks;
    }

    private async void FirstSpecialPropertyInfoBtn_Clicked(object sender, EventArgs e)
    {
        string wantedRank = await PromptForSpecificRank();
        if (wantedRank is null || wantedRank.Equals(Strings.cancel))
        {
            return;
        }

        await DisplayAlert($"Особлива властивість 1 для звання \"{wantedRank}\" це",
            specialPropertyInfoTuples[wantedRank].Item1, Strings.confirm);
    }
    private async Task<string> PromptForSpecificRank()
    {        
        return await DisplayActionSheet("Яке звання вас цікавить?", Strings.cancel, null,
            [.. PersonnelManager.allRanks]);
    }
    private async void SecondSpecialPropertyInfoBtn_Clicked(object sender, EventArgs e)
    {
        string wantedRank = await PromptForSpecificRank();
        if (wantedRank is null || wantedRank.Equals(Strings.cancel))
        {
            return;
        }

        await DisplayAlert($"Особлива властивість 2 для звання \"{wantedRank}\" це", 
            specialPropertyInfoTuples[wantedRank].Item2, Strings.confirm);
    }

    private async void DatabaseDeleteBtn_Clicked(object sender, EventArgs e)
    {
        bool isDeletionConfirmed = await DisplayAlert(Strings.attention, "Це безповоротня дія, " +
            "базу даних буде неможливо відновити",
            Strings.confirm, Strings.cancel, FlowDirection.LeftToRight);
        if (isDeletionConfirmed)
        {
            await DatabaseDeleter.DeleteUserDatabase();
        }
    }

    private void RankPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        string? pickedRank = RankPicker.SelectedItem.ToString();
        if (pickedRank is null || 
            !specialPropertyInfoTuples.TryGetValue(pickedRank, 
                out (string, string) specialPropsTuple))
        {
            return;
        }

        SpecialProp1_Label.Text = specialPropsTuple.Item1;
        SpecialProp2_Label.Text = specialPropsTuple.Item2;
    }
}