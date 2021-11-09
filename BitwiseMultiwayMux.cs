using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a mux with k input, each input with n wires. The output also has n wires.

    class BitwiseMultiwayMux : Gate
    {
        //Word size - number of bits in each output
        public int Size { get; private set; }

        //The number of control bits needed for k outputs
        public int ControlBits { get; private set; }

        public WireSet Output { get; private set; }
        public WireSet Control { get; private set; }
        public WireSet[] Inputs { get; private set; }

        //your code here

        private BitwiseMux[] bitwiseMuxes;
        public BitwiseMultiwayMux(int iSize, int cControlBits)
        {
            Size = iSize;
            Output = new WireSet(Size);
            Control = new WireSet(cControlBits);
            Inputs = new WireSet[(int)Math.Pow(2, cControlBits)];
            
            for (int i = 0; i < Inputs.Length; i++)
            {
                Inputs[i] = new WireSet(Size);
            }

            //your code here
            bitwiseMuxes = new BitwiseMux[(int)Math.Pow(2, cControlBits) - 1];
            for(int i = 0; i < bitwiseMuxes.Length; i++)
            {
                bitwiseMuxes[i] = new BitwiseMux(Size);
            }

            /*The idea is that We'll connect every two inputs to mux and every mux will decide which one "wins"
             * the next stage we'll remain with Size/2 inputs and we'll do the same on them, connect every two inputs to mux
             * and so on, at the end we will have only two outputs "at the final" and we will connect them to mux that will decide who will be the winner input and he will be the output
             * We'll connect the Inputs to the first bitwiseMuxes.length/2 bitwise muxeses.*/

            for(int i = 0, j = 0; i < bitwiseMuxes.Length/2 + 1; i++, j = j + 2)
            {
                bitwiseMuxes[i].ConnectInput1(Inputs[j]);
                bitwiseMuxes[i].ConnectInput2(Inputs[j+1]);  
            }


            for(int i = bitwiseMuxes.Length / 2 + 1, j = 0; i < bitwiseMuxes.Length ; i = i + 1, j = j + 2)
            {
                bitwiseMuxes[i].ConnectInput1(bitwiseMuxes[j].Output);
                bitwiseMuxes[i].ConnectInput2(bitwiseMuxes[j + 1].Output);
            }
            Output.ConnectInput(bitwiseMuxes[bitwiseMuxes.Length - 1].Output);

            //now we'll connect the controls to the mux gate
            int muxCounter = 0;
            for(int i = 0, j = (int)Math.Pow(2, Control.Size -1); i < Control.Size; i++, j = j / 2) 
            {
                for(int k = 0; k < j; k++)
                {
                    bitwiseMuxes[muxCounter].ConnectControl(Control[i]); 
                    muxCounter++; 
                }
            }
        }


        public void ConnectInput(int i, WireSet wsInput)
        {
            Inputs[i].ConnectInput(wsInput);
        }
        public void ConnectControl(WireSet wsControl)
        {
            Control.ConnectInput(wsControl);
        }




        public override bool TestGate()
        {

            //setting the inputs
            for(int i = 0; i < Math.Pow(2,ControlBits); i++)
            {
                int[] binaryI = makeBinary(i, Size);
                for (int j = 0; j < binaryI.Length; j++)
                    Inputs[i][j].Value = binaryI[j];
            }

            for(int i = 0; i < Math.Pow(2, ControlBits); i++)
            {
                int[] binaryI = makeBinary(i, ControlBits);
                for (int j = 0; j < binaryI.Length; j++) {
                    Control[i].Value = binaryI[j];
                }

                for (int j = 0; j < Size; j++)
                {
                    if (Output[j].Value != Inputs[i][j].Value)
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
