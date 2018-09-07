using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace LocalLendAppMobileCA
{
    
    [Activity(Label = "Home", Theme = "@style/AppTheme", MainLauncher = true)]
    public class HomePageActivity : AppCompatActivity
    {
        Android.Support.V7.Widget.Toolbar toolbar;
        TextView lblIntro;
        Button btnStartBorrowing;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //RequestWindowFeature(WindowFeatures.NoTitle);

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.HomePage);
            // Create your application here

            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "LocalLend";

            lblIntro = FindViewById<TextView>(Resource.Id.lblIntro);
            btnStartBorrowing = FindViewById<Button>(Resource.Id.btnStartBorrowing);

            btnStartBorrowing.Click += BtnStartBorrowing_Click;
        }

        //Opens main BorrowList page
        private void BtnStartBorrowing_Click(object sender, EventArgs e)
        {
            Intent openMainBorrowListIntent = new Intent(this, typeof(MainActivity));
            StartActivity(openMainBorrowListIntent);
        }
    }
}