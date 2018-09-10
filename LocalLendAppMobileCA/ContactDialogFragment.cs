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

namespace LocalLendAppMobileCA
{
    public class ContactDialogFragment : DialogFragment
    {
        private EditText txtFullname;
        private EditText txtMessage;
        private Button btnSendMessage;
        private Button btnCancel;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var contactView = inflater.Inflate(Resource.Layout.ContactDialog, container, false);

            txtFullname = contactView.FindViewById<EditText>(Resource.Id.txtFullname);
            txtMessage = contactView.FindViewById<EditText>(Resource.Id.txtMessage);
            btnSendMessage = contactView.FindViewById<Button>(Resource.Id.btnSendMessage);
            btnCancel = contactView.FindViewById<Button>(Resource.Id.btnCancel);

            btnSendMessage.Click += BtnSendMessage_Click; ;
            btnCancel.Click += BtnCancel_Click; ;
            return contactView;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Dismiss();
        }

        private void BtnSendMessage_Click(object sender, EventArgs e)
        {
            Toast.MakeText(Activity, Resource.String.contactToast, ToastLength.Long).Show();
            Dismiss();
        }
    }
}