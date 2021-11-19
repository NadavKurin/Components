using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a memory unit, containing k registers, each of size n bits.
    class Memory : SequentialGate
    {
        //The address size determines the number of registers
        public int AddressSize { get; private set; }
        //The word size determines the number of bits in each register
        public int WordSize { get; private set; }

        //Data input and output - a number with n bits
        public WireSet Input { get; private set; }
        public WireSet Output { get; private set; }
        //The address of the active register
        public WireSet Address { get; private set; }
        //A bit setting the memory operation to read or write
        public Wire Load { get; private set; }

        //your code here

        public BitwiseMultiwayDemux demux { get; private set; }
        public BitwiseMultiwayMux mux { get; private set; }
        public MultiBitRegister[] mbr { get; private set; }
        public Memory(int iAddressSize, int iWordSize)
        {
            AddressSize = iAddressSize;
            WordSize = iWordSize;

            Input = new WireSet(WordSize);
            Output = new WireSet(WordSize);
            Address = new WireSet(AddressSize);
            Load = new Wire();

            //your code here
            demux = new BitwiseMultiwayDemux(1, iAddressSize);
            mux = new BitwiseMultiwayMux(iWordSize, iAddressSize);
            WireSet loadWS = new WireSet(1);
            loadWS[0].ConnectInput(Load);
            demux.ConnectInput(loadWS);
            demux.ConnectControl(Address);
            mux.ConnectControl(Address);
            mbr = new MultiBitRegister[(int)Math.Pow(2, iAddressSize)];
            for(int i = 0; i < mbr.Length; i++)
            {
                mbr[i] = new MultiBitRegister(iWordSize);
                mbr[i].ConnectInput(Input);
                mbr[i].Load.ConnectInput(demux.Outputs[i][0]);
                mux.ConnectInput(i, mbr[i].Output);
            }
            Output.ConnectInput(mux.Output);
        }

        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }
        public void ConnectAddress(WireSet wsAddress)
        {
            Address.ConnectInput(wsAddress);
        }


        public override void OnClockUp()
        {
        }

        public override void OnClockDown()
        {
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public override bool TestGate()
        {
            for (int i = 0; i < mbr.Length; i++)
            {
                return mbr[i].TestGate();
            }
            return true;
        }
    }
}
