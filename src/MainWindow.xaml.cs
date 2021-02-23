using System;
using System.Collections.Generic;
using System.Windows;
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

            clearTix(AllTxt);

            var usedRect = new List<Rectangle>();
            for (var i = 0; i < Minute; i++)
                RectSet(usedRect, MinTix2, 6);

            usedRect = new List<Rectangle>();
            for (var i = 0; i < Minute2; i++)
                RectSet(usedRect, MinTix1, 9);

            usedRect = new List<Rectangle>();
            for (var i = 0; i < Hour; i++)
                RectSet(usedRect, HorTix1, 3);

            usedRect = new List<Rectangle>();
            for (var i = 0; i < Hour2; i++)
                RectSet(usedRect, HorTix2, 9);
        }

        private void RectSet(List<Rectangle> usedRect, List<Rectangle> MinTix, int max)
        {
            var Rnd = new Random();

            var rect = MinTix[Rnd.Next(0, max)];
            if (usedRect.Contains(rect))
            {
                RectSet(usedRect, MinTix, max);
            }
            else
            {
                rect.Visibility = Visibility.Visible;
                usedRect.Add(rect);
            }
        }

        private void clearTix(List<Rectangle> index)
        {
            foreach (var rect in index)
                rect.Visibility = Visibility.Hidden;
        }
    }
}