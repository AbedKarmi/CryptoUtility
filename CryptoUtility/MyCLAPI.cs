using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ILGPU.Runtime.OpenCL;

namespace CryptoUtility
{
    internal unsafe class MyCLAPI
    {
        public enum MapFlags : long
        {
            MapRead = ((int)(1 << 0)),
            MapWrite = ((int)(1 << 1)),
        }

        [System.Security.SuppressUnmanagedCodeSecurity()]

        [System.Runtime.InteropServices.DllImport("OpenCL.DLL", EntryPoint = "clEnqueueMapBuffer", ExactSpelling = true)]
        ///<summary>
        ///     Mapped pointers to device buffer for conventional pointer access
        ///</summary>
        internal extern static unsafe System.IntPtr EnqueueMapBuffer(
        IntPtr command_queue,
        IntPtr buffer,
        bool blocking_map,
        MapFlags map_flags,
        IntPtr offset,
        IntPtr cb,
        uint num_events_in_wait_list,
        IntPtr* event_wait_list,
        IntPtr* @event,
        out CLError errcode_ret);

        [System.Runtime.InteropServices.DllImport("OpenCL.DLL", EntryPoint = "clEnqueueUnmapMemObject", ExactSpelling = true)]
        ///<summary>
        ///     UnMapp pointers to device buffer 
        ///</summary>
        internal extern static unsafe System.IntPtr EnqueueUnmapMemObject(
        IntPtr command_queue,
        IntPtr buffer,
        IntPtr mapedPtr,
        uint num_events_in_wait_list,
        IntPtr* event_wait_list,
        IntPtr* @event);
    }
}
