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
        private DispatcherTimer _dispatcherTimer;
        private List<Rectangle> _usedRect = new List<Rectangle>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Topmost = true;
            DispatcherTimer_Tick(new object(), new EventArgs());

            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += DispatcherTimer_Tick;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            _dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            var allTxt = new List<Rectangle>
            {
                A0, A1, A2, B0, B1, B2, B3, B4, B5, B6, B7, B8, C0, C1, C2, C3, C4, C5, D0, D1, D2, D3, D4, D5, D6, D7,
                D8
            };
            var horTix1 = new List<Rectangle> {A0, A1, A2};
            var horTix2 = new List<Rectangle> {B0, B1, B2, B3, B4, B5, B6, B7, B8};
            var minTix1 = new List<Rectangle> {D0, D1, D2, D3, D4, D5, D6, D7, D8};
            var minTix2 = new List<Rectangle> {C0, C1, C2, C3, C4, C5};

            var minute = int.Parse(Clock.GetTimeChar("mm", 0).ToString(), 0);
            var minute2 = int.Parse(Clock.GetTimeChar("mm", 1).ToString(), 0);
            var hour = int.Parse(Clock.GetTimeChar("HH", 0).ToString(), 0);
            var hour2 = int.Parse(Clock.GetTimeChar("HH", 1).ToString(), 0);

            ClearTix(allTxt);

            _usedRect = new List<Rectangle>();
            for (var I = 0; I < minute; I++)
                RectSet(_usedRect, minTix2, 6);

            _usedRect = new List<Rectangle>();
            for (var I = 0; I < minute2; I++)
                RectSet(_usedRect, minTix1, 9);

            _usedRect = new List<Rectangle>();
            for (var I = 0; I < hour; I++)
                RectSet(_usedRect, horTix1, 3);

            _usedRect = new List<Rectangle>();
            for (var I = 0; I < hour2; I++)
                RectSet(_usedRect, horTix2, 9);
        }

        private static void RectSet(ICollection<Rectangle> usedRect, IReadOnlyList<Rectangle> minTix, int max)
        {
            while (true)
            {
                var rnd = new Random();

                var rect = minTix[rnd.Next(0, max)];
                if (usedRect.Contains(rect))
                    continue;
                rect.Visibility = Visibility.Visible;
                usedRect.Add(rect);
                break;
            }
        }

        private static void ClearTix(IEnumerable<Rectangle> index)
        {
            foreach (var rect in index)
                rect.Visibility = Visibility.Hidden;
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