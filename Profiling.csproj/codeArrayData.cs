using System;

namespace Profiling
{
    public class ArrayRunner : IRunner
    {
        void PC1()
        {
            var array = new C1[Constants.ArraySize];
            for (int i = 0; i < Constants.ArraySize; i++) array[i] = new C1();
        }

        void PS1()
        {
            var array = new S1[Constants.ArraySize];
        }

        void PC2()
        {
            var array = new C2[Constants.ArraySize];
            for (int i = 0; i < Constants.ArraySize; i++) array[i] = new C2();
        }

        void PS2()
        {
            var array = new S2[Constants.ArraySize];
        }

        void PC4()
        {
            var array = new C4[Constants.ArraySize];
            for (int i = 0; i < Constants.ArraySize; i++) array[i] = new C4();
        }

        void PS4()
        {
            var array = new S4[Constants.ArraySize];
        }

        void PC8()
        {
            var array = new C8[Constants.ArraySize];
            for (int i = 0; i < Constants.ArraySize; i++) array[i] = new C8();
        }

        void PS8()
        {
            var array = new S8[Constants.ArraySize];
        }

        void PC16()
        {
            var array = new C16[Constants.ArraySize];
            for (int i = 0; i < Constants.ArraySize; i++) array[i] = new C16();
        }

        void PS16()
        {
            var array = new S16[Constants.ArraySize];
        }

        void PC32()
        {
            var array = new C32[Constants.ArraySize];
            for (int i = 0; i < Constants.ArraySize; i++) array[i] = new C32();
        }

        void PS32()
        {
            var array = new S32[Constants.ArraySize];
        }

        void PC64()
        {
            var array = new C64[Constants.ArraySize];
            for (int i = 0; i < Constants.ArraySize; i++) array[i] = new C64();
        }

        void PS64()
        {
            var array = new S64[Constants.ArraySize];
        }

        void PC128()
        {
            var array = new C128[Constants.ArraySize];
            for (int i = 0; i < Constants.ArraySize; i++) array[i] = new C128();
        }

        void PS128()
        {
            var array = new S128[Constants.ArraySize];
        }

        void PC256()
        {
            var array = new C256[Constants.ArraySize];
            for (int i = 0; i < Constants.ArraySize; i++) array[i] = new C256();
        }

        void PS256()
        {
            var array = new S256[Constants.ArraySize];
        }

        void PC512()
        {
            var array = new C512[Constants.ArraySize];
            for (int i = 0; i < Constants.ArraySize; i++) array[i] = new C512();
        }

        void PS512()
        {
            var array = new S512[Constants.ArraySize];
        }

        public void Call(bool isClass, int size, int count)
        {
            if (isClass && size == 1)
            {
                for (int i = 0; i < count; i++) PC1();
                return;
            }

            if (!isClass && size == 1)
            {
                for (int i = 0; i < count; i++) PS1();
                return;
            }

            if (isClass && size == 2)
            {
                for (int i = 0; i < count; i++) PC2();
                return;
            }

            if (!isClass && size == 2)
            {
                for (int i = 0; i < count; i++) PS2();
                return;
            }

            if (isClass && size == 4)
            {
                for (int i = 0; i < count; i++) PC4();
                return;
            }

            if (!isClass && size == 4)
            {
                for (int i = 0; i < count; i++) PS4();
                return;
            }

            if (isClass && size == 8)
            {
                for (int i = 0; i < count; i++) PC8();
                return;
            }

            if (!isClass && size == 8)
            {
                for (int i = 0; i < count; i++) PS8();
                return;
            }

            if (isClass && size == 16)
            {
                for (int i = 0; i < count; i++) PC16();
                return;
            }

            if (!isClass && size == 16)
            {
                for (int i = 0; i < count; i++) PS16();
                return;
            }

            if (isClass && size == 32)
            {
                for (int i = 0; i < count; i++) PC32();
                return;
            }

            if (!isClass && size == 32)
            {
                for (int i = 0; i < count; i++) PS32();
                return;
            }

            if (isClass && size == 64)
            {
                for (int i = 0; i < count; i++) PC64();
                return;
            }

            if (!isClass && size == 64)
            {
                for (int i = 0; i < count; i++) PS64();
                return;
            }

            if (isClass && size == 128)
            {
                for (int i = 0; i < count; i++) PC128();
                return;
            }

            if (!isClass && size == 128)
            {
                for (int i = 0; i < count; i++) PS128();
                return;
            }

            if (isClass && size == 256)
            {
                for (int i = 0; i < count; i++) PC256();
                return;
            }

            if (!isClass && size == 256)
            {
                for (int i = 0; i < count; i++) PS256();
                return;
            }

            if (isClass && size == 512)
            {
                for (int i = 0; i < count; i++) PC512();
                return;
            }

            if (!isClass && size == 512)
            {
                for (int i = 0; i < count; i++) PS512();
                return;
            }

            throw new ArgumentException();
        }
    }
}