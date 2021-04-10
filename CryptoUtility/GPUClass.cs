using ILGPU;
using ILGPU.Runtime;
using ILGPU.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


// ILGPU Latest Version is Bugy 
namespace CryptoUtility
{
    public class gpuEventArgs : EventArgs
    {
        public string accelerator { get; set; }
        public gpuEventArgs(string acc)
        {
            accelerator = acc;
        }
    }
    class GPUClass
    {


        public event EventHandler<gpuEventArgs> ProcessCompleted;
        protected virtual void OnProcessCompleted(gpuEventArgs e)
        {
            ProcessCompleted?.Invoke(this, e);
        }

        // The following Summary is important for ILGPU.Algorithms to be used
        /// <summary>
        /// A custom kernel using <see cref="XMath"/> functions.
        /// </summary>
        public static void MathKernel(Index1 index,                    // The global thread index (1D in this case)
                              double P,
                              double Q,
                              ArrayView<double> result,
                              char func)
        {
            double R = 1;

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
                case '^': R = XMath.Pow(P , Q);
                     break;
                case '%':
                    R = P % Q; ;
                    break;
                default:
                    R = 0;
                    break;
            }
            result[0] = R;
            //    byte[] r = R.ToByteArray();
            //    for (index = 0; index < r.Length; index++) result[index] = r[index];
        }

        private static BigInteger GetBig(ArrayView<byte> data)
        {
            byte[] inArr = new byte[data.Length];
            for (int i = 0; i < data.Length; i++) inArr[i] = data[data.Length - i - 1];
            byte[] final = new byte[data.Length + 1];  // Add an empty byte at the end, to simulate unsigned BigInteger (no negatives!)
            Array.Copy(inArr, final, inArr.Length);
            return new BigInteger(final);
        }

        /*
         * struct Primes { public BigInteger P; 
                        public BigInteger Q;
                      };
        */

        public string[] GetAccelerators()
        {
            int i = 0;
            string[] accelerators = { "" };

            using (var context = new Context())
            {
                // For each available accelerator...
                foreach (var acceleratorId in Accelerator.Accelerators)
                {
                    Array.Resize(ref accelerators, ++i);
                    string s = acceleratorId.ToString();
                    accelerators[i - 1] = s; // s.Substring(s.LastIndexOf(":") + 1);
                }
            }
            return accelerators;
        }

        public  string AcceleratorInfo(Accelerator accelerator)
        {
            return String.Format($"Name: {accelerator.Name}") + "\r\n" + "\r\n" +
            String.Format($"MemorySize: {accelerator.MemorySize}") + "\r\n" +
            String.Format($"MaxThreadsPerGroup: {accelerator.MaxNumThreadsPerGroup}") + "\r\n" +
            String.Format($"MaxSharedMemoryPerGroup: {accelerator.MaxSharedMemoryPerGroup}") + "\r\n" +
            String.Format($"MaxGridSize: {accelerator.MaxGridSize}") + "\r\n" +
            String.Format($"MaxConstantMemory: {accelerator.MaxConstantMemory}") + "\r\n" +
            String.Format($"WarpSize: {accelerator.WarpSize}") + "\r\n" +
            String.Format($"NumMultiprocessors: {accelerator.NumMultiprocessors}");
        }

        public  string AcceleratorInfo(int index)
        {
            Accelerator acc = GetAccelerator(index);
            string s = AcceleratorInfo(acc);
            acc.Dispose();
            return s;
        }

        public  string AcceleratorInfo(AcceleratorId id)
        {
            Accelerator acc = GetAccelerator(id);
            string s = AcceleratorInfo(acc);
            acc.Dispose();
            return s;
        }
        public  string AcceleratorInfo(string name)
        {
            Accelerator acc = GetAccelerator(name);
            string s = AcceleratorInfo(acc);
            acc.Dispose();
            return s;
        }
        public  Accelerator GetAccelerator(int index)
        {
            using (var context = new Context())
            using (var acc = Accelerator.Create(context, Accelerator.Accelerators[index]))
                return acc;
        }
        public  Accelerator GetAccelerator(AcceleratorId id)
        {
            using (var context = new Context())
            using (var acc = Accelerator.Create(context, id))
                return acc; ;
        }
        public  Accelerator GetAccelerator(string name)
        {
            using (var context = new Context())
            {
                foreach (var acceleratorId in Accelerator.Accelerators)
                {
                    if (acceleratorId.ToString().Equals(name))
                    {
                        using (var acc = Accelerator.Create(context, acceleratorId))
                            return acc;
                    }
                }
            }
            return null;
        }

        public BigInteger Calc(String accelName, BigInteger P, BigInteger Q, Char func)
        {
            BigInteger R = 0;

            using (var context = new Context())
            {
                // Enable algorithms library
                context.EnableAlgorithms();

                // For each available accelerator...
                foreach (var acceleratorId in Accelerator.Accelerators)
                {
                    if (acceleratorId.ToString().Equals(accelName))
                    {
                        // Create default accelerator for the given accelerator id
                        using (var accelerator = Accelerator.Create(context, acceleratorId))
                        {
                            var kernel = accelerator.LoadAutoGroupedStreamKernel<Index1, double, double, ArrayView<double>, char>(MathKernel);

                            double p = (double)P;
                            double q = (double)Q;

                            using (var buffer = accelerator.Allocate<double>(1))
                            {
                                // Launch buffer.Length many threads and pass a view to buffer
                                // Note that the kernel launch does not involve any boxing
                                kernel(buffer.Length, p, q, buffer, func);

                                // Wait for the kernel to finish...
                                accelerator.Synchronize();

                                var data = buffer.GetAsArray();
                                // Resolve and verify data
                                R = (BigInteger)data[0];
                                buffer.Dispose();
                                OnProcessCompleted(new gpuEventArgs(acceleratorId.ToString())) ;
                            }
                        }
                    }
                }
            }
            return R;
        }

    }
}
