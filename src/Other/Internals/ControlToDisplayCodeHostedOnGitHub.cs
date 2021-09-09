﻿using CSHTML5;
using System;
using System.Windows.Browser;
#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#endif

namespace CSHTML5.Samples.Showcase
{
    public class ControlToDisplayCodeHostedOnGitHub : ContentControl
    {
        string _filePathOnGitHub;
        string _displayedHtmlString;

        public ControlToDisplayCodeHostedOnGitHub()
        {
            this.Loaded += OnLoaded;

            VerticalContentAlignment = VerticalAlignment.Stretch;
            HorizontalContentAlignment = HorizontalAlignment.Stretch;
        }

        string GetHtmlString(string filePath)
        {
            return string.Format("<script src=\"https://emgithub.com/embed.js?target={0}&style=github&showBorder=on&showLineNumbers=on&showCopy=on\"></script>", HttpUtility.UrlEncode("https://github.com" + filePath.Substring(6)));
        }

        void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_filePathOnGitHub))
            {
                string htmlString = GetHtmlString(_filePathOnGitHub);
                DisplayHtmlString(htmlString);
            }
        }

        void DisplayHtmlString(string htmlString)
        {
#if SLMIGRATION
            var webView = new WebBrowser();
#else
            var webView = new WebView(); 
#endif
            webView.NavigateToString(htmlString);
            this.Content = webView;
            _displayedHtmlString = htmlString;
        }

        public string FilePathOnGitHub
        {
            get
            {
                return _filePathOnGitHub;
            }
            set
            {
                _filePathOnGitHub = value;

                if (this.IsLoaded)
                {
                    string htmlString = GetHtmlString(FilePathOnGitHub);
                    if (htmlString != _displayedHtmlString)
                    {
                        DisplayHtmlString(htmlString);
                    }
                }
            }
        }

    }
}
