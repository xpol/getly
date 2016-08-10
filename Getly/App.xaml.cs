using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

using NotifyIcon = System.Windows.Forms.NotifyIcon;
using ContextMenu = System.Windows.Forms.ContextMenu;
using MenuItem = System.Windows.Forms.MenuItem;

namespace Getly
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public sealed partial class App: IDisposable
    {
        private readonly NotifyIcon _tray = new NotifyIcon();

        public App()
        {
            _tray.Icon = Getly.Properties.Resources.Tray;
            _tray.ContextMenu = CreateTrayMenu();
            _tray.Visible = true;
            _tray.DoubleClick += NewTaskOrShowTasks;
        }

        private void NewTaskOrShowTasks(object sender, EventArgs e)
        {
            if (GetLinks(Clipboard.GetText()).Count == 0)
            {
                ShowTasks(sender, e);
            }
        }

        private static List<string> GetLinks(string text) // TODO: move to util class
        {
            var urlRx = new Regex(@"((https?|ftp|file)\://|www.)[A-Za-z0-9\.\-]+(/[A-Za-z0-9\?\&\=;\+!'\(\)\*\-\._~%]*)*", RegexOptions.IgnoreCase);
            var matches = urlRx.Matches(text);
            return (from Match match in matches select match.Value).ToList();
        }

        private void Cleanup(object sender, ExitEventArgs e)
        {
            _tray.Dispose();
        }


        public void Dispose()
        {
            _tray.Dispose();
        }

        private ContextMenu CreateTrayMenu()
        {
            return new ContextMenu(new[]
            {
                new MenuItem("Show", ShowTasks),
                new MenuItem("Exit", Shutdown), 
            });
        }

        private void ShowTasks(object sender, EventArgs e)
        {
            var taskView = new MainWindow();
            taskView.Show();
        }

        private void Shutdown(object sender, EventArgs e)
        {
            Shutdown();
        }
    }
}
