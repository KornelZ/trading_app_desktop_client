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
        //test deleta, normalnie używamy serwisów
        //private async void LoginButton_Click(object sender, RoutedEventArgs e)
        //{
        //    Model.users_Authetication u = new Model.users_Authetication()
        //    {
        //        Update_Date = DateTime.Now,
        //        Update_Who = 1,
        //        password = "leble",
        //        users1 = new Model.users()
        //        {
        //            First_Name = "Ka",
        //            Last_Name = "Zet",
        //            Update_Date = DateTime.Now,
        //            Update_Who = 1
        //        }
        //    };
        //    using (var ctx = new Model.UnitOfWork.DbUnitOfWork())
        //    {
        //        try
        //        {
        //            ctx.AuthenticationRepository.Add(u);
        //            await ctx.Save();
        //            var users = await ctx.AuthenticationRepository.GetData(null);
        //            ctx.AuthenticationRepository.Delete(users.First());
        //            await ctx.Save();
        //            users = await ctx.AuthenticationRepository.GetData(null);
        //            ctx.Rollback();
        //        }
        //        catch (Exception ex)
        //        {
        //            ctx.Rollback();
        //            MessageBox.Show(ex.InnerException.ToString());
        //        }
        //    }
        //}
    }
}
