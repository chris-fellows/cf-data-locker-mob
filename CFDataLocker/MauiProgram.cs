﻿using Microsoft.Extensions.Logging;
using CFDataLocker.Interfaces;
using CFDataLocker.Services;
using CFDataLocker.Models;
using CommunityToolkit.Maui;
using CFDataLocker.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Android.Runtime;
using System.Reflection;

namespace CFDataLocker
{
    public static class MauiProgram
    {        
        public static MauiApp CreateMauiApp()
        {            
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
            builder.Services.AddSingleton<IDataItemTypeService, DataItemTypeService>();
            builder.Services.RegisterAllTypes<IDataItemTypeUtilities>(new[] { Assembly.GetExecutingAssembly() });
            builder.Services.AddSingleton<IEncryptionService, AesEncryptionService>();
            builder.Services.AddSingleton<ISecureItemService, SecureStorageSecureItemService>();            

            builder.Services.AddSingleton<IDataLockerService>((options) =>
            {
                // https://www.msdevbuild.com/2022/07/How-to-use-Net-MAUI-Secure-storage-in-your-Mobile-application-iOS-Android-Windows.html
                //byte[] key = new byte[32]; // 256-bit key
                //byte[] iv = new byte[16];  // 128-bit IV                
                return new XmlDataLockerService(FileSystem.AppDataDirectory, options.GetService<IEncryptionService>());                                       
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

        /// <summary>
        /// Registers all types implementing interface
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <param name="lifetime"></param>
        private static void RegisterAllTypes<T>(this IServiceCollection services, IEnumerable<Assembly> assemblies, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(T))));
            foreach (var type in typesFromAssemblies)
            {
                services.Add(new ServiceDescriptor(typeof(T), type, lifetime));
            }
        }
    }
}
