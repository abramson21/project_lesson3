using eMailSender.Library.Users;
using System;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;

namespace eMailSender
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //???????????????? Как можно улучшить код ниже? Откуда лучше брать данные для ComboSmtp?
            ComboSmtp.Items.Add("25").ToString();
            ComboSmtp.Items.Add("465").ToString();
            ComboSmtp.Items.Add("2525").ToString();
            ComboSmtp.Items.Add("587").ToString();
            
            BSendEmail.IsEnabled = false;
        }

        /// <summary>
        /// Проверка на заполненность данных для активации кнопки.
        /// </summary>
        private void activeButton()
        {
            //???????????????? Как осуществить проверку на Password и ComboSmtp, чтобы button загорался, когда все данные есть?
            if (LoginName.Text != "" && ToName.Text != "" && Topic.Text != "" && Body.Text != "")
            {
                BSendEmail.IsEnabled = true;
            }
            else BSendEmail.IsEnabled = false;
        }
        

        /// <summary>
        /// Отправка данных на почту по click на кнопку.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSendEmail(object sender, RoutedEventArgs e)
        {
            
                Library.StaticName.GetFromEmail = LoginName.Text;
                Library.StaticName.GeToEmail = ToName.Text;

                object newSmtp = ComboSmtp.SelectedItem;
                Library.StaticName.Smtp = Convert.ToInt32(newSmtp.ToString());
                //Пользователь, кому ДОСТАВЛЕНО сообщение

                var fromUser = new BaseUser(Library.StaticName.GetFromEmail, "Andrey Nikolaev");

                //Пользователь, который отправляет
                var toUser = new BaseUser(Library.StaticName.GeToEmail, "Andrey Nikolaev");

                MailAddress to = toUser.GetInfoUser();
                MailAddress from = fromUser.GetInfoUser();

                //Создаем переменную, хранящую данные о пользователях.
                var message = new MailMessage(from, to);

                //Добавляем заголовок письма
                message.Subject = Topic.Text + DateTime.Now;

                //Добавляем тело сообщения
                message.Body = Body.Text;

                //Smtp клиент ОТПРАВИТЕЛЯ
                var client = new SmtpClient("smtp.yandex.ru", Library.StaticName.Smtp);

                //???????????????? Не понял, что это.
                client.EnableSsl = true;

                client.Credentials = new NetworkCredential
                {
                    UserName = Library.StaticName.GetFromEmail,
                    SecurePassword = Password.SecurePassword
                };

                client.Send(message);
        }

        /// <summary>
        /// Запись новых пользователей в List.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bAddNewUser_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem itemsUsers = new ListBoxItem();
            itemsUsers.Content = NewUserItem.Text;
            usersItems.Items.Add(itemsUsers);

        }

        private void usersItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //???????????????? Если нажать на копку "Выбрать нового пользователя" исключение не обрабатывается, все равно программа вылетает.
                //???????????????? Как исправить?
                var newItem = ((ListBoxItem)usersItems.SelectedItem).Content.ToString();
                if (newItem != null)
                {
                    Library.StaticName.GeToEmail = newItem;
                    ToName.Text = newItem;
                }

            }
            catch
            {
                Exception newError = new Exception("Вы ничего не выбрали!");
                newError.exError();
                newError.Show();
            }
        }

        private void LoginName_TextChanged(object sender, TextChangedEventArgs e)
        {
            activeButton();
        }

        private void ToName_TextChanged(object sender, TextChangedEventArgs e)
        {
            activeButton();
        }

        private void Topic_TextChanged(object sender, TextChangedEventArgs e)
        {
            activeButton();
        }

        private void Body_TextChanged(object sender, TextChangedEventArgs e)
        {
            activeButton();
        }
    }
}
