using eOkruh.Presentation.ViewModels;

namespace eOkruh.Presentation.Pages;

public partial class MainPage : TabbedPage
{
	public MainPage(MainViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
        NavigationPage.SetBackButtonTitle(this, "");
        NavigationPage.SetHasBackButton(this, false);
    }

    protected override void OnAppearing()
    {
        MainViewModel viewModel = (MainViewModel)BindingContext;
        viewModel.ConfigureViewModel();
        base.OnAppearing();
    }
}