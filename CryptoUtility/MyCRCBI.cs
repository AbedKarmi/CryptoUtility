using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CryptoUtility
{
    public class CRCEventArgsBI : EventArgs
    {
        public CRCEventArgsBI(BigInteger poly, int percent)
        {
            Poly = poly;
            Percent = percent;
        }

        public BigInteger Poly { get; set; }
        public int Percent { get; set; }
    }

    internal class MyCRCBI
    {

        public class CRCParametersBI
        {
            public string[] Names { get; private set; }
            public int Width { get; private set; }
            public BigInteger Polynom { get; private set; }
            public BigInteger Init { get; private set; }
            public bool ReflectIn { get; private set; }
            public bool ReflectOut { get; private set; }
            public BigInteger XOROut { get; private set; }
            public BigInteger CheckValue { get; private set; }
            public BigInteger Residue { get; private set; }
            public CRCParametersBI(int width, BigInteger poly, BigInteger init, bool refIn, bool refOut, BigInteger xorOut, BigInteger check, BigInteger residue, params string[] names)
            {
                for (int i = 0; i < names.Length; i++) names[i] = names[i].ToUpper().Trim();
                Names = names;
                Width = width;
                Polynom = poly;
                Init = init;
                ReflectIn = refIn;
                ReflectOut = refOut;
                XOROut = xorOut;
                CheckValue = check;
                Residue = residue;
            }

        }

        public event EventHandler<CRCEventArgsBI> OnProgressHandler;

        protected virtual void OnProgress(CRCEventArgsBI e)
        {
            OnProgressHandler?.Invoke(this, e);
        }

        private readonly BigInteger m_CRCMask;
        private readonly BigInteger m_CRCHighBitMask;
        private readonly CRCParametersBI m_Params;
        private BigInteger[] m_CRCTable;

        public CRCParametersBI CurParam { get { return m_Params; } }

        // this holds all neccessary parameters for the various CRC algorithms
        public enum CRCType
        {
            CRC_3_GSM = 0,
            CRC_3_ROHC = 1,
            CRC_4_INTERLAKEN = 2,
            CRC_4_ITU = 3, CRC_4_G_704 = 3,
            CRC_5_EPC_C1G2 = 4,
            CRC_5_ITU = 5, CRC_5_G_704 = 5,
            CRC_5_USB = 6,
            CRC_6_CDMA2000_A = 7,
            CRC_6_CDMA2000_B = 8,
            CRC_6_DARC = 9,
            CRC_6_GSM = 10,
            CRC_6_ITU = 11, CRC_6_G_704 = 11,
            CRC_7 = 12, CRC_7_MMC = 12,
            CRC_7_ROHC = 13,
            CRC_7_UMTS = 14,
            CRC_8 = 15, CRC_8_SMBUS = 15,
            CRC_8_8H2F = 16, CRC_8_AUTOSAR = 16,
            CRC_8_BLUETOOTH = 17,
            CRC_8_CDMA2000 = 18,
            CRC_8_DARC = 19,
            CRC_8_DVB_S2 = 20,
            CRC_8_EBU = 21, CRC_8_TECH_3250 = 21,
            CRC_8_GSM_B = 22,
            CRC_8_I_CODE = 23,
            CRC_8_ITU = 24, CRC_8_I_432_1 = 24,
            CRC_8_LTE = 25,
            CRC_8_MAXIM = 26, CRC_8_DOW_CRC = 26, CRC_8_MAXIM_DOW = 26,
            CRC_8_NRSC_5 = 27,
            CRC_8_OPENSAFETY = 28,
            CRC_8_ROHC = 29,
            CRC_8_SAE_J1850 = 30,
            CRC_8_SAE_J1850_ZERO = 31, CRC_8_GSM_A = 31,
            CRC_8_WCDMA = 32,
            CRC_10 = 33, CRC_10_ATM = 33,
            CRC_10_CDMA2000 = 34,
            CRC_10_GSM = 35,
            CRC_11 = 36, CRC_11_FLEXRAY = 36,
            CRC_11_UMTS = 37,
            CRC_12_3GPP = 38, CRC_12_UMTS = 38,
            CRC_12_CDMA2000 = 39,
            CRC_12_DECT = 40, CRC_12_X_CRC_12 = 40,
            CRC_12_GSM = 41,
            CRC_13_BBC = 42,
            CRC_14_DARC = 43,
            CRC_14_GSM = 44,
            CRC_15 = 45, CRC_15_CAN = 45,
            CRC_15_MPT1327 = 46,
            CRC_16 = 47, CRC_16_ARC = 47, CRC_16_CRC_IBM = 47, CRC_16_LHA = 47,
            CRC_16_AUG_CCITT = 48, CRC_16_SPI_FUJITSU = 48,
            CRC_16_BUYPASS = 49, CRC_16_VERIFONE = 49, CRC_16_UMTS = 49,
            CRC_16_CCITT_FALSE = 50, CRC_16_IBM_3740 = 50,
            CRC_16_CDMA2000 = 51,
            CRC_16_CMS = 52,
            CRC_16_CRC_A = 53, CRC_16_ISO_IEC_14443_3_A = 53,
            CRC_16_DDS_110 = 54,
            CRC_16_DECT_R = 55, CRC_16_R_CRC_16 = 55,
            CRC_16_DECT_X = 56, CRC_16_X_CRC_16 = 56,
            CRC_16_DNP = 57,
            CRC_16_EN_13757 = 58,
            CRC_16_GENIBUS = 59, CRC_16_EPC = 59, CRC_16_I_CODE = 59, CRC_16_DARC = 59,
            CRC_16_GSM = 60,
            CRC_16_KERMIT = 61, CRC_16_CCITT = 61, CRC_16_CCITT_TRUE = 61, CRC_16_CRC_CCITT = 61,
            CRC_16_LJ1200 = 62,
            CRC_16_MAXIM = 63, CRC_16_MAXIM_DOW = 63,
            CRC_16_MCRF4XX = 64,
            CRC_16_MODBUS = 65,
            CRC_16_NRSC_5 = 66,
            CRC_16_OPENSAFETY_A = 67,
            CRC_16_OPENSAFETY_B = 68,
            CRC_16_PROFIBUS = 69,
            CRC_16_RIELLO = 70,
            CRC_16_T10_DIF = 71,
            CRC_16_TELEDISK = 72,
            CRC_16_TMS37157 = 73,
            CRC_16_USB = 74,
            CRC_16_X_25 = 75, CRC_16_IBM_SDLC = 75, CRC_16_ISO_HDLC = 75, CRC_16_CRC_B = 75,
            CRC_16_XMODEM = 76, CRC_16_ZMODEM = 76, CRC_16_ACORN = 76, CRC_16_CCIT_ZERO = 76,
            CRC_17_CAN_FD = 77,
            CRC_21_CAN_FD = 78,
            CRC_24 = 79, CRC_24_OPENPGP = 79,
            CRC_24_BLE = 80,
            CRC_24_FLEXRAY_A = 81,
            CRC_24_FLEXRAY_B = 82,
            CRC_24_INTERLAKEN = 83,
            CRC_24_LTE_A = 84,
            CRC_24_LTE_B = 85,
            CRC_24_OS_9 = 86,
            CRC_30_CDMA = 87,
            CRC_31_PHILLIPS = 88,
            CRC_32 = 89, CRC_32_ADCCP = 89, CRC_32_PKZIP = 89, CRC_32_ISO_HDLC = 89,
            CRC_32_AUTOSAR = 90,
            CRC_32_BZIP2 = 91, CRC_32_AAL5 = 91, CRC_32_DECT_B = 91, CRC_32_B_CRC_32 = 91,
            CRC_32_JAMCRC = 92,
            CRC_32_MPEG_2 = 93,
            CRC_32_POSIX = 94, CRC_32_CKSUM = 94,
            CRC_32_XFER = 95,
            CRC_32C = 96, CRC_32_ISCSI = 96, CRC_32_CASTAGNOLI = 96,
            CRC_32D = 97, CRC_32_BASE91_D = 97,
            CRC_32Q = 98, CRC_32_AIXM = 98,
            CRC_40_GSM2 = 99,
            CRC_64_ECMA_182 = 100,
            CRC_64_GO_ISO = 101,
            CRC_64_WE = 102,
            CRC_64_XZ = 103,
            CRC_82_DARC = 104
        }

        // source: http://reveng.sourceforge.net/crc-catalogue
        private static readonly CRCParametersBI[] s_CRCParams = new CRCParametersBI[]  {
             new CRCParametersBI(3,  0x3,  0x0,  false,  false,  0x7,  0x4,  0x2,  "CRC-3/GSM"),
             new CRCParametersBI(3,  0x3,  0x7,  true,  true,  0x0,  0x6,  0x0,  "CRC-3/ROHC"),
             new CRCParametersBI(4,  0x3,  0xf,  false,  false,  0xf,  0xb,  0x2,  "CRC-4/INTERLAKEN"),
             new CRCParametersBI(4,  0x3,  0x0,  true,  true,  0x0,  0x7,  0x0,  "CRC-4/ITU",    "CRC-4/G-704"),
             new CRCParametersBI(5,  0x09,  0x09,  false,  false,  0x00,  0x00,  0x00,  "CRC-5/EPC-C1G2"),
             new CRCParametersBI(5,  0x15,  0x00,  true,  true,  0x00,  0x07,  0x00,  "CRC-5/ITU",    "CRC-5/G-704"),
             new CRCParametersBI(5,  0x05,  0x1f,  true,  true,  0x1f,  0x19,  0x06,  "CRC-5/USB"),
             new CRCParametersBI(6,  0x27,  0x3f,  false,  false,  0x00,  0x0d,  0x00,  "CRC-6/CDMA2000-A"),
             new CRCParametersBI(6,  0x07,  0x3f,  false,  false,  0x00,  0x3b,  0x00,  "CRC-6/CDMA2000-B"),
             new CRCParametersBI(6,  0x19,  0x00,  true,  true,  0x00,  0x26,  0x00,  "CRC-6/DARC"),
             new CRCParametersBI(6,  0x2f,  0x00,  false,  false,  0x3f,  0x13,  0x3a,  "CRC-6/GSM"),
             new CRCParametersBI(6,  0x03,  0x00,  true,  true,  0x00,  0x06,  0x00,  "CRC-6/ITU",    "CRC-6/G-704"),
             new CRCParametersBI(7,  0x09,  0x00,  false,  false,  0x00,  0x75,  0x00,  "CRC-7",    "CRC-7/MMC"),
             new CRCParametersBI(7,  0x4f,  0x7f,  true,  true,  0x00,  0x53,  0x00,  "CRC-7/ROHC"),
             new CRCParametersBI(7,  0x45,  0x00,  false,  false,  0x00,  0x61,  0x00,  "CRC-7/UMTS"),
             new CRCParametersBI(8,  0x07,  0x00,  false,  false,  0x00,  0xf4,  0x00,  "CRC-8",    "CRC-8/SMBUS"),
             new CRCParametersBI(8,  0x2F,  0xFF,  false,  false,  0xFF,  0xDF,  0x42,  "CRC-8/8H2F",    "CRC-8/AUTOSAR"),
             new CRCParametersBI(8,  0xa7,  0x00,  true,  true,  0x00,  0x26,  0x00,  "CRC-8/BLUETOOTH"),
             new CRCParametersBI(8,  0x9B,  0xFF,  false,  false,  0x00,  0xDA,  0x00,  "CRC-8/CDMA2000"),
             new CRCParametersBI(8,  0x39,  0x00,  true,  true,  0x00,  0x15,  0x00,  "CRC-8/DARC"),
             new CRCParametersBI(8,  0xD5,  0x00,  false,  false,  0x00,  0xBC,  0x00,  "CRC-8/DVB-S2"),
             new CRCParametersBI(8,  0x1d,  0xff,  true,  true,  0x00,  0x97,  0x00,  "CRC-8/EBU",    "CRC-8/TECH-3250"),
             new CRCParametersBI(8,  0x49,  0x00,  false,  false,  0xff,  0x94,  0x53,  "CRC-8/GSM-B"),
             new CRCParametersBI(8,  0x1d,  0xfd,  false,  false,  0x00,  0x7e,  0x00,  "CRC-8/I-CODE"),
             new CRCParametersBI(8,  0x07,  0x00,  false,  false,  0x55,  0xa1,  0xac,  "CRC-8/ITU",    "CRC-8/I-432-1"),
             new CRCParametersBI(8,  0x9b,  0x00,  false,  false,  0x00,  0xea,  0x00,  "CRC-8/LTE"),
             new CRCParametersBI(8,  0x31,  0x00,  true,  true,  0x00,  0xa1,  0x00,  "CRC-8/MAXIM",    "CRC-8/DOW-CRC",     "CRC-8/MAXIM-DOW"),
             new CRCParametersBI(8,  0x31,  0xff,  false,  false,  0x00,  0xf7,  0x00,  "CRC-8/NRSC-5"),
             new CRCParametersBI(8,  0x2f,  0x00,  false,  false,  0x00,  0x3e,  0x00,  "CRC-8/OPENSAFETY"),
             new CRCParametersBI(8,  0x07,  0xff,  true,  true,  0x00,  0xd0,  0x00,  "CRC-8/ROHC"),
             new CRCParametersBI(8,  0x1D,  0xFF,  false,  false,  0xFF,  0x4B,  0xc4,  "CRC-8/SAE-J1850"),
             new CRCParametersBI(8,  0x1D,  0x00,  false,  false,  0x00,  0x37,  0x00,  "CRC-8/SAE-J1850-ZERO",    "CRC-8/GSM-A"),
             new CRCParametersBI(8,  0x9b,  0x00,  true,  true,  0x00,  0x25,  0x00,  "CRC-8/WCDMA"),
             new CRCParametersBI(10,  0x233,  0x000,  false,  false,  0x000,  0x199,  0x000,  "CRC-10",    "CRC-10/ATM"),
             new CRCParametersBI(10,  0x3d9,  0x3ff,  false,  false,  0x000,  0x233,  0x000,  "CRC-10/CDMA2000"),
             new CRCParametersBI(10,  0x175,  0x000,  false,  false,  0x3ff,  0x12a,  0x0c6,  "CRC-10/GSM"),
             new CRCParametersBI(11,  0x385,  0x01a,  false,  false,  0x000,  0x5a3,  0x000,  "CRC-11",    "CRC-11/FLEXRAY"),
             new CRCParametersBI(11,  0x307,  0x000,  false,  false,  0x000,  0x061,  0x000,  "CRC-11/UMTS"),
             new CRCParametersBI(12,  0x80f,  0x000,  false,  true,  0x000,  0xdaf,  0x000,  "CRC-12/3GPP",    "CRC-12/UMTS"),
             new CRCParametersBI(12,  0xf13,  0xfff,  false,  false,  0x000,  0xd4d,  0x000,  "CRC-12/CDMA2000"),
             new CRCParametersBI(12,  0x80f,  0x000,  false,  false,  0x000,  0xf5b,  0x000,  "CRC-12/DECT",    "CRC-12/X-CRC-12"),
             new CRCParametersBI(12,  0xd31,  0x000,  false,  false,  0xfff,  0xb34,  0x178,  "CRC-12/GSM"),
             new CRCParametersBI(13,  0x1cf5,  0x0000,  false,  false,  0x0000,  0x04fa,  0x0000,  "CRC-13/BBC"),
             new CRCParametersBI(14,  0x0805,  0x0000,  true,  true,  0x0000,  0x082d,  0x0000,  "CRC-14/DARC"),
             new CRCParametersBI(14,  0x202d,  0x0000,  false,  false,  0x3fff,  0x30ae,  0x031e,  "CRC-14/GSM"),
             new CRCParametersBI(15,  0x4599,  0x0000,  false,  false,  0x0000,  0x059e,  0x0000,  "CRC-15",    "CRC-15/CAN"),
             new CRCParametersBI(15,  0x6815,  0x0000,  false,  false,  0x0001,  0x2566,  0x6815,  "CRC-15/MPT1327"),
             new CRCParametersBI(16,  0x8005,  0x0000,  true,  true,  0x0000,  0xbb3d,  0x0000,  "CRC-16",    "CRC-16/ARC",     "CRC-16/CRC-IBM",     "CRC-16/LHA"),
             new CRCParametersBI(16,  0x1021,  0x1d0f,  false,  false,  0x0000,  0xe5cc,  0x0000,  "CRC-16/AUG-CCITT",    "CRC-16/SPI-FUJITSU"),
             new CRCParametersBI(16,  0x8005,  0x0000,  false,  false,  0x0000,  0xfee8,  0x0000,  "CRC-16/BUYPASS",    "CRC-16/VERIFONE",     "CRC-16/UMTS"),
             new CRCParametersBI(16,  0x1021,  0xffff,  false,  false,  0x0000,  0x29b1,  0x0000,  "CRC-16/CCITT-FALSE",    "CRC-16/IBM-3740"),
             new CRCParametersBI(16,  0xC867,  0xFFFF,  false,  false,  0x0000,  0x4C06,  0x0000,  "CRC-16/CDMA2000"),
             new CRCParametersBI(16,  0x8005,  0xffff,  false,  false,  0x0000,  0xaee7,  0x0000,  "CRC-16/CMS"),
             new CRCParametersBI(16,  0x1021,  0xc6c6,  true,  true,  0x0000,  0xbf05,  0x0000,  "CRC-16/CRC-A",    "CRC-16/ISO-IEC-14443-3-A"),
             new CRCParametersBI(16,  0x8005,  0x800d,  false,  false,  0x0000,  0x9ecf,  0x0000,  "CRC-16/DDS-110"),
             new CRCParametersBI(16,  0x0589,  0x0000,  false,  false,  0x0001,  0x007e,  0x0589,  "CRC-16/DECT-R",    "CRC-16/R-CRC-16"),
             new CRCParametersBI(16,  0x0589,  0x0000,  false,  false,  0x0000,  0x007f,  0x0000,  "CRC-16/DECT-X",    "CRC-16/X-CRC-16"),
             new CRCParametersBI(16,  0x3d65,  0x0000,  true,  true,  0xffff,  0xea82,  0x66c5,  "CRC-16/DNP"),
             new CRCParametersBI(16,  0x3d65,  0x0000,  false,  false,  0xffff,  0xc2b7,  0xa366,  "CRC-16/EN-13757"),
             new CRCParametersBI(16,  0x1021,  0xffff,  false,  false,  0xffff,  0xd64e,  0x1d0f,  "CRC-16/GENIBUS",    "CRC-16/EPC",     "CRC-16/I-CODE",     "CRC-16/DARC"),
             new CRCParametersBI(16,  0x1021,  0x0000,  false,  false,  0xffff,  0xce3c,  0x1d0f,  "CRC-16/GSM"),
             new CRCParametersBI(16,  0x1021,  0x0000,  true,  true,  0x0000,  0x2189,  0x0000,  "CRC-16/KERMIT",    "CRC-16/CCITT",     "CRC-16/CCITT-TRUE",     "CRC-16/CRC-CCITT"),
             new CRCParametersBI(16,  0x6f63,  0x0000,  false,  false,  0x0000,  0xbdf4,  0x0000,  "CRC-16/LJ1200"),
             new CRCParametersBI(16,  0x8005,  0x0000,  true,  true,  0xffff,  0x44c2,  0xb001,  "CRC-16/MAXIM",    "CRC-16/MAXIM-DOW"),
             new CRCParametersBI(16,  0x1021,  0xffff,  true,  true,  0x0000,  0x6f91,  0x0000,  "CRC-16/MCRF4XX"),
             new CRCParametersBI(16,  0x8005,  0xffff,  true,  true,  0x0000,  0x4b37,  0x0000,  "CRC-16/MODBUS"),
             new CRCParametersBI(16,  0x080b,  0xffff,  true,  true,  0x0000,  0xa066,  0x0000,  "CRC-16/NRSC-5"),
             new CRCParametersBI(16,  0x5935,  0x0000,  false,  false,  0x0000,  0x5d38,  0x0000,  "CRC-16/OPENSAFETY-A"),
             new CRCParametersBI(16,  0x755b,  0x0000,  false,  false,  0x0000,  0x20fe,  0x0000,  "CRC-16/OPENSAFETY-B"),
             new CRCParametersBI(16,  0x1dcf,  0xffff,  false,  false,  0xffff,  0xa819,  0xe394,  "CRC-16/PROFIBUS"),
             new CRCParametersBI(16,  0x1021,  0xb2aa,  true,  true,  0x0000,  0x63d0,  0x0000,  "CRC-16/RIELLO"),
             new CRCParametersBI(16,  0x8bb7,  0x0000,  false,  false,  0x0000,  0xd0db,  0x0000,  "CRC-16/T10-DIF"),
             new CRCParametersBI(16,  0xA097,  0x0000,  false,  false,  0x0000,  0x0FB3,  0x0000,  "CRC-16/TELEDISK"),
             new CRCParametersBI(16,  0x1021,  0x89ec,  true,  true,  0x0000,  0x26b1,  0x0000,  "CRC-16/TMS37157"),
             new CRCParametersBI(16,  0x8005,  0xffff,  true,  true,  0xffff,  0xb4c8,  0xb001,  "CRC-16/USB"),
             new CRCParametersBI(16,  0x1021,  0xffff,  true,  true,  0xffff,  0x906e,  0xf0b8,  "CRC-16/X-25",    "CRC-16/IBM-SDLC",     "CRC-16/ISO-HDLC",     "CRC-16/CRC-B"),
             new CRCParametersBI(16,  0x1021,  0x0000,  false,  false,  0x0000,  0x31c3,  0x0000,  "CRC-16/XMODEM",    "CRC-16/ZMODEM",     "CRC-16/ACORN",     "CRC-16/CCIT-ZERO"),
             new CRCParametersBI(17,  0x1685b,  0x00000,  false,  false,  0x00000,  0x04f03,  0x00000,  "CRC-17/CAN-FD"),
             new CRCParametersBI(21,  0x102899,  0x000000,  false,  false,  0x000000,  0x0ed841,  0x000000,  "CRC-21/CAN-FD"),
             new CRCParametersBI(24,  0x864cfb,  0xb704ce,  false,  false,  0x000000,  0x21cf02,  0x000000,  "CRC-24",    "CRC-24/OPENPGP"),
             new CRCParametersBI(24,  0x00065b,  0x555555,  true,  true,  0x000000,  0xc25a56,  0x000000,  "CRC-24/BLE"),
             new CRCParametersBI(24,  0x5d6dcb,  0xfedcba,  false,  false,  0x000000,  0x7979bd,  0x000000,  "CRC-24/FLEXRAY-A"),
             new CRCParametersBI(24,  0x5d6dcb,  0xabcdef,  false,  false,  0x000000,  0x1f23b8,  0x000000,  "CRC-24/FLEXRAY-B"),
             new CRCParametersBI(24,  0x328b63,  0xffffff,  false,  false,  0xffffff,  0xb4f3e6,  0x144e63,  "CRC-24/INTERLAKEN"),
             new CRCParametersBI(24,  0x864cfb,  0x000000,  false,  false,  0x000000,  0xcde703,  0x000000,  "CRC-24/LTE-A"),
             new CRCParametersBI(24,  0x800063,  0x000000,  false,  false,  0x000000,  0x23ef52,  0x000000,  "CRC-24/LTE-B"),
             new CRCParametersBI(24,  0x800063,  0xffffff,  false,  false,  0xffffff,  0x200fa5,  0x800fe3,  "CRC-24/OS-9"),
             new CRCParametersBI(30,  0x2030b9c7,  0x3fffffff,  false,  false,  0x3fffffff,  0x04c34abf,  0x34efa55a,  "CRC-30/CDMA"),
             new CRCParametersBI(31,  0x04c11db7,  0x7fffffff,  false,  false,  0x7fffffff,  0x0ce9e46c,  0x4eaf26f1,  "CRC-31/PHILLIPS"),
             new CRCParametersBI(32,  0x04c11db7,  0xffffffff,  true,  true,  0xffffffff,  0xcbf43926,  0xdebb20e3,  "CRC-32",    "CRC-32/ADCCP",     "CRC-32/PKZIP",     "CRC-32/ISO-HDLC"),
             new CRCParametersBI(32,  0xf4acfb13,  0xffffffff,  true,  true,  0xffffffff,  0x1697d06a,  0x904cddbf,  "CRC-32/AUTOSAR"),
             new CRCParametersBI(32,  0x04c11db7,  0xffffffff,  false,  false,  0xffffffff,  0xfc891918,  0xc704dd7b,  "CRC-32/BZIP2",    "CRC-32/AAL5",     "CRC-32/DECT-B",     "CRC-32/B-CRC-32"),
             new CRCParametersBI(32,  0x04c11db7,  0xffffffff,  true,  true,  0x00000000,  0x340bc6d9,  0x00000000,  "CRC-32/JAMCRC"),
             new CRCParametersBI(32,  0x04c11db7,  0xffffffff,  false,  false,  0x00000000,  0x0376e6e7,  0x00000000,  "CRC-32/MPEG-2"),
             new CRCParametersBI(32,  0x04C11DB7,  0x00000000,  false,  false,  0xFFFFFFFF,  0x765E7680,  0xc704dd7b,  "CRC-32/POSIX",    "CRC-32/CKSUM"),
             new CRCParametersBI(32,  0x000000af,  0x00000000,  false,  false,  0x00000000,  0xbd0be338,  0x00000000,  "CRC-32/XFER"),
             new CRCParametersBI(32,  0x1edc6f41,  0xffffffff,  true,  true,  0xffffffff,  0xe3069283,  0xb798b438,  "CRC-32C",    "CRC-32/ISCSI",     "CRC-32/CASTAGNOLI"),
             new CRCParametersBI(32,  0xa833982b,  0xffffffff,  true,  true,  0xffffffff,  0x87315576,  0x45270551,  "CRC-32D",    "CRC-32/BASE91-D"),
             new CRCParametersBI(32,  0x814141ab,  0x00000000,  false,  false,  0x00000000,  0x3010bf7f,  0x00000000,  "CRC-32Q",    "CRC-32/AIXM"),
             new CRCParametersBI(40,  0x0004820009,  0x0000000000,  false,  false,  0xffffffffff,  0xd4164fc646,  0xc4ff8071ff,  "CRC-40/GSM2"),
             new CRCParametersBI(64,  0x42f0e1eba9ea3693,  0x0000000000000000,  false,  false,  0x0000000000000000,  0x6C40DF5F0B497347,  0x0000000000000000,  "CRC-64/ECMA-182"),
             new CRCParametersBI(64,  0x000000000000001B,  0xFFFFFFFFFFFFFFFF,  true,  true,  0xFFFFFFFFFFFFFFFF,  0xB90956C775A41001,  0x5300000000000000,  "CRC-64/GO-ISO"),
             new CRCParametersBI(64,  0x42f0e1eba9ea3693,  0xFFFFFFFFFFFFFFFF,  false,  false,  0xFFFFFFFFFFFFFFFF,  0x62EC59E3F1A4F00A,  0xfcacbebd5931a992,  "CRC-64/WE"),
             new CRCParametersBI(64,  0x42f0e1eba9ea3693,  0xFFFFFFFFFFFFFFFF,  true,  true,  0xFFFFFFFFFFFFFFFF,  0x995DC9BBDF1939FA,  0x49958c9abd7d353f,  "CRC-64/XZ"),

             new CRCParametersBI(64,  0x020F0D010C0C05,  0,  false,  false,  0,  0,  0,  "CRC-64/QURAN-B-Abjadi"),
             new CRCParametersBI(64,  0x6E334527444447,  0,  false,  false,  0,  0,  0,  "CRC-64/QURAN-B-Ancient-B"),
             new CRCParametersBI(64,  0x020C180117171A,  0,  false,  false,  0,  0,  0,  "CRC-64/QURAN-B-Hijaie"),
             new CRCParametersBI(64,  0xA8B385A7848487,  0,  false,  false,  0,  0,  0,  "CRC-64/QURAN-B-UTF8Ancient"),
             new CRCParametersBI(64,  0x28334527444447,  0,  false,  false,  0,  0,  0,  "CRC-64/QURAN-B-UniAncient"),
             new CRCParametersBI(32,  0x25022022,  0x00,  false,  false,  0x00,  0x15,  0x00,  "CRC-8/QURAN"),

             new CRCParametersBI(82, BigInteger.Parse("0308c0111011401440411",NumberStyles.AllowHexSpecifier),  0x000000000000000000000,  true,  true,  0x000000000000000000000, BigInteger.Parse("09ea83f625023801fd612",NumberStyles.AllowHexSpecifier),  0x000000000000000000000,  "CRC-82/DARC")

        };
        /*

                public static void DoCRCTests()
                {
                    byte[] checkdata = Encoding.ASCII.GetBytes("123456789");

                    foreach (CRCParametersBI p in s_CRCParams)
                    {
                        MyCRCBI foo = new MyCRCBI(p);

                        if (p.Width > 7)
                        {
                            // do some additional sanity checks with random data, to check if direct and table-driven algorithms match
                            Random rnd = new Random();
                            for (int i = 0; i < 1000; i++)
                            {
                                int len = rnd.Next(256);
                                byte[] buf = new byte[len];
                                for (int j = 0; j < len; j++)
                                {
                                    buf[j] = (byte)(rnd.Next(256) & 0xff);
                                }
                                BigInteger crc1 = foo.CalculateCRCbyTable(buf, len);
                                BigInteger crc2 = foo.CalculateCRCdirect(buf, len);
                                if (crc1 != crc2)
                                {
                                    Console.WriteLine("CRC '{0}': Table-driven and direct algorithm mismatch: table=0x{0:x8}, direct=0x{0:x8}", crc1, crc2);
                                    break;
                                }
                            }
                        }

                        BigInteger crc = foo.CalculateCRC("123456789");
                        if (crc != p.CheckValue)
                            Console.WriteLine("CRC '{0}': failed sanity check, expected {1:x8}, got {2:x8}", p.Names[0], p.CheckValue, crc);
                        else
                            Console.WriteLine("CRC '{0}': passed", p.Names[0]);
                    }
                }
          */

        // create a well-known CRC Algorithm
        public static MyCRCBI Create(string name)
        {
            foreach (CRCParametersBI param in s_CRCParams)
            {
                if (param.Names.Contains(name.ToUpper()))
                    return new MyCRCBI(param);
            }

            return null;
        }
        public static MyCRCBI Create(CRCParametersBI param)
        {

            return new MyCRCBI(param);

        }
        public static MyCRCBI Create(CRCType name)
        {
            return new MyCRCBI(s_CRCParams[(int)name]);
        }
        public static MyCRCBI Create(BigInteger poly, int width)
        {

            return new MyCRCBI(new CRCParametersBI(width, poly, 0, false, false, 0, 0, 0, "CUSTOM"));

        }

        public static CRCParametersBI GetParams(string name)
        {
            foreach (CRCParametersBI param in s_CRCParams)
            {
                if (param.Names.Contains(name.ToUpper()))
                    return param;
            }

            return null;
        }
        // enumerate all CRC methods
        public static IEnumerable<string> AllCRCMethods
        {
            get
            {
                foreach (CRCParametersBI p in s_CRCParams)
                {
                    foreach (string s in p.Names)
                        yield return s;
                }
            }
        }

        // enumerate all CRC methods
        public static IEnumerable<string> UniqueAllCRCMethods
        {
            get
            {
                foreach (CRCParametersBI p in s_CRCParams)
                {
                    yield return p.Names[0];
                }
            }
        }



        // Construct a new CRC algorithm object
        public MyCRCBI(CRCParametersBI param)
        {
            if (param == null) param = s_CRCParams[0];

            m_Params = param;

            // initialize some bitmasks
            m_CRCMask = ((((BigInteger)1 << (m_Params.Width - 1)) - 1) << 1) | 1;
            m_CRCHighBitMask = (BigInteger)1 << (m_Params.Width - 1);

            if (m_Params.Width > 7)
            {
                GenerateTable();
            }
        }

        public static BigInteger Reflect(BigInteger value, int width)
        {
            // reflects the lower 'width' bits of 'value'

            BigInteger j = 1;
            BigInteger result = 0;

            for (BigInteger i = ((BigInteger)1 << (width - 1)); i != 0; i >>= 1)
            {
                if ((value & i) != 0)
                {
                    result |= j;
                }
                j <<= 1;
            }
            return result;
        }

        private void GenerateTable()
        {
            BigInteger bit;
            BigInteger crc;

            m_CRCTable = new BigInteger[256];

            for (int i = 0; i < 256; i++)
            {
                crc = (BigInteger)i;
                if (m_Params.ReflectIn)
                {
                    crc = Reflect(crc, 8);
                }
                crc <<= m_Params.Width - 8;

                for (int j = 0; j < 8; j++)
                {
                    bit = crc & m_CRCHighBitMask;
                    crc <<= 1;
                    if (bit != 0) crc ^= m_Params.Polynom;
                }

                if (m_Params.ReflectIn)
                {
                    crc = Reflect(crc, m_Params.Width);
                }
                crc &= m_CRCMask;
                m_CRCTable[i] = crc;
            }
        }

        // tables work only for 8, 16, 24, 32, 64 bit CRC

        private BigInteger CalculateCRCbyTable(byte[] data, int length)
        {
            BigInteger crc = m_Params.Init;

            if (m_Params.ReflectIn)
                crc = Reflect(crc, m_Params.Width);

            if (m_Params.ReflectIn)
            {
                for (int i = 0; i < length; i++)
                {
                    crc = (crc >> 8) ^ m_CRCTable[(int)(crc & 0xff) ^ data[i]];
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    crc = (crc << 8) ^ m_CRCTable[(int)((crc >> (m_Params.Width - 8)) & 0xff) ^ data[i]];
                }
            }

            if (m_Params.ReflectIn ^ m_Params.ReflectOut)
            {
                crc = Reflect(crc, m_Params.Width);
            }

            crc ^= m_Params.XOROut;
            crc &= m_CRCMask;

            return crc;
        }

        private BigInteger CalculateCRCdirect(byte[] data, int length)
        {
            return CalculateCRCdirect(data, m_Params, length);
        }
        public static BigInteger CalculateCRCdirect(byte[] data, CRCParametersBI Params, int length)
        {
            return CalculateCRCdirect(data, Params.Width, Params.Polynom, Params.Init, Params.ReflectIn, Params.ReflectOut, Params.XOROut, length);
        }
        public static BigInteger CalculateCRCdirect(byte[] data, int width, BigInteger poly, BigInteger init, bool refin, bool refout, BigInteger xorout, int length)
        {
            // fast bit by bit algorithm without augmented zero bytes.
            // does not use lookup table, suited for polynom orders between 1...32.
            BigInteger c, bit;
            BigInteger crc = init;

            BigInteger crcMask = ((((BigInteger)1 << (width - 1)) - 1) << 1) | 1;
            BigInteger crcHighBitMask = (BigInteger)1 << (width - 1);

            for (int i = 0; i < length; i++)
            {
                c = (BigInteger)data[i];
                if (refin)
                {
                    c = Reflect(c, 8);
                }

                for (BigInteger j = 0x80; j > 0; j >>= 1)
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

        public BigInteger FindPoly(byte[] data, int width, BigInteger crc, int percentNotify)
        {
            BigInteger maxValue =((BigInteger)1 << width);
            int curPercent = 0;
            for (BigInteger poly = 0; poly < ((BigInteger)1 << width); poly++)
            {
                if (CalculateCRCdirect(data, width, poly, 0, false, false, 0, data.Length) == crc) return poly;
                if (percentNotify > 0)
                {
                    int p = (int)BigInteger.Divide(poly*100, maxValue);
                    if (p % percentNotify == 0 && curPercent != p)
                    {
                        OnProgress(new CRCEventArgsBI(poly, p));
                        curPercent = p;
                    }

                }
            }
            return 0;
        }

        public BigInteger[] FindAllPoly(byte[] data, int width, BigInteger crc, int percentNotify)
        {
            List<BigInteger> polyList = new();
            BigInteger maxValue =  ((BigInteger)1 << width);
            int curPercent = 0;
            for (BigInteger poly = 0; poly < maxValue; poly++)
            {
                if (CalculateCRCdirect(data, width, poly, 0, false, false, 0, data.Length) == crc) polyList.Add(poly);
                if (percentNotify > 0)
                {
                    int p = (int)BigInteger.Divide(poly * 100, maxValue);
                    if (p % percentNotify == 0 && curPercent != p)
                    {
                        OnProgress(new CRCEventArgsBI(poly, p));
                        curPercent = p;
                    }

                }
            }
            return polyList.ToArray();
        }

        /*
         *      private BigInteger CalculateCRCdirect(byte[] data, int length)
                {
                    // fast bit by bit algorithm without augmented zero bytes.
                    // does not use lookup table, suited for polynom orders between 1...32.
                    BigInteger c, bit;
                    BigInteger crc = m_Params.Init;

                    for (int i = 0; i < length; i++)
                    {
                        c = (BigInteger)data[i];
                        if (m_Params.ReflectIn)
                        {
                            c = Reflect(c, 8);
                        }

                        for (BigInteger j = 0x80; j > 0; j >>= 1)
                        {
                            bit = crc & m_CRCHighBitMask;
                            crc <<= 1;
                            if ((c & j) > 0) bit ^= m_CRCHighBitMask;
                            if (bit > 0) crc ^= m_Params.Polynom;
                        }
                    }

                    if (m_Params.ReflectOut)
                    {
                        crc = Reflect(crc, m_Params.Width);
                    }
                    crc ^= m_Params.XOROut;
                    crc &= m_CRCMask;

                    return crc;
                }
        */
        public BigInteger CalculateCRC(byte[] data, int dataLength = 0)
        {
            // table driven CRC reportedly only works for 8, 16, 24, 32 bits
            // HOWEVER, it seems to work for everything > 7 bits, so use it
            // accordingly

            if (dataLength == 0) dataLength = data.Length;
            /*if (m_Params.Width % 8 == 0)*/
            if (m_Params.Width > 7)
                return CalculateCRCbyTable(data, dataLength);
            else
                return CalculateCRCdirect(data, dataLength);
        }

        public BigInteger CalculateCRC(string data)
        {
            return CalculateCRC(Encoding.ASCII.GetBytes(data), data.Length);
        }
    }

}
