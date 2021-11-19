using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a register that can maintain 1 bit.
    class SingleBitRegister : Gate
    {
        public Wire Input { get; private set; }
        public Wire Output { get; private set; }
        //A bit setting the register operation to read or write
        public Wire Load { get; private set; }

        public DFlipFlopGate dff {get; private set;}
        public MuxGate mux { get; private set; }
        public SingleBitRegister()
        {
            
            Input = new Wire();
            Load = new Wire();
            //your code here 

            mux = new MuxGate();
            dff = new DFlipFlopGate();

            mux.ConnectInput1(dff.Output);
            mux.ConnectInput2(Input);
            mux.ConnectControl(Load);

            dff.ConnectInput(mux.Output);

            Output = dff.Output;

        }

        public void ConnectInput(Wire wInput)
        {
            Input.ConnectInput(wInput);
        }

      

        public void ConnectLoad(Wire wLoad)
        {
            Load.ConnectInput(wLoad);
        }


        public override bool TestGate()
        {

            Load.Value = 1;
            Input.Value = 0;
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.Value != 0)
                return false;
            Load.Value = 0;
            Input.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.Value != 0)
                return false;
            Load.Value = 1;
            Input.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.Value != 1)
                return false;
            return true;
           
        }
    }
}
