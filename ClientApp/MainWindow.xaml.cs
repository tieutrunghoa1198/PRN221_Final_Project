using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using ClientApp.Networking;
namespace ClientApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Server _server;
        public MainWindow()
        {
            InitializeComponent();
            _server = new Server();
            _server.connectToServer();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int screenLeft = (int) SystemParameters.VirtualScreenLeft;
            int screenTop = (int)SystemParameters.VirtualScreenTop;
            int screenWidth = (int)SystemParameters.VirtualScreenWidth;
            int screenHeight = (int)SystemParameters.VirtualScreenHeight;
            
            Bitmap bitmapScreen = new Bitmap(screenWidth, screenHeight);

            Graphics g = Graphics.FromImage(bitmapScreen);
            g.CopyFromScreen(screenLeft, screenTop, 0, 0, bitmapScreen.Size);

            bitmapScreen.Save("G:\\" + "anh 123.png");
            
        }
    }
}
