using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This gate implements the or operation. To implement it, follow the example in the And gate.
    class OrGate : TwoInputGate
    {
        //your code here 
        private NotGate m_gNot1;
        private NotGate m_gNot2;
        private NAndGate m_gNand;

        public OrGate()
        {
            //init the gates
            m_gNand = new NAndGate();
            m_gNot1 = new NotGate();
            m_gNot2 = new NotGate();
            //wire the output of the nand gate to the input of the not
            m_gNot1.ConnectInput(Input1); //Not on input 1
            m_gNot2.ConnectInput(Input2); //Not on input 2

            m_gNand.ConnectInput1(m_gNot1.Output); // Nand on the first not output
            m_gNand.ConnectInput2(m_gNot2.Output); // Nand on the second not output


            //set the inputs and the output of the and gate
            Output = m_gNand.Output;


        }


        public override string ToString()
        {
            return "Or " + Input1.Value + "," + Input2.Value + " -> " + Output.Value;
        }

        public override bool TestGate()
        {
            Input1.Value = 0;
            Input2.Value = 0;
             if (Output.Value != 0)
                return false;
            Input1.Value = 0;
            Input2.Value = 1;
            if (Output.Value != 1)
                return false;
            Input1.Value = 1;
            Input2.Value = 0;
            if (Output.Value != 1)
                return false;
            Input1.Value = 1;
            Input2.Value = 1;
            if (Output.Value != 1)
                return false;
            return true;
        }
    }

}
