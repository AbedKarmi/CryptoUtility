using System.Collections.Generic;
using System.Drawing;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Advanced;
using PdfSharp.Pdf.IO;

namespace CryptoUtility;

public static class PdfSupport
{
    public static IEnumerable<Bitmap> GetBitmapsFromPdf(string fileName)
    {
        var document = PdfReader.Open(fileName);

        // Iterate pages
        foreach (PdfPage page in document.Pages)
        {
            // Get resources dictionary
            var resources = page.Elements.GetDictionary("/Resources");
            if (resources != null)
            {
                // Get external objects dictionary
                var xObjects = resources.Elements.GetDictionary("/XObject");
                if (xObjects != null)
                {
                    var items = xObjects.Elements.Values;
                    // Iterate references to external objects
                    foreach (var item in items)
                    {
                        var reference = item as PdfReference;
                        if (reference != null)
                        {
                            var xObject = reference.Value as PdfDictionary;
                            // Is external object an image?
                            if (xObject != null && xObject.Elements.GetString("/Subtype") == "/Image")
                            {
                                var bitmap = ExportImage(xObject);
                                if (bitmap != null) yield return bitmap;
                            }
                        }
                    }
                }
            }
        }
    }

    private static Bitmap ExportImage(PdfDictionary image)
    {
        var filter = string.Empty;
        var obj = image.Elements.GetObject("/Filter");
        if (obj is PdfArray)
        {
            filter = ((PdfArray)obj).Elements.GetName(0);
            foreach (var element in ((PdfArray)obj).Elements)
            {
                // TODO
            }
        }
        else
        {
            filter = image.Elements.GetName("/Filter");
        }

        switch (filter)
        {
            case "/DCTDecode":
                return ExportJpegImage(image);

            case "/FlateDecode":
                return ExportAsPngImage(image);

            case "/JBIG2Decode":
                break;
        }

        return null;
    }

    private static Bitmap ExportJpegImage(PdfDictionary image)
    {
        var stream = image.Stream.Value;
        using (var memStream = new MemoryStream())
        {
            using (var bw = new BinaryWriter(memStream))
            {
                bw.Write(stream);
                memStream.Position = 0;
                var result = (Bitmap)Image.FromStream(memStream);
                bw.Close();

                return result;
            }
        }
    }

    private static Bitmap ExportAsPngImage(PdfDictionary image)
    {
        var width = image.Elements.GetInteger(PdfImage.Keys.Width);
        var height = image.Elements.GetInteger(PdfImage.Keys.Height);
        var bitsPerComponent = image.Elements.GetInteger(PdfImage.Keys.BitsPerComponent);

        // TODO: You can put the code here that converts vom PDF internal image format to a Windows bitmap
        // and use GDI+ to save it in PNG format.
        // It is the work of a day or two for the most important formats. Take a look at the file
        // PdfSharp.Pdf.Advanced/PdfImage.cs to see how we create the PDF image formats.
        // We don't need that feature at the moment and therefore will not implement it.
        // If you write the code for exporting images I would be pleased to publish it in a future release
        // of PDFsharp.

        return null;
    }
}