using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio;
using NAudio.Wave;
using ILNumerics;
using System.Threading;
using NAudio.Wave.SampleProviders;
using NAudio.Gui;

namespace SpectrumAnalyzerLib
{
    public enum AudioType
    {
        Monaural,
        Stereo,
    }
    /*
        class SavingWaveProvider : IWaveProvider, IDisposable
        {
            private readonly IWaveProvider sourceWaveProvider;
            private readonly WaveFileWriter writer;
            public bool isWriterDisposed;

            public SavingWaveProvider(IWaveProvider sourceWaveProvider, string wavFilePath)
            {
                this.sourceWaveProvider = sourceWaveProvider;
                writer = new WaveFileWriter(wavFilePath, sourceWaveProvider.WaveFormat);
            }

            public int Read(byte[] buffer, int offset, int count)
            {
                var read = sourceWaveProvider.Read(buffer, offset, count);
                if (count > 0 && !isWriterDisposed)
                {
                    writer.Write(buffer, offset, read);
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
                    writer.Dispose();
                }
            }
        }
    */
    public class AudioSensor : IDisposable
    {
        const int maxBufferSize = 8000 * 2 * 600;
        private BufferedWaveProvider bufferedWaveProvider;
        private MonitoredWaveProvider monitoredgWaveProvider;
        private WaveOut player;
        private WaveFileWriter writer;
        private int channels = 0;
        public AudioSensorData Data { get { return data; } }
        public int Bits { get { return bits; } }
        public int Channels { get { return channels; } }
        public int SampleRate { get { return rate; } }

        AudioSensorData data;
        int rate, bits;
        WaveIn recorder = null;
        int curChannel = 0;
        bool lowBit = true;
        Action<byte[], int> action;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="rate">Sampling rate(Hz)</param>
        /// <param name="bits">bit rate(bit)</param>
        /// <param name="channels">Number of channels (1, for monaural, 2 for stereo)</param>
        /// <param name="onUpdate">Processing when data is updated </param>
        public AudioSensor(int rate = 8000, int bits = 16, AudioType audioType = AudioType.Monaural, Action<byte[], int> onUpdate = null)
        {
            this.rate = rate;
            this.bits = bits;
            this.channels = (audioType == AudioType.Monaural ? 1 : 2);
            this.data = new AudioSensorData(channels);
            this.data.bits = bits;
            this.action = onUpdate;
        }

        /// <summary>
        /// Stop recording and destroy the instance
        /// </summary>
        public void Dispose()
        {
            Stop();
        }

        private delegate ISampleProvider CreateSampleProvider(IWaveProvider waveProvider, int bits = 16);

        /// <summary>
        /// Start recording
        /// </summary>
        /// <param name="file">Output .wav FileName</param>
        /// <param name="play">Play while recording</param>
        public IWaveProvider Start(string file, bool record=true,bool play=false,Func<IWaveProvider,int,ISampleProvider> createSampleProvider=null)
        {
            Cleanup();
            
            recorder = new WaveIn();
            recorder.WaveFormat = new WaveFormat(rate, bits, data.Channels);
            if (record) recorder.DataAvailable += OnDataAvailable;
            recorder.RecordingStopped += OnRecordingStopped;
            // set up our signal chain
            
            if (play)
            {
                bufferedWaveProvider = new BufferedWaveProvider(recorder.WaveFormat);
                monitoredgWaveProvider = new MonitoredWaveProvider(bufferedWaveProvider);

                // set up playback
                player = new WaveOut();
                if (createSampleProvider != null)
                    player.Init(createSampleProvider(monitoredgWaveProvider, bits));
                else
                    player.Init(monitoredgWaveProvider);

                ResetPosition();
                // begin playback & record
                player.Play();
            }
            writer = new WaveFileWriter(file, recorder.WaveFormat);

            recorder.StartRecording();

            return play ? monitoredgWaveProvider :null;
        }

        public int PlayerPosition { get { return monitoredgWaveProvider.Position; } set { monitoredgWaveProvider.Position = value; } }

        /// <summary>
        /// Stop recording
        /// </summary>
        public void Stop()
        {
            Cleanup();
        }

        void Cleanup()
        {
            if (recorder != null)
            {
                recorder.StopRecording();
                recorder.Dispose();
                recorder = null;
            }
            if (writer != null )
            {
                writer.Dispose();
                writer = null;
            }
            if (player!=null)
            {
                player.Dispose();
                player = null;
            }
            if (bufferedWaveProvider!=null)
            {
                bufferedWaveProvider = null;
            }
            if (monitoredgWaveProvider!=null)
            {
                monitoredgWaveProvider = null;
            }
        }

        public void ResetPosition()
        {
            PlayerPosition = 0;
        }
        void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            AddBytes(e.Buffer, e.BytesRecorded);
        }

        void OnRecordingStopped(object sender, StoppedEventArgs e)
        {
            if (player!=null)
            {
                // stop playback
                player.Stop();
                monitoredgWaveProvider.Dispose();
            }
            
            // finalise the WAV file
            
            if (e.Exception != null)
                throw e.Exception;
        }
        public void AddBytes(byte[] bytes, int count)
        {
            if (bytes == null || bytes.Length < count)
                return;

            if (data.Buffer.Count <= curChannel)
                return;
            
            int segs =(bits==16?1:2);
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < segs; j++)
                {
                    byte n = bytes[i];
                    if (j == 1) n =0;
                    if (lowBit)
                    {
                        data.Buffer[curChannel].Add(n);
                        lowBit = false;
                    }
                    else
                    {
                        short highBits = (short)(n << 8);
                        if (data.Buffer[curChannel].Count < 1)
                            continue;
                        short lowBits = (short)data.Buffer[curChannel][data.Buffer[curChannel].Count - 1];
                        data.Buffer[curChannel].Add((short)(highBits | lowBits));
                        curChannel = (curChannel + 1) % data.Buffer.Count;
                        lowBit = true;
                    }
                }
            }

            int maxSize = maxBufferSize / 2 * 2;
            for (int i = 0; i < data.Channels; i++)
            {
                if (data.Buffer[i].Count > maxSize)
                    data.Buffer[i].RemoveRange(0, data.Buffer[i].Count - maxSize);
            }

            if (player != null)
                bufferedWaveProvider.AddSamples(bytes, 0, count);
            if (writer != null)
                writer.Write(bytes, 0, count);
            if (action != null)
                action(bytes, count);
        }
    }
}