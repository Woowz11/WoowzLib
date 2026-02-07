using WLO;

namespace WL;

public enum FileFormat{
    Unknown, PNG, JPEG, BMP, WEBP, TIFF, GIF
}

[WLModule(0, 5)]
public class Parser{
    /// <summary>
    /// Парсит формат данных
    /// </summary>
    /// <param name="Data">Данные</param>
    /// <param name="Format">Указать свой формат</param>
    public static ParsedContainer Parse(byte[] Data, FileFormat? Format = null){
        try{
            if(Format == null){ Format = Detect.WhatFormat(Data); }

            if(Format == FileFormat.Unknown){ throw new Exception("Неизвестный формат данных!"); }

            switch(Format){
                case FileFormat.BMP: return ParseBMP(Data);
                default: throw new Exception("Парсер для этого формата ещё не реализован!");
            }
            
        }catch(Exception e){
            throw new Exception("Произошла ошибка при парсинге данных формата [" + Format + "]!", e);
        }
    }

    /// <summary>
    /// Парсит BMP (Вернёт в любом случае RGBA)
    /// </summary>
    public static ParsedContainer_BMP ParseBMP(byte[] Data){
        try{
            if(!Detect.IsBMP(Data)){ throw new Exception("Это не BMP!"); }

            ParsedContainer_BMP Result = new ParsedContainer_BMP();

            int PixelOffset = BitConverter.ToInt32(Data, 0x0A);
            
            // Заголовок
            Result.Width        = (uint  )BitConverter.ToInt32(Data, 0x12);
            Result.Height       = (uint  )BitConverter.ToInt32(Data, 0x16);
            ushort BitsPerPixel = (ushort)BitConverter.ToInt16(Data, 0x1C);

            if(BitsPerPixel != 1 && BitsPerPixel != 8 && BitsPerPixel != 24 && BitsPerPixel != 32){ throw new Exception("Поддерживаются только 1 или 8 или 24 или 32 битные BMP! Сейчас: " + BitsPerPixel); }

            const int OutChannels = 4;
            int Channels = BitsPerPixel / 8;
            
            Result.Pixels_RGBA = new byte[Result.Width * Result.Height * OutChannels];

            int RowSize = (int)((BitsPerPixel * Result.Width + 31) / 32) * 4;

            byte[]? Palette = null;
            if(BitsPerPixel == 1){
                Palette = new byte[2 * 4];
                Buffer.BlockCopy(Data, 14 + 40, Palette, 0, 2 * 4);
            }else if(BitsPerPixel == 8){
                Palette = new byte[256 * 4];
                Buffer.BlockCopy(Data, 14 + 40, Palette, 0, 256 * 4);
            }
            
            for(int Y = 0; Y < Result.Height; Y++){
                int RowStart = PixelOffset + Y * RowSize;
                for(int X = 0; X < Result.Width; X++){
                    int OutIndex = ((int)(Result.Height - 1 - Y) * (int)Result.Width + X) * OutChannels;

                    if(BitsPerPixel == 1){
                        int ByteIndex = RowStart + (X >> 3);
                        int BitIndex = 7 - (X & 7);

                        byte Packed = Data[ByteIndex];
                        int PaletteIndex = (Packed >> BitIndex) & 1;

                        int P = PaletteIndex * 4;
                        
                        Result.Pixels_RGBA[OutIndex + 0] = Palette![P + 2]; // R
                        Result.Pixels_RGBA[OutIndex + 1] = Palette![P + 1]; // G
                        Result.Pixels_RGBA[OutIndex + 2] = Palette![P + 0]; // B
                        Result.Pixels_RGBA[OutIndex + 3] = 255            ; // A
                    }else if(BitsPerPixel == 8){
                        byte PaletteIndex = Data[RowStart + X];
                        int P = PaletteIndex * 4;
                        
                        Result.Pixels_RGBA[OutIndex + 0] = Palette![P + 2]; // R
                        Result.Pixels_RGBA[OutIndex + 1] = Palette![P + 1]; // G
                        Result.Pixels_RGBA[OutIndex + 2] = Palette![P + 0]; // B
                        Result.Pixels_RGBA[OutIndex + 3] = 255            ; // A
                    }else{
                        int PixelStart = RowStart + X * Channels;
                        
                        Result.Pixels_RGBA[OutIndex + 0] =                 Data[PixelStart + 2]            ; // R
                        Result.Pixels_RGBA[OutIndex + 1] =                 Data[PixelStart + 1]            ; // G
                        Result.Pixels_RGBA[OutIndex + 2] =                 Data[PixelStart + 0]            ; // B
                        Result.Pixels_RGBA[OutIndex + 3] = Channels == 4 ? Data[PixelStart + 3] : (byte)255; // A
                    }
                }
            }
            
            return Result;
        }catch(Exception e){
            throw new Exception("Произошла ошибка при парсинге BMP!", e);
        }
    }
    
    public static class Detect{
        /// <summary>
        /// Определяет формат по данным
        /// </summary>
        public static FileFormat WhatFormat(byte[] Data){
            try{
                if(Data == null || Data.Length == 0){ return FileFormat.Unknown; }

                if(IsPNG (Data)){ return FileFormat.PNG ; }
                if(IsJPEG(Data)){ return FileFormat.JPEG; }
                if(IsBMP (Data)){ return FileFormat.BMP ; }
                if(IsWEBP(Data)){ return FileFormat.WEBP; }
                if(IsGIF (Data)){ return FileFormat.GIF ; }
                if(IsTIFF(Data)){ return FileFormat.TIFF; }

                return FileFormat.Unknown;
            }catch(Exception e){
                throw new Exception("Произошла ошибка при определения формата данных у данных!", e);
            }
        }

        /// <summary>
        /// Это PNG?
        /// </summary>
        public static bool IsPNG(byte[] Data) => Data is[0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A, ..];

        /// <summary>
        /// Это JPEG?
        /// </summary>
        public static bool IsJPEG(byte[] Data) => Data is [0xFF, 0xD8, 0xFF, ..];

        /// <summary>
        /// Это BMP?
        /// </summary>
        public static bool IsBMP(byte[] Data) => Data is [0x42, 0x4D, ..];

        /// <summary>
        /// Это WEBP?
        /// </summary>
        public static bool IsWEBP(byte[] Data) => Data.Length >= 12 && Data[0]  == 0x52 && Data[1]  == 0x49 && Data[2]  == 0x46 && Data[3]  == 0x46 && Data[8]  == 0x57 && Data[9]  == 0x45 && Data[10] == 0x42 && Data[11] == 0x50;
        
        /// <summary>
        /// Это TIFF?
        /// </summary>
        public static bool IsTIFF(byte[] Data) => Data is [0x49, 0x49, 0x2A, 0x00, ..] || Data is [0x4D, 0x4D, 0x00, 0x2A, ..];

        /// <summary>
        /// Это GIF?
        /// </summary>
        public static bool IsGIF(byte[] Data) => Data is [0x47, 0x49, 0x46, 0x38, 0x37, 0x61, ..] || Data is [0x47, 0x49, 0x46, 0x38, 0x39, 0x61, ..];
    }
}