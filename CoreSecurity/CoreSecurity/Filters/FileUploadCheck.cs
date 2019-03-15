using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSecurity.Filters
{
    public static class FileUploadCheck
    {
        #region " Validations for File Types"
        private enum ImageFileExtension
        {
            none = 0,
            jpg = 1,
            jpeg = 2,
            bmp = 3,
            gif = 4,
            png = 5
        }

        private enum VideoFileExtension
        {
            none = 0,
            wmv = 1,
            mpg = 2,
            mpeg = 3,
            mp4 = 4,
            avi = 5,
            flv = 6
        }

        private enum PdfFileExtension
        {
            none = 0,
            PDF = 1
        }

        public enum FileType
        {
            Image = 1,
            Video = 2,
            PDF = 3,
            Text = 4,
            DOC = 5,
            DOCX = 6,
            PPT = 7,
        }

        public static bool XlMimeType(string mimeType, string ext)
        {
            bool isValid = false;

            if (mimeType == "application/x-msexcel" && (ext == ".xlsx" || ext == ".xls"))
            {
                isValid = true;
            }
            else if (mimeType == "application/x-excel" && (ext == ".xlsx" || ext == ".xls"))
            {
                isValid = true;
            }
            else if (mimeType == "application/vnd.ms-excel" && (ext == ".xlsx" || ext == ".xls"))
            {
                isValid = true;
            }
            else if (mimeType == "application/excel" && (ext == ".xlsx" || ext == ".xls"))
            {
                isValid = true;
            }

            return isValid;
        }

        public static bool IsValidFile(byte[] bytFile, FileType flType, String fileContentType)
        {
            bool isvalid = false;

            if (flType == FileType.Image)
            {
                isvalid = IsValidImageFile(bytFile, fileContentType);
            }
            else if (flType == FileType.Video)
            {
                isvalid = IsValidVideoFile(bytFile, fileContentType);
            }
            else if (flType == FileType.PDF)
            {
                isvalid = IsValidPdfFile(bytFile, fileContentType);
            }

            return isvalid;
        }

        public static bool IsValidImageFile(byte[] bytFile, String fileContentType)
        {
            bool isvalid = false;

            byte[] chkBytejpg = { 255, 216, 255, 224 };
            byte[] chkBytebmp = { 66, 77 };
            byte[] chkBytegif = { 71, 73, 70, 56 };
            byte[] chkBytepng = { 137, 80, 78, 71 };


            ImageFileExtension imgfileExtn = ImageFileExtension.none;

            if (fileContentType.Contains("jpg") | fileContentType.Contains("jpeg"))
            {
                imgfileExtn = ImageFileExtension.jpg;
            }
            else if (fileContentType.Contains("png"))
            {
                imgfileExtn = ImageFileExtension.png;
            }
            else if (fileContentType.Contains("bmp"))
            {
                imgfileExtn = ImageFileExtension.bmp;
            }
            else if (fileContentType.Contains("gif"))
            {
                imgfileExtn = ImageFileExtension.gif;
            }

            if (imgfileExtn == ImageFileExtension.jpg || imgfileExtn == ImageFileExtension.jpeg)
            {
                if (bytFile.Length >= 4)
                {
                    int j = 0;
                    for (Int32 i = 0; i <= 3; i++)
                    {
                        if (bytFile[i] == chkBytejpg[i])
                        {
                            j = j + 1;
                            if (j == 3)
                            {
                                isvalid = true;
                            }
                        }
                    }
                }
            }


            if (imgfileExtn == ImageFileExtension.png)
            {
                if (bytFile.Length >= 4)
                {
                    int j = 0;
                    for (Int32 i = 0; i <= 3; i++)
                    {
                        if (bytFile[i] == chkBytepng[i])
                        {
                            j = j + 1;
                            if (j == 3)
                            {
                                isvalid = true;
                            }
                        }
                    }
                }
            }


            if (imgfileExtn == ImageFileExtension.bmp)
            {
                if (bytFile.Length >= 4)
                {
                    int j = 0;
                    for (Int32 i = 0; i <= 1; i++)
                    {
                        if (bytFile[i] == chkBytebmp[i])
                        {
                            j = j + 1;
                            if (j == 2)
                            {
                                isvalid = true;
                            }
                        }
                    }
                }
            }

            if (imgfileExtn == ImageFileExtension.gif)
            {
                if (bytFile.Length >= 4)
                {
                    int j = 0;
                    for (Int32 i = 0; i <= 1; i++)
                    {
                        if (bytFile[i] == chkBytegif[i])
                        {
                            j = j + 1;
                            if (j == 3)
                            {
                                isvalid = true;
                            }
                        }
                    }
                }
            }

            return isvalid;
        }

        private static bool IsValidVideoFile(byte[] bytFile, String fileContentType)
        {
            byte[] chkBytewmv = { 48, 38, 178, 117 };
            byte[] chkByteavi = { 82, 73, 70, 70 };
            byte[] chkByteflv = { 70, 76, 86, 1 };
            byte[] chkBytempg = { 0, 0, 1, 186 };
            byte[] chkBytemp4 = { 0, 0, 0, 20 };
            bool isvalid = false;

            VideoFileExtension vdofileExtn = VideoFileExtension.none;
            if (fileContentType.Contains("wmv"))
            {
                vdofileExtn = VideoFileExtension.wmv;
            }
            else if (fileContentType.Contains("mpg") || fileContentType.Contains("mpeg"))
            {
                vdofileExtn = VideoFileExtension.mpg;
            }
            else if (fileContentType.Contains("mp4"))
            {
                vdofileExtn = VideoFileExtension.mp4;
            }
            else if (fileContentType.Contains("avi"))
            {
                vdofileExtn = VideoFileExtension.avi;
            }
            else if (fileContentType.Contains("flv"))
            {
                vdofileExtn = VideoFileExtension.flv;
            }

            if (vdofileExtn == VideoFileExtension.wmv)
            {
                if (bytFile.Length >= 4)
                {
                    int j = 0;
                    for (Int32 i = 0; i <= 3; i++)
                    {
                        if (bytFile[i] == chkBytewmv[i])
                        {
                            j = j + 1;
                            if (j == 3)
                            {
                                isvalid = true;
                            }
                        }
                    }
                }
            }
            else if ((vdofileExtn == VideoFileExtension.mpg || vdofileExtn == VideoFileExtension.mpeg) & isvalid)
            {
                if (bytFile.Length >= 4)
                {
                    int j = 0;
                    for (Int32 i = 0; i <= 3; i++)
                    {
                        if (bytFile[i] == chkBytempg[i])
                        {
                            j = j + 1;
                            if (j == 3)
                            {
                                isvalid = true;
                            }
                        }
                    }
                }
            }
            else if (vdofileExtn == VideoFileExtension.mp4 & isvalid)
            {
                if (bytFile.Length >= 4)
                {
                    int j = 0;
                    for (Int32 i = 0; i <= 3; i++)
                    {
                        if (bytFile[i] == chkBytemp4[i])
                        {
                            j = j + 1;
                            if (j == 3)
                            {
                                isvalid = true;
                            }
                        }
                    }
                }
            }
            else if (vdofileExtn == VideoFileExtension.avi & isvalid)
            {
                if (bytFile.Length >= 4)
                {
                    int j = 0;
                    for (Int32 i = 0; i <= 3; i++)
                    {
                        if (bytFile[i] == chkByteavi[i])
                        {
                            j = j + 1;
                            if (j == 3)
                            {
                                isvalid = true;
                            }
                        }
                    }
                }
            }
            else if (vdofileExtn == VideoFileExtension.flv & isvalid)
            {
                if (bytFile.Length >= 4)
                {
                    int j = 0;
                    for (Int32 i = 0; i <= 3; i++)
                    {
                        if (bytFile[i] == chkByteflv[i])
                        {
                            j = j + 1;
                            if (j == 3)
                            {
                                isvalid = true;
                            }
                        }
                    }
                }
            }

            return isvalid;

        }

        public static bool IsValidPdfFile(byte[] bytFile, String fileContentType)
        {
            byte[] chkBytepdf = { 37, 80, 68, 70 };
            bool isvalid = false;

            PdfFileExtension pdffileExtn = PdfFileExtension.none;
            if (fileContentType.Contains("pdf"))
            {
                pdffileExtn = PdfFileExtension.PDF;
            }

            if (pdffileExtn == PdfFileExtension.PDF)
            {
                if (bytFile.Length >= 4)
                {
                    int j = 0;
                    for (Int32 i = 0; i <= 3; i++)
                    {
                        if (bytFile[i] == chkBytepdf[i])
                        {
                            j = j + 1;
                            if (j == 3)
                            {
                                isvalid = true;
                            }
                        }
                    }
                }
            }

            return isvalid;
        }

        #endregion
    }
}
