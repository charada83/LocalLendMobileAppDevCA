using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System.Collections.Generic;
using LocalLendAppMobileCA.DataAccess;
using System;
using LocalLendAppMobileCA.Adapters;
using Android.Content;

namespace LocalLendAppMobileCA
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button btnLend;
        List<Item> itemList = new List<Item>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            btnLend = FindViewById<Button>(Resource.Id.btnLend);
            var lvItems = FindViewById<ListView>(Resource.Id.lvItems);

            LoadItemsFromDataStore();

            lvItems.Adapter = new BorrowListAdapter(this, itemList);

            lvItems.ItemClick += LvItems_ItemClick;
            btnLend.Click += BtnLend_Click;
        }

        private void BtnLend_Click(object sender, EventArgs e)
        {
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            LendDialogFrg lendDialog = new LendDialogFrg();
            lendDialog.Show(transaction, "lendDialog fragment");
        }

        private void LvItems_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var itemClickPosition = e.Position;
            var item = itemList[itemClickPosition] as Item;
            Intent getItem = new Intent(this, typeof(BorrowItemDetailActivity));
            getItem.PutExtra("itemName", item.ItemName.ToString());
            getItem.PutExtra("itemDescription", item.ItemDescription.ToString());
            getItem.PutExtra("itemImage", item.ItemImage);

            StartActivity(getItem);
        }

        private void LoadItemsFromDataStore()
        {
            itemList.Add(new Item("Power Drill", "Powerful Tool", Resource.Drawable.powerdrill));
            itemList.Add(new Item("Wheelbarrow", "Good condition, can lend for up to 3 days", Resource.Drawable.wheelbarrow));
        }
    }
}

