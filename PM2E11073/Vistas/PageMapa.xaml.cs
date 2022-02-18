using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.Forms.Maps;
using Xamarin.Essentials;

namespace PM2E11073.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMapa : ContentPage
    {
        public PageMapa()
        {
            //InitializeComponent();
            Xamarin.Forms.Maps.Map map = new Xamarin.Forms.Maps.Map();
            Content = map;

     
        }

    }
}
