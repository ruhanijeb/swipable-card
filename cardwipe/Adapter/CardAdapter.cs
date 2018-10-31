using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using cardwipe.Models;
using Com.Huxq17.Swipecardsview;
using Square.Picasso;

namespace cardwipe.Adapter
{
    public class CardAdapter : BaseCardAdapter
    {
        private Model mList;
        private Context context;
        public CardAdapter(Context _context ,Model _mList)
        {
            mList = _mList;
            context = _context;
        }

        public override int CardLayoutId 
        {
            get
            {
                return Resource.Layout.Card_Item;
            }
        }
        public void setData(Model datas)
        {
            this.mList = datas;
        }
        public override int Count {
            get{
                return mList.results.Count;
            }
        }

        public override void OnBindData(int position, View cardView)
        {
            Console.WriteLine(" onbinddata " + position);
            if (mList == null || mList.results.Count == 0)
                return;
            ImageView imageView = cardView.FindViewById<ImageView>(Resource.Id.imageView1);
            TextView textView = cardView.FindViewById<TextView>(Resource.Id.textView1);
            //Model model = mList.results[position];
            textView.Text = mList.results[position].name.title;
            Picasso.With(context).Load(mList.results[position].picture.medium).Into(imageView);
            Console.WriteLine(" onbinddata " + position+" title "+mList.results[position].name.title+" img url "+mList.results[position].picture.medium);
        }
    }
}
