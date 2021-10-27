using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A two input bitwise gate takes as input two WireSets containing n wires, and computes a bitwise function - z_i=f(x_i,y_i)
    class BitwiseAndGate : BitwiseTwoInputGate
    {
        //your code here
        private AndGate[] andGates;
        public BitwiseAndGate(int iSize)
            : base(iSize)
        {
            //your code here
            andGates = new AndGate[iSize];
            for(int i = 0; i <iSize; i++){
                andGates[i] = new AndGate();
                andGates[i].ConnectInput1(Input1[i]);
                andGates[i].ConnectInput2(Input2[i]);
                Output[i].ConnectInput(andGates[i].Output);
            }
        }

        //an implementation of the ToString method is called, e.g. when we use Console.WriteLine(and)
        //this is very helpful during debugging
        public override string ToString()
        {
            return "And " + Input1 + ", " + Input2 + " -> " + Output;
        }

        public override bool TestGate()
        {
            //Set all wires to 0
            for(int i = 0; i < base.Size; i++)
            {
                Input1[i].Value = 0;
                Input2[i].Value = 0;
                if (Output[i].Value != 0)
                    return false;
            }

            //Set all wires to 1
            for (int i = 0; i < base.Size; i++)
            {
                Input1[i].Value = 1;
                    Input2[i].Value = 1;
                if (Output[i].Value != 1)
                    return false;
            }
            //Set all x_i to 1 and all y_i to 0
            for (int i = 0; i < base.Size; i++)
            {
                Input1[i].Value = 1;
                Input2[i].Value = 0;
                if (Output[i].Value != 0)
                    return false;
            }
            //Set all x_i to 0 and all y_i to 1
            for (int i = 0; i < base.Size; i++)
            {
                Input1[i].Value = 0;
                Input2[i].Value = 1;
                if (Output[i].Value != 0)
                    return false;
            }
            return true;
        }
    }
}
