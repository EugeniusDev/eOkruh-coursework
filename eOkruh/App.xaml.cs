using eOkruh.Common.DataProcessing;

namespace eOkruh
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override async void CleanUp()
        {
            await DatabaseAccessor.CloseDatabaseConnection();
            base.CleanUp();
        }
    }
}
