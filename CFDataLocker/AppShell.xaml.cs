namespace CFDataLocker
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register page routes
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(EditDefaultPage), typeof(EditDefaultPage));
            Routing.RegisterRoute(nameof(EditBankAccountPage), typeof(EditBankAccountPage));
            Routing.RegisterRoute(nameof(EditCreditCardPage), typeof(EditCreditCardPage));
            Routing.RegisterRoute(nameof(EditDocumentPage), typeof(EditDocumentPage));
        }
    }
}
