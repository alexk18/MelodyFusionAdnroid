using MelodyFusion.DLL.Models;
using MelodyFusion.DLL.Service;
using Microcharts;
using SkiaSharp;
using Microcharts.Maui;
using MelodyFusion.DLL.Models.Request;
using CommunityToolkit.Maui.Alerts;

namespace MelodyFusionAdnroid.Pages;

public partial class StatisticPage : ContentPage
{

    private readonly StatisticService _statisticService;
    public StatisticPage(StatisticService statisticService)
	{
		InitializeComponent();
        _statisticService = statisticService;

    }

    public async void BackToAdminClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//{nameof(AdminControlPage)}");
    }

    public async void LoginStatisctiClicked(object sender, EventArgs e)
    {

        var _statisticRequest = new StatisticRequest()
        {
            DateFrom = datePickerFrom.Date,
            DateTo = datePickerTo.Date
        };
        var LoginStatistic = await _statisticService.GetLoginInfo(_statisticRequest);
        var entries = new List<ChartEntry>();

        if (LoginStatistic?.MonthlyLoginCount != null)
        {
            foreach (var monthlyLoginCount in LoginStatistic.MonthlyLoginCount)
            {
                entries.Add(new ChartEntry(monthlyLoginCount.MonthTotalLogins ?? 0)
                {
                    Label = "Total logins",
                    ValueLabel = (monthlyLoginCount.MonthTotalLogins ?? 0).ToString(),
                    Color = SKColor.Parse("#266489")
                });

                entries.Add(new ChartEntry(monthlyLoginCount.MonthTotalRegistrations ?? 0)
                {
                    Label = "Total registrations",
                    ValueLabel = (monthlyLoginCount.MonthTotalRegistrations ?? 0).ToString(),
                    Color = SKColor.Parse("#68B9C0")
                });
            }
        }
        var toast = Toast.Make("Success", CommunityToolkit.Maui.Core.ToastDuration.Long, 24);
        toast.Show();
        var chart = new BarChart { Entries = entries, LabelTextSize = 40 };
        chartView.Chart = chart;
    }

    public async void UserStatisticClicked(object sender, EventArgs e)
    {

        var _statisticRequest = new StatisticRequest()
        {
            DateFrom = datePickerFrom.Date,
            DateTo = datePickerTo.Date
        };
        var userStatistic = await _statisticService.GetUserInfo(_statisticRequest);
        var entries = new List<ChartEntry>();

        //if (LoginStatistic?.MonthlyLoginCount != null)
        //{
        //    foreach (var monthlyLoginCount in LoginStatistic.MonthlyLoginCount)
        //    {
        //        entries.Add(new ChartEntry(monthlyLoginCount.MonthTotalLogins ?? 0)
        //        {
        //            Label = "Total logins",
        //            ValueLabel = (monthlyLoginCount.MonthTotalLogins ?? 0).ToString(),
        //            Color = SKColor.Parse("#266489")
        //        });

        //        entries.Add(new ChartEntry(monthlyLoginCount.MonthTotalRegistrations ?? 0)
        //        {
        //            Label = "Total registrations",
        //            ValueLabel = (monthlyLoginCount.MonthTotalRegistrations ?? 0).ToString(),
        //            Color = SKColor.Parse("#68B9C0")
        //        });
        //    }
        //}

        var chart = new BarChart { Entries = entries, LabelTextSize = 40 };
        chartView.Chart = chart;
    }
}