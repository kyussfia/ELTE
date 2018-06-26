using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace LightDuel
{
    /// <summary>
    /// Négyzetesen méreteződő gomb típusa.
    /// </summary>
    public class SquareButton : Button
    {
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width != height)
            {
                // a magasság egyezzen meg a szélességgel
                this.HeightRequest = width;
                this.MinimumHeightRequest = width;
            }
        }
    }

    /// <summary>
    /// Automatikusan méreteződő nézet típusa.
    /// </summary>
    public class AutoSizeContentView : ContentView
    {
        public AutoSizeContentView()
        {
            this.ChildAdded += new EventHandler<ElementEventArgs>(AutoSizeContentView_ChildAdded);
            // kezeljük az eseményt, amikor megkapja a tartalmat
        }

        private void AutoSizeContentView_ChildAdded(object sender, ElementEventArgs e)
        {
            // he megkapja a tartalmat, akkor rákötünk egy eseménykezelőt
            if (Content != null)
                Content.SizeChanged += new EventHandler(Content_SizeChanged);
        }

        private void Content_SizeChanged(object sender, EventArgs e)
        {
            // ha változik a tartalom mérete, akkor méretezzük
            Double heightScale = Height / this.Content.Height;
            Double widthScale = Width / this.Content.Width;

            if (widthScale > 0 && heightScale > 0)
                // a tartalmat átméretezzük, hogy illeszkedjen a képernyőhöz
                this.Content.Scale = Math.Min(heightScale, widthScale);
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            // ha változik az egész nézet mérete, akkor méretezzük a tartalmat
            base.OnSizeAllocated(width, height);

            Double heightScale = height / Content.Height;
            Double widthScale = width / Content.Width;

            if (widthScale > 0 && heightScale > 0)
                // a tartalmat átméretezzük, hogy illeszkedjen a képernyőhöz
                Content.Scale = Math.Min(heightScale, widthScale);
        }
    }
}