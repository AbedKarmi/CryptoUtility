using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using ILGPU;
using ILGPU.Algorithms;
using ILGPU.Runtime;
using ILGPU.Runtime.OpenCL;
using ILGPU.Runtime.Cuda;
using System.Threading;
using System.Runtime.InteropServices;


// ILGPU Latest Version is Bugy 
namespace CryptoUtility;

public class GpuEventArgs : EventArgs
{
    public GpuEventArgs(string acc, ulong progress)
    {
        Accelerator = acc;
        Progress= progress;
    }

    public string Accelerator { get; set; }
    public ulong Progress { get; set; }
}


internal class GPUClass
{
    public bool Abort { get; set; } = false;
    public const ulong progressScale = 1000000;
    public static ulong ProgressScale { get { return progressScale; } }

    public event EventHandler<GpuEventArgs> ProcessCompleted, ProcessProgress;

    protected virtual void OnProcessCompleted(GpuEventArgs e)
    {
        ProcessCompleted?.Invoke(this, e);
    }

    protected virtual void OnProcessProgress(GpuEventArgs e)
    {
        ProcessProgress?.Invoke(this, e);
    }


    // The following Summary is important for ILGPU.Algorithms to be used
    /// <summary>
    ///     A custom kernel using <see cref="XMath" /> functions.
    /// </summary>
    public static void MathKernel(Index1D index, // The global thread index (1D in this case)
                                 double P,
                                 double Q,
                                 ArrayView<double> result,
                                 char func)
    {
        double R;
        switch (func)
        {
            case '+':
                R = P + Q;
                break;
            case '-':
                R = P - Q;
                break;
            case '*':
                R = P * Q;
                break;
            case '/':
                R = P / Q;
                break;
            case '^':
                R = XMath.Pow(P, Q);
                break;
            case '%':
                R = P % Q;
                ;
                break;
            default:
                R = 0;
                break;
        }

        result[0] = R;
        //    byte[] r = R.ToByteArray();
        //    for (index = 0; index < r.Length; index++) result[index] = r[index];
    }

  /*  private static BigInteger GetBig(ArrayView<byte> data)
    {
        var inArr = new byte[data.Length];
        for (var i = 0; i < data.Length; i++) inArr[i] = data[data.Length - i - 1];
        var final = new byte[data.Length +
                             1]; // Add an empty byte at the end, to simulate unsigned BigInteger (no negatives!)
        Array.Copy(inArr, final, inArr.Length);
        return new BigInteger(final);
    }

    /*
     * struct Primes { public BigInteger P; 
                    public BigInteger Q;
                  };
    */

    public static string[] GetAccelerators()
    {
        var i = 0;
        string[] accelerators = { "" };

        using (var context = Context.CreateDefault())
        {
            // For each available accelerator...
            foreach (var device in context)
            {
                using var accelerator = device.CreateAccelerator(context);
                Array.Resize(ref accelerators, ++i);
                var s = accelerator.Name;
                accelerators[i - 1] = s; // s.Substring(s.LastIndexOf(":") + 1);
            }
        }

        return accelerators;
    }

    public static string AcceleratorInfo(Accelerator accelerator)
    {
        if (accelerator == null) return "";
        StringWriter sw = new();
        accelerator.PrintInformation(sw);
        return sw.ToString();
      /*  return string.Format($"Name: {accelerator.Name}") + "\r\n" + "\r\n" +
               string.Format($"MemorySize: {accelerator.MemorySize}") + "\r\n" +
               string.Format($"MaxThreadsPerGroup: {accelerator.MaxNumThreadsPerGroup}") + "\r\n" +
               string.Format($"MaxThreads: {accelerator.MaxNumThreads}") + "\r\n" +
               string.Format($"MaxNumThreadsPerMultiprocessor: {accelerator.MaxNumThreadsPerMultiprocessor}") + "\r\n" +
               string.Format($"MaxSharedMemoryPerGroup: {accelerator.MaxSharedMemoryPerGroup}") + "\r\n" +
               string.Format($"MaxGroupSize: {accelerator.MaxGroupSize}") + "\r\n" +
               string.Format($"MaxGridSize: {accelerator.MaxGridSize}") + "\r\n" +
               string.Format($"MaxConstantMemory: {accelerator.MaxConstantMemory}") + "\r\n" +
               string.Format($"WarpSize: {accelerator.WarpSize}") + "\r\n" +
               string.Format($"NumMultiprocessors: {accelerator.NumMultiprocessors}" +"\r\n" +
               "------------------------------------------------------------------- \r\n\r\n" + sw);
        */      
               
    }

    public static int MaxThreads(int index)
    {
        try
        {
            var acc = GetAccelerator(index);
            if (acc != null)
            {
                var m = acc.MaxNumThreads;
                acc.Dispose();
                return m;
            }
        }
        catch { }

        return 0;
    }
    public static string AcceleratorInfo(int index)
    {
        try
        {
            var acc = GetAccelerator(index);
            if (acc != null)
            {
                var s = AcceleratorInfo(acc);
                acc.Dispose();
                return s;
            }
        }
        catch { }

        return "";
    }

    public static string AcceleratorInfo(CLDevice id)
    {
        try
        { 
            var acc = GetAccelerator(id);
            if (acc != null)
            {
                var s = AcceleratorInfo(acc);
                acc.Dispose();
                return s;
            }
        }
        catch { }

        return "";
    }

    public static string AcceleratorInfo(string name)
    {
        try 
        { 
            var acc = GetAccelerator(name);
            if (acc != null)
            {
                var s = AcceleratorInfo(acc);
                acc.Dispose();
                return s;
            }
        }
        catch { }

        return "";
    }

    public static Accelerator GetAccelerator(int index)
    {
        using (var context = Context.CreateDefault())
        {
            var i = 0;
            foreach (var device in context)
            {
                i++;
                if (i == index)
                    using (var acc = device.CreateAccelerator(context))
                    {
                        return acc;
                    }
            }
        }

        return null;
    }

    public static Accelerator GetAccelerator(CLDevice id)
    {
        using (var context = Context.CreateDefault())
        using (var acc = id.CreateAccelerator(context))
        {
            return acc;
        }

        ;
    }

    public static Accelerator GetAccelerator(string name)
    {
        using (var context = Context.CreateDefault())
        {
            foreach (var device in context)
                if (device.Name.ToString().Equals(name))
                    using (var acc = device.CreateAccelerator(context))
                    {
                        return acc;
                    }
        }

        return null;
    }

    public BigInteger Calc(string accelName, BigInteger P, BigInteger Q, char func)
    {
        BigInteger R = 0;

        using (var context = Context.Create(builder => builder.Default().EnableAlgorithms()))
        {
            // For each available accelerator...
            foreach (var device in context)
                if (device.CreateAccelerator(context).Name.Equals(accelName))
                    // Create default accelerator for the given accelerator id
                    using (var accelerator = device.CreateAccelerator(context))
                    {
                        var kernel =
                            accelerator.LoadAutoGroupedStreamKernel<Index1D, double, double, ArrayView<double>, char>(MathKernel);

                        var p = (double)P;
                        var q = (double)Q;

                        using var buffer = accelerator.Allocate1D<double>(1);
                        // Launch buffer.Length many threads and pass a view to buffer
                        // Note that the kernel launch does not involve any boxing
                        kernel((int)buffer.Length, p, q, (ArrayView<double>)buffer, func);

                        // Wait for the kernel to finish...
                        accelerator.Synchronize();

                        var data = buffer.GetAsArray1D();
                        // Resolve and verify data
                        R = (BigInteger)data[0];
                        buffer.Dispose();
                        OnProcessCompleted(new GpuEventArgs(accelerator.Name,0));
                    }
        }

        return R;
    }
    public static ulong Reflect(ulong value, int width)
    {
        // reflects the lower 'width' bits of 'value'

        ulong j = 1;
        ulong result = 0;

        for (ulong i = 1UL << (width - 1); i != 0; i >>= 1)
        {
            if ((value & i) != 0)
            {
                result |= j;
            }
            j <<= 1;
        }
        return result;
    }

    public static ulong CalculateCRCdirect(ArrayView<byte> data, int width, ulong poly, ulong init, bool refin, bool refout, ulong xorout, int length)
    {
        // fast bit by bit algorithm without augmented zero bytes.
        // does not use lookup table, suited for polynom orders between 1...32.
        ulong c, bit;
        ulong crc = init;

        ulong crcMask = ((((ulong)1 << (width - 1)) - 1) << 1) | 1;
        ulong crcHighBitMask = (ulong)1 << (width - 1);

        for (int i = 0; i < length; i++)
        {
            c = (ulong)data[i];
            if (refin)
            {
                c = Reflect(c, 8);
            }

            for (ulong j = 0x80; j > 0; j >>= 1)
            {
                bit = crc & crcHighBitMask;
                crc <<= 1;
                if ((c & j) > 0) bit ^= crcHighBitMask;
                if (bit > 0) crc ^= poly;
            }
        }

        if (refout)
        {
            crc = Reflect(crc, width);
        }
        crc ^= xorout;
        crc &= crcMask;

        return crc;
    }


    public static void FindAllPoly(Index1D index, 
                                   ArrayView<byte> data, 
                                   int width, 
                                   ulong crc, 
                                   ArrayView2D<ulong, Stride2D.DenseX> polyList,
                                   ulong maxSegment,
                                   ulong maxValue,
                                   int maxResults, 
                                   ArrayView<byte> abort,
                                   ArrayView<ulong> progress)
    {
        int i = 0;

        for (ulong poly = (ulong)index * maxSegment; poly < ((ulong)index * maxSegment + maxSegment) && (poly < maxValue) && i < maxResults && abort[0]!=1; poly++)
        {
            if (CalculateCRCdirect(data, width, poly, 0, false, false, 0, (int)data.Length) == crc) polyList[index, ++i] =  poly;
            if (poly % progressScale == 0) Atomic.Add(ref progress[0], 1);   //  can be read by cpu
        }
        
     
        polyList[index,0] =(ulong)i ;
        return;
    }

    public unsafe ulong[] GPUFindAllPoly(string accelName,byte[] data,int width, ulong crc,int maxThreads,int maxResults)
    {
        ulong maxValue = width == 64 ? ulong.MaxValue : ((ulong)1 << width);
        if ((ulong)maxThreads > maxValue) maxThreads = (int) maxValue;
        ulong maxSegment = maxValue / (ulong)maxThreads;
        if ((ulong)maxSegment * (ulong)maxThreads != maxValue) maxSegment++;

        ulong[,] result=new ulong[maxThreads,maxResults];
        List<ulong> finalResult = new();

        using (var context = Context.Create(builder => builder.Default().EnableAlgorithms()))
        {
            // For each available accelerator...
            foreach (var device in context)
                if (device.CreateAccelerator(context).Name.Equals(accelName))
                    // Create default accelerator for the given accelerator id
                    using (var accelerator = device.CreateAccelerator(context))
                    {
                        var kernel = accelerator.LoadAutoGroupedStreamKernel<Index1D, ArrayView<byte>, int, ulong, ArrayView2D<ulong, Stride2D.DenseX>,ulong,ulong, int,ArrayView<byte>, ArrayView<ulong>>(FindAllPoly);

                        using var buffer = accelerator.Allocate2DDenseX<ulong>(new Index2D(maxThreads,maxResults));
                        using var gdata = accelerator.Allocate1D<byte>(data.Length);

                        // These will not work here, will not be updated by CPU
                        // Just to make the call work with same signature
                        using var abort = accelerator.Allocate1D<byte>(1);
                        using var progress = accelerator.Allocate1D<ulong>(1);

                        gdata.CopyFromCPU(data);

                      //  using var marker = accelerator.AddProfilingMarker();

                        // Launch buffer.Length many threads and pass a view to buffer
                        // Note that the kernel launch does not involve any boxing
                        kernel(maxThreads, gdata.View, width, crc, buffer.View,maxSegment,maxValue, maxResults,abort.View,progress.View);

                        //     marker.Synchronize();

                        // Wait for the kernel to finish...
                        var thread = new Thread(o => accelerator.Synchronize());
                        thread.Start();

                        Abort = false;
                        var e = new GpuEventArgs(accelerator.Name, 0);
                        ulong[] p=new ulong[1];

                        OnProcessProgress(new GpuEventArgs("{Started}", 0));

                        while (!Abort && thread.IsAlive)
                        {
                            System.Windows.Forms.Application.DoEvents(); Thread.Sleep(100);
                            progress.CopyToCPU(p);
                            OnProcessProgress(new GpuEventArgs(accelerator.Name, p[0]));
                        }

                        if (Abort)
                        {
                            //thread.Abort();
                            thread.Interrupt();
                            thread.Join();

                            while (thread.IsAlive)
                            {
                                System.Windows.Forms.Application.DoEvents(); Thread.Sleep(100);
                            }
                        }

                        result = buffer.GetAsArray2D();

                        buffer.Dispose();

                        progress.CopyToCPU(p);
                        OnProcessCompleted(new GpuEventArgs(accelerator.Name,p[0]));
                        
                    }
        }

        if (result!=null)        
        for (int i = 0; i < result.GetLength(0); i++)
            for (int j= 0; j < (int)result[i, 0];j++)
                finalResult.Add(result[i, j+1]);
        
        return finalResult.ToArray();
    }


    public ulong[] CudaFindAllPolyMonitored(string accelName, byte[] data, int width, ulong crc, int maxThreads, int maxResults)
    {
        ulong maxValue = width == 64 ? ulong.MaxValue : ((ulong)1 << width);
        if ((ulong)maxThreads > maxValue) maxThreads = (int)maxValue;
        ulong maxSegment = maxValue / (ulong)maxThreads;
        if ((ulong)maxSegment * (ulong)maxThreads != maxValue) maxSegment++;

        ulong[,] result = new ulong[maxThreads, maxResults];
        List<ulong> finalResult = new();

        using var context = Context.Create(builder => builder.Cuda().Profiling());
        {
            // For each available accelerator...
            foreach (var device in context.GetCudaDevices())
                if (device.CreateAccelerator(context).Name.Equals(accelName))
                {
                    // Create default accelerator for the given accelerator id
                    using var accelerator = device.CreateCudaAccelerator(context);

                    var kernel = accelerator.LoadAutoGroupedStreamKernel<Index1D, ArrayView<byte>, int, ulong, ArrayView2D<ulong, Stride2D.DenseX>, ulong, ulong, int, ArrayView< byte >, ArrayView<ulong>> (FindAllPoly);

                    // Create a host buffer to store the progress value.
                    // Can be modified by GPU and CPU   <<<<<<<<<<<<<<<<<<<<<<<
                    using var abort = new CudaProgress<byte>(accelerator);
                    using var progress = new CudaProgress<ulong>(accelerator);

                    using var buffer = accelerator.Allocate2DDenseX<ulong>(new Index2D(maxThreads, maxResults));
                    using var gdata = accelerator.Allocate1D<byte>(data.Length);
                    gdata.CopyFromCPU(data);

                    // Run the kernel, and apply workaround for issue on Windows with WDDM.
                    // https://stackoverflow.com/questions/20345702/how-can-i-check-the-progress-of-matrix-multiplication/20381924#comment55308772_20381924
                    // https://stackoverflow.com/questions/33455396/cuda-mapped-memory-device-host-writes-are-not-visible-on-host
                    using var marker = accelerator.AddProfilingMarker();

                    // Launch buffer.Length many threads and pass a view to buffer
                    // Note that the kernel launch does not involve any boxing
                    kernel(maxThreads, gdata.View, width, crc, buffer.View, maxSegment, maxValue, maxResults, abort.View, progress.View);
                      
                    marker.Synchronize();

                    // Wait for the kernel to finish...
                    var thread = new Thread(o => accelerator.Synchronize());
                    thread.Start();
                    
                    Abort = false;
                    var e = new GpuEventArgs(accelerator.Name, 0);

                    OnProcessProgress(new GpuEventArgs("{Started}", 0));

                    while (!Abort && thread.IsAlive)
                    {
                        System.Windows.Forms.Application.DoEvents(); Thread.Sleep(100);
                        e.Progress = progress.Value;
                        OnProcessProgress(e);
                    }

                    if (Abort)
                    {
                        abort.Value = 1;
                        while (thread.IsAlive)
                        {
                            System.Windows.Forms.Application.DoEvents(); Thread.Sleep(100);
                        }
                    }
                    
                    result = buffer.GetAsArray2D();

                    buffer.Dispose();
                    e.Progress = progress.Value;
                    OnProcessCompleted(e);
                }
                    
        }

        if (result != null)
            for (int i = 0; i < result.GetLength(0); i++)
                for (int j = 0; j < (int)result[i, 0]; j++)
                    finalResult.Add(result[i, j + 1]);

        return finalResult.ToArray();
    }
    public ulong[] OpenCLFindAllPolyMonitored(string accelName, byte[] data, int width, ulong crc, int maxThreads, int maxResults)
    {
        ulong maxValue = width == 64 ? ulong.MaxValue : ((ulong)1 << width);
        if ((ulong)maxThreads > maxValue) maxThreads = (int)maxValue;
        ulong maxSegment = maxValue / (ulong)maxThreads;
        if ((ulong)maxSegment * (ulong)maxThreads != maxValue) maxSegment++;

        ulong[,] result = new ulong[maxThreads, maxResults];
        List<ulong> finalResult = new();

        using var context = Context.Create(builder => builder.OpenCL().Profiling());
        {
            // For each available accelerator...
            foreach (var device in context.GetCLDevices())
                if (device.CreateAccelerator(context).Name.Equals(accelName))
                {
                    // Create default accelerator for the given accelerator id
                    using var accelerator = device.CreateCLAccelerator(context);

                    var kernel = accelerator.LoadAutoGroupedStreamKernel<Index1D, ArrayView<byte>, int, ulong, ArrayView2D<ulong, Stride2D.DenseX>, ulong, ulong, int, ArrayView<byte>, ArrayView<ulong>>(FindAllPoly);

                    
                    // Create a host buffer to store the progress value.
                    // Can be modified by GPU and CPU   <<<<<<<<<<<<<<<<<<<<<<<
                    using var abort = new OpenCLProgress<byte>(accelerator);
                    using var progress = new OpenCLProgress<ulong>(accelerator);

                    using var buffer = accelerator.Allocate2DDenseX<ulong>(new Index2D(maxThreads, maxResults));
                    using var gdata = accelerator.Allocate1D<byte>(data.Length);
                    gdata.CopyFromCPU(data);

                    // Run the kernel, and apply workaround for issue on Windows with WDDM.
                    // https://stackoverflow.com/questions/20345702/how-can-i-check-the-progress-of-matrix-multiplication/20381924#comment55308772_20381924
                    // https://stackoverflow.com/questions/33455396/cuda-mapped-memory-device-host-writes-are-not-visible-on-host
                    using var marker = accelerator.AddProfilingMarker();

                    // Launch buffer.Length many threads and pass a view to buffer
                    // Note that the kernel launch does not involve any boxing
                    try
                    {
                        kernel(maxThreads, gdata.View, width, crc, buffer.View, maxSegment, maxValue, maxResults, abort.View, progress.View);
                    } catch { progress.Value = ulong.MaxValue; }

                    marker.Synchronize();

                    // Wait for the kernel to finish...
                    var thread = new Thread(o => accelerator.Synchronize());
                    thread.Start();

                    Abort = false;
                    var e = new GpuEventArgs(accelerator.Name, 0);

                    OnProcessProgress(new GpuEventArgs("{Started}", 0));

                    while (!Abort && thread.IsAlive)
                    {
                        System.Windows.Forms.Application.DoEvents(); Thread.Sleep(100);
                        e.Progress = progress.Value;
                        OnProcessProgress(e);
                    }

                    if (Abort)
                    {
                        abort.Value = 1;
                        while (thread.IsAlive)
                        {
                            System.Windows.Forms.Application.DoEvents(); Thread.Sleep(100);
                        }
                    }

                    result = buffer.GetAsArray2D();

                    buffer.Dispose();
                    e.Progress = progress.Value;
                    OnProcessCompleted(e);
                }

        }

        if (result != null)
            for (int i = 0; i < result.GetLength(0); i++)
                for (int j = 0; j < (int)result[i, 0]; j++)
                    finalResult.Add(result[i, j + 1]);

        return finalResult.ToArray();
    }

}