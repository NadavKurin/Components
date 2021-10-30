using System;
using System.Collections.Generic;
using System.Text;

namespace Components
{
    class MultiwayMux : Gate
    {
        public Wire Output { get; private set; }

        public WireSet Inputs { get; private set; }

        public WireSet Control { get; private set; }

        private NotGate[] controlNotGates;

        private MultiBitAndGate[] andGates;

        private Boolean[] FixedSizeBinaryArray;



        public MultiwayMux(int cControlBits)
        {
            int size = (int)Math.Pow(2, cControlBits);
            Output = new Wire();
            Inputs = new WireSet(size);
            Control = new WireSet(cControlBits);
            controlNotGates = new NotGate[cControlBits];
            andGates = new MultiBitAndGate[cControlBits + 1];
            //first well connect each control to notGate
            for (int i = 0; i < cControlBits; i++)
            {
                controlNotGates[i].ConnectInput(Control[i]);
            }

            //Connecting controllers to inputs
            FixedSizeBinaryArray = new Boolean[cControlBits];
            for (int i = 0; i < Inputs.Size; i++)
            {
                WireSet currentSet = MakeWireSetFromControllesrs(FixedSizeBinaryArray);
            }

            
        }

        private void incrementBinaryArrays(Boolean[] FixedSizeBinaryArray)
        {
            Boolean isChanged = false;
            int index = FixedSizeBinaryArray.Length-1;
            while (!isChanged)
            {
                if(!FixedSizeBinaryArray[index])
                {
                    FixedSizeBinaryArray[index] = !FixedSizeBinaryArray[index];
                    isChanged = true;
                }
                else
                {
                    while (FixedSizeBinaryArray[index])
                    {
                        FixedSizeBinaryArray[index] = !FixedSizeBinaryArray[index];
                        index--;
                    }
                    FixedSizeBinaryArray[index] = !FixedSizeBinaryArray[index];
                }
            }
        }

        private WireSet MakeWireSetFromControllesrs(Boolean[] arr)
        {
            WireSet wireSet = new WireSet(Control.Size + 1);
            for(int i = 1; i < wireSet.Size; i++)
            {
                if (arr[i - 1])
                    wireSet[i].Value = 1;
                else
                    wireSet[i].Value = 0;
            }
            return wireSet;
        }
        public override bool TestGate()
        {
            throw new NotImplementedException();
        }
    }
}
