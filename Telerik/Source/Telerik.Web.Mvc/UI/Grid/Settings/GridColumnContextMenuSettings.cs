﻿// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    public class GridColumnContextMenuSettings : IClientSerializable
    {
        private readonly IGrid grid;

        public GridColumnContextMenuSettings(IGrid grid)
        {
            this.grid = grid;                        
        }

        public bool Enabled
        {
            get;
            set;
        }        

        public void SerializeTo(string key, IClientSideObjectWriter writer)
        {
            if (Enabled)
            {                
                writer.Append("columnContextMenu", Enabled);
            }
        }
    }
}
