using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A bitwise gate takes as input WireSets containing n wires, and computes a bitwise function - z_i=f(x_i)
    class BitwiseMux : BitwiseTwoInputGate
    {
        public Wire ControlInput { get; private set; }

        //your code here
        private MuxGate[] muxGates;
        public BitwiseMux(int iSize)
            : base(iSize)
        {
            ControlInput = new Wire();
            //your code here
            muxGates = new MuxGate[iSize];
            for (int i = 0; i < iSize; i++)
            {
                muxGates[i] = new MuxGate();
                muxGates[i].ConnectInput1(Input1[i]);
                muxGates[i].ConnectInput2(Input2[i]);
                muxGates[i].ConnectControl(ControlInput);
                Output[i].ConnectInput(muxGates[i].Output);
            }
        }

        public void ConnectControl(Wire wControl)
        {
            ControlInput.ConnectInput(wControl);
        }



        public override string ToString()
        {
            return "Mux " + Input1 + "," + Input2 + ",C" + ControlInput.Value + " -> " + Output;
        }




        public override bool TestGate()
        {
            //Set all wires to 0
            for (int i = 0; i < base.Size; i++)
            {
                Input1[i].Value = 0;
                Input2[i].Value = 0;
                ControlInput.Value = 0;
                if (Output[i].Value != 0)
                    return false;
            }

            //Set x_i to 1 and y_i to 0 control 0
            for (int i = 0; i < base.Size; i++)
            {
                Input1[i].Value = 1;
                Input2[i].Value = 0;
                ControlInput.Value = 0;
                if (Output[i].Value != 1)
                    return false;
            }
            //Set x_i to 1 and y_i to 0 control 1
            for (int i = 0; i < base.Size; i++)
            {
                Input1[i].Value = 1;
                Input2[i].Value = 0;
                ControlInput.Value = 1;
                if (Output[i].Value != 0)
                    return false;
            }

            //Set x_i to 0 and y_i to 1 control 0
            for (int i = 0; i < base.Size; i++)
            {
                Input1[i].Value = 0;
                Input2[i].Value = 1;
                ControlInput.Value = 0;
                if (Output[i].Value != 0)
                    return false;
            }
            //Set x_i to 0 and y_i to 1 control 0
            for (int i = 0; i < base.Size; i++)
            {
                Input1[i].Value = 0;
                Input2[i].Value = 1;
                ControlInput.Value = 1;
                if (Output[i].Value != 1)
                    return false;
            }
            return true;
        }
    }
}
