﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using LocalLendAppMobileCA.DataAccess;

namespace LocalLendAppMobileCA
{
    [Register("LocalLendAppMobileCA.LendDialogFrg")]
    public class LendDialogFrg : DialogFragment
    {
        DataStore database = new DataStore();

        private EditText txtEditItemName;
        private EditText txtEditItemDescription;
        private ImageView imgUpload;
        private Button btnAddImage;
        private Button btnAddItem;
        private Button btnCancel;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var lendView = inflater.Inflate(Resource.Layout.LendDialog, container, false);

            txtEditItemName = lendView.FindViewById<EditText>(Resource.Id.txtEditItemName);
            txtEditItemDescription = lendView.FindViewById<EditText>(Resource.Id.txtEditItemDescription);
            imgUpload = lendView.FindViewById<ImageView>(Resource.Id.imgUpload);
            btnAddImage = lendView.FindViewById<Button>(Resource.Id.btnAddImage);
            btnAddItem = lendView.FindViewById<Button>(Resource.Id.btnAddItem);
            btnCancel = lendView.FindViewById<Button>(Resource.Id.btnCancel);

            btnAddItem.Click += BtnAddItem_Click;
            btnCancel.Click += BtnCancel_Click;
            btnAddImage.Click += BtnAddImage_Click;
            return lendView;
        }

        private void BtnAddImage_Click(object sender, EventArgs e)
        {
            Intent uploadImageIntent = new Intent(Intent.ActionPick, Android.Provider.MediaStore.Images.Media.ExternalContentUri);
            StartActivityForResult(uploadImageIntent, 300);
        }

        public override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if ((requestCode == 300) && (resultCode == Result.Ok))
            {
                Android.Net.Uri selectedImage = data.Data;
                imgUpload.SetImageURI(selectedImage);
            }

        }
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Dismiss();
        }

        private void BtnAddItem_Click(object sender, EventArgs e)
        {
            Item item = new Item()
            {
                ItemName = txtEditItemName.Text,
                ItemDescription = txtEditItemDescription.Text,
                // ImageDrawableID = imgUpload
            };

            //database.InsertIntoTableItem(item);
            Toast.MakeText(Activity, "Your item has been added", ToastLength.Long).Show();
            Dismiss();
            

        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
        }

    }
}
