using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Com.Huxq17.Swipecardsview;
using System.Collections.Generic;
using cardwipe.Models;
using System;
using cardwipe.Adapter;
using System.Net;
using Newtonsoft.Json;
using static Com.Huxq17.Swipecardsview.SwipeCardsView;
using Android.Views;
using Android.Content;

namespace cardwipe
{
    [Activity(Label = "cardwipe", MainLauncher = true,Theme ="@style/Theme.AppCompat.Light.NoActionBar")]
    public class MainActivity : AppCompatActivity
    {
        private SwipeCardsView swipeCardsView;
        private Model models = new Model();
        CardAdapter cardAdapter;
        public int curIndex;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            swipeCardsView = FindViewById<SwipeCardsView>(Resource.Id.swipeCardsView);
            swipeCardsView.RetainLastCard(false);
            swipeCardsView.EnableSwipe(true);
            swipeCardsView.SetCardsSlideListener(new CardSlidListner(this));
            GetData();
        }

        public void GetData()
        {
           
            using (WebClient httpClient = new WebClient())
            {
                var jsonData = httpClient.DownloadString("https://randomuser.me/api/");
                models = JsonConvert.DeserializeObject<Model>(jsonData);
            }

            this.RunOnUiThread(() =>
            {
                //if (cardAdapter == null)
                //{
                cardAdapter = new CardAdapter(this, models);

                swipeCardsView.SetAdapter(cardAdapter);
                //}
                //else
                //{
                //    cardAdapter.setData(models);
                //    swipeCardsView.NotifyDatasetChanged(curIndex);
                //}
            });

        }
    }
    public class CardSlidListner : Java.Lang.Object,ICardsSlideListener
    {
        Context _context;
        public CardSlidListner(Context context)
        {
            _context = context;
        }
        public void OnCardVanish(int p0, SlideType p1)
        {
            Toast.MakeText(_context, "OnCardVanish "+p1.ToString(), ToastLength.Long).Show();
            ((MainActivity)_context).GetData();
        }

        public void OnItemClick(View p0, int p1)
        {
            Toast.MakeText(_context, "Item clicked", ToastLength.Long).Show();
        }

        public void OnShow(int p0)
        {
            ((MainActivity)_context).curIndex = p0;
            Toast.MakeText(_context, "OnShow " + p0, ToastLength.Long).Show();
        }
    }
}

