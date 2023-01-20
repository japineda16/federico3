namespace System
{
    public class Object
    {
        // El diseño del objeto es un contrato con el compilador.
        public IntPtr m_pEEType;
    }
    public struct Void { }

    // El layout del tipo de dato elemental es Upper Cased porque puede ser recursivo
    // Estos no necesitan ningun campo setteado para funcionar
    public struct Boolean { }
    public struct Char { }
    public struct SByte { }
    public struct Byte { }
    public struct Int16 { }
    public struct UInt16 { }
    public struct Int32 { }
    public struct UInt32 { }
    public struct Int64 { }
    public struct UInt64 { }
    public struct UIntPtr { }
    public struct Single { }
    public struct Double { }

    // Un pointer para acceder a la memoria
    public unsafe struct IntPtr
    {
        private void* _value;
        public IntPtr(void* value) { _value = value; }
    }

    public abstract class ValueType { }
    public abstract class Enum : ValueType { }

    public struct Nullable<T> where T : struct { }

    public sealed class String
    {
        // El layout del tipo string es un contrato con el compilador
        public readonly int Length;
        public char _firstChar;

        public unsafe char this[int index]
        {
            [System.Runtime.CompilerServices.Intrinsic]
            get
            {
                return Internal.Runtime.CompilerServices.Unsafe.Add(ref _firstChar, index);
            }
        }
    }
    public abstract class Array { }
    public abstract class Delegate { }
    public abstract class MulticastDelegate : Delegate { }

    public struct RuntimeTypeHandle { }
    public struct RuntimeMethodHandle { }
    public struct RuntimeFieldHandle { }

    public class Attribute { }
}

// Un metodo para decirle al compilador generar X codigo en especifico
namespace System.Runtime.CompilerServices
{
    internal sealed class IntrinsicAttribute : Attribute { }

    public class RuntimeHelpers
    {
        public static unsafe int OffsetToStringData => sizeof(IntPtr) + sizeof(int);
    }

    public enum MethodImplOptions
    {
        NoInlining = 0x0008,
    }

    public sealed class MethodImplAttribute : Attribute
    {
        public MethodImplAttribute(MethodImplOptions methodImplOptions) { }
    }
}

// Es una capa Interoperability. Es usada para llamar codigo nativo.
namespace System.Runtime.InteropServices
{
    public enum CharSet
    {
        None = 1,
        Ansi = 2,
        Unicode = 3,
        Auto = 4,
    }

    public sealed class DllImportAttribute : Attribute
    {
        public string EntryPoint;
        public CharSet CharSet;
        public DllImportAttribute(string dllName) { }
    }

    public enum LayoutKind
    {
        Sequential = 0,
        Explicit = 2,
        Auto = 3,
    }

    public sealed class StructLayoutAttribute : Attribute
    {
        public StructLayoutAttribute(LayoutKind layoutKind) { }
    }
}
namespace Internal.Runtime.CompilerServices
{
    public static unsafe partial class Unsafe
    {
        // El cuerpo de este metodo es generado por el compilador
        // Lo que hara es lo que Unsafe.add esta programado para hacer. No es posible expresarlo en C#
        [System.Runtime.CompilerServices.Intrinsic]
        public static extern ref T Add<T>(ref T source, int elementOffset);
    }
}
