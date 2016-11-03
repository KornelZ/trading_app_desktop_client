using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace LGSA
{
    /// <summary>
    /// Interaction logic for LoginAndRegister.xaml
    /// </summary>
    public partial class LoginAndRegister : Window
    {
        private ViewModel.AuthenticationViewModel _authenticationVM;

        public LoginAndRegister()
        {
            _authenticationVM = new ViewModel.AuthenticationViewModel(new Model.UnitOfWork.DbUnitOfWorkFactory());
            this.DataContext = _authenticationVM;
        }

        private void textBx_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if(this.DataContext != null)
            {
                _authenticationVM.User.Password = ((PasswordBox)sender).Password;
            }
        }
        // test deleta, normalnie używamy serwisów
        //private async void LoginButton_Click(object sender, RoutedEventArgs e)
        //{
        //    Model.users u = new Model.users()
        //    {
        //        Last_Name = "System",
        //        Update_Who = 1,
        //        ID = 1,
        //        Update_Date = DateTime.Now
        //    };
        //    using (var ctx = new Model.UnitOfWork.DbUnitOfWork())
        //    {
        //        try
        //        {
        //            ctx.UserRepository.Delete(u);
        //            await ctx.Save();
        //            ctx.Rollback();
        //        }
        //        catch(Exception ex)
        //        {
        //            ctx.Rollback();
        //            MessageBox.Show(ex.InnerException.ToString());
        //        }
        //    }
        //}
    }
}
