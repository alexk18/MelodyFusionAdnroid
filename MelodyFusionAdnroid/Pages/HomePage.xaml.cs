using MelodyFusionAdnroid.Models;
using MelodyFusionAdnroid.Models.Request;
using MelodyFusionAdnroid.Service;
using Microsoft.Maui.Animations;
//using static Android.Icu.Text.CaseMap;

namespace MelodyFusionAdnroid.Pages;

public partial class HomePage : ContentPage
{
    private readonly SongService _songService;

    private Dictionary<string, string> _firstTrackDictionary = new Dictionary<string, string>();
    private Dictionary<string, string> _secondTrackDictionary = new Dictionary<string, string>();
    private string _firstTrackId;
    private string _secondTrackId;
    public HomePage(SongService songService)
	{
		InitializeComponent();
        _songService = songService;

	}

    public async void TestClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ProfilePage));
    }

    private async void OnEntryTextChanged(object sender, TextChangedEventArgs e)
  {
        if (e.NewTextValue.Length >= 3)
        {
            pickerFrame.IsVisible = true;
            var title = firstTrackEntry.Text;
            var songResponse = await _songService.GetSongsBySearchString(title);
            trackPicker.Items.Clear();
            _firstTrackDictionary.Clear();
            foreach (var song in songResponse)
            {
                trackPicker.Items.Add(song.Name);
                _firstTrackDictionary[song.Name] = song.Id;
            }
        }
        else
        {
            pickerFrame.IsVisible = false;
        }
    }

    private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        if (trackPicker.SelectedIndex != -1)
        {
            firstTrackEntry.Text = trackPicker.SelectedItem.ToString();
            pickerFrame.IsVisible = false; 
        }
    }

    private async void OnEntryTextChanged_sec(object sender, TextChangedEventArgs e)
    {
        if (e.NewTextValue.Length >= 3)
        {
            pickerFrame_sec.IsVisible = true;
            var title =secondTrackEntry.Text;
            var songResponse = await _songService.GetSongsBySearchString(title);
            _secondTrackDictionary.Clear();
            trackPicker_sec.Items.Clear();
            foreach (var song in songResponse)
            {
                trackPicker_sec.Items.Add(song.Name);
                _secondTrackDictionary[song.Name] = song.Id;
            }
        }
        else
        {
            pickerFrame_sec.IsVisible = false;
        }
    }

    private void OnPickerSelectedIndexChanged_sec(object sender, EventArgs e)
    {
        if (trackPicker_sec.SelectedIndex != -1)
        {
            secondTrackEntry.Text = trackPicker_sec.SelectedItem.ToString();
            pickerFrame_sec.IsVisible = false;
        }
    }

    public async void StartClicked(object sender, EventArgs e)
    {
        string firstTrackName = firstTrackEntry.Text;
        string secondTrackName = secondTrackEntry.Text; 

        if (_firstTrackDictionary.TryGetValue(firstTrackName, out var firstTrackId))
        {
            _firstTrackId = firstTrackId;
        }
        else
        {
            // Обработка ситуации, когда трек не найден
            _firstTrackId = null;
        }

        if (_secondTrackDictionary.TryGetValue(secondTrackName, out var secondTrackId))
        {
            _secondTrackId = secondTrackId;
        }
        else
        {
            // Обработка ситуации, когда трек не найден
            _secondTrackId = null;
        }
        var songRecommendationRequest = new SongRecommendationRequest
        {
            FirstSongId = _firstTrackId,
            SecondSongId = _secondTrackId
        };

        var recomendationResponse = await _songService.GetRecommendation(songRecommendationRequest);

        RecomendedTrackList.ItemsSource = recomendationResponse;

    }
}