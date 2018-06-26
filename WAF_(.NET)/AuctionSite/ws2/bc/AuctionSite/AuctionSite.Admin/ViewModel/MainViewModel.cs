using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using AuctionSite.Data;
using AuctionSite.Admin.Persistence;
using AuctionSite.Admin.Model;
using System.Diagnostics;
using System.Windows;
using Microsoft.Win32;
using System.IO;

namespace AuctionSite.Admin.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IAuctionSiteModel _model;
        private ObservableCollection<ItemDTO> _items;
        private ObservableCollection<CategoryDTO> _categories;
        private ItemDTO _selectedItem;
        private Boolean _isLoaded; 
        private Boolean _isEditable;

        public ObservableCollection<ItemDTO> Items
        {
            get { return _items; }
            private set
            {
                if (_items != value)
                {
                    _items = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<CategoryDTO> Categories
        {
            get { return _categories; }
            private set
            {
                if (_categories != value)
                {
                    _categories = value;
                    OnPropertyChanged();
                }
            }
        }

        public Boolean IsLoaded
        {
            get { return _isLoaded; }
            private set
            {
                if (_isLoaded != value)
                {
                    _isLoaded = value;
                    OnPropertyChanged();
                }
            }
        }


        public Boolean IsEditable
        {
            get { return _isEditable; }
            private set
            {
                if (_isEditable != value)
                {
                    _isEditable = value;
                    OnPropertyChanged();
                }
            }
        }

        public ItemDTO SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged();
                }
            }
        }

        public ItemDTO EditedItem { get; set; }

        public DelegateCommand CreateItemCommand { get; private set; }

        public DelegateCommand CreateImageCommand { get; private set; }

        public DelegateCommand UpdateItemCommand { get; private set; }

        public DelegateCommand CloseItemRequestCommand { get; private set; }

        public DelegateCommand SaveChangesCommand { get; private set; }

        public DelegateCommand CancelChangesCommand { get; private set; }

        public DelegateCommand ExitCommand { get; private set; }

        public DelegateCommand LoadCommand { get; private set; }

        public DelegateCommand SaveCommand { get; private set; }

        public DelegateCommand ShowCommand { get; private set; }

        public event EventHandler ExitApplication;

        public event EventHandler ItemEditingStarted;

        public event EventHandler ItemShowStarted;

        public event EventHandler ItemClose;

        public event EventHandler ItemEditingFinished;

        public event EventHandler noItems;

        public event EventHandler Loaded;

        //public event EventHandler<ItemEventArgs> ImageEditingStarted;

        public MainViewModel(IAuctionSiteModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            _model = model;
            _model.ItemChanged += Model_ItemChanged;
            _isLoaded = false;

            CreateItemCommand = new DelegateCommand(param =>
            {
                _isEditable = true;
                EditedItem = new ItemDTO();  // a szerkesztett új lesz
                OnItemEditingStarted();
            });
            CreateImageCommand = new DelegateCommand(param => ImageUpload(param as ItemDTO));
            UpdateItemCommand = new DelegateCommand(param => UpdateItem(param as ItemDTO));
            CloseItemRequestCommand = new DelegateCommand(param => CloseItemRequest(param as ItemDTO));
            SaveChangesCommand = new DelegateCommand(param => createItem(param as ItemDTO));
            CancelChangesCommand = new DelegateCommand(param => CancelChanges());
            LoadCommand = new DelegateCommand(param => LoadAsync());
            SaveCommand = new DelegateCommand(param => SaveAsync());
            ExitCommand = new DelegateCommand(param => OnExitApplication());
            ShowCommand = new DelegateCommand(param => ShowItem(param as ItemDTO));
        }

        private void ImageUpload(ItemDTO item)
        {
            if (item == null)
                return;

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".png"; // Required file extension 
            fileDialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            Nullable<bool> result = fileDialog.ShowDialog();

            if (result == true)
            {
                byte[] bytes = System.IO.File.ReadAllBytes(fileDialog.FileName);
                item.Picture = bytes;
                EditedItem = item;
                SaveChanges();
            }
        }

        private void UpdateItem(ItemDTO item)
        {
            if (item == null)
                return;
            _isEditable = true;

            EditedItem = new ItemDTO
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                OriginalBid = item.OriginalBid,
                CategoryId = item.Category.Id,
                Category = Categories.SingleOrDefault(c => c.Id == item.CategoryId),
                Currency = item.Currency,
                AdvertiserId = item.AdvertiserId,
                ClosedAt = item.ClosedAt,
                CreatedAt = item.CreatedAt,
                Picture = item.Picture
            };

            OnItemEditingStarted();
        }

        private void CloseItemRequest(ItemDTO item)
        {
            if (item == null)
                return;

            var dialogResult = MessageBox.Show("Biztosan lezárod a termék licitálását?", "AuctionSite - Close Item", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                EditedItem = item;
                CloseItem(item);
            }
        }

        private void ShowItem(ItemDTO item)
        {
            EditedItem = item;
            EditedItem.Category = _categories.FirstOrDefault(c => c.Id == item.CategoryId);
            _isEditable = false;
            OnItemShowStarted();
            return;
        }

        private void CloseItem(ItemDTO item)
        {
            if (item == null)
                return;

            if (!item.HasBid)
            {
                OnMessageApplication("Nincs a cikken még egy licit sem! Nem zárható le.");
                return;
            }

            if (item.ClosedAt < DateTime.Now)
            {
                OnMessageApplication("A licit erre a termékre már lejárt!");
                return;
            }

            item.ClosedAt = DateTime.Now;
            EditedItem = item;
            SaveChanges();
        }

        private void createItem(ItemDTO item)
        {
            if (String.IsNullOrEmpty(EditedItem.Name))
            {
                OnMessageApplication("Az név nincs megadva!");
                return;
            }
            if (EditedItem.Category == null)
            {
                OnMessageApplication("A kategória nincs megadva!");
                return;
            }
            if ((int)EditedItem.OriginalBid <= 0)
            {
                OnMessageApplication("Az alapár nincs megadva!");
                return;
            }
            if (String.IsNullOrEmpty(EditedItem.Currency))
            {
                OnMessageApplication("A deviza nincs megadva!");
                return;
            }
            if (EditedItem.ClosedAt < DateTime.Now)
            {
                OnMessageApplication("A lejárat ideje jövőbeni kell hogy legyen! A megadott érték: " + EditedItem.ClosedAt.ToShortDateString());
                return;
            }
            EditedItem.CategoryId = EditedItem.Category.Id;
            SaveChanges();
            OnItemEditingFinished();
        }

        private void SaveChanges()
        {
            // mentés
            if (EditedItem.Id == 0)
            {
                _model.CreateItem(EditedItem);
                Items.Add(EditedItem);
                SelectedItem = EditedItem;
            }
            else // ha már létezik
            {
                _model.UpdateItem(EditedItem);
            }

            EditedItem = null;
        }

        private void CancelChanges()
        {
            EditedItem = null;
            OnItemEditingFinished();
        }

        private async void LoadAsync()
        {
            try
            {
                await _model.LoadAsync();
                Items = new ObservableCollection<ItemDTO>(_model.Items); // az adatokat egy követett gyűjteménybe helyezzük
                if (Items.Count() < 1)
                {
                    noItems(this, EventArgs.Empty);
                }

                Loaded(this, EventArgs.Empty);
                Categories = new ObservableCollection<CategoryDTO>(_model.Categories);              
                IsLoaded = true;
            }
            catch (PersistenceUnavailableException)
            {
                OnMessageApplication("A betöltés sikertelen! Nincs kapcsolat a kiszolgálóval.");
            }
        }

        private async void SaveAsync()
        {
            try
            {
                await _model.SaveAsync();
                OnMessageApplication("A mentés sikeres!");
            }
            catch (PersistenceUnavailableException)
            {
                OnMessageApplication("A mentés sikertelen! Nincs kapcsolat a kiszolgálóval.");
            }
        }

        private void Model_ItemChanged(object sender, ItemEventArgs e)
        {
            Int32 index = Items.IndexOf(Items.FirstOrDefault(item => item.Id == e.ItemId));
            Items.RemoveAt(index); // módosítjuk a kollekciót
            Items.Insert(index, _model.Items[index]);

            SelectedItem = Items[index]; 
        }


        private void OnExitApplication()
        {
            if (ExitApplication != null)
                ExitApplication(this, EventArgs.Empty);
        }

        private void OnItemEditingStarted()
        {
            if (ItemEditingStarted != null)
            {
                ItemEditingStarted(this, EventArgs.Empty);
            }
        }

        private void OnItemClose()
        {
            if (ItemClose != null)
                ItemClose(this, EventArgs.Empty);
        }

        private void OnItemEditingFinished()
        {
            if (ItemEditingFinished != null)
                ItemEditingFinished(this, EventArgs.Empty);
        }

        private void OnItemShowStarted()
        {
            if (ItemShowStarted != null)
                ItemShowStarted(this, EventArgs.Empty);
        }

        /*private void OnImageEditingStarted(Int32 itemId)
        {
            if (ImageEditingStarted != null)
                ImageEditingStarted(this, new ItemEventArgs { ItemId = itemId });
        }*/
    }
}
