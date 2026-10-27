using Android.Graphics;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using Bumptech.Glide;
using Bumptech.Glide.Util;
using Bumptech.Glide.Load.Engine;
using AndroidViews = Android.Views;



namespace MauiRecyclerViewRoSchmi.Platforms.Android;

public class CustomRecyclerViewAdapter : RecyclerView.Adapter
{
    public CustomRecyclerViewAdapter(List<ProductResponse> products)
    {
        _products = products;
    }

    private List<ProductResponse> _products;
    public override int ItemCount => _products.Count;

    
    public void UpdateData(ObservableCollection<ProductResponse> newProducts)
    {
        
        
        // Convert ObservableCollection to List
        //_products = new List<ProductResponse>(newProducts);

        _products.AddRange(newProducts);

        
        // Notify the adapter that the data has changed
        NotifyDataSetChanged();
    }

    public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
    {
        //if (holder is CustomViewHolder viewHolder)
        //{
        //    viewHolder.TextView.Text = _products[position].Name;
        //    //viewHolder.ImageView = _products[position].ImageUrl;

        //    LoadImageAsync(viewHolder.ImageView, _products[position].ImageUrl);
        //    viewHolder.DescriptionTextView.Text = _products[position].Description;
        //    viewHolder.PublishedTextView.Text = _products[position].PublishedDate.ToString("yyyy-MM-dd-HH-ss-mm");
        //}


        if (holder is CustomViewHolder viewHolder)
        {
            var product = _products[position];
            // Set the name (for example, chat name)

            
            viewHolder.TextView.Text = product.Name;

            // Set the last message
            viewHolder.DescriptionTextView.Text = product.Description;
 
            // Set the date of the last message
            viewHolder.PublishedTextView.Text = product.PublishedDate.ToString("yyyy-MM-dd");


            // Load the image (avatar)
            
            Glide.With(viewHolder.ImageView.Context)
                .Load(product.ImageUrl)
                .Placeholder(Resource.Drawable.loading_picture)  // Wird gezeigt während es lädt
                .Error(Resource.Drawable.error_picture)          // Wird gezeigt bei Fehler
                .Listener(new GlideExceptionListener())          // Fügt den Protokoll-Listener hinzu
                .Into(viewHolder.ImageView);
            



            viewHolder.Title2.Text = product.Name;

            
            // Set the number of unread messages
            if (product.UnreadMessagesCount > 0)
            {
                viewHolder.UnreadMessagesCount.Text = product.UnreadMessagesCount.ToString();
                viewHolder.UnreadMessagesCount.Visibility = ViewStates.Visible;
            }
            else
            {
                viewHolder.UnreadMessagesCount.Visibility = ViewStates.Gone;
            }
        }
    }

    public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
    {
        // Inflate the item view
        AndroidViews.View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.recycler_view_item, parent, false);
        return new CustomViewHolder(itemView);
    }

    private async Task<Bitmap> GetImageBitmapFromUrlAsync(string url)
    {
        Bitmap imageBitmap = null;
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var imageBytes = await response.Content.ReadAsByteArrayAsync();
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                    int breakpoint107 = 1;
                }
            }
        }
        return imageBitmap;
    }

    private async void LoadImageAsync(ImageView imageView, string url)
    {
        Bitmap bitmap = await GetImageBitmapFromUrlAsync(url);
        imageView.SetImageBitmap(bitmap);
    }
}

public class GlideExceptionListener : Java.Lang.Object, Bumptech.Glide.Request.IRequestListener
{

    public bool OnResourceReady(global::Java.Lang.Object p0, global::Java.Lang.Object p1, global::Bumptech.Glide.Request.Target.ITarget? p2, global::Bumptech.Glide.Load.DataSource p3, bool p4)
    {
        // Protokolliert den Fehler im Ausgabefenster von Visual Studio
        //System.Diagnostics.Debug.WriteLine($"[GlideError] ResourceReady fehlgeschlagen: {e?.Message}");
        return false; }
    public bool OnLoadFailed(Bumptech.Glide.Load.Engine.GlideException e, Java.Lang.Object model, Bumptech.Glide.Request.Target.ITarget target, bool isFirstResource)
    {
        // Protokolliert den Fehler im Ausgabefenster von Visual Studio
        System.Diagnostics.Debug.WriteLine($"[GlideError] Laden fehlgeschlagen: {e?.Message}");
        return false;
    }
}



public class CustomViewHolder : RecyclerView.ViewHolder
{
    public TextView TextView { get; }
    public ImageView ImageView { get; set; }
    public TextView DescriptionTextView { get; set; }
    public TextView PublishedTextView { get; set; }
    public TextView UnreadMessagesCount { get; set; }
    public TextView Title2 { get; set; }

    public CustomViewHolder(AndroidViews.View itemView) : base(itemView)
    {
        TextView = itemView.FindViewById<TextView>(Resource.Id.title);
        ImageView = itemView.FindViewById<ImageView>(Resource.Id.productImage);
        DescriptionTextView = itemView.FindViewById<TextView>(Resource.Id.productDescription);
        PublishedTextView = itemView.FindViewById<TextView>(Resource.Id.productPublishedDate);
        UnreadMessagesCount = itemView.FindViewById<TextView>(Resource.Id.unreadMessagesCount);
        Title2 = itemView.FindViewById<TextView>(Resource.Id.title2);
    }
}
