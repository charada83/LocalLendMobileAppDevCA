using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.IO;
using LocalLendAppMobileCA.DataAccess;
using static Android.Graphics.Bitmap;

namespace LocalLendAppMobileCA
{
    public class AddItemToListEventArgs : EventArgs
    {
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string Image { get; set; }
        public string Availability { get; set; }

        public AddItemToListEventArgs(string itemName, string itemDesc, string image, string availability) : base()
        {
            ItemName = itemName;
            ItemDescription = itemDesc;
            Image = image;
            Availability = availability;
        }
    }

    public class LendDialogFrg : DialogFragment
    {
        DataStore database = new DataStore();

        public static readonly int ChooseImageId = 300;

        private EditText txtEditItemName;
        private EditText txtEditItemDescription;
        private ImageView imgUpload;
        private string imgUri;
        private Button btnAddImage;
        private Button btnAddItem;
        private Button btnCancel;
        private string availability;

        public event EventHandler<AddItemToListEventArgs> OnCreateItem;

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

        //Opens phones gallery to select an image
        private void BtnAddImage_Click(object sender, EventArgs e)
        {
            Intent uploadImageIntent = new Intent();
            uploadImageIntent.SetType("image/*");
            uploadImageIntent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(uploadImageIntent, "Choose Image"), ChooseImageId);
        }

        //Returns image from Gallery to LendDialog
        public override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if ((requestCode == ChooseImageId) && (resultCode == Result.Ok))
            {
                Android.Net.Uri selectedImage = data.Data;
                imgUri = selectedImage.ToString();
                imgUpload.SetImageURI(selectedImage);
            }

        }
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Dismiss();
        }

        //Adds item to DB after sumbission in LendDialog
        private void BtnAddItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEditItemName.Text) | string.IsNullOrEmpty(txtEditItemDescription.Text))
            {
                Toast toastMsg = Toast.MakeText(Activity, Resource.String.validFields, ToastLength.Short);
                toastMsg.SetGravity(GravityFlags.CenterHorizontal | GravityFlags.CenterVertical, 0, 0);
                toastMsg.Show();
            }
            else
            {
                if (OnCreateItem != null)
                {
                    availability = "Available";
                    OnCreateItem.Invoke(this, new AddItemToListEventArgs(txtEditItemName.Text, txtEditItemDescription.Text, imgUri, availability));

                    Toast.MakeText(Activity, Resource.String.itemAdded, ToastLength.Long).Show();

                    this.Dismiss();
                }
            }          
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
        }

    }
   
}
