using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace LocalLendAppMobileCA
{
    [Activity(Label = "Item Details")]
    public class BorrowItemDetailActivity : Activity
    {
        ImageView imgItemPhoto;
        TextView lblItemName;
        TextView lblItemDescription;
        Button btnContactLender;
        Button btnBackToList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.BorrowItemDetail);

            imgItemPhoto = FindViewById<ImageView>(Resource.Id.imgItemPhoto);
            lblItemName = FindViewById<TextView>(Resource.Id.lblItemName);
            lblItemDescription = FindViewById<TextView>(Resource.Id.lblItemDescription);
            btnContactLender = FindViewById<Button>(Resource.Id.btnContactLender);
            btnBackToList = FindViewById<Button>(Resource.Id.btnBack);

            string itemName = Intent.GetStringExtra("itemName");
            string itemDesc = Intent.GetStringExtra("itemDescription");
            int itemImage = Intent.GetIntExtra("itemImage", 0); //---need to convert byte[]?

            lblItemName.Text = itemName.ToString();
            lblItemDescription.Text = itemDesc.ToString();
            imgItemPhoto.SetImageResource(itemImage);

            btnContactLender.Click += BtnContactLender_Click;
            btnBackToList.Click += BtnBackToList_Click;
        }

        private void BtnBackToList_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void BtnContactLender_Click(object sender, EventArgs e)
        {
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            ContactDialogFragment contactDialog = new ContactDialogFragment();
            contactDialog.Show(transaction, "contactDialog fragment");
        }
    }
}