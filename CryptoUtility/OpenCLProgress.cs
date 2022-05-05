// ---------------------------------------------------------------------------------------
//                                    ILGPU Samples
//                           Copyright (c) 2021 ILGPU Project
//                                    www.ilgpu.net
//
// File: OpenCLProgress.cs
//
// This file is part of ILGPU and is distributed under the University of Illinois Open
// Source License. See LICENSE.txt for details.
// ---------------------------------------------------------------------------------------

using ILGPU;
using ILGPU.Runtime;
using ILGPU.Runtime.OpenCL;
using System;
using System.Runtime.CompilerServices;

namespace CryptoUtility
{

    /// <summary>
    /// Helper class to represent a single value that can be updated by the GPU to
    /// indicate progress of the kernel, and also read by the CPU to determine progress.
    /// </summary>
    class OpenCLProgress<T> : AcceleratorObject where T : unmanaged
    {
        #region Nested Types


        /// Constructs a OpenCL buffer using host memory.
        /// </summary>
        class OpenCLProgressMemoryBuffer : MemoryBuffer
        {

            public IntPtr BufferPtr { get; set; }
            public IntPtr MappedPtr { get; set; }
            public CLStream Stream { get; set; }
            /// <summary>
            public unsafe OpenCLProgressMemoryBuffer(CLAccelerator accelerator, long length, int elementSize) : base(accelerator, length, elementSize)
            {
                Stream = accelerator.DefaultStream as CLStream;
                CLException.ThrowIfFailed(CLAPI.CurrentAPI.CreateBuffer(accelerator.NativePtr, CLBufferFlags.CL_MEM_ALLOC_HOST_PTR | CLBufferFlags.CL_MEM_READ_WRITE, new IntPtr(LengthInBytes), IntPtr.Zero, out IntPtr bufferPtr));
                // Map the buffer into the host address space
                IntPtr mappedPtr = MyCLAPI.EnqueueMapBuffer(Stream.CommandQueue, bufferPtr, true, MyCLAPI.MapFlags.MapWrite | MyCLAPI.MapFlags.MapRead, new IntPtr(0), new IntPtr(LengthInBytes), 0, null, null, out CLError errCode);

                if (errCode != 0)
                    NativePtr = IntPtr.Zero;
                else
                    NativePtr = mappedPtr;
                MappedPtr= mappedPtr;
                BufferPtr= bufferPtr;
            }

            protected unsafe override void DisposeAcceleratorObject(bool disposing)
            {
                if (BufferPtr != IntPtr.Zero)
                {
                    MyCLAPI.EnqueueUnmapMemObject(Stream.CommandQueue, BufferPtr, MappedPtr, 0, null, null);
                    CLException.ThrowIfFailed(CLAPI.CurrentAPI.ReleaseBuffer(BufferPtr));
                }
                NativePtr = IntPtr.Zero;
            }

            protected override void CopyFrom(AcceleratorStream stream,
                                             in ArrayView<byte> sourceView,
                                             in ArrayView<byte> targetView) =>
                                    throw new NotImplementedException();

            protected override void CopyTo(AcceleratorStream stream,
                                            in ArrayView<byte> sourceView,
                                            in ArrayView<byte> targetView) =>
                                    throw new NotSupportedException();

            protected override void MemSet(AcceleratorStream stream,
                                           byte value,
                                           in ArrayView<byte> targetView) =>
                                    throw new NotSupportedException();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns an array view that can be used to update the progress on the GPU.
        /// </summary>
        public readonly ArrayView<T> View;

        /// <summary>
        /// Holds the OpenCL memory buffer that contains the progress value.
        /// </summary>
        private readonly OpenCLProgressMemoryBuffer memoryBuffer;

        /// <summary>
        /// Gets or sets the current progress value from the CPU.
        /// </summary>
        public unsafe T Value
        {
            [NotInsideKernel]
            get
            {
                var ptr = (T*)memoryBuffer.NativePtr.ToPointer();
                return *ptr;
            }
            [NotInsideKernel]
            set
            {
                var ptr = (T*)memoryBuffer.NativePtr.ToPointer();
                *ptr = value;
            }
        }
        #endregion

        #region Instance
        public IntPtr BufferPtr {get { return memoryBuffer.BufferPtr; }}
        public IntPtr MappedPtr { get { return memoryBuffer.MappedPtr; }}

        public OpenCLProgress(CLAccelerator accelerator) : base(accelerator)
        {
            memoryBuffer = new OpenCLProgressMemoryBuffer(accelerator, 1, Interop.SizeOf<T>());
            View = new ArrayView<T>(memoryBuffer, 0, memoryBuffer.Length);
            Value = default;
        }

        protected override void DisposeAcceleratorObject(bool disposing) =>  memoryBuffer.Dispose();
  

        #endregion
    }
}
