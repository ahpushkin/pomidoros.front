using Autofac;
using Pomidoros.Interfaces;
using Pomidoros.Services;
using Pomidoros.Utils;
using Pomidoros.View;
using Pomidoros.View.Authorization;
using Pomidoros.ViewModel;
using Xamarin.Forms;

namespace Pomidoros
{
    public partial class App : Application
    {
        public static string TestPhone = "0633430412";
        public static IContainer Container { get; set; }
        public static int CurrentLat { get; set; }
        static UserItemDatabase database;
        public static UserItemDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new UserItemDatabase();
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            InitIoc();

            //set launch page
            //pls dont change this code)
            MainPage = new NavigationPage(new LoginPage());
        }

        private static void InitIoc()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Requests>().As<IRequestsToServer>();
            builder.RegisterType<CallService>().As<ICallService>();
            builder.RegisterType<SmsService>().As<ISmsService>();

            Container = builder.Build();
        }

        //OnStart method
        protected override void OnStart()
        {
        }
        //OnSleep method
        protected override void OnSleep()
        {
        }
        //OnResum method
        protected override void OnResume()
        {
        }
    }
}
