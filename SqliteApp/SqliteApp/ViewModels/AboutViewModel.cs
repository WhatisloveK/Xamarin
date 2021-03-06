﻿using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SqliteApp.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://www.instagram.com/l_whatislove_l/"));
            OpenGitCommand = new Command(async () => await Browser.OpenAsync("https://github.com/WhatisloveK/Xamarin"));
        }

        public ICommand OpenWebCommand { get; }
        public ICommand OpenGitCommand { get; }
    }
}