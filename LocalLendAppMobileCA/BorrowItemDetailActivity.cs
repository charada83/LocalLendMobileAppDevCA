﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LocalLendAppMobileCA.DataAccess;

namespace LocalLendAppMobileCA
{
    [Activity(Label = "Item Details")]
    public class BorrowItemDetailActivity : Activity
    {

        ImageView imgItemPhoto;
        TextView lblItemName;
        TextView lblItemDescription;
        TextView lblAvailability;
        Button btnContactLender;
        Button btnBackToList;
        Button btnBorrowItem;
        Button btnReturnItem;
        string itemAvailability;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.BorrowItemDetail);

            imgItemPhoto = FindViewById<ImageView>(Resource.Id.imgItemPhoto);
            lblItemName = FindViewById<TextView>(Resource.Id.lblItemName);
            lblItemDescription = FindViewById<TextView>(Resource.Id.lblItemDescription);
            lblAvailability = FindViewById<TextView>(Resource.Id.lblAvailability);
            btnContactLender = FindViewById<Button>(Resource.Id.btnContactLender);
            btnBackToList = FindViewById<Button>(Resource.Id.btnBack);
            btnBorrowItem = FindViewById<Button>(Resource.Id.btnBorrowItem);
            btnReturnItem = FindViewById<Button>(Resource.Id.btnReturnItem);

            btnReturnItem.Enabled = false;

            string itemName = Intent.GetStringExtra("itemName");
            string itemDesc = Intent.GetStringExtra("itemDescription");
            string itemImage = Intent.GetStringExtra("itemImage");
            itemAvailability = Intent.GetStringExtra("itemAvailability");

            lblItemName.Text = itemName.ToString();
            lblItemDescription.Text = itemDesc.ToString();
            imgItemPhoto.SetImageURI(Android.Net.Uri.Parse(itemImage));

            btnContactLender.Click += BtnContactLender_Click;
            btnBackToList.Click += BtnBackToList_Click;
            btnBorrowItem.Click += BtnBorrowItem_Click;
            btnReturnItem.Click += BtnReturnItem_Click;
        }

        //Didn't find a solution to change the status from Available to Borrowed in ItemList
        private void BtnReturnItem_Click(object sender, EventArgs e)
        {
            btnBorrowItem.Enabled = true;
            btnReturnItem.Enabled = false;
            
            //Intent returnedIntent = new Intent();
            //returnedIntent.PutExtra("Available", "@string/available");
            //SetResult(Result.Ok, returnedIntent);
            //Finish();
        }

        private void BtnBorrowItem_Click(object sender, EventArgs e)
        {
            btnBorrowItem.Enabled = false;
            btnReturnItem.Enabled = true;

            Intent borrowedIntent = new Intent();
            borrowedIntent.PutExtra("Borrowed", "@string/borrowed");
            SetResult(Result.Ok, borrowedIntent);
            Finish();
        }

        private void BtnBackToList_Click(object sender, EventArgs e)
        {
            Finish();
        }

        //Opens Contact Dialog for borrower to send message to lender
        private void BtnContactLender_Click(object sender, EventArgs e)
        {
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            ContactDialogFragment contactDialog = new ContactDialogFragment();
            contactDialog.Show(transaction, "contactDialog fragment");
        }
    }
}