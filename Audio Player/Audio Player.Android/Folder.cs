using Android.Animation;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Text.Method;
using Android.Text.Style;
using Android.Util;
using Android.Views;
using Android.Views.Accessibility;
using Android.Views.Animations;
using Android.Views.Autofill;
using Android.Views.InputMethods;
using Android.Views.TextClassifiers;
using Android.Widget;
using Java.Interop;
using Java.Lang;
using Java.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Audio_Player.Droid
{
    class Folder : TextView
    {
        private bool selected;
        private Color deselectedColor = new Color(Resource.Color.primary_dark_material_dark);
        private Color selectedColor = new Color(Resource.Color.buttonSelected);

        public Folder(Context context) : base(context)
        {
            selected = false;
            SetBackgroundColor(deselectedColor);
            TextSize = 24;
        }


        public void changeColor()
        {
            if (selected)
            {
                SetBackgroundColor(deselectedColor);
                selected = false;
            }

            else
            {
                SetBackgroundColor(selectedColor);
                selected = true;
            }
        }

        public bool isSelected()
        {
            return selected;
        }

        public void setText(System.String text)
        {
            Text = text;
        }

    }
}