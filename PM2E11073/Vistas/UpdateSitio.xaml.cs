using PM2E11073.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Threading;

namespace PM2E11073.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateSitio : ContentPage
    {
        CancellationTokenSource cts;
        public UpdateSitio()
        {
            InitializeComponent();
        }

        byte[] GuardarImagen;

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(id.Text))
            { }
                var sitio = new Sitios
            {
                Codigo = Convert.ToInt32(id.Text),
                Latitud = latitud.Text,
                Longitud = longitud.Text,
                Descripcion = descripcion.Text,
                Imagen = GuardarImagen
            };

            //var resultado = await App.BaseDatos.GuardarSitio(sitio);

            if (String.IsNullOrEmpty(latitud.Text) || String.IsNullOrEmpty(longitud.Text) || String.IsNullOrEmpty(descripcion.Text))
            {
                await DisplayAlert("Aviso", "Ha Ocurrido un Error", "Ok");

            }
            else
            {
                await DisplayAlert("Aviso", "Persona Ingresada con Exito!!!", "Ok");
                await App.BaseDatos.GuardarSitio(sitio);
            }

            await Navigation.PopAsync();
        }

        async private void btncamara_Clicked(object sender, EventArgs e)
        {
            var TomarFoto = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "PhotoApp",
                Name = DateTime.Now.ToString() + ".jpg",
                SaveToAlbum = true
            });

            await DisplayAlert("Ubicacion de la foto: ", TomarFoto.Path, "Ok");

            if (TomarFoto != null)
            {
                GuardarImagen = null;
                MemoryStream memoryStream = new MemoryStream();

                TomarFoto.GetStream().CopyTo(memoryStream);
                GuardarImagen = memoryStream.ToArray();

                img.Source = ImageSource.FromStream(() => { return TomarFoto.GetStream(); });
            }

            try
            {

                var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));

                cts = new CancellationTokenSource();

                var location = await Geolocation.GetLocationAsync(request, cts.Token);


                if (location != null)
                {
                    latitud.Text = location.Latitude.ToString();
                    longitud.Text = location.Longitude.ToString();

                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {

                await DisplayAlert("Mi App", "Mi dispositivo no soporta GPS", "Continuar");
            }
            catch (FeatureNotEnabledException fneEx)
            {

                await DisplayAlert("Mi App", "Mi dispositivo genero un error", "Continuar");
            }
            catch (PermissionException pEx)
            {

                await DisplayAlert("Mi App", "Mi dispositivo no tiene permiso para el gps", "Continuar");
            }
            catch (Exception ex)
            {

                await DisplayAlert("Mi App", "Mi dispositivo fallo al traer mi ubicación", "Continuar");
            }
        }

        private byte[] GetImageBytes(Stream stream)
        {
            byte[] ImageBytes;
            using (var memoryStream = new System.IO.MemoryStream())
            {
                stream.CopyTo(memoryStream);
                ImageBytes = memoryStream.ToArray();
            }
            return ImageBytes;
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var sitio = await App.BaseDatos.BuscarSitio(Convert.ToInt32(id.Text));

            if (sitio != null)
            {
                await App.BaseDatos.EliminarSitio(sitio);
                await DisplayAlert("Aviso", "Sitio Eliminada con Exito!!!", "Ok");
            }
            else
            {
                await DisplayAlert("Aviso", "Ha Ocurrido un Error", "Ok");
            }

            await Navigation.PopAsync();
        }

    }
}