using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AuctionSite.Data;
using System.Diagnostics;

namespace AuctionSite.Admin.Persistence
{
    public class AuctionSiteServicePersistence : IAuctionSitePersistence
    {
        private HttpClient _client;

        public AuctionSiteServicePersistence(String baseAddress)
        {
            _client = new HttpClient(); // a szolgáltatás kliense
            _client.BaseAddress = new Uri(baseAddress); // megadjuk neki a címet
        }

        public async Task<AdvertiserDTO> ReadAdvertiserIdAsync(String username)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("api/advertisers/" + username);
                if (response.IsSuccessStatusCode) // amennyiben sikeres a művelet
                {
                    return await response.Content.ReadAsAsync<AdvertiserDTO>();
                }
                else
                {

                    throw new PersistenceUnavailableException("Service returned response: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                throw new PersistenceUnavailableException(ex);
            }
        }

        public async Task<IEnumerable<ItemDTO>> ReadItemsAsync(int advertId)
        {
            try
            {
                // a kéréseket a kliensen keresztül végezzük
                HttpResponseMessage response = await _client.GetAsync("api/items/"+advertId);
                if (response.IsSuccessStatusCode) // amennyiben sikeres a művelet
                {
                    Debug.WriteLine(response.Content.ReadAsStringAsync());
                    var result = await response.Content.ReadAsAsync<IEnumerable<ItemDTO>>();
                    IEnumerable<ItemDTO> items = result;
                    // a tartalmat JSON formátumból objektumokká alakítjuk

                     foreach (ItemDTO item in items)
                     {
                         response = await _client.GetAsync("api/Categories/" + item.CategoryId);
                         if (response.IsSuccessStatusCode)
                         {
                            item.Category = await response.Content.ReadAsAsync<CategoryDTO>();                           
                         }

                        response = await _client.GetAsync("api/Bids/" + item.Id);
                        if (response.IsSuccessStatusCode)
                        {
                            item.Bids = (await response.Content.ReadAsAsync<IEnumerable<BidDTO>>()).ToList();
                            foreach (BidDTO bid in item.Bids)
                            {
                                response = await _client.GetAsync("api/Users/" + bid.UserId);
                                if (response.IsSuccessStatusCode)
                                {
                                    bid.User = await response.Content.ReadAsAsync<UserDTO>();
                                }
                            }
                        }
                    }

                    return items;
                }
                else
                {
                    
                    throw new PersistenceUnavailableException("Service returned response: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                throw new PersistenceUnavailableException(ex);
            }

        }

        public async Task<IEnumerable<CategoryDTO>> ReadCategoriesAsync()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("api/categories/");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<IEnumerable<CategoryDTO>>();
                }
                else
                {
                    throw new PersistenceUnavailableException("Service returned response: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                throw new PersistenceUnavailableException(ex);
            }
        }

        public async Task<Boolean> CreateItemAsync(ItemDTO item)
        {
            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync("api/items/", item); // az értékeket azonnal JSON formátumra alakítjuk
                item.Id = (await response.Content.ReadAsAsync<ItemDTO>()).Id; // a válaszüzenetben megkapjuk a végleges azonosítót
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new PersistenceUnavailableException(ex);
            }
        }

        public async Task<Boolean> UpdateItemAsync(ItemDTO item)
        {
            try
            {
                HttpResponseMessage response = await _client.PutAsJsonAsync("api/items/"+item.Id.ToString(), item);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new PersistenceUnavailableException(ex);
            }
        }

        public async Task<Boolean> LoginAsync(String userName, String userPassword)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("api/Security/Login/" + userName + "/" + userPassword);
                return response.IsSuccessStatusCode; // a művelet eredménye megadja a bejelentkezés sikeressségét
            }
            catch (Exception ex)
            {
                throw new PersistenceUnavailableException(ex);
            }
        }

        public async Task<Boolean> LogoutAsync()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("api/Security/Logout");
                return !response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new PersistenceUnavailableException(ex);
            }
        }
    }
}
