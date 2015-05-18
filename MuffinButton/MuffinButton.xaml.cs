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
            this.Click += Button_Click;
        }

        //Just using this to get the parent window.
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

        //Used for closing purposes.
        private Window owner;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(owner == null)
                owner = GetParent(typeof(Window)) as Window;

            if (owner != null)
            {
                //Create unstyled window
                Window w = new Window();
                w.Topmost = true;

                w.WindowStyle = WindowStyle.None;
                w.AllowsTransparency = true;
                w.ShowInTaskbar = false;
                w.Background = new SolidColorBrush(Colors.Transparent);
                w.ResizeMode = ResizeMode.NoResize;
                w.WindowStartupLocation = WindowStartupLocation.Manual;

                //Random size and position
                Random r = new Random(Convert.ToInt32(DateTime.Now.Millisecond));
                int size = r.Next(50, 150);
                w.Width = size;
                w.Height = size;

                w.Left = r.Next(0, Convert.ToInt32(System.Windows.SystemParameters.PrimaryScreenWidth) - size);
                w.Top = r.Next(0, Convert.ToInt32(System.Windows.SystemParameters.PrimaryScreenHeight) - size);

                //Get the stored image. This was a pain to get right.
                BitmapImage bi = new BitmapImage(new Uri(@"pack://application:,,,/MButton;component/smallmuffin.png", UriKind.Absolute));

                Image im = new Image();
                im.Source = bi;
                im.Stretch = Stretch.Fill;

                //Let's make sure these muffins aren't permanent.
                w.MouseUp += (sndr, eventArgs) =>
                    {
                        w.Close();
                    };

                w.Content = im;

                //Set the owner so all muffins will go away when the owner closes.
                w.Owner = owner;
                w.Show();
            }
        }
    }
}
