using eOkruh.Common;
using eOkruh.Common.DataProcessing;
using eOkruh.Domain.MilitaryStructures;

namespace eOkruh.Presentation.Pages.Tabs;

public partial class StructuresTab : ContentPage
{
	public StructuresTab()
	{
		InitializeComponent();
        SearchTypePicker.SelectedIndex = 0;
        StructureTypePicker.ItemsSource = StructureManager.structureTypeStrings;
    }
    private async void SpecialPropertyInfoBtn_Clicked(object sender, EventArgs e)
    {
        string wantedType = await PromptForSpecificType();
        if (wantedType is null || wantedType.Equals(Strings.cancel))
        {
            return;
        }

        await DisplayAlert($"Особлива властивість для типу \"{wantedType}\" це",
            StructureManager.specialPropertyInfos[wantedType], Strings.confirm);
    }
    private async Task<string> PromptForSpecificType()
    {
        return await DisplayActionSheet("Який тип структури вас цікавить?", Strings.cancel, null,
            [.. StructureManager.structureTypeStrings]);
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
        string? pickedType = StructureTypePicker.SelectedItem.ToString();
        if (pickedType is null ||
            !StructureManager.specialPropertyInfos.TryGetValue(pickedType,
                out string? specialProperty))
        {
            return;
        }

        SpecialProp_Label.Text = specialProperty ?? string.Empty;
    }
}