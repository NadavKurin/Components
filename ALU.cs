using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class is used to implement the ALU
    class ALU : Gate
    {
        //The word size = number of bit in the input and output
        public int Size { get; private set; }

        //Input and output n bit numbers
        //inputs
        public WireSet InputX { get; private set; }
        public WireSet InputY { get; private set; }
        public WireSet Control { get; private set; }

        //outputs
        public WireSet Output { get; private set; }
        public Wire Zero { get; private set; }
        public Wire Negative { get; private set; }


        //your code here
        public WireSet One { get; private set; }
        public WireSet ZeroBits { get; private set; }
        public BitwiseMultiwayMux mainMux { get; private set;}
        public BitwiseNotGate notX { get; private set; }
        public BitwiseNotGate notY { get; private set; }
        public BitwiseMultiwayMux notMux { get; private set; }
        public MultiBitAdder notMinusAdder { get; private set; }
        public BitwiseMultiwayMux firstAdditionMux  { get; private set; }
        public BitwiseMultiwayMux secondAdditionMux { get; private set; }
        public MultiBitAdder adder { get; private set; }

        public BitwiseAndGate xyBitwiseAnd { get; private set;}
        public MultiBitOrGate multiOrX { get; private set; }
        public MultiBitOrGate multiOrY { get; private set; }
        public AndGate logicXYRes { get; private set; }
        public WireSet logicXY { get; private set; }
        public BitwiseOrGate bitwiseXorY { get; private set; }
        public BitwiseOrGate logicXorY { get; private set; }
        public WireSet MinusOne { get; private set; }
        public ALU(int iSize)
        {
            Size = iSize;
            InputX = new WireSet(Size); // connect directly to main mux
            InputY = new WireSet(Size); // connect directly to main mux
            Control = new WireSet(6);
            Negative = new Wire();
            Zero = new Wire();
            Output = new WireSet(Size);
            One = new WireSet(Size); // connect to main mux
            One.Set2sComplement(1);
            MinusOne = new WireSet(iSize);
            MinusOne.Set2sComplement(-1);
            ZeroBits = new WireSet(Size);
            mainMux = new BitwiseMultiwayMux(iSize, Control.Size);
            notX = new BitwiseNotGate(iSize);
            notY = new BitwiseNotGate(iSize);
            notMux = new BitwiseMultiwayMux(iSize, 1);// connect to notMinusAdder
            notMinusAdder = new MultiBitAdder(iSize); // connect to main mux
            firstAdditionMux = new BitwiseMultiwayMux(iSize, 3); // connect to adder
            secondAdditionMux = new BitwiseMultiwayMux(iSize, 3);// connect to adder
            adder = new MultiBitAdder(iSize); // connect to main mux
            xyBitwiseAnd = new BitwiseAndGate(iSize); // connect to main mux
            multiOrX = new MultiBitOrGate(iSize);// connect to logicXYRes
            multiOrY = new MultiBitOrGate(iSize);
            logicXY = new WireSet(iSize);
            logicXYRes = new AndGate();// connect to main mux
            bitwiseXorY = new BitwiseOrGate(iSize);// connect to main mux
            logicXorY = new BitwiseOrGate(iSize);// connect to main mux

            //Create and connect all the internal components
            mainMux.ConnectControl(Control);
            mainMux.ConnectInput(0, ZeroBits);
            mainMux.ConnectInput(1, One);
            mainMux.ConnectInput(2, InputX);
            mainMux.ConnectInput(3, InputY);
            
            notX.ConnectInput(InputX);
            notY.ConnectInput(InputY);
            mainMux.ConnectInput(4, notX.Output);
            mainMux.ConnectInput(5, notY.Output);
            notMux.ConnectInput(0, notX.Output);
            notMux.ConnectInput(1, notY.Output);
            WireSet notMuxControl = new WireSet(1);
            notMuxControl[0].ConnectInput(Control[0]);
            notMux.ConnectControl(notMuxControl);
            notMinusAdder.ConnectInput1(notMux.Output);
            notMinusAdder.ConnectInput2(One);
            mainMux.ConnectInput(6, notMinusAdder.Output);
            mainMux.ConnectInput(7, notMinusAdder.Output);


            firstAdditionMux.ConnectInput(0, InputX);
            firstAdditionMux.ConnectInput(1, InputY);
            firstAdditionMux.ConnectInput(2, InputX);
            firstAdditionMux.ConnectInput(3, InputY);
            firstAdditionMux.ConnectInput(4, InputX);
            firstAdditionMux.ConnectInput(5, InputX);
            firstAdditionMux.ConnectInput(6, InputY);
            WireSet firstAddCont = new WireSet(3);
            firstAddCont[0].ConnectInput(Control[0]);
            firstAddCont[1].ConnectInput(Control[1]);
            firstAddCont[2].ConnectInput(Control[2]);
            firstAdditionMux.ConnectControl(firstAddCont);

            secondAdditionMux.ConnectInput(0, One);
            secondAdditionMux.ConnectInput(1, One);
            secondAdditionMux.ConnectInput(2, MinusOne);
            secondAdditionMux.ConnectInput(3, MinusOne);
            secondAdditionMux.ConnectInput(4, InputY);
            secondAdditionMux.ConnectInput(5, notMinusAdder.Output);
            secondAdditionMux.ConnectInput(6, notMinusAdder.Output);
            WireSet secondAddCont = new WireSet(3);
            secondAddCont[0].ConnectInput(Control[0]);
            secondAddCont[1].ConnectInput(Control[1]);
            secondAddCont[2].ConnectInput(Control[2]);
            secondAdditionMux.ConnectControl(secondAddCont);

            adder.ConnectInput1(firstAdditionMux.Output);
            adder.ConnectInput2(secondAdditionMux.Output);

            mainMux.ConnectInput(8, adder.Output);
            mainMux.ConnectInput(9, adder.Output);
            mainMux.ConnectInput(10, adder.Output);
            mainMux.ConnectInput(11, adder.Output);
            mainMux.ConnectInput(12, adder.Output);
            mainMux.ConnectInput(13, adder.Output);
            mainMux.ConnectInput(14, adder.Output);

            xyBitwiseAnd.ConnectInput1(InputX);
            xyBitwiseAnd.ConnectInput2(InputY);
            mainMux.ConnectInput(15, xyBitwiseAnd.Output);

            multiOrX.ConnectInput(InputX);
            multiOrY.ConnectInput(InputY);
            logicXYRes.ConnectInput1(multiOrX.Output);
            logicXYRes.ConnectInput2(multiOrY.Output);
            logicXY[0].ConnectInput(logicXYRes.Output);
            mainMux.ConnectInput(16, logicXY);

            bitwiseXorY.ConnectInput1(InputX);
            bitwiseXorY.ConnectInput2(InputY);
            mainMux.ConnectInput(17, bitwiseXorY.Output);

            logicXorY.ConnectInput1(InputX);
            logicXorY.ConnectInput2(InputY);
            mainMux.ConnectInput(18, logicXorY.Output);

            Output.ConnectInput(mainMux.Output);

            MultiBitOrGate zr = new MultiBitOrGate(Size);
            zr.ConnectInput(Output);
            NotGate notZr = new NotGate();
            notZr.ConnectInput(zr.Output);
            Zero.ConnectInput(notZr.Output);

            Negative.ConnectInput(Output[Size - 1]);
        }

        public override bool TestGate()
        {
            throw new NotImplementedException();
        }

    }
}
