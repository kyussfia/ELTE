using System;
using Xamarin.Forms;

namespace ELTE.Calculator.View
{
    /// <summary>
    /// Tájolást felügyelő lap típusa.
    /// </summary>
    public class OrientationAwareContentPage : ContentPage
    {
        private Boolean _isLandscape;

        /// <summary>
        /// Fekvő tájolás fennállásának kezelése.
        /// </summary>
        public Boolean IsLandscape
        {
            get
            {
                return _isLandscape;
            }
            private set
            {
                if (_isLandscape != value)
                {
                    _isLandscape = value;
                    OnPropertyChanged(); // jelezzük a tulajdonság megváltozását
                }
            }
        }

        protected override void OnSizeAllocated(Double width, Double height)
        {
            base.OnSizeAllocated(width, height);
            
            // tájolás meghatározása
            IsLandscape = width > height;
        }
    }
}
