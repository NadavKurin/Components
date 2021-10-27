using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //Multibit gates take as input k bits, and compute a function over all bits - z=f(x_0,x_1,...,x_k)

    class MultiBitOrGate : MultiBitGate
    {
        //your code here
        private List<OrGate> orGates;
        public MultiBitOrGate(int iInputCount)
            : base(iInputCount)
        {
            //your code here
            orGates = new List<OrGate>();

            for (int i = 1; i < base.m_wsInput.Size - 1; i++)
            {
                if (i == 1)
                {
                    orGates.Add(new OrGate());
                    orGates.Last().ConnectInput1(base.m_wsInput[0]);
                    orGates.Last().ConnectInput2(base.m_wsInput[1]);
                }
                else
                {
                    orGates.Add(new OrGate());
                    orGates.Last().ConnectInput1(orGates[i - 2].Output);
                    orGates.Last().ConnectInput2(base.m_wsInput[i + 1]);
                }

            }

            Output = orGates.Last().Output;

        }

        public override string ToString()
        {
            String print = "";
            print = "MultiOrGate Input Values: ";
            for (int i = 0; i < base.m_wsInput.Size; i++)
            {
                print = print + ", " + base.m_wsInput[i].Value;
            }
            print = print + " Output: " + Output;
            return print;
        }

        public override bool TestGate()
        {
            //Set all wires to zero
            for (int i = 0; i < base.m_wsInput.Size; i++)
            {
                base.m_wsInput[i].Value = 0;
            }
            if (Output.Value != 0)
                return false;
            //Set all wires to 1
            for (int i = 0; i < base.m_wsInput.Size; i++)
            {
                base.m_wsInput[i].Value = 1;
            }
            if (Output.Value != 1)
                return false;
            return true;

            //Set even indexes to 1 and uneven to 0
            for (int i = 0; i < base.m_wsInput.Size; i++)
            {
                if (i % 2 == 0)
                {
                    base.m_wsInput[i].Value = 1;
                }
                else
                    base.m_wsInput[i].Value = 2;
            }
            if (Output.Value != 1)
                return false;
            return true;
        }
    }
   
}
