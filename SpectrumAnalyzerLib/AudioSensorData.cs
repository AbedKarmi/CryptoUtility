using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpectrumAnalyzerLib
{
    public class AudioSensorData
    {
        readonly List<List<short>> buffer = new();
        public List<List<short>> Buffer { get { return buffer; } }
        public int Channels { get { return (buffer == null ? 0 : buffer.Count); } }
        public int bits;
        public AudioSensorData(int channels)
        {
            for (int i = 0; i < channels; i++)
                buffer.Add(new List<short>());
        }
    }
}
