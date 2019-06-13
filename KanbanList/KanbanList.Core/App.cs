using System;
using Acr.UserDialogs;
using AutoMapper;
using KanbanList.Core.Entities;
using KanbanList.Core.MapperProfiles;
using KanbanList.Core.Services.Interfaces;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace KanbanList.Core
{
    public class App : MvxApplication
    {

        #region Constructors

        public App()
        {

        }

        #endregion Constructors

        #region Overrides

        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            CreatableTypes()
                .EndingWith("Repository")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            CreatableTypes()
                .EndingWith("Provider")
                .AsInterfaces()
                .RegisterAsLazySingleton();


            Mvx.IoCProvider.RegisterSingleton(() => UserDialogs.Instance);

            RegisterCustomAppStart<AppStart>();

            ConfigureAutoMapper();
        }

        #endregion Overrides

        #region Methods

        private void ConfigureAutoMapper()
        {
            try
            {
                var mapperConfiguration = new MapperConfiguration(config =>
                {
                    config.AddProfile(new KanbanProfile());
                });

                IMapper mapper = mapperConfiguration.CreateMapper();

                Mvx.IoCProvider.RegisterSingleton(mapper);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        #endregion Methods
    }
}
