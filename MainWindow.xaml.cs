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
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Drawing;

namespace Quan_ly_tai_nguyen_rung
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát chương trình?", "Thông báo", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void Use_name(object sender, TextChangedEventArgs e)
        {

        }

        private void btLogin(object sender, RoutedEventArgs e)
        {
            string username = usename.Text;
            string password = passwordbox.Password;

            if (CheckLogin(username, password))
            {
                MessageBox.Show("Đăng nhập thành công!");
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác.");
            }
        }

        private void btExit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool CheckLogin(string username, string password)
        {
            // Chuỗi kết nối đến cơ sở dữ liệu
            string connectionString = "Data Source = LY-HOAI-NAM; Initial Catalog = QL_TaiNguyenRung; Integrated Security = True; Trust Server Certificate = True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Truy vấn SQL để lấy mật khẩu từ cơ sở dữ liệu
                    string query = "SELECT MAT_KHAU FROM USER_TABLE WHERE TEN_DANG_NHAP = @Username";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Thêm tham số vào câu lệnh
                        command.Parameters.AddWithValue("@Username", username);

                        // Thực hiện truy vấn
                        var result = command.ExecuteScalar();

                        if (result != null)
                        {
                            string storedPassword = result.ToString(); // Lấy mật khẩu từ cơ sở dữ liệu

                            // So sánh mật khẩu
                            return storedPassword == password; // Bạn có thể thay đổi cách so sánh nếu cần mã hóa mật khẩu
                        }
                        else
                        {
                            // Không tìm thấy tên người dùng
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                    return false;
                }
            }
        }

        private void Create_Acc(object sender, RoutedEventArgs e)
        {
            CreateAccount createAccountWindow = new CreateAccount();
            this.Hide();
            createAccountWindow.Closed += (s, args) => this.Show();
            createAccountWindow.Show();
        }

        private void FogetPassWord(object sender, RoutedEventArgs e)
        {

        }
    }
}