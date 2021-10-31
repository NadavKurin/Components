using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a demux with k outputs, each output with n wires. The input also has n wires.

    class BitwiseMultiwayDemux : Gate
    {
        //Word size - number of bits in each output
        public int Size { get; private set; }

        //The number of control bits needed for k outputs
        public int ControlBits { get; private set; }

        public WireSet Input { get; private set; }
        public WireSet Control { get; private set; }
        public WireSet[] Outputs { get; private set; }

        //your code here
        private BitwiseDemux[] bitwiseDemuxes;
        public BitwiseMultiwayDemux(int iSize, int cControlBits)
        {
            Size = iSize;
            Input = new WireSet(Size);
            Control = new WireSet(cControlBits);
            Outputs = new WireSet[(int)Math.Pow(2, cControlBits)];
            for (int i = 0; i < Outputs.Length; i++)
            {
                Outputs[i] = new WireSet(Size);
            }
            //your code here
            bitwiseDemuxes = new BitwiseDemux[(int)Math.Pow(2, cControlBits) - 1];
            for (int i = 0; i < bitwiseDemuxes.Length; i++)
            {
                bitwiseDemuxes[i] = new BitwiseDemux(Size);
            }
            bitwiseDemuxes[0].ConnectInput(Input);

            for (int i = Outputs.Length-1, j = bitwiseDemuxes.Length - 1; i >= 0; i = i - 2, j--) 
            {
                Outputs[i].ConnectInput(bitwiseDemuxes[j].Output1);
                Outputs[i-1].ConnectInput(bitwiseDemuxes[j].Output2);
            }

            
            for (int i = 0; i < bitwiseDemuxes.Length / 2 - 1; i++)
            {
                bitwiseDemuxes[i*2 + 1].ConnectInput(bitwiseDemuxes[i].Output1);
                bitwiseDemuxes[i*2 + 2].ConnectInput(bitwiseDemuxes[i].Output2);
            }
            
            //now we'll connect the controls to the demux gate
            int controlNumber = Control.Size-1; 
            int demuxCounter = 0; 
            for (int i = 0, j = 1; i < Control.Size; i++, j = j * 2) 
            {
                for(int k = 0; k < j; k++)
                {
                    bitwiseDemuxes[demuxCounter].Control.ConnectInput(Control[controlNumber]);
                    demuxCounter++;
                }
                controlNumber--; 
            }

        }


        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }
        public void ConnectControl(WireSet wsControl)
        {
            Control.ConnectInput(wsControl);
        }


        public override bool TestGate()
        {
            throw new NotImplementedException();
        }
    }
}
