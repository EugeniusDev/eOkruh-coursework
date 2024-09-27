using eOkruh.Common;
using eOkruh.Common.DataProcessing;
using eOkruh.Domain.Personnel;

namespace eOkruh.Presentation.Pages.Tabs;

public partial class PersonnelTab : ContentPage
{
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
            PersonnelManager.specialPropertyInfoTuples[wantedRank].Item1, Strings.confirm);
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
            PersonnelManager.specialPropertyInfoTuples[wantedRank].Item2, Strings.confirm);
    }

    private async void DatabaseDeleteBtn_Clicked(object sender, EventArgs e)
    {
        bool isDeletionConfirmed = await DisplayAlert(Strings.attention, "Це безповоротня дія, " +
            "базу даних буде неможливо відновити",
            Strings.confirm, Strings.cancel, FlowDirection.LeftToRight);
        if (isDeletionConfirmed)
        {
            await NeoDeleter.DeleteUserDatabase();
        }
    }

    private void RankPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        string? pickedRank = RankPicker.SelectedItem.ToString();
        if (pickedRank is null || 
            !PersonnelManager.specialPropertyInfoTuples.TryGetValue(pickedRank, 
                out (string, string) specialPropsTuple))
        {
            return;
        }

        SpecialProp1_Label.Text = specialPropsTuple.Item1;
        SpecialProp2_Label.Text = specialPropsTuple.Item2;
    }
}