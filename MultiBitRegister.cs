﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class represents an n bit register that can maintain an n bit number
    class MultiBitRegister : Gate
    {
        public WireSet Input { get; private set; }
        public WireSet Output { get; private set; }
        //A bit setting the register operation to read or write
        public Wire Load { get; private set; }

        //Word size - number of bits in the register
        public int Size { get; private set; }

        public SingleBitRegister[] sb { get; private set;}

        public MultiBitRegister(int iSize)
        {
            Size = iSize;
            Input = new WireSet(Size);
            Output = new WireSet(Size);
            Load = new Wire();
            //your code here
            sb = new SingleBitRegister[iSize];

            for(int i = 0; i < iSize; i++)
            {
                sb[i] = new SingleBitRegister();
                sb[i].ConnectLoad(Load);
                sb[i].ConnectInput(Input[i]);
                Output[i].ConnectInput(sb[i].Output);
            }

        }

        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }

        
        public override string ToString()
        {
            return Output.ToString();
        }


        public override bool TestGate()
        {
            for(int i = 0; i < Size; i++)
            {
                return sb[i].TestGate();
            }
            return true;
        }
    }
}
