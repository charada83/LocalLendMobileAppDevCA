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

namespace LocalLendAppMobileCA.Adapters
{
    class BorrowListAdapter : BaseAdapter<Item>
    {
        public List<Item> ItemList { get; }
        Context context;

        public BorrowListAdapter(Context context, List<Item> itemList)
        {
            this.context = context;
            ItemList = itemList;
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var itemInfoRowView = convertView;
            BorrowListAdapterViewHolder itemViewHolder = null;
            if (itemInfoRowView == null)
            {
                var inflator = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                itemInfoRowView = inflator.Inflate(Resource.Layout.BorrowItemRow, parent, false);

                var itemImageView = itemInfoRowView.FindViewById<ImageView>(Resource.Id.imageViewBorrowItem);
                var lblItemNameView = itemInfoRowView.FindViewById<TextView>(Resource.Id.lblBorrowItemName);
                var lblItemDescription = itemInfoRowView.FindViewById<TextView>(Resource.Id.lblBorrowItemDescription);
                var lblAvailability = itemInfoRowView.FindViewById<TextView>(Resource.Id.lblAvailability);

                if (ItemList[position].ItemImage != null)
                {
                    itemImageView.SetImageURI(Android.Net.Uri.Parse(ItemList[position].ItemImage));              
                }

                itemViewHolder = new BorrowListAdapterViewHolder(itemImageView, lblItemNameView, lblItemDescription, lblAvailability);

                itemInfoRowView.Tag = itemViewHolder;

            }

            itemViewHolder = itemInfoRowView.Tag as BorrowListAdapterViewHolder;

            itemViewHolder.ItemImage.SetImageURI(Android.Net.Uri.Parse(ItemList[position].ItemImage));
            itemViewHolder.ItemName.Text = ItemList[position].ItemName;
            itemViewHolder.ItemDescription.Text = ItemList[position].ItemDescription;
            itemViewHolder.Availability.Text = ItemList[position].Availability;

            return itemInfoRowView;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return ItemList.Count;
            }
        }

        public override Item this[int position] => ItemList[position];

        public override void NotifyDataSetChanged()
        {
            base.NotifyDataSetChanged();
        }

        public void Clear()
        {
            ItemList.Clear();
        }

    }

}