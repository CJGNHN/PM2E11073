using PM2E11073.Controladores;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;


namespace PM2E11073
{
    public partial class App : Application
    {
        static SitiosDB basedatos;
        

        public static SitiosDB BaseDatos
        {
            get
            {
                if (basedatos == null)
                {
                    basedatos = new SitiosDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SitiosDB.db3"));
                }
                return basedatos;

            }
        }

        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
           MainPage = new NavigationPage(new Vistas.CrearSitio());

        }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
