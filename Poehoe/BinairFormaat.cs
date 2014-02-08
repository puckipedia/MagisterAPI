using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poehoe
{
    public class BinairFormaat
    {
        public string Name
        {
            get;
            private set;
        }

        private Stream _stream;

        public List<Field> Fields
        {
            get;
            private set;
        }

        public List<Row> Rows
        {
            get;
            private set;
        }

        public BinairFormaat(byte[] Stream)
        {
            _stream = new MemoryStream(Stream);

            Read();
        }

        public BinairFormaat(Stream Stream)
        {
            _stream = Stream;
            _stream.Seek(0, SeekOrigin.Begin);

            Read();
        }

        private void Read()
        {
            Name = String();
            
            ParseFields();
            
            ParseRows();
        }

        private void ParseRows()
        {

            int RowCount = Integer();
            Rows = new List<Row>(RowCount);
            for (int i = 0; i < RowCount; i++)
            {
                Row row = new Row();
                int count = Integer();
                row.Objects = new List<object>(count);
                for (int j = 0; j < count; j++)
                {
                    object obj = Object();
                    row.Objects.Add(obj);
                }
                Rows.Add(row);
            }
        }

        private void ParseFields()
        {
            int FieldCount = Integer();

            Fields = new List<Field>(FieldCount);
            for (int i = 0; i < FieldCount; i++)
            {
                Field field = new Field(String(),
                    Byte(),
                    Integer(),
                    String(),
                    Integer(),
                    Bool(),
                    Bool(),
                    String());
                Fields.Add(field);
            }
        }

        public byte[] Bytes(int count)
        {
            byte[] b = new byte[count];
            _stream.Read(b, 0, count);

            return b;
        }

        public object Object()
        {
            ushort type = UShort();
            switch (type)
            {
                case 0:
                case 1:
                case 10:
                    return null;
                case 2:
                    return Short();
                case 3:
                    return Integer();
                case 4:
                    return Float();
                case 5:
                case 6:
                    return Double();
                case 7:
                    return DateTime();
                case 8:
                    return String();
                case 9:
                case 12:
                case 13:
                case 15:
                    throw new Exception("I Have no idea!");
                case 11:
                    return Bool();
                case 14:
                    return Decimal();
                case 16:
                    return SByte();
                case 17:
                    return Byte();
                case 18:
                    return UShort();
                case 19:
                    return UInt();
                case 20:
                    return Long();
                case 21:
                    return ULong();
                case 22:
                    return Char();
            }
            return null;
        }
        public char Char()
        {
            return (char)Byte();
        }
        public ulong ULong()
        {
            return (ulong)Long();
        }
        public uint UInt()
        {
            return (uint)Integer();
        }
        public decimal Decimal()
        {
            byte[] read = Bytes(16);

            int[] ar = new int[4];
            Buffer.BlockCopy(read, 0, ar, 0, 16);
            
            return new Decimal(ar);
        }
        public sbyte SByte()
        {
            return (sbyte) Byte();
        }
        public byte Byte()
        {
            byte[] read = Bytes(1);
            return read[0];
        }
        public bool Bool()
        {
            return Byte() != 0;
        }
        public short Short()
        {
            return (short)UShort();
        }
        public float Float()
        {
            byte[] read = Bytes(4);
            return BitConverter.ToSingle(read, 0);
        }
        public double Double()
        {
            byte[] read = Bytes(8);
            return BitConverter.ToDouble(read, 0);
        }
        public ushort UShort()
        {
            byte[] read = Bytes(2);
            return (ushort)(read[0] | read[1] << 8);
        }
        public String String()
        {
            int Length = Integer();
            byte[] Read = Bytes(Length);
            var String = Encoding.UTF8.GetString(Read, 0, Length);
            return String;
        }
        public int Integer()
        {
            byte[] read = Bytes(4);
            return (int)(read[0] | (read[1] << 8) | (read[2] << 16) | (read[3] << 24));
        }
        public long Long()
        {
            byte[] read = Bytes(8);
            return (long)(read[0] | (read[1] << 8) | (read[2] << 16) | (read[3] << 24) | (read[4] << 32) | read[5] << 40 | read[6] << 48 | read[7] << 56);//DERP:  | read[8] << 64);
        }
        public DateTime DateTime()
        {
            double val = Double();
            return FromOADate(val);
        }

        public static DateTime FromOADate(double D)
        {
            long Days = (long)(D > 0 ? Math.Floor(D) : Math.Ceiling(D));
            double DayPart = Math.Abs(D - Days);

            DateTime Base = new DateTime(1899, 12, 30);
            return Base.AddDays(Days).AddDays(DayPart);
        }
    }
}
