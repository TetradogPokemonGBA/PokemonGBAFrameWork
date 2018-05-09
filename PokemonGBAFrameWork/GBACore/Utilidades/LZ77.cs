using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork
{
   public static class LZ77
    { 
    public static readonly Creditos Creditos;

    static LZ77()
    {
        Creditos = new Creditos();
        //no he hablado con el usuario pero diria que ha sido él...
        Creditos.Add("CodePlex", "lastlinkx", "coordinar proyecto y desarrollar NSE_Framework.Data 2.0-> comprimir y descomprimir");
    }
    // For picking what type of Compression Look-up we want
    public enum CompressionMode
    {
        Old, // Good
        New  // Perfect!
    }

    public static byte[] Descomprimir(byte[] datos, int offsetInicio = 0)
    {
        byte[] data;
        int dataLength;
        int offset;
        int i, iAux, pos;
        byte[] r;
        byte rPart1;
        int length;
        int start;
        bool[] auxWatch = null;

        if (CheckCompresionLZ77(datos, offsetInicio))
        {
            dataLength = Longitud(datos, offsetInicio);
            data = new byte[dataLength];

            offset = (int)offsetInicio + 4;
            i = 0;
            pos = 8;
            unsafe
            {
                byte* ptDatosDescomprimidos;
                byte* ptDatosComprimidos;
                fixed (byte* ptrDatosComprimidos = datos)
                {
                    ptDatosComprimidos = ptrDatosComprimidos;
                    ptDatosComprimidos += offset;
                    fixed (byte* ptrDatosDescomprimidos = data)
                    {
                        ptDatosDescomprimidos = ptrDatosDescomprimidos;
                        while (i < dataLength)
                        {
                            if (pos != 8)
                            {
                                fixed (bool* ptWatch = auxWatch)
                                {
                                    if (!*(ptWatch + pos))
                                    {

                                        *ptDatosDescomprimidos = *ptDatosComprimidos;
                                    }
                                    else
                                    {
                                        rPart1 = *ptDatosComprimidos;
                                        ptDatosComprimidos++;
                                        r = new byte[] { rPart1, *ptDatosComprimidos };
                                        length = r[0] >> 4;
                                        start = ((r[0] - ((r[0] >> 4) << 4)) << 8) + r[1];
                                        iAux = AmmendArray(data, i, i - start - 1, length + 3);
                                        ptDatosDescomprimidos += iAux - i;
                                        i = iAux;
                                    }
                                }
                                ptDatosComprimidos++;
                                ptDatosDescomprimidos++;
                                i++;
                                pos++;

                            }
                            else
                            {
                                auxWatch = (*ptDatosComprimidos).ToBits();
                                ptDatosComprimidos++;
                                pos = 0;
                            }
                        }


                    }
                }
            }

        }
        else
        {
            throw new Exception("This data is not Lz77 compressed!");
        }

        return data;
    }

    public static bool CheckCompresionLZ77(byte[] datos, int offsetInicio)
    {
        const byte BYTECOMPRESSIONLZ77 = 0x10;
        return datos[offsetInicio] == BYTECOMPRESSIONLZ77;
    }

    public static int Longitud(byte[] datos, int offsetInicio)
    {
        int longitud;
        unsafe
        {
            fixed (byte* ptrDatos = datos)
                longitud = Serializar.ToInt(new byte[] { ptrDatos[offsetInicio + 1], ptrDatos[offsetInicio + 2], ptrDatos[offsetInicio + 3], 0x0 });
        }
        return longitud;
    }
    //metodo usado para descomprimir
    static int AmmendArray(byte[] bytes, int index, int start, int length)
    {
        int a = 0; // Act
        int r = 0; // Rel
        byte backup = 0;

        unsafe
        {
            fixed (byte* ptrBytes = bytes)
            {
                if (index > 0)
                {
                    backup = ptrBytes[index - 1];
                }

                while (a != length)
                {
                    if (index + r >= 0 && start + r >= 0 && index + a < bytes.Length)
                    {
                        if (start + r >= index)
                        {
                            r = 0;
                            ptrBytes[index + a] = ptrBytes[start + r];
                        }
                        else
                        {
                            ptrBytes[index + a] = ptrBytes[start + r];
                            backup = ptrBytes[index + r];
                        }
                    }
                    a++;
                    r++;
                }
            }
        }
        index += length - 1;
        return index;
    }




    public static byte[] Comprimir(byte[] datos, CompressionMode Mode = CompressionMode.New)
    {
        const byte BYTECOMPRESSLZ77 = 0x10;
        const int BYTESHEADER = 3;
        byte[] header = BitConverter.GetBytes(datos.Length);
        List<byte> bytesComprimidos = new List<byte>();
        List<byte> preBytes = new List<byte>();
        byte watch = 0;
        byte shortPosition = 2;
        int actualPosition = 2;
        int match = -1;
        int start;
        int bestLength = 0;
        int length;
        bool compatible;
        byte[] b;
        unsafe
        {
            byte* ptrHeader;
            fixed (byte* ptHeader = header)
            fixed (byte* ptrDatos = datos)
            {

                ptrHeader = ptHeader;
                // Adds the Lz77 header to the bytes 0x10 3 bytes size reversed
                bytesComprimidos.Add(BYTECOMPRESSLZ77);
                for (int i = 0; i < BYTESHEADER; i++)
                {
                    bytesComprimidos.Add(*ptrHeader);
                    ptrHeader++;
                }


                // Lz77 Compression requires SOME starting data, so we provide the first 2 bytes
                preBytes.Add(*ptrDatos);
                preBytes.Add(ptrDatos[1]);

                // Compress everything
                while (actualPosition < datos.Length)
                {
                    //If we've compressed 8 of 8 bytes
                    if (shortPosition == 8)
                    {
                        // Add the Watch Mask
                        // Add the 8 steps in PreBytes
                        bytesComprimidos.Add(watch);
                        bytesComprimidos.AddRange(preBytes);

                        watch = 0;
                        preBytes.Clear();

                        // Back to 0 of 8 compressed bytes
                        shortPosition = 0;
                    }
                    else
                    {
                        // If we are approaching the end
                        if (actualPosition + 1 < datos.Length)
                        {
                            // Old NSE 1.x compression lookup
                            if (Mode == CompressionMode.Old)
                            {
                                match = SearchBytesOld(
                                    datos,
                                    actualPosition,
                                    Math.Min(4096, actualPosition));
                            }
                            else
                            {
                                // New NSE 2.x compression lookup
                                match = SearchBytes(
                                    datos,
                                    actualPosition,
                                    Math.Min(4096, actualPosition), out bestLength);
                            }
                        }
                        else
                        {
                            match = -1;
                        }

                        // If we have NOT found a match in the compression lookup
                        if (match == -1)
                        {
                            // Add the byte
                            preBytes.Add(ptrDatos[actualPosition]);
                            // Add a 0 to the mask
                            watch = BitConverter.GetBytes((int)watch << 1)[0];

                            actualPosition++;
                        }
                        else
                        {
                            // How many bytes match
                            length = -1;

                            start = match;
                            if (Mode == CompressionMode.Old || bestLength == -1)
                            {
                                // Old look-up technique
                                #region GetLength_Old
                                start = match;

                                compatible = true;

                                while (compatible == true && length < 18 && length + actualPosition < datos.Length - 1)
                                {
                                    length++;
                                    if (ptrDatos[actualPosition + length] != ptrDatos[actualPosition - start + length])
                                    {
                                        compatible = false;
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                // New lookup (Perfect Compression!)
                                length = bestLength;
                            }

                            // Add the rel-compression pointer (P) and length of bytes to copy (L)
                            // Format: L P P P
                            b = BitConverter.GetBytes(((length - 3) << 12) + (start - 1));
                            fixed (byte* ptrB = b)
                            {
                                preBytes.Add(ptrB[1]);
                                preBytes.Add(ptrB[0]);
                            }
                            // Move to the next position
                            actualPosition += length;

                            // Add a 1 to the bit Mask
                            watch = BitConverter.GetBytes(((int)watch << 1) + 1)[0];
                        }

                        // We've just compressed 1 more 8
                        shortPosition++;
                    }


                }

                // Finnish off the compression
                if (shortPosition != 0)
                {
                    //Tyeing up any left-over data compression
                    watch = BitConverter.GetBytes((int)watch << (8 - shortPosition))[0];

                    bytesComprimidos.Add(watch);
                    bytesComprimidos.AddRange(preBytes);
                }
            }
        }

        // Return the Compressed bytes as an array!
        return bytesComprimidos.ToArray();
    }

    static int SearchBytesOld(byte[] data, int index, int length)
    {
        int found = -1;
        int pos = 2;
        int poscionFinal;
        unsafe
        {
            fixed (byte* ptrData = data)
            {
                if (index + 2 < data.Length)
                {
                    while (pos < length + 1 && found == -1)
                    {
                        if (ptrData[index - pos] == ptrData[index] && ptrData[index - pos + 1] == ptrData[index + 1])
                        {

                            if (index > 2)
                            {
                                if (ptrData[index - pos + 2] == ptrData[index + 2])
                                {
                                    found = pos;
                                }
                                else
                                {
                                    pos++;
                                }
                            }
                            else
                            {
                                found = pos;
                            }


                        }
                        else
                        {
                            pos++;
                        }
                    }

                    poscionFinal = found;
                }
                else
                {
                    poscionFinal = -1;
                }
            }
        }
        return poscionFinal;

    }

    static int SearchBytes(byte[] data, int index, int length, out int match)
    {

        int pos = 2;
        int found = -1;
        int posFinal;
        int matchAux;
        bool compatible;
        match = 0;
        unsafe
        {
            if (index + 2 < data.Length)
            {
                fixed (byte* ptrData = data)
                    while (pos < length + 1 && match != 18)
                    {
                        if (ptrData[index - pos] == ptrData[index] && ptrData[index - pos + 1] == ptrData[index + 1])
                        {

                            if (index > 2)
                            {
                                if (ptrData[index - pos + 2] == ptrData[index + 2])
                                {
                                    matchAux = 2;
                                    compatible = true;
                                    while (compatible && matchAux < 18 && matchAux + index < data.Length - 1)
                                    {
                                        matchAux++;
                                        if (ptrData[index + matchAux] != ptrData[index - pos + matchAux])
                                        {
                                            compatible = false;
                                        }
                                    }
                                    if (matchAux > match)
                                    {
                                        match = matchAux;
                                        found = pos;
                                    }

                                }
                                pos++;
                            }
                            else
                            {
                                found = pos;
                                match = -1;
                                pos++;
                            }


                        }
                        else
                        {
                            pos++;
                        }
                    }

                posFinal = found;
            }
            else
            {
                posFinal = -1;
            }
        }
        return posFinal;

    }





}

}
