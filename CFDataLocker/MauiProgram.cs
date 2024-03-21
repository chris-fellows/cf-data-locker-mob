using Microsoft.Extensions.Logging;
using CFDataLocker.Interfaces;
using CFDataLocker.Services;
using CFDataLocker.Models;
using CommunityToolkit.Maui;
using CFDataLocker.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Android.Runtime;

namespace CFDataLocker
{
    public static class MauiProgram
    {        
        public static MauiApp CreateMauiApp()
        {
            //CFDataLocker.Utilities.AesEncryptionUtilities.Test();

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()      // For validation
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //builder.Services.AddSingleton<IDataLockerService, MemoryDataLockerService>();
            builder.Services.AddSingleton<IEncryptionService, AesEncryptionService>();
            builder.Services.AddSingleton<ISecureItemService, SecureStorageSecureItemService>();
            builder.Services.AddSingleton<IDataLockerService>((options) =>
            {
                // https://www.msdevbuild.com/2022/07/How-to-use-Net-MAUI-Secure-storage-in-your-Mobile-application-iOS-Android-Windows.html
                //byte[] key = new byte[32]; // 256-bit key
                //byte[] iv = new byte[16];  // 128-bit IV
                var encryptionService = options.GetService<IEncryptionService>();                
                return new XmlDataLockerService(FileSystem.AppDataDirectory, encryptionService);                                       
            });            

            // Register pages & models
            builder.Services.AddTransient<MainPageModel>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<EditDataItemPageModel>();
            builder.Services.AddTransient<EditDataItemPage>();
            builder.Services.AddTransient<EditBankAccountPageModel>();
            builder.Services.AddTransient<EditBankAccountPage>();
            builder.Services.AddTransient<EditCreditCardPageModel>();
            builder.Services.AddTransient<EditCreditCardPage>();
            builder.Services.AddTransient<EditDocumentPageModel>();
            builder.Services.AddTransient<EditDocumentPage>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
