﻿using System;
using System.Collections.Generic;
using Android.Views;
using Android.Widget;
using Cirrious.CrossCore;
using BabyBus.Logic.Shared;
using Object = Java.Lang.Object;
using System.IO;
using Android.Graphics;
using Cirrious.MvvmCross.Droid.Fragging;
using Android.OS;


// Analysis disable CheckNamespace
using Com.Squareup.Picasso;


namespace BabyBus.Droid
{
	public class RecommendationAdpter : BaseAdapter
	{
		protected readonly MvxFragmentActivity activity;
		public List<ECPostInfo> List;
		protected FindMoreIndexViewModel indexVM;
		private IPictureService pic;

		public RecommendationAdpter(MvxFragmentActivity activity, List<ECPostInfo> list)
		{
			this.activity = activity;
			this.List = list;
			pic = Mvx.Resolve<IPictureService>();
		}

		public override int Count {
			get { return List.Count; }
		}

		public override Object GetItem(int position)
		{
			return null;
		}

		public override long GetItemId(int position)
		{
			return 0;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{

			ViewHolder holder;
			View view;
			if (convertView == null) {

				view = activity.LayoutInflater.Inflate(Resource.Layout.Item_EC_Recommendation, parent, false);
				holder = new ViewHolder();
				holder.Address = view.FindViewById<TextView>(Resource.Id.address);
				holder.ClassTime = view.FindViewById<TextView>(Resource.Id.time);
				holder.Icon_recommendation = view.FindViewById<ImageView>(Resource.Id.icon_recommendation);
				holder.InvolvedCount = view.FindViewById<TextView>(Resource.Id.number);
				holder.Price = view.FindViewById<TextView>(Resource.Id.cost);
				holder.Rating = view.FindViewById<RatingBar>(Resource.Id.user_recommendation);
				holder.Title = view.FindViewById<TextView>(Resource.Id.title);
				view.Tag = holder;
			} else {
				view = convertView;
				holder = (ViewHolder)view.Tag;
			}
			var item = List[position];
			holder.Address.Text = item.Address;
			holder.ClassTime.Text = item.LessonTime;
			holder.Title.Text = item.Title;
			holder.InvolvedCount.Text = string.Format("{0}/{1}", item.InvolvedCount, item.TotalCount); 
			holder.Price.Text = string.Format("￥{0} 元", item.CurrentPrice);
			holder.Rating.Rating = (float)item.Rating;
			if (!string.IsNullOrEmpty(item.ImageUrls)) {

				Picasso.With(activity).Load(Constants.ThumbServerPath + item.FirstImage).Placeholder(Resource.Drawable.icon_course)
					.CenterCrop().Resize(80, 80).Tag("PicassoAdvertisingTag").Into(holder.Icon_recommendation);
			}
			var ll = view.FindViewById<LinearLayout>(Resource.Id.item_recommendation);
			ll.Tag = position;
			ll.Click -= ll_Click;
			ll.Click += ll_Click;
			return view;
		}

		protected void ll_Click(object sender, EventArgs e)
		{
			indexVM = indexVM ?? ((FindMoreIndexView)activity).ViewModel;
			var positon = (int)((LinearLayout)sender).Tag;
			var item = List[positon];
			indexVM.ShowRecommendationDetailViewModel(item.PostInfoId, (ECColumnType)item.ColumnType);
		}

		protected class ViewHolder : Object
		{

			public ImageView Icon_recommendation{ get; set; }

			public TextView Title{ get; set; }

			public TextView Address{ get; set; }

			public RatingBar Rating{ get; set; }

			public TextView InvolvedCount{ get; set; }

			public  TextView ClassTime{ get; set; }

			public  TextView Price{ get; set; }


		}



	}
}

