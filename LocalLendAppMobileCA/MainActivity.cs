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
using Android.Support.V7.Widget;

namespace LocalLendAppMobileCA
{

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class MainActivity : AppCompatActivity
    {
        DataStore database = new DataStore();

        Button btnLend;
        List<Item> itemList = new List<Item>();
        ListView lvItems;
        BorrowListAdapter adapter;
        EditText txtSearch;
        Android.Support.V7.Widget.Toolbar toolbar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "LocalLend";
            toolbar.SetTitleTextAppearance(this, Resource.Style.TitleTextApperance);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

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

        //Search filters based on typing item name
        private void TxtSearch_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var itemToLower = txtSearch.Text.ToLower();
            List<Item> searchedItems = (from item in itemList
                                        where item.ItemName.ToLower().StartsWith(itemToLower)
                                        select item).ToList<Item>();

            adapter = new BorrowListAdapter(this, searchedItems);
            lvItems.Adapter = adapter;
        }

        //Opens LendDialog to add an item to the BorrowList
        private void BtnLend_Click(object sender, EventArgs e)
        {
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            LendDialogFrg lendDialog = new LendDialogFrg();
            lendDialog.Show(transaction, "lendDialog fragment");

            lendDialog.OnCreateItem += LendDialog_OnCreateItem;
        }

        //Adds item and refreshes BorrowList in MainActivity 
        private void LendDialog_OnCreateItem(object sender, AddItemToListEventArgs e)
        {
            Item item = new Item()
            {
                ItemName = e.ItemName,
                ItemDescription = e.ItemDescription,
                ItemImage = e.Image.ToString()
            };

            database.InsertIntoTableItem(item);

            LoadItemsFromDataStore();

            adapter = new BorrowListAdapter(this, itemList);
            lvItems.Adapter = adapter;
            adapter.NotifyDataSetChanged();

        }

        //Opens detail page for an item
        private void LvItems_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var itemClickPosition = e.Position;
            var item = itemList[itemClickPosition] as Item;
            Intent getItem = new Intent(this, typeof(BorrowItemDetailActivity));
            getItem.PutExtra("itemName", item.ItemName);
            getItem.PutExtra("itemDescription", item.ItemDescription);
            getItem.PutExtra("itemImage", item.ItemImage);

            StartActivity(getItem);
        }

        //Loads items to BorrowList from Database
        private void LoadItemsFromDataStore()
        {
            /*Test List*/
            //itemList.Add(new Item("Power Drill", "Powerful Tool", Resource.Drawable.powerdrill));
            //itemList.Add(new Item("Wheelbarrow", "Good condition, can lend for up to 3 days", Resource.Drawable.wheelbarrow));

            IEnumerable<Item> items = database.GetItems();
            itemList = items.ToList();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
           }
        }
    }
}

