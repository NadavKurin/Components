using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A demux has 2 outputs. There is a single input and a control bit, selecting whether the input should be directed to the first or second output.
    class Demux : Gate
    {
        public Wire Output1 { get; private set; }
        public Wire Output2 { get; private set; }
        public Wire Input { get; private set; }
        public Wire Control { get; private set; }

        //your code here
        private AndGate and1;
        private AndGate and2;
        private NotGate not;
        public Demux()
        {
            Input = new Wire();
            //your code here
            Control = new Wire();
            and1 = new AndGate();
            and2 = new AndGate();
            not = new NotGate();

            //connect not conrol and input to and1 gate
            not.ConnectInput(Control);
            and1.ConnectInput1(not.Output);
            and1.ConnectInput2(Input);


            //connect control and input to and2 gate
            and2.ConnectInput1(Control);
            and2.ConnectInput2(Input);

            Output1 = and1.Output;
            Output2 = and2.Output;
        }

        public void ConnectControl(Wire wControl)
        {
            Control.ConnectInput(wControl);
        }
        public void ConnectInput(Wire wInput)
        {
            Input.ConnectInput(wInput);
        }

        public override string ToString()
        {
            return "DeMux " + Input.Value + ",C" + Control.Value + " Output1-> " + Output1.Value + " Output2-> " + Output2.Value;
        }


        public override bool TestGate()
        {
            Input.Value = 0;
            Control.Value = 0;
            if (Output1.Value != 0 || Output2.Value != 0)
                return false;
            Input.Value = 1;
            Control.Value = 0;
            if (Output1.Value != 1 || Output2.Value != 0)
                return false;
            Input.Value = 0;
            Control.Value = 1;
            if (Output1.Value != 0 || Output2.Value != 0)
                return false;
            Input.Value = 1;
            Control.Value = 1;
            if (Output1.Value != 0 || Output2.Value != 1)
                return false;
            return true;
        }
    }
}
