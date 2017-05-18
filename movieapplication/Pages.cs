using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace movieapplication
{
    static class Pages
    {
        // Создаем каждую страницу один раз на все время запуска программы
        // Для этого используем static
        private static MainPage _mainPage = new MainPage();

        private static LoginPage _loginPage = new LoginPage();

        private static MainPageAsAdmin _mainPageAsAdmin = new MainPageAsAdmin();

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

            set
            {
                _mainPageAsAdmin = value;
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
