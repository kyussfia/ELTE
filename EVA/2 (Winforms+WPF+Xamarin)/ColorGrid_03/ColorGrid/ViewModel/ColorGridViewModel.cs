using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace ELTE.Windows.ColorGrid.ViewModel
{
    /// <summary>
    /// Színrács nézetmodell típusa.
    /// </summary>
    public class ColorGridViewModel : ViewModelBase
    {
        private Int32 _rowCount;
        private Int32 _columnCount;
        private Random _random;

        /// <summary>
        /// Sorok számának lekérdezée, vagy beállítása.
        /// </summary>
        public Int32 RowCount
        {
            get { return _rowCount; }
            set
            {
                if (_rowCount != value)
                {
                    _rowCount = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Oszlopok számának lekérdezése, vagy beállítása.
        /// </summary>
        public Int32 ColumnCount
        {
            get { return _columnCount; }
            set
            {
                if (_columnCount != value)
                {
                    _columnCount = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Mezők lekérdezése.
        /// </summary>
        public ObservableCollection<ColorFieldViewModel> Fields { get; private set; }

        /// <summary>
        /// Méretváltás parancsának lekérdezése.
        /// </summary>
        public DelegateCommand ChangeSizeCommand { get; private set; }

        /// <summary>
        /// Színrács nézetmodell példányosítása.
        /// </summary>
        public ColorGridViewModel()
        {
            Fields = new ObservableCollection<ColorFieldViewModel>();
            _random = new Random();

            ChangeSizeCommand = new DelegateCommand(x => GenerateFields());
        }

        /// <summary>
        /// Mezők generálása.
        /// </summary>
        private void GenerateFields()
        {
            Fields.Clear();
            for (Int32 i = 0; i < RowCount; i++)
                for (Int32 j = 0; j < ColumnCount; j++)
                {
                    Fields.Add(new ColorFieldViewModel // mező létrehozása
                    {
                        Color = Colors.White, // megadjuk a kezdőértékeket
                        Row = i,
                        Column = j,
                        FieldChangeCommand = new DelegateCommand(field => FieldChange(field as ColorFieldViewModel)) // és a végrehajtandó parancsot
                    });
                }
        }

        /// <summary>
        /// Mezőváltozás kezelése.
        /// </summary>
        /// <param name="selectedField">A kiválasztott mező.</param>
        private void FieldChange(ColorFieldViewModel selectedField)
        { 
            Color color = Color.FromRgb(Convert.ToByte(_random.Next(256)), Convert.ToByte(_random.Next(256)), Convert.ToByte(_random.Next(256)));
            // véletlen szín

            foreach (ColorFieldViewModel field in Fields)
            {
                if (field.Column == selectedField.Column || field.Row == selectedField.Row) // adott oszlopban és sorban
                    field.Color = color; // átszínezés végrehajtása
            }
        }
    }
}
