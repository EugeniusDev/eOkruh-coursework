using eOkruh.Presentation.Pages;

namespace eOkruh
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        }
    }
}
