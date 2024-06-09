using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelodyFusionAdnroid.Resources.Localization;

namespace MelodyFusionAdnroid.TranslatorLan
{
    public class Translator : INotifyPropertyChanged
    {
        public static Translator Instance { get; set; } = new Translator();

        public CultureInfo CultureInfo { get; set; }

        public string this[string key]
        {
            get => AppResources.ResourceManager.GetString(key, CultureInfo);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
