using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PM2E11073.Modelos;
using Plugin.Media;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Xamarin.Essentials;
using System.Threading;

namespace PM2E11073.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrearSitio : ContentPage
    {
        CancellationTokenSource cts;
        public CrearSitio()
        {
            InitializeComponent();
        }
        byte[] GuardarImagen;

        private async void btnAgregar_Clicked(object sender, EventArgs e)
        {
            var sitio = new Sitios
            {
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
                await DisplayAlert("Aviso", "Sitio Registrado con Exito!!!", "Ok");
                await App.BaseDatos.GuardarSitio(sitio);
            }

            await Navigation.PopAsync();
        }

        private async void btnListaSitio_Clicked(object sender, EventArgs e)
        {
            var sitio = new Vistas.SitiosPage();
            await Navigation.PushAsync(sitio);
        }

        private void btnSalir_Clicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
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

    }
}