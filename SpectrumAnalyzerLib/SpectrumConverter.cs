using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio;
using NAudio.Wave;
using ILNumerics;
using System.Threading;

namespace SpectrumAnalyzerLib
{
    public static class SpectrumConverter
    {
        /// <summary>
        /// FFT the waveform data of the sensor to obtain the spectrum
        /// </summary>
        /// <param name="buffer">Waveform data</param>
        /// <param name="offset">Start position of the waveform area you want to FFT</param>
        /// <param name="count">Length of waveform area you want to FFT</param>
        /// <param name="outSpectrums">Output: Amplitude spectrum (per channel). Has count / 2 elements per channel</param>
        /// <returns></returns>
        public static bool GetSpectrum(List<List<short>> buffer, int offset, int count, List<List<double>> outSpectrums)
        {
            if (buffer == null || outSpectrums == null)
                return false;

            if (buffer.Count <= 0 || outSpectrums.Count < buffer.Count)
                return false;

            int from = offset;
            int to = offset + count;

            if (to <= 0 || from < 0)
                return false;

            double[] _fftIn = new double[count];

            float ratio = 1f / short.MaxValue;

            for (int channel = 0; channel < buffer.Count; channel++)
            {
                 if (buffer[channel].Count <= to)
                    return false;

                outSpectrums[channel].Clear();

                // Apply humming function
                double cangle = 2 * Math.PI / (to - from - 1);
                var buf = buffer[channel];
                for (int i = from; i < to; i++)
                {
                    double hamming = 0.54 - 0.46 * Math.Cos(cangle * (i - from));
                    double val = buf[i] * ratio * hamming;
                    _fftIn[i - from] = val;
                }

                // FFT
                ILArray<double> fftIn = _fftIn;
                ILArray<complex> fft = ILMath.fft(fftIn);

                // spectrum
                foreach (var z in fft)
                {
                    outSpectrums[channel].Add(z.Abs());
                    if (outSpectrums[channel].Count >= count / 2)
                        break;
                }
            }

            return true;
        }

    }
}
