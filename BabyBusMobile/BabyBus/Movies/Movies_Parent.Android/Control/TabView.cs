﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace BabyBus.Droid.Control
{
    public class TabView : LinearLayout
    {
        private ImageView imageView;
        private TextView textView;

        protected TabView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public TabView(Context context) : base(context)
        {
        }

        public TabView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public TabView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
        }

        public TabView(Context context, int select_drawable,string label) : base(context)
        {
            imageView = new ImageView(context);
            textView = new TextView(context);

            var listDrawable = new StateListDrawable();
            listDrawable.AddState(new int[] { Android.Resource.Attribute.Selectable }
                , Resources.GetDrawable(select_drawable));
            listDrawable.AddState(new int[] { Android.Resource.Attribute.StateEnabled }
                , Resources.GetDrawable(select_drawable));
            
            imageView.SetImageDrawable(listDrawable);
            imageView.SetBackgroundColor(Color.Transparent);
            
            textView.Text = label;
            SetGravity(GravityFlags.Center);
            AddView(imageView);
            AddView(textView);
        }
    }
}