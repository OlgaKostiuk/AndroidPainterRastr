using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;

namespace Painter.Droid
{
	[Activity (Label = "Painter.Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
        private DrawView drawView;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
            SetContentView(Resource.Layout.Main);
            drawView = FindViewById<DrawView>(Resource.Id.drawView);
            DisplayMetrics metrics = new DisplayMetrics();
            metrics = Resources.DisplayMetrics;
            drawView.init(metrics);
        }
	}
}


