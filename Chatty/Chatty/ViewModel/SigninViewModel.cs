using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Chatty.Utils;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Chatty.ViewModel
{
    public class SignUpViewModel : Utils.BaseNotify
    {
        private string username;
        public string Username
        {
            get { return username; }
            set { SetField(ref username, value, "Username"); }
        }

        private string firstname;
        public string Firstname
        {
            get { return firstname; }
            set { SetField(ref firstname, value, "Firstname"); }
        }

        private string lastname;
        public string Lastname
        {
            get { return lastname; }
            set { SetField(ref lastname, value, "Lastname"); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { SetField(ref password, value, "Password"); }
        }

        private string password2;
        public string Password2
        {
            get { return password2; }
            set { SetField(ref password2, value, "Password2"); }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { SetField(ref email, value, "Email"); }
        }

        private string thumbnail;
        public string Thumbnail
        {
            get { return thumbnail; }
            set { SetField(ref thumbnail, value, "Thumbnail"); }
        }

        private List<Dbo.Department> departments;
        public List<Dbo.Department> Departments
        {
            get { return departments; }
            set { SetField(ref departments, value, "Departments"); }
        }

        private Dbo.Department department;
        public Dbo.Department Department
        {
            get { return department; }
            set { SetField(ref department, value, "Department"); }
        }


        private ICommand _goBackCommand;
        public ICommand GoBackCommand
        {
            get
            {
                if (_goBackCommand == null)
                    _goBackCommand = new RelayCommand(() => OnGoBack(EventArgs.Empty), () => CanGoBack());
                return _goBackCommand;
            }
        }

        bool isSigningUp = false;
        private ICommand _signUpCommand;
        public ICommand SignUpCommand
        {
            get
            {
                if (_signUpCommand == null)
                    _signUpCommand = new RelayCommand(() => SignUp(), () => CanSignIn());
                return _signUpCommand;
            }
        }


        private ICommand _browseCommand;
        public ICommand BrowseCommand
        {
            get
            {
                if (_browseCommand == null)
                    _browseCommand = new RelayCommand(Browse);
                return _browseCommand;
            }
        }

        async public void LoadData()
        {
            var list = await MainViewModel.Proxy.Invoke<IEnumerable<Dbo.Department>>("Execute", new object[] { new string[] { "department-all" } });
            Departments = new List<Dbo.Department>(list);
        }

        private void Browse()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                Thumbnail = op.FileName;
            }
        }

        async private void SignUp()
        {
            isSigningUp = true;

            string base64 = "";
            if (!string.IsNullOrWhiteSpace(thumbnail))
            {
                Bitmap thumb = CreateThumbnail(Thumbnail, 50, 50);
                BitmapImage bitmapImage = new BitmapImage();
                using (MemoryStream memory = new MemoryStream())
                {
                    thumb.Save(memory, ImageFormat.Png);
                    memory.Position = 0;
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = memory;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();
                }
                byte[] data;
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    data = ms.ToArray();
                }
                base64 = Convert.ToBase64String(data);
            }

            var res = await MainViewModel.Proxy.Invoke<Dbo.User>("Execute", new object[] { new string[] { "user-insert", username, lastname, firstname, email, password.sha1(), true.ToString(), base64, Department.Id.ToString() } });
            isSigningUp = false;
            if (res != null)
                OnSigned(EventArgs.Empty);
        }

        public static Bitmap CreateThumbnail(string lcFilename, int lnWidth, int lnHeight)
        {
            System.Drawing.Bitmap bmpOut = null;
            try
            {
                Bitmap loBMP = new Bitmap(lcFilename);
                ImageFormat loFormat = loBMP.RawFormat;

                decimal lnRatio;
                int lnNewWidth = 0;
                int lnNewHeight = 0;

                //*** If the image is smaller than a thumbnail just return it
                if (loBMP.Width < lnWidth && loBMP.Height < lnHeight)
                    return loBMP;

                if (loBMP.Width > loBMP.Height)
                {
                    lnRatio = (decimal)lnWidth / loBMP.Width;
                    lnNewWidth = lnWidth;
                    decimal lnTemp = loBMP.Height * lnRatio;
                    lnNewHeight = (int)lnTemp;
                }
                else
                {
                    lnRatio = (decimal)lnHeight / loBMP.Height;
                    lnNewHeight = lnHeight;
                    decimal lnTemp = loBMP.Width * lnRatio;
                    lnNewWidth = (int)lnTemp;
                }
                bmpOut = new Bitmap(lnNewWidth, lnNewHeight);
                Graphics g = Graphics.FromImage(bmpOut);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
                g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);

                loBMP.Dispose();
            }
            catch
            {
                return null;
            }

            return bmpOut;
        }

        private bool CanGoBack()
        {
            return !isSigningUp;
        }

        private bool CanSignIn()
        {
            return !isSigningUp
                && !string.IsNullOrWhiteSpace(username)
                && !string.IsNullOrWhiteSpace(firstname)
                && !string.IsNullOrWhiteSpace(lastname)
                && !string.IsNullOrWhiteSpace(password)
                && !string.IsNullOrWhiteSpace(password2)
                && !string.IsNullOrWhiteSpace(email)
                && password == password2;
        }

        public event EventHandler SignUpped;
        protected virtual void OnSigned(EventArgs e)
        {
            EventHandler handler = SignUpped;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler GoBack;
        protected virtual void OnGoBack(EventArgs e)
        {
            EventHandler handler = GoBack;
            if (handler != null)
                handler(this, e);
        }
    }

    public class SignUpEventArgs : EventArgs
    {
        public Dbo.User user { get; set; }
    }

    public delegate void SignUpEventHandler(Object sender, SignUpEventArgs e);
}
