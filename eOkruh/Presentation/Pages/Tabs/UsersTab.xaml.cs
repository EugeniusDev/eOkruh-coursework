using eOkruh.Common;
using eOkruh.Common.DataProcessing;

namespace eOkruh.Presentation.Pages.Tabs;

public partial class UsersTab : ContentPage
{
	public UsersTab()
	{
		InitializeComponent();
	}

    private async void DatabaseDeleteBtn_Clicked(object sender, EventArgs e)
    {
        bool isDeletionConfirmed = await DisplayAlert(Strings.attention, "Це безповоротня дія, " +
            "дані користувачів бази даних буде неможливо відновити",
            Strings.confirm, Strings.cancel, FlowDirection.LeftToRight);
        if (isDeletionConfirmed)
        {
            await NeoDeleter.DeleteUserDatabase();
        }
    }
}