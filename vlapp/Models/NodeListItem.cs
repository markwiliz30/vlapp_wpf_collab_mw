using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Dragablz;

namespace vlapp.Models
{
    public class NodeListItem
    {

        public byte nodeIndex { get; set; }

        public byte[] BlindIp { get; set; }

        public byte BlindNumber { get; set; }

        public NodeListItem(byte BlindIndex , byte BlindNumber , byte[] BlindIp)
        {
           this.nodeIndex = BlindIndex;
           this.BlindIp = BlindIp;
           this.BlindNumber = BlindNumber;
        }   

    }
}
