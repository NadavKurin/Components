using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a HalfAdder, taking as input 2 bits - 2 numbers and computing the result in the output, and the carry out.

    class HalfAdder : TwoInputGate
    {
        public Wire CarryOutput { get; private set; }

        //your code here
        private AndGate andGate;
        private XorGate xorGate;

        public HalfAdder()
        {
            //your code here
            andGate = new AndGate();
            xorGate = new XorGate();
            CarryOutput = new Wire();

            andGate.ConnectInput1(Input1);
            andGate.ConnectInput2(Input2);

            xorGate.ConnectInput1(Input1);
            xorGate.ConnectInput2(Input2);

            Output.ConnectInput(xorGate.Output);
            CarryOutput.ConnectInput(andGate.Output);

        }


        public override string ToString()
        {
            return "HA " + Input1.Value + "," + Input2.Value + " -> " + Output.Value + " (C" + CarryOutput + ")";
        }

        public override bool TestGate()
        {
            Input1.Value = 0;
            Input2.Value = 0;
            if (Output.Value != 0 || CarryOutput.Value != 0)
                return false;
            Input1.Value = 0;
            Input2.Value = 1;
            if (Output.Value != 1 || CarryOutput.Value != 0)
                return false;
            Input1.Value = 1;
            Input2.Value = 0;
            if (Output.Value != 1 || CarryOutput.Value != 0)
                return false;
            Input1.Value = 1;
            Input2.Value = 1;
            if (Output.Value != 0 || CarryOutput.Value != 1)
                return false;
            return true;
        }
    }
}
