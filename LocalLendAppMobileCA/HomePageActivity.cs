using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace LocalLendAppMobileCA
{
    
    [Activity(Label = "Home", Theme = "@style/AppTheme", MainLauncher = true)]
    public class HomePageActivity : AppCompatActivity
    {
        TextView lblIntro;
        Button btnStartBorrowing;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.HomePage);
            // Create your application here

            lblIntro = FindViewById<TextView>(Resource.Id.lblIntro);
            btnStartBorrowing = FindViewById<Button>(Resource.Id.btnStartBorrowing);

            btnStartBorrowing.Click += BtnStartBorrowing_Click;
        }

        private void BtnStartBorrowing_Click(object sender, EventArgs e)
        {
            Intent openMainBorrowListIntent = new Intent(this, typeof(MainActivity));
            StartActivity(openMainBorrowListIntent);
        }
    }
}