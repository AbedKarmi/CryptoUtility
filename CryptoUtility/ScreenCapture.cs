using System.Drawing;

internal static class ScreenCapture
{
    public static Image CaptureScreen()
    {
        var scrSize = MyClass.PhysicalScreenSize();

        var bitmap = new Bitmap(scrSize.Width, scrSize.Height);

        using (var g = Graphics.FromImage(bitmap))
        {
            g.CopyFromScreen(Point.Empty, Point.Empty, bitmap.Size);
        }

        return bitmap;
    }
}