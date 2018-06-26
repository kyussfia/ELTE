using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSite.Admin.Model
{
    public class ItemEventArgs : EventArgs
    {
        public int ItemId { get; set; }
    }
}
