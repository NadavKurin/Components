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


            for (int j = 0; j < Outputs.Length; j += 2)
            {
                Outputs[j].ConnectInput(bitwiseDemuxes[j / 2].Output1);
                Outputs[j+1].ConnectInput(bitwiseDemuxes[j / 2].Output2);
            }

            for (int i = Outputs.Length/2, j = 0; i < Outputs.Length-1; i = i + 1, j = j + 2)
            {
                bitwiseDemuxes[j].ConnectInput(bitwiseDemuxes[i].Output1);
                bitwiseDemuxes[j+1].ConnectInput(bitwiseDemuxes[i].Output2);
            }
            bitwiseDemuxes[bitwiseDemuxes.Length - 1].ConnectInput(Input);

            //now we'll connect the controls to the demux gate
            int controlNumber = Control.Size-1; 
            int demuxNumber = bitwiseDemuxes.Length - 1; 
            for (int i = 0, j = 1; i < Control.Size; i++, j = j * 2) 
            {
                for(int k = 0; k < j; k++)
                {
                    bitwiseDemuxes[demuxNumber].Control.ConnectInput(Control[controlNumber]);
                    demuxNumber--;
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
            //setting the Outputs
            for (int i = 0; i < Math.Pow(2, ControlBits); i++)
            {
                int[] binaryI = makeBinary(i, Size);
                for (int j = 0; j < binaryI.Length; j++)
                    Outputs[i][j].Value = binaryI[j];
            }

            for (int i = 0; i < Math.Pow(2, ControlBits); i++)
            {
                int[] binaryI = makeBinary(i, ControlBits);
                for (int j = 0; j < binaryI.Length; j++)
                {
                    Control[i].Value = binaryI[j];
                }

                for (int j = 0; j < Size; j++)
                {
                    if (Input[j].Value != Outputs[i][j].Value)
                        return false;
                }
            }

            return true;
        }


        public static int[] makeBinary(int num, int length)
        {
            string b = Convert.ToString(num, 2);
            while (b.Length < length)
                b = "0" + b;

            int[] binaryNumInArray = new int[length];
            for (int i = 0; i < length; i++)
                binaryNumInArray[i] = int.Parse(b[i].ToString());

            return binaryNumInArray;
        }
    }
}
