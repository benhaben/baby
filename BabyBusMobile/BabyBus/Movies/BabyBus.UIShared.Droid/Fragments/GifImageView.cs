﻿//using System;
//using Android.Views;
//using Android.Graphics;
//using System.Runtime.Remoting.Contexts;
//using Android.Content.Res;
//using Android.OS;
//
//namespace BabyBus.Droid.Fragments
//{
//	public class GifImageView : View
//	{
//		private long movieStart;
//		private Movie movie;
//
//		public GifImageView( Context context) )
//		{
//			this(context,null);
//			//movie =  Movie.DecodeStream();
//		}
//		public GifImageView( Context context,Attribute attrs) )
//		{
//			this(context, attrs,Resource.Styleable.calendarthis_calendar_background);
//		}
//		public GifImageView(Context context, Attribute attrs, int defStyle) { 
//			this(context, attrs, defStyle); 
//			//SetViewAttributes(context, attrs, defStyle); 
//		}  
//
//		protected override void OnDraw(Canvas canvas)
//		{
//			long curTime = Android.OS.SystemClock.UptimeMillis();
//			if (movieStart == 0) {
//				movieStart = curTime;
//			}
//			if (movie != null) {
//				int duraction = movie.Duration();
//				int relTime = (int)((curTime - movieStart) % duraction);
//				movie.SetTime(relTime);
//				movie.Draw(canvas, 0, 0);
//				//强制重绘
//				Invalidate();
//			}
//			base.OnDraw(canvas);
//		}
//	}
//}
//
