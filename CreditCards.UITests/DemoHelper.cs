using System.Threading;

namespace CreditCards.UITests
{
    internal static class DemoHelper
    {
        ///// <summary>
        ///// Brief delay to slow down browser interactions for
        ///// demo video recording purposes
        ///// </summary>
        public static void Pause(int secondsToPause = 3000)
        {
            Thread.Sleep(secondsToPause);
        }
    }
}
