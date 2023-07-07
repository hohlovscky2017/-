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

namespace ООО__Книжный_клуб_
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Entities _context = new Entities();
        private User LoginUser;
        private bool GuestUser;
        public MainWindow()
        {
            InitializeComponent();
            ListProduct.ItemsSource = _context.Product.OrderBy(product => product.ProductName).ToList();
        }
        public MainWindow(User user = null, bool guest = false)
        {
            InitializeComponent();
            LoginUser = user;
            GuestUser = guest;
            ListProduct.ItemsSource = _context.Product.OrderBy(product => product.ProductName).ToList();
            ListProduct.ItemsSource = _context.Product.OrderBy(product => product.ProductName).ToList();
            if (GuestUser == true)
            {
                TextBlockUserInfo.Text = "Гостевой режим";
            }
            else
            {
                TextBlockUserInfo.Text = user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;
            }
            Refresh();
        }
        private void Refresh()
        {
            _context = null;
            _context = new Entities();
            ListProduct.ItemsSource = _context.Product.OrderBy(product => product.ProductName).ToList();
        }

        private void ButtonAddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (LoginUser != null)
            {
                if (LoginUser.Role.RoleName == "Администратор")
                {
                    Windows.AddProductWindow addProductWindow = new Windows.AddProductWindow();
                    addProductWindow.ShowDialog();
                    Refresh();
                }
                if (LoginUser.Role.RoleName == "Менеджер" || LoginUser.Role.RoleName == "Клиент")
                {
                    MessageBox.Show("У Вас недостаточно прав для выполнения этой операции.");
                }
            }
            if (GuestUser == true)
            {
                MessageBox.Show("Вв находитесь в Гостевом режме. Зарегистрирутесьв системе для выполнения этой операции.");
            }
        }

        private void ListProduct_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (LoginUser != null)
            {
                if (LoginUser.Role.RoleName == "Администратор")
                {
                    string Key = ((Product)ListProduct.SelectedItem).ProductArticleNumber;
                    Windows.EditDeleteProductWindow editDeleteProductWindow = new Windows.EditDeleteProductWindow(Key);
                    editDeleteProductWindow.ShowDialog();
                    Refresh();
                }
                if (LoginUser.Role.RoleName == "Менеджер" || LoginUser.Role.RoleName == "Клиент")
                {
                    MessageBox.Show("У Вас недостаточно прав для выполнения этой операции.");
                }
            }
            if (GuestUser == true)
            {
                MessageBox.Show("Вы зарегистрирована как гость. У Вас недостаточно прав для выполнения этой операции.");
            }
        }
    }
}
