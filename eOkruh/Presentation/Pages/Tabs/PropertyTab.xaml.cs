using eOkruh.Common;
using eOkruh.Common.DataProcessing;
using eOkruh.Domain.Property;

namespace eOkruh.Presentation.Pages.Tabs;

public partial class PropertyTab : ContentPage
{
	public PropertyTab()
	{
		InitializeComponent();
        SearchTypePicker.SelectedIndex = 0;
        PropertyTypePicker.ItemsSource = PropertyManager.allPropertyTypes;
    }

    private async void FirstSpecialPropertyInfoBtn_Clicked(object sender, EventArgs e)
    {
        string wantedType = await PromptForSpecificType();
        if (wantedType is null || wantedType.Equals(Strings.cancel))
        {
            return;
        }

        await DisplayAlert($"Особлива властивість 1 для типу \"{wantedType}\" це",
            PropertyManager.specialPropertyInfoTuples[wantedType].Item1, Strings.confirm);
    }
    private async Task<string> PromptForSpecificType()
    {
        return await DisplayActionSheet("Який тип власності вас цікавить?", Strings.cancel, null,
            [.. PropertyManager.allPropertyTypes]);
    }
    private async void SecondSpecialPropertyInfoBtn_Clicked(object sender, EventArgs e)
    {
        string wantedType = await PromptForSpecificType();
        if (wantedType is null || wantedType.Equals(Strings.cancel))
        {
            return;
        }

        await DisplayAlert($"Особлива властивість 2 для типу \"{wantedType}\" це",
            PropertyManager.specialPropertyInfoTuples[wantedType].Item2, Strings.confirm);
    }

    private async void DatabaseDeleteBtn_Clicked(object sender, EventArgs e)
    {
        bool isDeletionConfirmed = await DisplayAlert(Strings.attention, "Це безповоротня дія, " +
            "базу даних буде неможливо відновити",
            Strings.confirm, Strings.cancel, FlowDirection.LeftToRight);
        if (isDeletionConfirmed)
        {
            await NeoDeleter.DeleteMainDatabase();
        }
    }

    private void TypePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        string? pickedRank = PropertyTypePicker.SelectedItem.ToString();
        if (pickedRank is null ||
            !PropertyManager.specialPropertyInfoTuples.TryGetValue(pickedRank,
                out (string, string) specialPropsTuple))
        {
            return;
        }

        SpecialProp1_Label.Text = specialPropsTuple.Item1;
        SpecialProp2_Label.Text = specialPropsTuple.Item2;
    }
}