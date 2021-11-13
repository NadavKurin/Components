using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class represents a set of n wires (a cable)
    class WireSet
    {
        //Word size - number of bits in the register
        public int Size { get; private set; }

        public bool InputConected { get; private set; }

        //An indexer providing access to a single wire in the wireset
        public Wire this[int i]
        {
            get
            {
                return m_aWires[i];
            }
        }
        private Wire[] m_aWires;

        public WireSet(int iSize)
        {
            Size = iSize;
            InputConected = false;
            m_aWires = new Wire[iSize];
            for (int i = 0; i < m_aWires.Length; i++)
                m_aWires[i] = new Wire();
        }
        public override string ToString()
        {
            string s = "[";
            for (int i = m_aWires.Length - 1; i >= 0; i--)
                s += m_aWires[i].Value;
            s += "]";
            return s;
        }

        //Transform a positive integer value into binary and set the wires accordingly, with 0 being the LSB
        public void SetValue(int iValue)
        {
            int num = iValue;
            int i = 0;
            while (num >= 0 && i < Size)
            {
                if (num % 2 == 0)
                    m_aWires[i].Value = 0;
                else
                    m_aWires[i].Value = 1;
                num = num / 2;
                i++;
            }
        }

        //Transform the binary code into a positive integer
        public int GetValue()
        {
            int sum = 0;
            for (int i = 0; i < m_aWires.Length; i++)
                sum = sum + m_aWires[i].Value * (int)Math.Pow(2, i);
            return sum;
        }

        //Transform an integer value into binary using 2`s complement and set the wires accordingly, with 0 being the LSB
        //Transform an integer value into binary using 2`s complement and set the wires accordingly, with 0 being the LSB
        public void Set2sComplement(int iValue)
        {
            if (iValue >= 0)
                SetValue(iValue);
            else
            {
                int posVal = iValue * (-1);
                SetValue(posVal);
                for (int i = 0; i < m_aWires.Length; i++)
                {
                    if (m_aWires[i].Value == 0)
                        m_aWires[i].Value = 1;
                    else
                        m_aWires[i].Value = 0;
                }
                WireSet one = new WireSet(Size);
                one[0].Value = 1;
                MultiBitAdder add = new MultiBitAdder(Size);
                add.ConnectInput1(this);
                add.ConnectInput2(one);
                for (int i = 0; i < m_aWires.Length; i++)
                {
                    m_aWires[i].ConnectOutput(add.Output[i]);
                }
            }
        }

        //Transform the binary code in 2`s complement into an integer
        public int Get2sComplement()
        {
            if (m_aWires[m_aWires.Length - 1].Value == 0)
                return GetValue();
            else
            {
                for (int i = 0; i < m_aWires.Length; i++)
                {
                    if (m_aWires[i].Value == 0)
                        m_aWires[i].Value = 1;
                    else
                        m_aWires[i].Value = 0;
                }
                WireSet one = new WireSet(Size);
                one[0].Value = 1;
                MultiBitAdder add = new MultiBitAdder(Size);
                add.ConnectInput1(this);
                add.ConnectInput2(one);
                for (int i = 0; i < m_aWires.Length; i++)
                {
                    m_aWires[i].ConnectOutput(add.Output[i]);
                }
                int x = GetValue();
                return x * -1;
            }
        }

        public void ConnectInput(WireSet wIn)
        {
            if (InputConected)
                throw new InvalidOperationException("Cannot connect a wire to more than one inputs");
            if (wIn.Size != Size)
                throw new InvalidOperationException("Cannot connect two wiresets of different sizes.");
            for (int i = 0; i < m_aWires.Length; i++)
                m_aWires[i].ConnectInput(wIn[i]);

            InputConected = true;

        }

        public bool testSet2sComp()
        {
            //Tests 2complements methods
            int i;
            Set2sComplement(4);
            if (m_aWires[0].Value != 0 | m_aWires[1].Value != 0 | m_aWires[2].Value != 1 | m_aWires[3].Value != 0)
                return false;
            i = Get2sComplement();
            if (i != 4)
                return false;
            Set2sComplement(-4);
            if (m_aWires[0].Value != 0 | m_aWires[1].Value != 0 | m_aWires[2].Value != 1 | m_aWires[3].Value != 1)
                return false;
            i = Get2sComplement();
            if (i != -4)
                return false;

            Set2sComplement(7);
            if (m_aWires[0].Value != 1 | m_aWires[1].Value != 1 | m_aWires[2].Value != 1 | m_aWires[3].Value != 0)
                return false;
            i = Get2sComplement();
            if (i != 7)
                return false;
            Set2sComplement(-7);
            if (m_aWires[0].Value != 1 | m_aWires[1].Value != 0 | m_aWires[2].Value != 0 | m_aWires[3].Value != 1)
                return false;
            i = Get2sComplement();
            if (i != -7)
                return false;
            Set2sComplement(-8);
            if (m_aWires[0].Value != 0 | m_aWires[1].Value != 0 | m_aWires[2].Value != 0 | m_aWires[3].Value != 1)
                return false;
            i = Get2sComplement();
            if (i != -8)
                return false;

            return true;
        }
    }
} 