namespace Thesis_MAUI_App;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System;
using System.Collections.Generic;
using Microsoft.Maui.Graphics;
using System.Threading.Tasks;


public partial class MainPage : ContentPage
{
    private bool isRunning = false;
    private int counter = 0;
    private List<string> stringList = new List<string>();
    private readonly Random random = new Random();

    public MainPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

    private async void OnStartButtonClicked(object sender, EventArgs e)
    {
        isRunning = true;
        StartButton.IsEnabled = false;
        StartButton.Text = "Started";

        // First loop to change the background color
        System.Threading.Timer colorTimer = null;
        colorTimer = new System.Threading.Timer(state =>
        {
            Console.WriteLine("Hello World");
            counter++;
            if (counter % 2 == 0)
            {
                Console.WriteLine($"Color loop: setting background color to red. Counter: {counter}");
                MainThread.BeginInvokeOnMainThread(() => BackgroundColor = new Color(255, 0, 0));
            }
            else
            {
                Console.WriteLine($"Color loop: setting background color to blue. Counter: {counter}");
                MainThread.BeginInvokeOnMainThread(() => BackgroundColor = new Color(0, 0, 255));
            }

            if (!isRunning)
            {
                colorTimer.Dispose();
                Console.WriteLine("Color loop stopped.");
            }

        }, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(1));

        // Second loop to insert random strings into the list
        System.Threading.Timer stringTimer = null;
        stringTimer = new System.Threading.Timer(state =>
        {
            stringList.Add(GetRandomString(200000));
            Console.WriteLine($"String loop: added a string. Total count: {stringList.Count}");

            if (!isRunning)
            {
                stringTimer.Dispose();
                Console.WriteLine("String loop stopped.");
            }

        }, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(250));

        await Task.Delay(30000);
        isRunning = false;
        StartButton.IsEnabled = true;
        StartButton.Text = "Start";
    }


    private void OnStopButtonClicked(object sender, EventArgs e)
    {
        isRunning = false;
        StartButton.IsEnabled = true;

        // Empty out the list
        stringList.Clear();
    }
    
    private string GetRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        var result = new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        return result;
    }


}