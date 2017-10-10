using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace Painter.Droid
{
    public class FingerPath
    {
        public Color color;
        public int strokeWidth;
        public Path path;

        public FingerPath(Color color, int strokeWidth, Path path)
        {
            this.color = color;
            this.strokeWidth = strokeWidth;
            this.path = path;
        }
    }
}