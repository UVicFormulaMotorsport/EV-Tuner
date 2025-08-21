using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV_Tuner
{
    //we use <> to specify parameter type
    public class GFG<T>
    {
        //private data member
        private T data;

        //using properties
        public T value
        {
            //using accessors
            get
            {
                return this.data;
            }
            set
            {
                this.data = value;
            }
        }
    }
    public struct bitField
    {
        public uint RawValue;

        //Haven't map the information goes to which position
        public bool pos1 => (RawValue & 0x01) != 0;
        public bool pos2 => (RawValue & 0x02) != 0;
        public bool pos3 => (RawValue & 0x04) != 0; 
        public bool pos4 => (RawValue & 0x08) != 0;
        public bool pos5 => (RawValue & 0x10) != 0;
        public bool pos6 => (RawValue & 0x20) != 0;
        public bool pos7 => (RawValue & 0x40) != 0;
        public bool pos8 => (RawValue & 0x80) != 0;

        //Get specific bit by position
        public bool GetBit(int position)
        {
            return (RawValue & (1u << position)) != 0;
        }

        //Set specific bit
        public void SetBit(int position, bool value)
        {
            if (value)
                RawValue |= (1u << position);
            else
                RawValue &= (1u << position);
        }
    }
}
