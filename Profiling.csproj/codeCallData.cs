using System;

namespace Profiling
{
    public class CallRunner : IRunner
    {
        void PC1(C1 o) {}
        void PS1(S1 o) {}
        void PC2(C2 o) {}
        void PS2(S2 o) {}
        void PC4(C4 o) {}
        void PS4(S4 o) {}
        void PC8(C8 o) {}
        void PS8(S8 o) {}
        void PC16(C16 o) {}
        void PS16(S16 o) {}
        void PC32(C32 o) {}
        void PS32(S32 o) {}
        void PC64(C64 o) {}
        void PS64(S64 o) {}
        void PC128(C128 o) {}
        void PS128(S128 o) {}
        void PC256(C256 o) {}
        void PS256(S256 o) {}
        void PC512(C512 o) {}
        void PS512(S512 o) {}
        public void Call(bool isClass, int size, int count)
        {
            if (isClass && size == 1)
            {var o=new C1(); for (int i=0; i<count; i++) PC1(o);
                return;
            }
            if (!isClass && size == 1)
            {var o=new S1(); for (int i=0; i<count; i++) PS1(o);
                return;
            }
            if (isClass && size == 2)
            {var o=new C2(); for (int i=0; i<count; i++) PC2(o);
                return;
            }
            if (!isClass && size == 2)
            {var o=new S2(); for (int i=0; i<count; i++) PS2(o);
                return;
            }
            if (isClass && size == 4)
            {var o=new C4(); for (int i=0; i<count; i++) PC4(o);
                return;
            }
            if (!isClass && size == 4)
            {var o=new S4(); for (int i=0; i<count; i++) PS4(o);
                return;
            }
            if (isClass && size == 8)
            {var o=new C8(); for (int i=0; i<count; i++) PC8(o);
                return;
            }
            if (!isClass && size == 8)
            {var o=new S8(); for (int i=0; i<count; i++) PS8(o);
                return;
            }
            if (isClass && size == 16)
            {var o=new C16(); for (int i=0; i<count; i++) PC16(o);
                return;
            }
            if (!isClass && size == 16)
            {var o=new S16(); for (int i=0; i<count; i++) PS16(o);
                return;
            }
            if (isClass && size == 32)
            {var o=new C32(); for (int i=0; i<count; i++) PC32(o);
                return;
            }
            if (!isClass && size == 32)
            {var o=new S32(); for (int i=0; i<count; i++) PS32(o);
                return;
            }
            if (isClass && size == 64)
            {var o=new C64(); for (int i=0; i<count; i++) PC64(o);
                return;
            }
            if (!isClass && size == 64)
            {var o=new S64(); for (int i=0; i<count; i++) PS64(o);
                return;
            }
            if (isClass && size == 128)
            {var o=new C128(); for (int i=0; i<count; i++) PC128(o);
                return;
            }
            if (!isClass && size == 128)
            {var o=new S128(); for (int i=0; i<count; i++) PS128(o);
                return;
            }
            if (isClass && size == 256)
            {var o=new C256(); for (int i=0; i<count; i++) PC256(o);
                return;
            }
            if (!isClass && size == 256)
            {var o=new S256(); for (int i=0; i<count; i++) PS256(o);
                return;
            }
            if (isClass && size == 512)
            {var o=new C512(); for (int i=0; i<count; i++) PC512(o);
                return;
            }
            if (!isClass && size == 512)
            {var o=new S512(); for (int i=0; i<count; i++) PS512(o);
                return;
            }
            throw new ArgumentException();}
    }
}