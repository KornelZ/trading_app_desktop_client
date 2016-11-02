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
using System.Windows.Shapes;

namespace LGSA
{
    /// <summary>
    /// Interaction logic for LoginAndRegister.xaml
    /// </summary>
    public partial class LoginAndRegister : Window
    {
        public LoginAndRegister()
        {
            InitializeComponent();
        }
        //database test
        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            using (var ctx = new Model.UnitOfWork.DbUnitOfWork())
            {
                try
                {
                    ctx.AuthenticationRepository.Add(new Model.users_Authetication()
                    {
                        password = "dede",
                        Update_Date = DateTime.Now,
                        Update_Who = 1,
                        User_id = 2,
                        users1 = new Model.users()
                        {
                            First_Name = "A",
                            Last_Name = "B",
                            Update_Date = DateTime.Now,
                            Update_Who = 1
                        }
                    });

                    await ctx.Save();
                    ctx.Rollback();
                }
                catch(Exception ex)
                {
                    ctx.Rollback();
                    MessageBox.Show(ex.InnerException.ToString());
                }
            }
        }
    }
}
