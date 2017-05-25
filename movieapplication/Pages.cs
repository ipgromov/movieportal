using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace movieapplication
{
    static class Pages
    {
        private static MainPage _mainPage = new MainPage();

        private static LoginPage _loginPage = new LoginPage();

        private static SignUpPage _signUpPage = new SignUpPage();

        private static ControlPage _controlPage = new ControlPage();

        private static MainPageAsAdmin _mainPageAsAdmin = new MainPageAsAdmin();

        private static MainPageAsUser _mainPageAsUser = new MainPageAsUser();

        private static MainWindow _mainWindow;

        public static MainPage MainPage
        {
            get
            {
                return _mainPage;
            }
        }

        public static LoginPage LoginPage
        {
            get
            {
                return _loginPage;
            }
        }

        public static SignUpPage SignUpPage
        {
            get
            {
                return _signUpPage;
            }
        }

        public static ControlPage ControlPage
        {
            get
            {
                return _controlPage;
            }
        }

        public static MainWindow MainWindow
        {
            get
            {
                return _mainWindow;
            }

            set
            {
                _mainWindow = value;
            }
        }

        public static MainPageAsAdmin MainPageAsAdmin
        {
            get
            {
                return _mainPageAsAdmin;
            }
        }

        public static MainPageAsUser MainPageAsUser
        {
            get
            {
                return _mainPageAsUser;
            }
        }

        public static void ChangeFrameSize(int height, int width)
        {
            MainWindow.frameMain.Height = height;
            MainWindow.frameMain.Width = width;
        }

        public static void ChangeFrameSize(double height, double width)
        {
            MainWindow.frameMain.Height = height;
            MainWindow.frameMain.Width = width;
        }
    }
}
