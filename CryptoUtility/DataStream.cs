using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoUtility
{
    /// <summary>
    /// Data is stream of bytes that represents words and lines, were words are seperated by (0's) and lines are seperated by (00's)
    /// </summary>
    internal class DataStream
    {
        public struct DataSegment { public int Position;public int Length; }
        public byte[] Data { get; set; }
        public int Position { get; set; }

        public DataStream(byte[] data)
        {
            Data = data;
            Position = 0;
        }

        public DataSegment GetWord()
        {
            DataSegment segment = new();
            int j = 0,p;
            while (Position < Data.Length && Data[Position] == 0) Position++;
            p = Position;
            while (Position < Data.Length)
            {
                if (Data[Position++] != 0) 
                    j++;
                else
                    break;
            }
            segment.Position = p;
            segment.Length = j;
            return segment;
        }

        public DataSegment GetLine()
        {
            DataSegment segment = new();
            int j = 0, p;
            while (Position < Data.Length && Data[Position] == 0) Position++;
            p = Position;
            segment = GetWord();
            while (segment.Length>0)
            {
                j += segment.Length+1;
                if (Position<Data.Length) 
                    if (Data[Position] != 0)
                        segment = GetWord();
                    else 
                        break;
                else 
                    break;
            }
            segment.Position = p;
            segment.Length = j;
            return segment;
        }

        public bool EndOfData()
        {
            return (Position == Data.Length);
        }

        public void Reset()
        {
            Position = 0;
        }
    }

}
