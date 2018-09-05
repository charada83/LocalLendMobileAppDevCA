using System;
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
    public class AddItemToListEventArgs : EventArgs
    {
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }

        public AddItemToListEventArgs(string itemName, string itemDesc) : base()
        {
            ItemName = itemName;
            ItemDescription = itemDesc;
        }
    }

    //[Register("LocalLendAppMobileCA.LendDialogFrg")]
    public class LendDialogFrg : DialogFragment
    {
        DataStore database = new DataStore();

        public static readonly int ChooseImageId = 300;

        private EditText txtEditItemName;
        private EditText txtEditItemDescription;
        private ImageView imgUpload;
        private Button btnAddImage;
        private Button btnAddItem;
        private Button btnCancel;

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

        private void BtnAddImage_Click(object sender, EventArgs e)
        {
            Intent uploadImageIntent = new Intent();
            uploadImageIntent.SetType("image/*");
            uploadImageIntent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(uploadImageIntent, "Choose Image"), ChooseImageId);
           
        }

        public override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if ((requestCode == ChooseImageId) && (resultCode == Result.Ok))
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
            if (OnCreateItem != null)
            {

                OnCreateItem.Invoke(this, new AddItemToListEventArgs(txtEditItemName.Text, txtEditItemDescription.Text));
                
                Toast.MakeText(Activity, "Your item has been added", ToastLength.Long).Show();
            }
            this.Dismiss();
            

        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
        }

    }
}
