using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace Painter.Droid
{
    [Register("Painter.Droid.DrawView")]
    public class DrawView : View
    {
        public static int BRUSH_SIZE = 10;
        public static readonly Color DEFAULT_COLOR = Color.Red;
        public static readonly Color DEFAULT_BG_COLOR = Color.White;
        private const float TOUCH_TOLERANCE = 4;
        private float mX, mY;
        private Path mPath;
        private Paint mPaint;
        private List<FingerPath> paths = new List<FingerPath>();
        private Color currentColor;
        private Color backgroundColor = DEFAULT_BG_COLOR;
        private int strokeWidth;
        private Bitmap mBitmap;
        private Canvas mCanvas;
        private Paint mBitmapPaint = new Paint(PaintFlags.Dither);


        public DrawView(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            mPaint = new Paint();
            mPaint.AntiAlias = true;
            mPaint.Dither = true;
            mPaint.Color = DEFAULT_COLOR;
            mPaint.SetStyle(Paint.Style.Stroke);
            mPaint.StrokeJoin = Paint.Join.Round;
            mPaint.StrokeCap = Paint.Cap.Round;
            mPaint.SetXfermode(null);
            mPaint.Alpha = 0xff;
        }

        public DrawView(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
        }

        public Color GetRandomColor()
        {
            Random rnd = new Random();
            int red = rnd.Next(255);
            int green = rnd.Next(255);
            int blue = rnd.Next(255);

            return new Color(red, green, blue, 255);
        }

        public void init(DisplayMetrics metrics)
        {
            int height = metrics.HeightPixels;
            int width = metrics.WidthPixels;

            mBitmap = Bitmap.CreateBitmap(width, height, Bitmap.Config.Argb8888);
            mCanvas = new Canvas(mBitmap);

            currentColor = DEFAULT_COLOR;
            strokeWidth = BRUSH_SIZE;
        }

        public void clear()
        {
            backgroundColor = DEFAULT_BG_COLOR;
            paths.Clear();
            Invalidate();
        }

        protected override void OnDraw(Canvas canvas)
        {
            canvas.Save();
            mCanvas.DrawColor(backgroundColor);

            foreach (FingerPath fp in paths)
            {
                mPaint.Color = fp.color;
                mPaint.StrokeWidth = fp.strokeWidth;

                mCanvas.DrawPath(fp.path, mPaint);

            }

            canvas.DrawBitmap(mBitmap, 0, 0, mBitmapPaint);
            canvas.Restore();
        }

        private void touchStart(float x, float y)
        {
            mPath = new Path();
            //currentColor = GetRandomColor();
            FingerPath fp = new FingerPath(currentColor, strokeWidth, mPath);
            paths.Add(fp);

            mPath.Reset();
            mPath.MoveTo(x, y);
            mX = x;
            mY = y;
        }

        private void touchMove(float x, float y)
        {
            float dx = Math.Abs(x - mX);
            float dy = Math.Abs(y - mY);

            if (dx >= TOUCH_TOLERANCE || dy >= TOUCH_TOLERANCE)
            {
                mPath.QuadTo(mX, mY, x, y);
                mX = x;
                mY = y;
            }

            //foreach (var fp in paths)
            //{
            //    fp.color = GetRandomColor();
            //}

        }

        private void touchUp()
        {
            mPath.LineTo(mX, mY);
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            float x = e.GetX();
            float y = e.GetY();

            switch (e.Action)
            {
                case MotionEventActions.Down:
                    touchStart(x, y);
                    Invalidate();
                    break;
                case MotionEventActions.Move:
                    touchMove(x, y);
                    Invalidate();
                    break;
                case MotionEventActions.Up:
                    touchUp();
                    Invalidate();
                    break;
            }
            return true;
        }
    }
}