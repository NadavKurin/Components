using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This bitwise gate takes as input one WireSet containing n wires, and computes a bitwise function - z_i=f(x_i)
    class BitwiseNotGate : Gate
    {
        public WireSet Input { get; private set; }
        public WireSet Output { get; private set; }
        public int Size { get; private set; }

        //your code here
        private NotGate[] notGates;
        public BitwiseNotGate(int iSize)
        {
            Size = iSize;
            Input = new WireSet(Size);
            Output = new WireSet(Size);
            //your code here
            notGates = new NotGate[iSize];
            for (int i = 0; i < Size; i++)
            {
                notGates[i] = new NotGate();
                notGates[i].ConnectInput(Input[i]);
                Output[i].ConnectInput(notGates[i].Output);
            }

        }

        public void ConnectInput(WireSet ws)
        {
            Input.ConnectInput(ws);
        }

        //an implementation of the ToString method is called, e.g. when we use Console.WriteLine(not)
        //this is very helpful during debugging
        public override string ToString()
        {
            return "Not " + Input + " -> " + Output;
        }

        public override bool TestGate()
        {
            //Set all wires to 0
            for (int i = 0; i < Size; i++)
            {
                Input[i].Value = 0;
                if (Output[i].Value != 1)
                    return false;
            }

            //Set all wires to 1
            for (int i = 0; i < Size; i++)
            {
                Input[i].Value = 1;
                if (Output[i].Value != 0)
                    return false;
            }
            return true;
        }
    }
}
