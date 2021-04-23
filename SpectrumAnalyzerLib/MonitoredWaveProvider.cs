using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpectrumAnalyzerLib
{
    public class MonitoredWaveProvider : IWaveProvider, IDisposable
    {
        private readonly IWaveProvider sourceWaveProvider;
        private readonly Action<byte[], int> OnReadData;
        public bool isWriterDisposed;
        public int Position { get; set; }
        public MonitoredWaveProvider(IWaveProvider sourceWaveProvider, Action<byte[], int> onReadData =null)
        {
            this.sourceWaveProvider = sourceWaveProvider;
            OnReadData+=onReadData;
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            var read = sourceWaveProvider.Read(buffer, offset, count);
            if (count > 0 && !isWriterDisposed)
            {
                Position += read;
                if (OnReadData != null)
                    OnReadData.Invoke(buffer, read);
            }
            if (count == 0)
            {
                Dispose(); // auto-dispose in case users forget
            }
            return read;
        }

        public WaveFormat WaveFormat { get { return sourceWaveProvider.WaveFormat; } }

        public void Dispose()
        {
            if (!isWriterDisposed)
            {
                isWriterDisposed = true;
            }
        }
    }
}
