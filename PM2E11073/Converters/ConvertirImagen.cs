using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.IO;

namespace PM2E11073.Modelos
{
        public class ConvertirImagen : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                ImageSource retSource = null;
                if (value != null)
                {
                    byte[] imageAsBytes = (byte[])value;
                    retSource = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));
                }
                return retSource;
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }

