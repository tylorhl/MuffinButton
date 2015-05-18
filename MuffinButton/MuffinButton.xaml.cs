using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MButton
{
    /// <summary>
    /// Interaction logic for MuffinButton.xaml
    /// </summary>
    public class MuffinButton : Button
    {
        public MuffinButton()
        {
            InitializeComponent();
            this.Click += Button_Click;
        }

        private FrameworkElement GetParent(Type t)
        {
            FrameworkElement f = this;
            do
            {
                f = f.Parent as FrameworkElement;
            }
            while (f.Parent != null && f.Parent.GetType() != t);
            return f;
        }

        private Window owner;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(owner == null)
                owner = GetParent(typeof(Window)) as Window;

            if (owner != null)
            {
                Window w = new Window() { Width = 50, Height = 50 };
                w.Topmost = true;

                w.WindowStyle = WindowStyle.None;
                w.AllowsTransparency = true;
                w.Background = new SolidColorBrush(Colors.Transparent);
                w.ResizeMode = ResizeMode.NoResize;
                w.WindowStartupLocation = WindowStartupLocation.Manual;

                Random r = new Random(Convert.ToInt32(DateTime.Now.Millisecond));
                w.Left = r.Next(0, Convert.ToInt32(System.Windows.SystemParameters.PrimaryScreenWidth) - 300);
                w.Top = r.Next(0, Convert.ToInt32(System.Windows.SystemParameters.PrimaryScreenHeight) - 300);

                BitmapImage bi = new BitmapImage(new Uri(@"pack://application:,,,/MButton;component/smallmuffin.png", UriKind.Absolute));

                Image im = new Image();
                im.Source = bi;
                im.Stretch = Stretch.Fill;

                w.MouseUp += (sndr, eventArgs) =>
                    {
                        w.Close();
                    };

                w.Content = im;


                w.Owner = owner;
                w.Show();
            }
        }
    }
}
