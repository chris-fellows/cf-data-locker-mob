namespace CFDataLocker
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(EditDataItemPage), typeof(EditDataItemPage));
            Routing.RegisterRoute(nameof(EditBankAccountPage), typeof(EditBankAccountPage));
            Routing.RegisterRoute(nameof(EditCreditCardPage), typeof(EditCreditCardPage));
            Routing.RegisterRoute(nameof(EditDocumentPage), typeof(EditDocumentPage));
        }
    }
}
