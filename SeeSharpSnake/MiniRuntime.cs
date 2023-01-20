namespace Internal.Runtime.CompilerHelpers
{
    // Una clase que el compilador observa para que tenga los helpers para inicializar el
    // procesos. El compilador puede manejar con gracia que los ayudantes no estén presentes,
    // pero la clase en sí misma que está ausente no se maneja. Agreguemos una clase vacía.
    class StartupCodeHelpers
    {
        [System.Runtime.RuntimeExport("RhpReversePInvoke2")]
        static void RhpReversePInvoke2(System.IntPtr frame) { }
        [System.Runtime.RuntimeExport("RhpReversePInvokeReturn2")]
        static void RhpReversePInvokeReturn2(System.IntPtr frame) { }
        [System.Runtime.RuntimeExport("RhpPInvoke")]
        static void RhpPinvoke(System.IntPtr frame) { }
        [System.Runtime.RuntimeExport("RhpPInvokeReturn")]
        static void RhpPinvokeReturn(System.IntPtr frame) { }
    }
}

namespace System
{
    class Array<T> : Array { }
}

namespace System.Runtime
{
    // Un atributo que el compilador entiende que instruccion esto para
    // exportar el metodo bajo el nombre simbolico dado.
    internal sealed class RuntimeExportAttribute : Attribute
    {
        public RuntimeExportAttribute(string entry) { }
    }
}

namespace System.Runtime.InteropServices
{
    public class UnmanagedType { }

    // Atributo personalizado que marca una clase teniendo una "Call" intrinseca.
    internal class McgIntrinsicsAttribute : Attribute { }

    internal enum OSPlatform
    {
        Windows,
        Linux,
    }
}

namespace System.Runtime.CompilerServices
{
    // Una clase responsable de correr los constructores. El compilador llamara este
    // codigo para asegurarse que los constructores estaticos corran y que solo se ejecuten una sola vez.
    [System.Runtime.InteropServices.McgIntrinsics]
    internal static class ClassConstructorRunner
    {
        private static unsafe IntPtr CheckStaticClassConstructionReturnNonGCStaticBase(ref StaticClassConstructionContext context, IntPtr nonGcStaticBase)
        {
            CheckStaticClassConstruction(ref context);
            return nonGcStaticBase;
        }

        private static unsafe void CheckStaticClassConstruction(ref StaticClassConstructionContext context)
        {
            // Clase constructora simplificada al maximo. En el mundo real, la clase constructora
            // necesitaria lidiar con potencialmente multiples hilos corriendo para inicializar
            // una sola clase, y esta necesitaria lidiar con esos posibles deadlocks
            // entre clases

            if (context.initialized == 1)
                return;

            context.initialized = 1;

            // Corriendo la clase constructor
            Call<int>(context.cctorMethodAddress);
        }

        // Es una llamada especial al compilador intrinseco que llama a este metodo
        [System.Runtime.CompilerServices.Intrinsic]
        public static extern T Call<T>(System.IntPtr pfn);
    }

    // Esta estructura de data es contratada con el compilador. Esta mantiene la direccion 
    // a un static constructor y a una bandera que especifica si el constructor fue ya ejecutado.
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct StaticClassConstructionContext
    {
        // Apunta el codigo para una clase estatica. Es inicializada por el binder/runtime 
        public IntPtr cctorMethodAddress;

        // La inicializacion del estado de la clase. Es iniciado en 0. Cada vez que sea gestionado el codigo
        // verifica el estado del cctor, el runtime llamara la clase CheckStaticClassConstruction con ese contexto
        // a menos que la estructura sea iniciada == 1. Esta verificacion especifica si la clase esta permitida almacenar mas que
        // un estado binario para cada cctor si asi lo desea.
        public int initialized;
    }

    [System.Runtime.InteropServices.McgIntrinsicsAttribute]
    internal class RawCalliHelper
    {
        public static unsafe ulong StdCall<T, U, W, X>(IntPtr pfn, T* arg1, U* arg2, W* arg3, X* arg4) where T : unmanaged where U : unmanaged where W : unmanaged where X : unmanaged
        {
            // Esto se completará con una transformación IL
            return 0;
        }
        public static unsafe ulong StdCall<T, U, W, X>(IntPtr pfn, T arg1, U* arg2, W* arg3, X* arg4) where T : struct where U : unmanaged where W : unmanaged where X : unmanaged
        {
            // Esto se completará con una transformación IL
            return 0;
        }
        public static unsafe ulong StdCall<T, U, W>(IntPtr pfn, T* arg1, U* arg2, W* arg3) where T : unmanaged where U : unmanaged where W : unmanaged
        {
            // Esto se completará con una transformación IL
            return 0;
        }
        public static unsafe ulong StdCall<T, U, W>(IntPtr pfn, T arg1, U* arg2, W* arg3) where T : unmanaged where U : unmanaged where W : unmanaged
        {
            // Esto se completará con una transformación IL
            return 0;
        }
        public static unsafe ulong StdCall<T, U, W>(IntPtr pfn, T* arg1, U arg2, W arg3) where T : unmanaged where U : unmanaged where W : unmanaged
        {
            // Esto se completará con una transformación IL
            return 0;
        }
        public static unsafe ulong StdCall<T, U>(IntPtr pfn, T* arg1, U* arg2) where T : unmanaged where U : unmanaged
        {
            // Esto se completará con una transformación IL
            return 0;
        }
        public static unsafe ulong StdCall<T, U>(IntPtr pfn, T* arg1, U arg2) where T : unmanaged where U : unmanaged
        {
            // Esto se completará con una transformación IL
            return 0;
        }
        public static unsafe ulong StdCall<T>(IntPtr pfn, T* arg1) where T : unmanaged
        {
            // Esto se completará con una transformación IL
            return 0;
        }
        public static unsafe ulong StdCall<T>(IntPtr pfn, T arg1) where T : unmanaged
        {
            // Esto se completará con una transformación IL
            return 0;
        }
    }
}
