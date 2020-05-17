using System;
namespace Amber.Data.Enums
{
    [Flags]
    public enum Side
    {
        top=0,
        left=1,
        right=2,
        front=4,
        back=8
    }

    public enum ComponentType
    {
        body=0,
        sideskirt=1,
        sidearmor=2,
        mudguard=3,
        frontplate=4
    }
}
