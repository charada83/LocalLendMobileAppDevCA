using Android.Widget;

namespace LocalLendAppMobileCA.Adapters
{
    class BorrowListAdapterViewHolder : Java.Lang.Object
    {
        public ImageView ItemImage { get; }
        public TextView ItemName { get; }
        public TextView ItemDescription { get; }
        public TextView Availability { get; }

        public BorrowListAdapterViewHolder(ImageView itemImage, TextView itemName,
                                       TextView itemDescription, TextView availability)
        {
            ItemImage = itemImage;
            ItemName = itemName;
            ItemDescription = itemDescription;
            Availability = availability;
        }
    }
    
}