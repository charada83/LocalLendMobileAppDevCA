using Android.Widget;

namespace LocalLendAppMobileCA.Adapters
{
    class BorrowListAdapterViewHolder : Java.Lang.Object
    {
        public ImageView ItemImage { get; }
        public TextView ItemName { get; }
        public TextView ItemDescription { get; }

        public BorrowListAdapterViewHolder(ImageView itemImage, TextView itemName,
                                       TextView itemDescription)
        {
            ItemImage = itemImage;
            ItemName = itemName;
            ItemDescription = itemDescription;
        }
    }
    
}