using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Bloxstrap.UI.Elements.Controls
{
    public partial class AmbientBackground : UserControl
    {
        private Storyboard? _drift;

        public AmbientBackground()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            SizeChanged += (_, _) => LayoutBlobs();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _drift = TryFindResource("AmbientDrift") as Storyboard;
            ApplyMode();
        }

        public void ApplyMode()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                AmbientLayer.Visibility = Visibility.Collapsed;
                StaticSheen.Visibility = Visibility.Visible;
                return;
            }

            bool performance = App.Settings.Prop.UiPerformanceMode
                || App.Settings.Prop.WPFSoftwareRender
                || App.LaunchSettings.NoGPUFlag.Active;

            if (performance)
            {
                _drift?.Stop(this);
                AmbientLayer.Visibility = Visibility.Collapsed;
                StaticSheen.Visibility = Visibility.Visible;
            }
            else
            {
                StaticSheen.Visibility = Visibility.Collapsed;
                AmbientLayer.Visibility = Visibility.Visible;
                LayoutBlobs();
                if (_drift is not null)
                    _drift.Begin(this, true);
            }
        }

        private void LayoutBlobs()
        {
            if (ActualWidth <= 0 || ActualHeight <= 0)
                return;

            Canvas.SetLeft(Blob1, ActualWidth * 0.05);
            Canvas.SetTop(Blob1, ActualHeight * 0.02);
            Canvas.SetLeft(Blob2, ActualWidth * 0.42);
            Canvas.SetTop(Blob2, ActualHeight * 0.08);
            Canvas.SetLeft(Blob3, ActualWidth * 0.12);
            Canvas.SetTop(Blob3, ActualHeight * 0.45);
        }
    }
}
