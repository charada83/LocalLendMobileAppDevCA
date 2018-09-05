using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System.Collections.Generic;
using LocalLendAppMobileCA.DataAccess;
using System;
using LocalLendAppMobileCA.Adapters;
using Android.Content;
using System.Linq;
using Android.Views;
using Android.Support.V4.View;
using Android.Runtime;

namespace LocalLendAppMobileCA
{
    public class AddItemToListEventArgs : EventArgs
    {
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }

        public AddItemToListEventArgs(string itemName, string itemDesc)
        {
            ItemName = itemName;
            ItemDescription = itemDesc;
        }
    }

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        DataStore database = new DataStore();

        Button btnLend;
        List<Item> itemList = new List<Item>();
        ListView lvItems;
        BorrowListAdapter adapter;
        EditText txtSearch;

        public EventHandler<AddItemToListEventArgs> OnCreateItem;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            btnLend = FindViewById<Button>(Resource.Id.btnLend);
            lvItems = FindViewById<ListView>(Resource.Id.lvItems);
            txtSearch = FindViewById<EditText>(Resource.Id.txtSearch);

            LoadItemsFromDataStore();

            adapter = new BorrowListAdapter(this, itemList);
            lvItems.Adapter = adapter;

            txtSearch.TextChanged += TxtSearch_TextChanged;
            lvItems.ItemClick += LvItems_ItemClick;
            btnLend.Click += BtnLend_Click;
           
        }

        private void TxtSearch_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var itemToLower = txtSearch.Text.ToLower();
            List<Item> searchedItems = (from item in itemList
                                        where item.ItemName.ToLower().StartsWith(itemToLower)
                                        select item).ToList<Item>();

            adapter = new BorrowListAdapter(this, searchedItems);
            lvItems.Adapter = adapter;
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
            //IEnumerable<Item> items = database.GetItems();
            //itemList = items.ToList();
        }

        
    }
}

