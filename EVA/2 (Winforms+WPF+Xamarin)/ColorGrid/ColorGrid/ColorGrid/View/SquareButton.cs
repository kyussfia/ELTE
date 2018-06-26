using Xamarin.Forms;

namespace ELTE.ColorGrid.View
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
            }
        }
    }
}
