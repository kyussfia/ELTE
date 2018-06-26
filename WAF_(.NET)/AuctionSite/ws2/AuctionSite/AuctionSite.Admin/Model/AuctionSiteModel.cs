using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionSite.Data;
using AuctionSite.Admin.Persistence;
using System.Diagnostics;

namespace AuctionSite.Admin.Model
{
    public class AuctionSiteModel : IAuctionSiteModel
    {
        private enum DataFlag
        {
            Create,
            Update,
            Delete
        }

        private IAuctionSitePersistence _persistence;
        private List<ItemDTO> _items;
        private Dictionary<ItemDTO, DataFlag> _itemFlags;
        private List<CategoryDTO> _categories;
        private AdvertiserDTO _advertiser;

        private String username;

        public AuctionSiteModel(IAuctionSitePersistence persistence)
        {
            if (persistence == null)
                throw new ArgumentNullException(nameof(persistence));

            IsUserLoggedIn = false;
            _persistence = persistence;
        }

        public IReadOnlyList<CategoryDTO> Categories
        {
            get { return _categories; }
        }

        public IReadOnlyList<ItemDTO> Items
        {
            get { return _items; }
        }

        public Boolean IsUserLoggedIn { get; private set; }

        public event EventHandler<ItemEventArgs> ItemChanged;

        public void CreateItem(ItemDTO item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            if (_items.Contains(item))
                throw new ArgumentException("The item is already in the collection.", nameof(item));

            item.Id = (_items.Count > 0 ? _items.Max(b => b.Id) : 0) + 1; // generálunk egy új, ideiglenes azonosítót (nem fog átkerülni a szerverre)
            item.AdvertiserId = _advertiser.Id;
            item.CreatedAt = DateTime.Now;
            _itemFlags.Add(item, DataFlag.Create);
            _items.Add(item);
        }

        public void UpdateItem(ItemDTO item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            // keresés azonosító alapján
            ItemDTO itemToModify = _items.FirstOrDefault(b => b.Id == item.Id && b.AdvertiserId == _advertiser.Id);

            if (itemToModify == null)
                throw new ArgumentException("The item does not exist.", nameof(item));

            // módosítások végrehajtása
            itemToModify.Name = item.Name;
            itemToModify.OriginalBid = item.OriginalBid;
            itemToModify.Picture = item.Picture;
            itemToModify.Description = item.Description;
            itemToModify.CreatedAt = item.CreatedAt;
            itemToModify.Currency = item.Currency;
            itemToModify.CategoryId = item.CategoryId;
            itemToModify.ClosedAt = item.ClosedAt;

            // külön állapottal jelezzük, ha egy adat újonnan hozzávett
            if (_itemFlags.ContainsKey(itemToModify) && _itemFlags[itemToModify] == DataFlag.Create)
            {
                _itemFlags[itemToModify] = DataFlag.Create;
            }
            else
            {
                _itemFlags[itemToModify] = DataFlag.Update;
            }

            // jelezzük a változást
            OnItemChanged(item.Id);
        }

        public async Task LoadAsync()
        {
            // adatok
            _advertiser = await _persistence.ReadAdvertiserIdAsync(username);
            _items = (await _persistence.ReadItemsAsync(_advertiser.Id)).ToList();
            _categories = (await _persistence.ReadCategoriesAsync()).ToList();

            // állapotjelzések
            _itemFlags = new Dictionary<ItemDTO, DataFlag>();
        }

        public async Task SaveAsync()
        {
            List<ItemDTO> itemsToSave = _itemFlags.Keys.ToList();

            foreach (ItemDTO item in itemsToSave)
            {
                Boolean result = true;

                // az állapotjelzőnek megfelelő műveletet végezzük el
                switch (_itemFlags[item])
                {
                    case DataFlag.Create:
                        result = await _persistence.CreateItemAsync(item);
                        break;
                    case DataFlag.Update:
                        result = await _persistence.UpdateItemAsync(item);
                        break;
                }

                if (!result)
                    throw new InvalidOperationException("Operation " + _itemFlags[item] + " failed on item " + item.Id);

                // ha sikeres volt a mentés, akkor törölhetjük az állapotjelzőt
                _itemFlags.Remove(item);
            }
        }

        public async Task<Boolean> LoginAsync(String userName, String userPassword)
        {
            IsUserLoggedIn = await _persistence.LoginAsync(userName, userPassword);
            username = userName;
            return IsUserLoggedIn;
        }

        public async Task<Boolean> LogoutAsync()
        {
            if (!IsUserLoggedIn)
                return true;

            IsUserLoggedIn = !(await _persistence.LogoutAsync());

            return IsUserLoggedIn;
        }

        private void OnItemChanged(Int32 itemId)
        {
            if (ItemChanged != null)
                ItemChanged(this, new ItemEventArgs { ItemId = itemId });
        }
    }
}
