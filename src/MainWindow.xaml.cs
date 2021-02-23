using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace VirtualTixClock
{
    public partial class MainWindow
    {
        private DispatcherTimer DispatcherTimer;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Topmost = true;
            DispatcherTimer_Tick(new object(), new EventArgs());

            DispatcherTimer = new DispatcherTimer();
            DispatcherTimer.Tick += DispatcherTimer_Tick;
            DispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            DispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            var AllTxt = new List<Rectangle> {A0, A1, A2, B0, B1, B2, B3, B4, B5, B6, B7, B8, C0, C1, C2, C3, C4, C5, D0, D1, D2, D3, D4, D5, D6, D7, D8};
            var HorTix1 = new List<Rectangle> {A0, A1, A2};
            var HorTix2 = new List<Rectangle> {B0, B1, B2, B3, B4, B5, B6, B7, B8};
            var MinTix1 = new List<Rectangle> {D0, D1, D2, D3, D4, D5, D6, D7, D8};
            var MinTix2 = new List<Rectangle> {C0, C1, C2, C3, C4, C5};

            var Minute = int.Parse(Clock.GetTimeChar("mm", 0).ToString(), 0);
            var Minute2 = int.Parse(Clock.GetTimeChar("mm", 1).ToString(), 0);
            var Hour = int.Parse(Clock.GetTimeChar("HH", 0).ToString(), 0);
            var Hour2 = int.Parse(Clock.GetTimeChar("HH", 1).ToString(), 0);

            ClearTix(AllTxt);

            var UsedRect = new List<Rectangle>();
            for (var I = 0; I < Minute; I++)
                RectSet(UsedRect, MinTix2, 6);

            UsedRect = new List<Rectangle>();
            for (var I = 0; I < Minute2; I++)
                RectSet(UsedRect, MinTix1, 9);

            UsedRect = new List<Rectangle>();
            for (var I = 0; I < Hour; I++)
                RectSet(UsedRect, HorTix1, 3);

            UsedRect = new List<Rectangle>();
            for (var I = 0; I < Hour2; I++)
                RectSet(UsedRect, HorTix2, 9);
        }

        private static void RectSet(ICollection<Rectangle> usedRect, IReadOnlyList<Rectangle> minTix, int max)
        {
            while (true)
            {
                var Rnd = new Random();

                var Rect = minTix[Rnd.Next(0, max)];
                if (usedRect.Contains(Rect))
                    continue;
                Rect.Visibility = Visibility.Visible;
                usedRect.Add(Rect);
                break;
            }
        }

        private static void ClearTix(IEnumerable<Rectangle> index)
        {
            foreach (var Rect in index)
                Rect.Visibility = Visibility.Hidden;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void ClickExit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ClickOnTop(object sender, RoutedEventArgs e)
        {
            Topmost = !Topmost;
        }
    }
}