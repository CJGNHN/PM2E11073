using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PM2E11073.Modelos;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E11073.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SitiosPage : ContentPage
    {

        Sitios users = new Sitios();
        public SitiosPage()
        {
            InitializeComponent();
        }



        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //ListaPersonas.ItemsSource = await App.BaseDatos.listapersonas();

            var listaSitios = await App.BaseDatos.ListaSitios();
            //Creamos un colleccion observable para que los cambios que se realizan en el modelo se reflejen de maner automatica
            //en la vista
            ObservableCollection<Sitios> observableCollectionFotos = new ObservableCollection<Sitios>();
            listasitios.ItemsSource = observableCollectionFotos;
            foreach (Sitios imagen in listaSitios)
            {
                observableCollectionFotos.Add(imagen);
            }


        }

        private async void listasitios_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                //await DisplayAlert("Aviso", "Has dado clik en una persona!!!", "Ok");
                Modelos.Sitios item = (Modelos.Sitios)e.Item;
                var newpage = new Vistas.UpdateSitio();
                newpage.BindingContext = item;
                await Navigation.PushAsync(newpage);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Desea ir a la ubicacion indicada", ex.Message, "NO", "SI");
            }
        }

        private async void btnEliminarSitio_Clicked(object sender, EventArgs e)
        {
           

            var result = await DisplayAlert("Confirmar", "Estas seguro de eliminar el registro", "Aceptar", "Cancelar");
            if (result)//True si se preciona Aceptar, y se elimina el registro.
            {
                users = (Sitios)BindingContext;
                await App.BaseDatos.EliminarSitio(users);
                await Navigation.PushAsync(new CrearSitio());
            }
            else
            {//False si se preciona Cancelar, y se cancela la elimación del registro.
                return;
            }
        }

        private void btnVerMapa_Clicked(object sender, EventArgs e)
        {

        }
    }
}